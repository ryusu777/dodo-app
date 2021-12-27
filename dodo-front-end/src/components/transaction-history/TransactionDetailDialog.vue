<template>
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
        <transaction-detail :header-id="headerId" />
        <base-button
          v-if="!transactionIsDone"
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

export default defineComponent({
  props: {
    headerId: {
      type: [Number, String],
      required: true
    },
    transactionType: {
      type: String as PropType<'sell' | 'purchase'>,
      required: true
    },
    transactionIsDone: Boolean
  },
  emits: [...useDialogPluginComponent.emits],
  setup(props) {
    const { dialogRef, onDialogHide, onDialogOK, onDialogCancel } =
      useDialogPluginComponent();

    const $router = useRouter();

    async function doTransaction() {
      console.log(props.transactionType);
      await $router.push(
        `/transaction/${props.transactionType}/${props.headerId}`
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
