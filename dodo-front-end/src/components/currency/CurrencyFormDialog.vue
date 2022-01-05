<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide">
    <base-card>
      <q-card-section>
        <q-card-actions class="row justify-between items-start">
          <p class="text-h5 text-bold">{{ title || 'Dialog' }}</p>
          <base-button
            flat
            icon="close"
            @click="onCancelClick"
            class="q-mb-sm"
          />
        </q-card-actions>
        <currency-form
          :currency="currency"
          @submit="onDialogOK"
        ></currency-form>
      </q-card-section>
    </base-card>
  </q-dialog>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';
import { useDialogPluginComponent } from 'quasar';
import BaseCard from 'components/ui/BaseCard.vue';
import BaseButton from 'components/ui/BaseButton.vue';
import CurrencyForm from './CurrencyForm.vue';
import { ICurrency } from 'src/models/currency';

export default defineComponent({
  props: {
    currency: Object as PropType<ICurrency>,
    title: String
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
  components: { BaseCard, BaseButton, CurrencyForm }
});
</script>
