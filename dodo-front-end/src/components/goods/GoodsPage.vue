<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
    <h3 class="text-bold q-mx-lg q-mt-sm">Daftar Barang</h3>
    <goods-table
      :rows="grid.data"
      :pagination="pageFilter"
      row-key="id"
      @paging="paging"
      @create="create"
      @update="update"
      @delete="remove"
      @filter="onFilter"
    />
  </q-page>
</template>

<script lang="ts">
import { defineComponent, onBeforeMount } from 'vue';
import GoodsTable from 'components/goods/GoodsTable.vue';
import { useCrudEntity } from 'src/models/crud';
import { IGoods } from 'src/models/goods';
export default defineComponent({
  components: {
    GoodsTable
  },
  setup() {
    const { grid, pageFilter, create, paging, getAll, update, remove } =
      useCrudEntity<IGoods>('/goods');

    async function onFilter(searchText: string) {
      await paging({
        ...pageFilter.value,
        searchText
      });
    }

    onBeforeMount(async () => await getAll());

    return {
      grid,
      pageFilter,
      onFilter,
      create,
      paging,
      update,
      remove
    };
  }
});
</script>
