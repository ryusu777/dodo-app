import { ref } from 'vue';
import { useCrudEntity } from 'src/models/use-crud-entity';
import { ICurrency, ICurrencySummaryPerDay } from 'src/models/currency';
import { date } from 'quasar';
import { api } from 'boot/axios';

export function useCurrencyEntity() {
  const { grid, pageFilter, getAll, get, paging, create, filter } =
    useCrudEntity<ICurrency | ICurrencySummaryPerDay>('/currency');

  const isPerDay = ref(false);

  async function getSummary(
    dateFrom: string = date.formatDate(new Date(), 'YYYY-MM-DD'),
    dateTo?: string
  ) {
    try {
      if (dateTo) {
        const response = await api.get<Array<ICurrency>>('/currency/summary', {
          params: {
            dateFrom,
            dateTo
          }
        });

        grid.value.data = response.data;
        isPerDay.value = true;
      } else {
        const response = await api.get<Array<ICurrency>>('/currency/summary', {
          params: {
            dateFrom
          }
        });

        grid.value.data = response.data;
        isPerDay.value = false;
      }
    } catch {}
  }
  return {
    grid,
    pageFilter,
    getAll,
    get,
    paging,
    create,
    filter,
    isPerDay,
    getSummary
  };
}
