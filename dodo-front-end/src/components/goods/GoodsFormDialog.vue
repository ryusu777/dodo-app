<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide">
    <base-card>
      <q-card-sticky position="bottom-right" >
        <base-button flat fab icon="close" @click="onCancelClick" />
      </q-card-sticky>
      <q-card-section>
        <!-- TODO: Use q-fab for close button -->
        <!-- <q-card-actions align="right">
          <base-button color="primary" label="X" flat @click="onCancelClick" />
        </q-card-actions> -->
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
import { IGoods } from 'src/domain/goods.interface';

export default defineComponent({
  props: {
    goods: Object as PropType<IGoods>
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
