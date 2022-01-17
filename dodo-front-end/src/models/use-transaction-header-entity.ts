import { ITransactionHeader } from './transaction';
import { useCrudEntity } from './use-crud-entity';
import { Dialog } from 'quasar';
import TransactionDetailDialog from 'components/transaction-history/TransactionDetailDialog.vue';
import BaseDialog from 'components/ui/BaseDialog.vue';

export function useTransactionHeaderEntity() {
  const {
    grid,
    get,
    getAll,
    create,
    pageFilter,
    update,
    filter,
    paging,
    remove,
    search
  } = useCrudEntity<ITransactionHeader>('/transaction/header');

  async function showDetail(id: number) {
    const transactionHeader = (await get(id)) as ITransactionHeader;
    Dialog.create({
      component: TransactionDetailDialog,
      componentProps: {
        transactionHeader
      }
    });
  }

  function confirmDelete(id: number) {
    Dialog.create({
      component: BaseDialog,
      componentProps: {
        body: 'Yakin ingin menghapus transaksi?',
        okLabel: 'Ya',
        cancelLabel: 'Tidak'
      }
    }).onOk(async () => await remove(id));
  }

  return {
    grid,
    get,
    getAll,
    create,
    pageFilter,
    update,
    filter,
    paging,
    remove: confirmDelete,
    search,
    showDetail
  };
}
