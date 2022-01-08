<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
    <h3 class="text-bold q-mx-lg q-mt-sm">Data Keuangan</h3>
    <currency-summary-per-day-chart v-if="isPerDay" :rows="summary" />
    <currency-summary-per-transaction-chart v-else :rows="summary" />

    <div class="row justify-start q-gutter-y-sm">
      <base-input-date class="col-11" v-model="dateFromInput" label="Dari" />
      <base-input-date class="col-11" v-model="dateToInput" label="Sampai" />
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
import { api } from 'src/boot/axios';
import { ICurrency, ICurrencySummaryPerDay } from 'src/models/currency';
import { defineComponent, ref, onMounted } from 'vue';
import { date } from 'quasar';
import BaseInputDate from '../ui/BaseInputDate.vue';
import BaseButton from '../ui/BaseButton.vue';
import CurrencySummaryPerTransactionChart from './CurrencySummaryPerTransactionChart.vue';
import CurrencySummaryPerDayChart from './CurrencySummaryPerDayChart.vue';

export default defineComponent({
  components: {
    BaseInputDate,
    BaseButton,
    CurrencySummaryPerTransactionChart,
    CurrencySummaryPerDayChart
  },
  setup() {
    const dateFromInput = ref(date.formatDate(new Date(), 'YYYY-MM-DD'));
    const dateToInput = ref('');
    const summary = ref<Array<ICurrency> | Array<ICurrencySummaryPerDay>>([]);
    const isPerDay = ref(false);

    async function getSummary(
      dateFrom: string = date.formatDate(new Date(), 'YYYY-MM-DD'),
      dateTo?: string
    ) {
      try {
        if (dateTo) {
          const response = await api.get<Array<ICurrency>>(
            '/currency/summary',
            {
              params: {
                dateFrom,
                dateTo
              }
            }
          );

          summary.value = response.data;
          isPerDay.value = true;
        } else {
          const response = await api.get<Array<ICurrency>>(
            '/currency/summary',
            {
              params: {
                dateFrom
              }
            }
          );

          summary.value = response.data;
          isPerDay.value = false;
        }
      } catch {}
    }

    onMounted(async () => await getSummary(dateFromInput.value));

    return { summary, dateFromInput, dateToInput, date, isPerDay, getSummary };
  }
});
</script>
