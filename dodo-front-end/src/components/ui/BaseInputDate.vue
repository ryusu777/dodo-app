<template>
  <q-input outlined stack-label v-model="model" class="text-bold" type="date">
  </q-input>
</template>

<script lang="ts">
import { defineComponent, computed } from 'vue';

export default defineComponent({
  emits: ['update:modelValue'],
  props: {
    modelValue: {
      type: String,
      required: true
    }
  },
  setup(props, { emit }) {
    const model = computed({
      get(): string {
        return props.modelValue;
      },
      set(val: string) {
        emit('update:modelValue', val);
      }
    });
    return {
      model
    };
  }
});
</script>
