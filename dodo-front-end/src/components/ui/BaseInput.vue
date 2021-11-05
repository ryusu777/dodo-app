<template>
  <q-input
      outlined
      stack-label
      v-model="model"
      class="text-bold"
    >
    <template #append>
      <slot name="append"></slot>
    </template>
  </q-input>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  emits: ['input'],
  props: {
    value: {
      type: String,
      required: false,
      default: '',
    }
  },
  data() {
    return {
      content: this.value,
    };
  },
  watch: {
    content(value) {
      this.$emit('input', value);
    },
  },
});
</script>
