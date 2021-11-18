<template>
  <q-input outlined stack-label v-model="model" class="text-bold" type="date">
    <!-- <template v-slot:append>
        <q-icon name="event" class="cursor-pointer">
          <q-popup-proxy transition-show="scale" transition-hide="scale">
            <q-date v-model="date">
              <div class="row items-center justify-end">
                <q-btn v-close-popup label="Close" color="primary" flat />
              </div>
            </q-date>
          </q-popup-proxy>
        </q-icon>
      </template> -->
  </q-input>
</template>

<script lang="ts">
import { defineComponent, computed } from 'vue';

export default defineComponent({
  emits: ['modelValue:update'],
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
        emit('modelValue:update', val);
      }
    });
    return {
      model
    };
  }
});
</script>
