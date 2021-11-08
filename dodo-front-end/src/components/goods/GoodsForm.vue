<template>
  <form @submit.prevent="submitData" class="row justify-start q-gutter-y-lg">
    <!-- TODO: Id field -->
    <BaseInput v-model="goodsName" class="col-11" label="Nama Barang" />

    <BaseInput v-model="goodsCode" class="col-11" label="Kode Barang" />

    <BaseInput v-model="carType" class="col-11" label="Kategori Barang" />

    <BaseInput v-model="partNumber" class="col-11" label="Part Number" />

    <BaseInput
      v-model="minimalAvailable"
      class="col-11"
      label="Minimal Tersedia"
      type="number"
    />

    <BaseInput
      v-model="stockAvailable"
      class="col-11"
      label="Stok Tersedia"
      type="number"
    />

    <BaseInput
      v-model="purchasePrice"
      class="col-11"
      label="Harga Beli"
      type="number"
    />

    <!-- <BaseInputDate
        v-model="tanggalMasuk"
      >
      <b>Tanggal Masuk</b>
      </BaseInputDate> -->

    <base-button class="col-2 q-mt-md" type="submit"> Submit </base-button>
  </form>
</template>

<script lang="ts">
import { defineComponent, ref, PropType } from 'vue';
import BaseButton from 'components/ui/BaseButton.vue';
import BaseInput from 'components/ui/BaseInput.vue';
import { IGoods } from 'src/domain/goods.interface';

export default defineComponent({
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
        goodsId: goodsId.value,
        goodsName: goodsName.value,
        goodsCode: goodsCode.value,
        carType: carType.value,
        partNumber: partNumber.value,
        minimalAvailable: minimalAvailable.value,
        stockAvailable: stockAvailable.value,
        purchasePrice: purchasePrice.value
      });
    }
    // TODO: validateGoodsInfo()

    return {
      goodsId,
      goodsName,
      goodsCode,
      carType,
      partNumber,
      minimalAvailable,
      stockAvailable,
      purchasePrice,
      submitData
    };
  }
});
</script>
