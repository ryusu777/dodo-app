<template>
  <q-form class="row justify-start q-gutter-y-lg" @submit.prevent="submitData">
    <BaseInput
      v-model="goodsName"
      class="col-12 q-my-sm"
      label="Nama Barang"
      lazy-rules
      :rules="[requiredRule]"
    />

    <BaseInput
      v-model="goodsCode"
      class="col-12 q-my-sm"
      label="Kode Barang"
      lazy-rules
      :rules="[requiredRule]"
    />

    <BaseInput
      v-model="carType"
      class="col-12 q-my-sm"
      label="Tipe Mobil"
      lazy-rules
      :rules="[requiredRule]"
    />

    <BaseInput
      v-model="partNumber"
      class="col-12 q-my-sm"
      label="Part Number"
      lazy-rules
      :rules="[requiredRule]"
    />

    <BaseInput
      v-model="purchasePrice"
      class="col-12 q-my-sm"
      label="Harga Beli"
      type="number"
      lazy-rules
      :rules="[atLeastOneRule]"
    />

    <BaseInput
      v-model="stockAvailable"
      class="col-12 q-my-sm"
      label="Stok Tersedia"
    />

    <BaseInput
      v-model="minimalAvailable"
      class="col-12 q-my-sm"
      label="Minimal Tersedia"
      type="number"
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
import { IGoods } from 'pages/goods/goods.interface';

export default defineComponent({
  emits: ['submit'],
  components: {
    BaseButton,
    BaseInput
  },
  props: {
    goods: {
      type: Object as PropType<IGoods>,
      required: false
    }
  },
  setup(props, { emit }) {
    const goodsId = ref(props.goods?.id || null);
    const goodsName = ref(props.goods?.goodsName || '');
    const goodsCode = ref(props.goods?.goodsCode || '');
    const carType = ref(props.goods?.carType || '');
    const partNumber = ref(props.goods?.partNumber || '');
    const minimalAvailable = ref(props.goods?.minimalAvailable || 0);
    const stockAvailable = ref(props.goods?.stockAvailable || 0);
    const purchasePrice = ref(props.goods?.purchasePrice || 0);

    function submitData() {
      emit('submit', {
        id: goodsId.value,
        goodsName: goodsName.value,
        goodsCode: goodsCode.value,
        carType: carType.value,
        partNumber: partNumber.value,
        minimalAvailable: minimalAvailable.value,
        stockAvailable: stockAvailable.value,
        purchasePrice: purchasePrice.value
      });
    }

    function reset() {
      goodsName.value = '';
      goodsCode.value = '';
      carType.value = '';
      partNumber.value = '';
      minimalAvailable.value = 0;
      stockAvailable.value = 0;
      purchasePrice.value = 0;
    }

    return {
      goodsId,
      goodsName,
      goodsCode,
      carType,
      partNumber,
      minimalAvailable,
      stockAvailable,
      purchasePrice,
      submitData,
      reset,
      requiredRule: (val: string) => (val && val.length > 0) || 'Mohon diisi',
      atLeastOneRule: (val: number) => (val && val > 0) || 'Mohon diisi'
    };
  }
});
</script>
