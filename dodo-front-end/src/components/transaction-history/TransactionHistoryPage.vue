<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
    <h3 class="text-bold q-mx-lg q-mt-sm">Histori Transaksi</h3>
    <transaction-table
      :rows="grid.data"
      :pagination="pageFilter"
      row-key="id"
      @paging="paging"
      @get="showDetail"
      @filter="filter"
    />
  </q-page>
</template>

<script lang="ts">
import TransactionTable from 'components/transaction-history/TransactionTable.vue';
import { defineComponent, onMounted } from 'vue';
import { ITransactionHeader } from 'src/models/transaction';
import { useQuasar } from 'quasar';
import TransactionDetailDialog from 'components/transaction-history/TransactionDetailDialog.vue';
import { useCrudEntity } from 'src/models/crud';

export default defineComponent({
  components: {
    TransactionTable
  },
  setup() {
    const $q = useQuasar();
    const { grid, pageFilter, paging, getAll, get, filter } =
      useCrudEntity<ITransactionHeader>('/transaction/header');

    onMounted(async () => await getAll());

    async function showDetail(id: number) {
      const transactionHeader = (await get(id)) as ITransactionHeader;
      $q.dialog({
        component: TransactionDetailDialog,
        componentProps: {
          transactionHeader
        }
      });
    }

    return {
      showDetail,
      paging,
      filter,
      grid,
      pageFilter
    };
  }
});
</script>
