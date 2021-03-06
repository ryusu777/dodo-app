<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
    <h3 class="text-bold q-mx-lg q-mt-sm">
      {{ transactionType === 'sell' ? 'Menjual' : 'Membeli' }}
    </h3>
    <goods-table
      :rows="gridGoods.data"
      :pagination="pageFilterGoods"
      :transaction-type="transactionType"
      @request="pagingGoods"
      @search="searchGoods"
      @add-to-cart="
        createAndGetDetail({
          ...$event,
          goodsTransactionHeaderId: transactionHeader?.id || -1
        })
      "
    >
      <template #top-right>
        <base-button
          @click="showDetail = true"
          icon="shopping_cart"
          class="q-mr-md"
        />
      </template>
    </goods-table>

    <base-add-dialog v-model="showDetail" title="Detail transaksi">
      <transaction-detail-table
        v-if="gridDetail.data"
        :rows="gridDetail.data"
        editable
        @delete="confirmRemoveDetail"
        @update="updateDetail"
      />
      <base-button
        label="Selesaikan transaksi"
        class="q-mt-sm"
        color="primary"
        @click="updateHeaderHandler"
      />
    </base-add-dialog>
  </q-page>
</template>

<script lang="ts">
import GoodsTable from 'components/goods/GoodsTable.vue';
import { useQuasar } from 'quasar';
import { useCrudEntity } from 'src/models/crud';
import { IGoods } from 'src/models/goods';
import { ITransactionDetail, ITransactionHeader } from 'src/models/transaction';
import { defineComponent, onBeforeMount, ref } from 'vue';
import BaseAddDialog from '../ui/BaseAddDialog.vue';
import BaseDialog from '../ui/BaseDialog.vue';
import BaseButton from '../ui/BaseButton.vue';
import { useRouter } from 'vue-router';
import TransactionDetailTable from './TransactionDetailTable.vue';

export default defineComponent({
  components: {
    GoodsTable,
    TransactionDetailTable,
    BaseAddDialog,
    BaseButton
  },
  props: {
    transactionType: String,
    id: Number
  },
  setup(props) {
    const $q = useQuasar();
    const router = useRouter();
    const {
      grid: gridGoods,
      pageFilter: pageFilterGoods,
      getAll: getAllGoods,
      paging: pagingGoods,
      search: searchGoods
    } = useCrudEntity<IGoods>('/goods');

    const { get: getHeader, update: updateHeader } =
      useCrudEntity<ITransactionHeader>('/transaction/header');

    const transactionHeader = ref<ITransactionHeader>();

    async function getDetails() {
      transactionHeader.value = (await getHeader(
        props.id || -1
      )) as ITransactionHeader;

      gridDetail.value.data = transactionHeader.value.goodsTransactionDetails;
    }

    async function updateHeaderHandler() {
      const request = transactionHeader.value as ITransactionHeader;
      request.purchaseDate = new Date().toISOString();
      request.receiveDate = new Date().toISOString();
      delete request.goodsTransactionDetails;
      // TODO: Successfull request should redirect user
      await updateHeader(request);
    }

    const {
      grid: gridDetail,
      create: createDetail,
      update: updateDetail,
      remove: removeDetail
    } = useCrudEntity<ITransactionDetail>('/transaction/detail');

    function confirmRemoveDetail(id: number) {
      $q.dialog({
        component: BaseDialog,
        componentProps: {
          body: 'Yakin untuk menghapus barang?',
          okLabel: 'Ya',
          cancelLabel: 'Tidak'
        }
      }).onOk(async () => {
        await removeDetail(id);
      });
    }

    async function createAndGetDetail(entity: ITransactionDetail) {
      await createDetail(entity);
      await getDetails();

      $q.notify({
        message: 'Permintaan telah dikirim'
      });
    }

    onBeforeMount(async () => {
      await getDetails();

      if (!transactionHeader.value) {
        $q.dialog({
          component: BaseDialog,
          componentProps: {
            body: 'Transaksi tidak ditemukan',
            okLabel: 'Kembali'
          }
        }).onOk(() => router.back());
        return;
      }

      if (transactionHeader.value.purchaseDate) {
        $q.dialog({
          component: BaseDialog,
          componentProps: {
            body: 'Transaksi telah selesai dilakukan',
            okLabel: 'Kembali'
          }
        }).onOk(() => router.back());
        return;
      }

      await getAllGoods();
    });

    const showDetail = ref(false);

    return {
      gridGoods,
      gridDetail,
      pageFilterGoods,
      transactionHeader,
      showDetail,
      pagingGoods,
      searchGoods,
      createAndGetDetail,
      updateDetail,
      confirmRemoveDetail,
      updateHeaderHandler
    };
  }
});
</script>
