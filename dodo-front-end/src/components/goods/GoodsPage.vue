<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
    <h3 class="text-bold q-mx-lg q-mt-sm">Daftar Barang</h3>
    <goods-table
      :rows="grid.data"
      :pagination="pageFilter"
      row-key="id"
      @paging="paging"
      @update-request="updateGoods"
      @delete="confirmRemove"
      @search="search"
    >
      <template #top-right>
        <base-button
          label="Tambah Barang"
          @click="createGoods"
          class="q-mr-md"
        />
      </template>
    </goods-table>
  </q-page>
</template>

<script lang="ts">
// TODO: Create goods request dialog should closes when request is success
import { defineComponent, onBeforeMount } from 'vue';
import { useCrudEntity } from 'src/models/crud';
import { IGoods } from 'src/models/goods';
import { useQuasar } from 'quasar';
import GoodsTable from 'components/goods/GoodsTable.vue';
import GoodsFormDialog from './GoodsFormDialog.vue';
import BaseButton from '../ui/BaseButton.vue';
import BaseDialog from '../ui/BaseDialog.vue';
export default defineComponent({
  components: {
    GoodsTable,
    BaseButton
  },
  setup() {
    const { grid, pageFilter, create, paging, getAll, update, remove, search } =
      useCrudEntity<IGoods>('/goods');

    onBeforeMount(async () => await getAll());

    const $q = useQuasar();

    function createGoods() {
      $q.dialog({
        component: GoodsFormDialog,
        componentProps: {
          title: 'Tambah Barang'
        }
      }).onOk(async (goods: IGoods) => {
        await create(goods);
      });
    }

    function updateGoods(goods: IGoods) {
      $q.dialog({
        component: GoodsFormDialog,
        componentProps: {
          goods,
          title: 'Ubah data barang'
        }
      }).onOk(async (goods: IGoods) => {
        await update(goods);
      });
    }

    function confirmRemove(id: number) {
      $q.dialog({
        component: BaseDialog,
        componentProps: {
          body: 'Yakin ingin menghapus data barang?',
          okLabel: 'Ya',
          cancelLabel: 'Tidak'
        }
      }).onOk(async () => {
        await remove(id);
      });
    }
    return {
      grid,
      pageFilter,
      createGoods,
      paging,
      updateGoods,
      search,
      confirmRemove
    };
  }
});
</script>
