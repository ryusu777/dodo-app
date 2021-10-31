<template>
  <q-field v-model="content" dense class="input">
    <slot></slot>
  </q-field>
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

<style lang="scss" scoped>
.input {
  font-family: inherit;
  font-size: 18px;
  padding: 0.1rem 0.5rem;
  color: primary;
}
</style>
