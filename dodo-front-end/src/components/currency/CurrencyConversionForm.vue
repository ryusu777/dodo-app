<template>
  <q-form class="row justify-start q-gutter-y-lg" @submit.prevent="submit">
    <q-radio v-model="to" val="fund" label="Keuntungan ke Permodalan" />
    <q-radio v-model="to" val="profit" label="Permodalan ke Keuntungan" />

    <base-input
      class="col-12"
      type="number"
      v-model="changingAmount"
      lazy-rules
      :rules="[conversionRule]"
    />

    <div class="row justify-end col-12">
      <base-button class="col-2 q-mt-md" type="submit">Submit</base-button>
    </div>
  </q-form>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import BaseInput from '../ui/BaseInput.vue';
import BaseButton from '../ui/BaseButton.vue';

export default defineComponent({
  emits: ['submit'],
  components: {
    BaseInput,
    BaseButton
  },
  props: {
    fundAmount: {
      type: Number,
      required: true
    },
    profitAmount: {
      type: Number,
      required: true
    }
  },
  setup(props, { emit }) {
    const changingAmount = ref(0);
    const to = ref('fund');

    function submit() {
      if (to.value === 'fund') {
        emit('submit', {
          changeDescription: 'Konversi keuntungan menjadi modal',
          dateOfChange: new Date().toISOString(),
          changingFundAmount: changingAmount.value,
          changingProfitAmount: changingAmount.value * -1
        });
      } else {
        emit('submit', {
          changeDescription: 'Konversi modal menjadi keuntungan',
          dateOfChange: new Date().toISOString(),
          changingFundAmount: changingAmount.value * -1,
          changingProfitAmount: changingAmount.value
        });
      }
    }

    return {
      changingAmount,
      to,
      conversionRule: (val: number) =>
        (to.value === 'fund' && val <= props.profitAmount) ||
        (to.value === 'profit' && val <= props.fundAmount) ||
        'Keuangan tidak mencukupi',
      submit
    };
  }
});
</script>
