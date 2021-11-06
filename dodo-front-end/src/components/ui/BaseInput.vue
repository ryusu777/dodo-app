<template>
  <q-input outlined stack-label v-model="model" class="text-bold">
    <template #append>
      <slot name="append"></slot>
    </template>
  </q-input>
</template>

<script lang="ts">
import { defineComponent, computed } from 'vue';

export default defineComponent({
  emits: ['update:modelValue'],
  props: {
    modelValue: {
      type: [String, Number],
      required: false
    }
  },
  setup(props, { emit }) {
    const model = computed({
      get(): string | number | undefined {
        return props.modelValue;
      },
      set(val: string | number | undefined) {
        emit('update:modelValue', val);
      }
    });

    return {
      model
    };
  }
});
</script>
