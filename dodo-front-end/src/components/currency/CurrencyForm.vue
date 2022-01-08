<template>
  <q-form class="row justify-start q-gutter-y-lg" @submit.prevent="submitData">
    <q-radio v-model="type" val="income" label="Pemasukkan" />
    <q-radio v-model="type" val="expenses" label="Pengeluaran" />

    <BaseInput
      v-model="changingProfitAmount"
      class="col-12 q-my-sm"
      label="Keuntungan"
      type="number"
      lazy-rules
    />

    <BaseInput
      v-if="type !== 'expenses'"
      v-model="changingFundAmount"
      class="col-12 q-my-sm"
      label="Permodalan"
      type="number"
      lazy-rules
    />

    <BaseInput
      v-model="changeDescription"
      class="col-12 q-my-sm"
      label="Deskripsi"
      lazy-rules
      :rules="[requiredRule]"
    />

    <div class="row justify-end col-12">
      <base-button class="col-2 q-mt-md" type="submit">Submit</base-button>
    </div>
  </q-form>
</template>

<script lang="ts">
import { defineComponent, ref, PropType } from 'vue';
import BaseButton from 'components/ui/BaseButton.vue';
import BaseInput from 'components/ui/BaseInput.vue';
import { ICurrency } from 'src/models/currency';

export default defineComponent({
  emits: ['submit'],
  components: {
    BaseButton,
    BaseInput
  },
  props: {
    currency: {
      type: Object as PropType<ICurrency>,
      required: false
    }
  },
  setup(props, { emit }) {
    const currencyId = ref(props.currency?.id || null);
    const changingProfitAmount = ref(props.currency?.changingProfitAmount || 0);
    const changingFundAmount = ref(props.currency?.changingFundAmount || 0);
    const changeDescription = ref(props.currency?.changeDescription || '');
    const type = ref('income');

    function submitData() {
      if (
        !(changingProfitAmount.value === 0 && changingFundAmount.value === 0)
      ) {
        emit('submit', {
          id: currencyId.value,
          changingProfitAmount:
            type.value === 'expenses'
              ? -1 * changingProfitAmount.value
              : changingProfitAmount.value,
          changingFundAmount:
            type.value === 'expenses'
              ? -1 * changingFundAmount.value
              : changingFundAmount.value,
          changeDescription: changeDescription.value
        });
      }
    }

    function reset() {
      changingFundAmount.value = 0;
      changingProfitAmount.value = 0;
      changeDescription.value = '';
    }

    return {
      type,
      currencyId,
      changingFundAmount,
      changingProfitAmount,
      changeDescription,
      submitData,
      reset,
      requiredRule: (val: string) => (val && val.length > 0) || 'Mohon diisi',
      atLeastOneRule: (val: number) => (val && val > 0) || 'Mohon diisi'
    };
  }
});
</script>
