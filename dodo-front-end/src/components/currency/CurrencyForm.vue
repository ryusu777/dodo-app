<template>
  <q-form class="row justify-start q-gutter-y-lg" @submit.prevent="submitData">
    <q-radio v-model="type" val="income" label="Pemasukkan" />
    <q-radio v-model="type" val="expenses" label="Pengeluaran" />

    <BaseInput
      v-model="changingAmount"
      class="col-12 q-my-sm"
      label="Nilai"
      type="number"
      lazy-rules
      :rules="[requiredRule]"
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
    const changingAmount = ref(props.currency?.changingAmount || 0);
    const changeDescription = ref(props.currency?.changeDescription || '');
    const type = ref('income');

    function submitData() {
      emit('submit', {
        id: currencyId.value,
        changingAmount: type.value === 'expenses' ? (-1 * changingAmount.value) : changingAmount.value,
        changeDescription: changeDescription.value,
      });
    }

    function reset() {
      changingAmount.value = 0;
      changeDescription.value = '';
    }

    return {
      type,
      currencyId,
      changingAmount,
      changeDescription,
      submitData,
      reset,
      requiredRule: (val: string) => (val && val.length > 0) || 'Mohon diisi',
      atLeastOneRule: (val: number) => (val && val > 0) || 'Mohon diisi'
    };
  }
});
</script>
