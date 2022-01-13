<template>
  <!-- TODO: Fixed dialog width -->
  <q-dialog ref="dialogRef" @hide="onDialogHide">
    <base-card>
      <q-card-section>
        <q-card-actions class="row justify-between items-start">
          <p class="text-h5 text-bold">Detil Transaksi</p>
          <base-button
            flat
            icon="close"
            @click="onCancelClick"
            class="q-mb-sm"
          />
        </q-card-actions>
        <transaction-detail :transaction-header="transactionHeader" />
        <base-button
          class="q-mt-md"
          v-if="
            !(transactionHeader?.purchaseDate && transactionHeader?.receiveDate)
          "
          label="Lakukan transaksi"
          @click="doTransaction"
        />
      </q-card-section>
    </base-card>
  </q-dialog>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';
import { useDialogPluginComponent } from 'quasar';
import BaseCard from 'components/ui/BaseCard.vue';
import BaseButton from 'components/ui/BaseButton.vue';
import TransactionDetail from './TransactionDetail.vue';
import { useRouter } from 'vue-router';
import { ITransactionHeader } from 'src/models/transaction';

export default defineComponent({
  props: {
    transactionHeader: Object as PropType<ITransactionHeader>
  },
  emits: [...useDialogPluginComponent.emits],
  setup(props) {
    const { dialogRef, onDialogHide, onDialogOK, onDialogCancel } =
      useDialogPluginComponent();

    const $router = useRouter();

    async function doTransaction() {
      await $router.push(
        `/transaction/${props.transactionHeader?.transactionType || ''}/${
          props.transactionHeader?.id || -1
        }`
      );
    }

    return {
      dialogRef,
      onDialogHide,
      onDialogOK,
      onCancelClick: onDialogCancel,
      doTransaction
    };
  },
  components: { BaseCard, BaseButton, TransactionDetail }
});
</script>
