import { IGoods } from './goods';
import { useCrudEntity } from './use-crud-entity';
import { Dialog } from 'quasar';
import GoodsFormDialog from 'components/goods/GoodsFormDialog.vue';
import BaseDialog from 'components/ui/BaseDialog.vue';

export function useGoodsEntity() {
  const {
    grid,
    pageFilter,
    get,
    getAll,
    update,
    remove,
    create,
    search,
    paging,
    filter
  } = useCrudEntity<IGoods>('/goods');

  function createGoods() {
    Dialog.create({
      component: GoodsFormDialog,
      componentProps: {
        title: 'Tambah Barang'
      }
    }).onOk(async (goods: IGoods) => {
      await create(goods);
    });
  }

  function updateGoods(goods: IGoods) {
    Dialog.create({
      component: GoodsFormDialog,
      componentProps: {
        goods,
        title: 'Ubah data barang'
      }
    }).onOk(async (goods: IGoods) => {
      await update(goods);
    });
  }

  function removeGoods(id: number) {
    Dialog.create({
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
    get,
    getAll,
    update: updateGoods,
    remove: removeGoods,
    create: createGoods,
    search,
    paging,
    filter
  };
}
