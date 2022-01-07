<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
    <h3 class="text-bold q-mx-lg q-mt-sm">Data Keuangan</h3>
    <LineChart :chartData="dataset" :options="options" />

    <div class="row justify-start q-gutter-y-sm">
      <base-input-date class="col-10" v-model="dateFromInput" label="Dari" />
      <base-input-date class="col-10" v-model="dateToInput" label="Sampai" />
      <base-button
        label="Terapkan"
        color="primary"
        @click="
          getSummary(
            date.formatDate(dateFromInput, 'YYYY-MM-DD'),
            date.formatDate(dateToInput, 'YYYY-MM-DD')
          )
        "
      />
    </div>
  </q-page>
</template>

<script lang="ts">
import { ChartOptions, ChartData } from 'chart.js';
import { api } from 'src/boot/axios';
import { useCrudEntity } from 'src/models/crud';
import { ICurrency } from 'src/models/currency';
import { computed, defineComponent, ref, onMounted } from 'vue';
import { LineChart } from 'vue-chart-3';
import { date } from 'quasar';
import BaseInputDate from '../ui/BaseInputDate.vue';
import BaseButton from '../ui/BaseButton.vue';

export default defineComponent({
  name: 'Home',
  components: { LineChart, BaseInputDate, BaseButton },
  setup() {
    const { grid: gridCurrency } = useCrudEntity<ICurrency>('/currency');
    const dateFromInput = ref('');
    const dateToInput = ref('');

    async function getSummary(
      dateFrom: string = date.formatDate(new Date(), 'YYYY-MM-DD'),
      dateTo?: string
    ) {
      try {
        const response = await api.get<Array<ICurrency>>('/currency/summary', {
          params: {
            dateFrom,
            dateTo
          }
        });

        gridCurrency.value.data = response.data;
      } catch {}
    }

    const options = ref<ChartOptions<'line'>>({
      interaction: {
        mode: 'index',
        intersect: false
      },
      responsive: true,
      scales: {
        x: {
          ticks: {
            callback(value, index) {
              return index.toString();
            }
          }
        }
      },
      plugins: {
        legend: {
          position: 'top'
        },
        title: {
          display: true,
          text: 'Currency summary'
        }
      }
    });

    const profitSummary = computed(() => {
      return (
        gridCurrency.value.data?.reduce((result, curr) => {
          result.push(curr.profitAmount || 0);
          return result;
        }, [] as Array<number>) || []
      );
    });

    const fundSummary = computed(() => {
      return (
        gridCurrency.value.data?.reduce((result, curr) => {
          result.push(curr.fundAmount || 0);
          return result;
        }, [] as Array<number>) || []
      );
    });

    const descriptionSummary = computed(() => {
      return (
        gridCurrency.value.data?.reduce((result, curr) => {
          result.push(curr.changeDescription || '');
          return result;
        }, [] as Array<string>) || []
      );
    });

    const dataset = computed<ChartData<'line'>>(() => ({
      labels: descriptionSummary.value,
      datasets: [
        {
          label: 'Keuntungan',
          data: profitSummary.value,
          pointRadius: 0,
          fill: false,
          borderColor: 'hsl(155, 60%, 50%)',
          tension: 0.1
        },
        {
          label: 'Permodalan',
          data: fundSummary.value,
          pointRadius: 0,
          borderColor: 'hsl(212, 50%, 50%)',
          fill: false,
          tension: 0.1
        }
      ]
    }));

    onMounted(
      async () =>
        await getSummary(date.formatDate(new Date('2022-01-06'), 'YYYY-MM-DD'))
    );

    return { dataset, options, dateFromInput, dateToInput, date, getSummary };
  }
});
</script>
