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
import { ICurrency } from 'src/models/currency';
import { useCrudEntity } from 'src/models/use-crud-entity';
import { useTransactionHeaderEntity } from 'src/models/use-transaction-header-entity';
export default defineComponent({
  components: {
    CurrencyTable
  },
  setup() {
    const { grid, create, getAll, paging, pageFilter } =
      useCrudEntity<ICurrency>('/currency');

    const { showDetail } = useTransactionHeaderEntity();

    onBeforeMount(async () => await getAll());

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
