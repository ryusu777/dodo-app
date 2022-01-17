<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
    <h3 class="text-bold q-mx-lg q-mt-sm">Daftar Barang</h3>
    <goods-table
      :rows="grid.data"
      :pagination="pageFilter"
      row-key="id"
      @paging="paging"
      @update-request="update"
      @delete="remove"
      @search="search"
    >
      <template #top-right>
        <base-button label="Tambah Barang" @click="create" class="q-mr-md" />
      </template>
    </goods-table>
  </q-page>
</template>

<script lang="ts">
import { defineComponent, onBeforeMount } from 'vue';
import GoodsTable from 'components/goods/GoodsTable.vue';
import BaseButton from '../ui/BaseButton.vue';
import { useGoodsEntity } from 'src/models/use-goods-entity';

export default defineComponent({
  components: {
    GoodsTable,
    BaseButton
  },
  setup() {
    const { grid, pageFilter, create, paging, getAll, update, remove, search } =
      useGoodsEntity();

    onBeforeMount(async () => await getAll());

    return {
      grid,
      pageFilter,
      create,
      paging,
      update,
      search,
      remove
    };
  }
});
</script>
