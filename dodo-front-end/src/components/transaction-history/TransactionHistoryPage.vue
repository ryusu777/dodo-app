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
      @delete="remove"
    />
  </q-page>
</template>

<script lang="ts">
import TransactionTable from 'components/transaction-history/TransactionTable.vue';
import { defineComponent, onMounted } from 'vue';
import { useTransactionHeaderEntity } from 'src/models/use-transaction-header-entity';

export default defineComponent({
  components: {
    TransactionTable
  },
  setup() {
    const { grid, pageFilter, paging, getAll, filter, remove, showDetail } =
      useTransactionHeaderEntity();

    onMounted(async () => await getAll());

    return {
      showDetail,
      paging,
      filter,
      grid,
      pageFilter,
      remove
    };
  }
});
</script>
