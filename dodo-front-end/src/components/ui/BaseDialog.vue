<template>
  <teleport to="body">
    <q-dialog v-model="showDialog" persistent v-bind="$attrs">
      <base-card class="q-pa-sm hide-scrollbar" style="width: 75vw">
        <q-card-section class="row justify-evenly q-ma-none">
          <p class="col-10 text-h6 text-center q-ma-none text-bold">
            {{ title }}
          </p>
          <q-btn class="col-1" icon=" close" flat round dense v-close-popup />
        </q-card-section>

        <q-separator class="q-my-sm" />

        <q-card-section class="q-pa-none q-ma-md">
          <slot></slot>
        </q-card-section>
      </base-card>
    </q-dialog>
  </teleport>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import BaseCard from 'src/components/ui/BaseCard.vue';

export default defineComponent({
  inheritAttrs: false,
  emits: ['update:modelValue'],
  data() {
    return {};
  },
  components: {
    BaseCard
  },
  props: {
    title: {
      type: String,
      required: true
    },
    modelValue: {
      type: Boolean,
      required: true
    },
    width: String,
    disableClose: Boolean
  },
  computed: {
    showDialog: {
      get(): boolean {
        return this.modelValue;
      },
      set(value: boolean) {
        this.$emit('update:modelValue', value);
      }
    }
  }
});
</script>
