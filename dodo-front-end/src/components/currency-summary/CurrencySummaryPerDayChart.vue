<template>
  <LineChart :chartData="dataset" :options="options" />
</template>

<script lang="ts">
import { ChartOptions, ChartData } from 'chart.js';
import { ICurrencySummaryPerDay } from 'src/models/currency';
import { computed, defineComponent, ref, PropType } from 'vue';
import { LineChart } from 'vue-chart-3';
import { date } from 'quasar';

export default defineComponent({
  props: {
    rows: {
      type: Array as PropType<Array<ICurrencySummaryPerDay>>,
      required: false
    }
  },
  components: { LineChart },
  setup(props) {
    const options = ref<ChartOptions<'line'>>({
      interaction: {
        mode: 'index',
        intersect: false
      },
      responsive: true,
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
        props.rows?.reduce((result, curr) => {
          result.push(curr.profitAmount || 0);
          return result;
        }, [] as Array<number>) || []
      );
    });

    const fundSummary = computed(() => {
      return (
        props.rows?.reduce((result, curr) => {
          result.push(curr.fundAmount || 0);
          return result;
        }, [] as Array<number>) || []
      );
    });

    const daySummary = computed(() => {
      return (
        props.rows?.reduce((result, curr) => {
          result.push(date.formatDate(curr.day, 'MMM D, YY') || '');
          return result;
        }, [] as Array<string>) || []
      );
    });

    const dataset = computed<ChartData<'line'>>(() => ({
      labels: daySummary.value,
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

    return { dataset, options };
  }
});
</script>
