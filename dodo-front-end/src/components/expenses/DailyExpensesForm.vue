<template>
  <q-form class="row justify-start q-gutter-y-lg" @submit.prevent="submitData">
    <BaseInput
      v-model="expenses"
      class="col-12 q-my-sm"
      label="Nilai"
      type="number"
      lazy-rules
      :rules="[requiredRule]"
    />

    <BaseInput
      v-model="description"
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
import { IExpenses } from 'src/models/interfaces/dailyexpenses.interface';

export default defineComponent({
  emits: ['submit'],
  components: {
    BaseButton,
    BaseInput
  },
  props: {
    dailyexpenses: {
      type: Object as PropType<IExpenses>,
      required: false
    }
  },
  setup(props, { emit }) {
    const expensesId = ref(props.dailyexpenses?.id || null);
    const expenses = ref(props.dailyexpenses?.expenses || 0);
    const description = ref(props.dailyexpenses?.description || '');


    function submitData() {
      emit('submit', {
        id: expensesId.value,
        expenses: expenses.value,
        description: description.value,
      });
    }

    function reset() {
      expenses.value = 0;
      description.value = '';
    }

    return {
      expensesId,
      expenses,
      description,
      submitData,
      reset,
      requiredRule: (val: string) => (val && val.length > 0) || 'Mohon diisi',
      atLeastOneRule: (val: number) => (val && val > 0) || 'Mohon diisi'
    };
  }
});
</script>
