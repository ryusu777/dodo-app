<template>
  <div class="row items-center justify-between">
    <slot class="col-4"></slot>
    <q-select
      v-model="content"
      borderless
      dense 
      class="input col-8"
      multiple
      :options="options"
    >
      <template #append>
        <slot name="append"></slot>
      </template>
    </q-select>
  </div>
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
    },
    options: Array,
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
  padding: 0.1rem 0.5rem;
  border: solid 2px black;
  border-radius: 10px;
  color: primary;
}

.input:focus {
  outline: none;
}
</style>
