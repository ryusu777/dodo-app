<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
    <h3 class="text-bold q-mx-lg q-mt-sm">Histori Transaksi</h3>
    <transaction-table
      :rows="rows"
      row-key="id"
      @get-all="sendGetHeaders"
      @get="showDetail"
    />
  </q-page>
</template>

<script lang="ts">
import TransactionTable from 'components/transaction-history/TransactionTable.vue';
import { defineComponent,ref, inject, onMounted  } from 'vue';
import { IPageFilter } from 'src/models/requests.interface';
import { ITransactionHeader } from 'src/models/transaction';
import { api } from 'boot/axios';
import { ICreateResponse, IPagination } from 'src/models/responses.interface';
import { AxiosError, AxiosResponse } from 'axios';
import { useQuasar } from 'quasar';
import TransactionDetailDialog from 'components/transaction-history/TransactionDetailDialog.vue';

export default defineComponent({
  components: {
    TransactionTable,
  },
  setup() {
    const $q = useQuasar();
    const filter = ref('');
    const requestPagination = ref<IPageFilter>({
      page: 1,
      rowsPerPage: 5
    });

    const purchaseDateFrom = ref('');
    const purchaseDateTo = ref('');
    const receiveDateFrom = ref('');
    const receiveDateTo = ref('');


    onMounted(async () => await sendGetHeaders({ page: 1, rowsPerPage: 5 }));

    const notifyError: ((err: unknown | AxiosError) => void) | undefined =
      inject('notifyError');
    
    const rows = ref<ITransactionHeader[]>([]);

    async function sendGetHeaders(requestPagination: IPageFilter) {
      try {
        const response : AxiosResponse<IPagination<ITransactionHeader>> = await api.get (
          '/transaction/header',
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

    // async function clearFilter() {
    //   purchaseDateFrom.value = '';
    //   purchaseDateTo.value = '';
    //   receiveDateFrom.value = '';
    //   receiveDateTo.value = '';
    //   await sendGetHeaders();
      
    async function sendHeaderFilter() {
      try {
        const response = await api.post<IPagination<ITransactionHeader>>(
          '/transaction/header-filter',
          {
            purchaseDateFrom: purchaseDateFrom.value || null,
            purchaseDateTo: purchaseDateTo.value || null,
            receiveDateFrom: receiveDateFrom.value || null,
            receiveDateTo: receiveDateTo.value || null
          },
          {
            params: {
              ...requestPagination.value
            }
          }
        );

        if (response.data.data) {
          rows.value = response.data.data;
          requestPagination.value.rowsNumber = response.data.rowsNumber;
          requestPagination.value.page = response.data.pageNumber;
          requestPagination.value.rowsPerPage = response.data.itemPerPage;
        }
      } catch (err) {
        notifyError?.(err);
      }
    }

    function showDetail(id: number) {
      $q.dialog({
        component: TransactionDetailDialog,
        componentProps: {
          headerId: id,
          transactionType : true,
          transactionIsDone : true
        }
      });
    }
    
    return {
      requestPagination,
      receiveDateFrom,
      receiveDateTo,
      purchaseDateFrom,
      purchaseDateTo,
      showDetail,
      sendHeaderFilter,
    };
  },
  
});
</script>
