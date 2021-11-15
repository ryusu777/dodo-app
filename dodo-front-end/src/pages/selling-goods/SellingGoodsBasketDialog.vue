<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide">
    <base-card style="width: 300px">
      <q-card-section>
        <p class="text-center text-h6 text-bold">Keranjang</p>
      </q-card-section>

      <q-card-section>
        <selling-goods-basket
          :transaction-header="transactionHeader"
          @deleted-detail="onDialogOK"
        ></selling-goods-basket>
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
import { PropType } from 'vue';
import { useDialogPluginComponent } from 'quasar';
import BaseCard from 'src/components/ui/BaseCard.vue';
import BaseButton from 'src/components/ui/BaseButton.vue';
import { ITransactionHeader } from './selling-goods.interface';
import SellingGoodsBasket from './SellingGoodsBasket.vue';

export default {
  props: {
    transactionHeader: Object as PropType<ITransactionHeader>
  },
  emits: [...useDialogPluginComponent.emits],
  setup() {
    const { dialogRef, onDialogHide, onDialogOK, onDialogCancel } =
      useDialogPluginComponent();
    return {
      dialogRef,
      onDialogHide,
      onDialogOK,
      onOKClick() {
        onDialogOK();
      },
      onCancelClick: onDialogCancel
    };
  },
  components: { BaseCard, BaseButton, SellingGoodsBasket }
};
</script>
