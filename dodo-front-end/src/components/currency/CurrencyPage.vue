<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
    <h3 class="text-bold q-mx-lg q-mt-sm">Data Keuangan</h3>
    <currency-table
      :rows="rows"
      row-key="id"
      @create="sendCreateRequest"
      @get-all="sendGetCurrency"
      @get="showDetail"
    />
  </q-page>
</template>

<script lang="ts">
import { defineComponent, ref, inject, onMounted } from 'vue';
import CurrencyTable from 'components/currency/CurrencyTable.vue';
import { ICurrency } from 'src/models/currency';
import { api } from 'boot/axios';
import { IPageFilter } from 'src/models/requests.interface';
import { ICreateResponse, IPagination } from 'src/models/responses.interface';
import { AxiosError, AxiosResponse } from 'axios';
import TransactionDetailDialog from 'components/transaction-history/TransactionDetailDialog.vue';
import { useQuasar } from 'quasar';
export default defineComponent({
  components: {
    CurrencyTable
  },
  setup() {
    const $q = useQuasar();
    const filter = ref('');
    const requestPagination = ref<IPageFilter>({
      page: 1,
      rowsPerPage: 5
    });

    onMounted(async () => await sendGetCurrency({ page: 1, rowsPerPage: 5 }));

    const notifyError: ((err: unknown | AxiosError) => void) | undefined =
      inject('notifyError');

    const rows = ref<ICurrency[]>([]);

    async function sendGetCurrency(requestPagination: IPageFilter) {
      try {
        const response: AxiosResponse<IPagination<ICurrency>> = await api.get(
          '/currency',
          {
            params: {
              ...requestPagination,
              searchText: filter.value
            }
          }
        );

        if (response.data.data) {
          rows.value = response.data.data;
          requestPagination.rowsNumber = response.data.rowsNumber;
          requestPagination.page = response.data.pageNumber;
          requestPagination.searchText = response.data.searchText;
          requestPagination.rowsPerPage = response.data.itemPerPage;
        }
      } catch (err) {
        notifyError?.(err);
      }
    }

    async function sendCreateRequest(currency: ICurrency): Promise<void> {
      try {
        const response = await api.post<ICreateResponse>('/currency', {
          changingAmount: currency.changingAmount,
          changeDescription: currency.changeDescription
        });
        // TODO: Create Currency should return created result
        rows.value.unshift({
          id: response.data.id,
          changingAmount: currency.changingAmount,
          changeDescription: currency.changeDescription,
          currencyAmount:
            rows.value[0].currencyAmount || currency?.changingAmount || 0,
          dateOfChange: new Date()
        });
      } catch (err) {
        notifyError?.(err);
      }
    }

    function showDetail(id: number) {
      $q.dialog({
        component: TransactionDetailDialog,
        componentProps: {
          headerId: id,
          transactionIsDone: true
        }
      });
    }

    return {
      rows,
      filter,
      requestPagination,
      sendCreateRequest,
      sendGetCurrency,
      showDetail
    };
  }
});
</script>
