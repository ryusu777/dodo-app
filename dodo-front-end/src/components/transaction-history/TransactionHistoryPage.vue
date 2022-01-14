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
      @delete="confirmDelete"
    />
  </q-page>
</template>

<script lang="ts">
import TransactionTable from 'components/transaction-history/TransactionTable.vue';
import { defineComponent, onMounted } from 'vue';
import { ITransactionHeader } from 'src/models/transaction';
import { useQuasar } from 'quasar';
import TransactionDetailDialog from 'components/transaction-history/TransactionDetailDialog.vue';
import BaseDialog from '../ui/BaseDialog.vue';
import { useCrudEntity } from 'src/models/use-crud-entity';

export default defineComponent({
  components: {
    TransactionTable
  },
  setup() {
    const $q = useQuasar();
    const { grid, pageFilter, paging, getAll, get, filter, remove } =
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

    function confirmDelete(id: number) {
      $q.dialog({
        component: BaseDialog,
        componentProps: {
          body: 'Yakin ingin menghapus transaksi?',
          okLabel: 'Ya',
          cancelLabel: 'Tidak'
        }
      }).onOk(async () => await remove(id));
    }

    return {
      showDetail,
      paging,
      filter,
      grid,
      pageFilter,
      confirmDelete
    };
  }
});
</script>
