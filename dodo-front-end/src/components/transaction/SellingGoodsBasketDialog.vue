<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide">
    <base-card style="width: 325px">
      <q-card-section>
        <p class="text-center text-h6 text-bold">Keranjang</p>
      </q-card-section>

      <q-card-section>
        <selling-goods-basket
          :editable="editable"
          :transaction-header="transactionHeader"
        ></selling-goods-basket>
        <div class="row justify-end q-mb-md">
          <base-button
            label="Lakukan Transaksi"
            @click="sendCompleteTransaction"
          />
        </div>
      </q-card-section>

      <q-card-actions align="right">
        <base-button
          color="primary"
          label="Close"
          @click="onCancelClick"
          flat
        />
      </q-card-actions>
    </base-card>
  </q-dialog>
</template>

<script lang="ts">
import { PropType, defineComponent } from 'vue';
import { useDialogPluginComponent } from 'quasar';
import BaseCard from 'components/ui/BaseCard.vue';
import BaseButton from 'components/ui/BaseButton.vue';
import { ITransactionHeader } from 'src/models/interfaces/transaction.interface';
import SellingGoodsBasket from './SellingGoodsBasket.vue';
import { api } from 'src/boot/axios';
import axios, { AxiosError } from 'axios';
import { useQuasar } from 'quasar';
import { useRouter } from 'vue-router';

export default defineComponent({
  props: {
    transactionHeader: {
      type: Object as PropType<ITransactionHeader>,
      required: true
    },
    editable: Boolean
  },
  emits: [...useDialogPluginComponent.emits, 'deletedDetail'],
  setup(props) {
    const $q = useQuasar();
    const $router = useRouter();
    const { dialogRef, onDialogHide, onDialogOK, onDialogCancel } =
      useDialogPluginComponent();
    function notifyError(err: unknown | AxiosError) {
      if (axios.isAxiosError(err)) {
        const { response } = err;
        // eslint-disable-next-line
        response?.data.errors.forEach((element: string) => {
          $q.notify({
            message: element
          });
        });
      }
    }
    async function sendCompleteTransaction() {
      const transactionHeader = props.transactionHeader;
      if (!transactionHeader.goodsTransactionDetails.length) {
        $q.notify({
          message: 'Tidak dapat menyelesaikan transaksi kosong'
        });
        return;
      }
      if (transactionHeader) {
        transactionHeader.purchaseDate = new Date();
        transactionHeader.receiveDate = new Date();
        try {
          await api.put(
            `/transaction/header/${props.transactionHeader.id || 0}`,
            transactionHeader
          );
          $q.notify({
            message: 'Berhasil menyelesaikan transaksi'
          });
          await $router.push('/');
        } catch (err) {
          notifyError?.(err);
        }
      }
    }

    return {
      dialogRef,
      onDialogHide,
      onDialogOK,
      onOKClick() {
        onDialogOK();
      },
      onCancelClick: onDialogCancel,
      sendCompleteTransaction
    };
  },
  components: { BaseCard, BaseButton, SellingGoodsBasket }
});
</script>
