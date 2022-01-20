<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide">
    <base-card>
      <q-card-section>
        <q-card-actions class="row justify-between items-start">
          <p class="text-h6 text-bold">{{ title || 'Dialog' }}</p>
          <base-button
            flat
            icon="close"
            @click="onCancelClick"
            class="q-mb-sm"
          />
        </q-card-actions>
        <currency-conversion-form
          :fund-amount="fundAmount"
          :profit-amount="profitAmount"
          @submit="onDialogOK"
        ></currency-conversion-form>
      </q-card-section>
    </base-card>
  </q-dialog>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useDialogPluginComponent } from 'quasar';
import BaseCard from 'components/ui/BaseCard.vue';
import BaseButton from 'components/ui/BaseButton.vue';
import CurrencyConversionForm from './CurrencyConversionForm.vue';

export default defineComponent({
  props: {
    title: String,
    fundAmount: {
      type: Number,
      required: true
    },
    profitAmount: {
      type: Number,
      required: true
    }
  },
  emits: [...useDialogPluginComponent.emits],
  setup() {
    const { dialogRef, onDialogHide, onDialogOK, onDialogCancel } =
      useDialogPluginComponent();

    return {
      dialogRef,
      onDialogHide,
      onDialogOK,
      onCancelClick: onDialogCancel
    };
  },
  components: { BaseCard, BaseButton, CurrencyConversionForm }
});
</script>
