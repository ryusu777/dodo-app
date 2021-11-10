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
        <goods-form :goods="goods" @submit="onDialogOK"></goods-form>
      </q-card-section>
    </base-card>
  </q-dialog>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';
import { useDialogPluginComponent } from 'quasar';
import BaseCard from 'components/ui/BaseCard.vue';
import BaseButton from 'components/ui/BaseButton.vue';
import GoodsForm from './GoodsForm.vue';
import { IGoods } from 'pages/goods/goods.interface';

export default defineComponent({
  props: {
    goods: Object as PropType<IGoods>,
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
  components: { BaseCard, BaseButton, GoodsForm }
});
</script>
