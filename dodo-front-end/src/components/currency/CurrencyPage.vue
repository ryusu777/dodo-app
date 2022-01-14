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
import { defineComponent, onBeforeMount } from 'vue';
import CurrencyTable from 'components/currency/CurrencyTable.vue';
import TransactionDetailDialog from 'components/transaction-history/TransactionDetailDialog.vue';
import { useQuasar } from 'quasar';
import { ICurrency } from 'src/models/currency';
import { ITransactionHeader } from 'src/models/transaction';
import { useCrudEntity } from 'src/models/use-crud-entity';
export default defineComponent({
  components: {
    CurrencyTable
  },
  setup() {
    const $q = useQuasar();
    const { grid, create, getAll, paging, pageFilter } =
      useCrudEntity<ICurrency>('/currency');

    onBeforeMount(async () => await getAll());

    async function showDetail(id: number) {
      const { get } = useCrudEntity<ITransactionHeader>('/transaction/header');
      const transactionHeader = (await get(id)) as ITransactionHeader;
      $q.dialog({
        component: TransactionDetailDialog,
        componentProps: {
          transactionHeader
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
