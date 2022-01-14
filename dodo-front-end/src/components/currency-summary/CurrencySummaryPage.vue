<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
    <h3 class="text-bold q-mx-lg q-mt-sm">Data Keuangan</h3>
    <currency-summary-per-day-chart
      v-if="isPerDay && grid.data"
      :rows="grid.data"
    />
    <currency-summary-per-transaction-chart v-else :rows="grid.data" />

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
import { defineComponent, ref, onMounted } from 'vue';
import { date } from 'quasar';
import BaseInputDate from '../ui/BaseInputDate.vue';
import BaseButton from '../ui/BaseButton.vue';
import CurrencySummaryPerTransactionChart from './CurrencySummaryPerTransactionChart.vue';
import CurrencySummaryPerDayChart from './CurrencySummaryPerDayChart.vue';
import { useCurrencyEntity } from 'src/models/use-currency-entity';

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
    const { grid, getSummary, isPerDay } = useCurrencyEntity();

    onMounted(async () => await getSummary(dateFromInput.value));

    return {
      grid,
      dateFromInput,
      dateToInput,
      date,
      isPerDay,
      getSummary
    };
  }
});
</script>
