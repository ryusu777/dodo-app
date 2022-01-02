<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
    <h3 class="text-bold q-mx-lg q-mt-sm">Data Keuangan</h3>
    <currency-table
      :rows="grid.data"
      row-key="id"
      :pagination="pageFilter"
      @create="create"
      @paging="paging"
      @get="showDetail"
    />
  </q-page>
</template>

<script lang="ts">
// TODO: Currency creation should re-getAll currency data
import { defineComponent, onBeforeMount } from 'vue';
import CurrencyTable from 'components/currency/CurrencyTable.vue';
import TransactionDetailDialog from 'components/transaction-history/TransactionDetailDialog.vue';
import { useQuasar } from 'quasar';
import { useCrudEntity } from 'src/models/crud';
import { ICurrency } from 'src/models/currency';
export default defineComponent({
  components: {
    CurrencyTable
  },
  setup() {
    const $q = useQuasar();
    const { grid, create, getAll, paging, pageFilter } =
      useCrudEntity<ICurrency>('/currency');

    onBeforeMount(async () => await getAll());

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
      grid,
      create,
      paging,
      pageFilter,
      showDetail
    };
  }
});
</script>
