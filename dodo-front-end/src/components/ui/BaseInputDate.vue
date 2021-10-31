<template>
  <div class="row items-center justify-between">
    <slot class="col-4"></slot>

    <q-input
        borderless 
        dense 
        class="input col-8" 
        v-model="date" 
        mask="date" 
        :rules="['date']"
    >
      <template v-slot:append>
        <q-icon name="event" class="cursor-pointer">
          <q-popup-proxy transition-show="scale" transition-hide="scale">
            <q-date v-model="date">
              <div class="row items-center justify-end">
                <q-btn v-close-popup label="Close" color="primary" flat />
              </div>
            </q-date>
          </q-popup-proxy>
        </q-icon>
      </template>
    </q-input>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';

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
  setup () {
    return {
      date: ref('2020/01/01')
    }
  }
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
