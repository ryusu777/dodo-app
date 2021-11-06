<template>
  <q-page class="q-mx-lg q-mt-xl">
    <h3 class="text-bold q-mt-sm">Tambah Barang</h3>
    <form
      @submit.prevent="sendCreateRequest"
      class="row justify-start q-gutter-y-lg"
    >
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
  </q-page>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { api } from 'boot/axios';
import BaseButton from 'components/ui/BaseButton.vue';
import BaseInput from 'components/ui/BaseInput.vue';
import { ICreateResponse } from 'src/domain/responses.interface';
// import BaseInputDate from 'components/ui/BaseInputDate.vue';
// import { EUserActions } from 'src/store/user/types/enumeration';

export default defineComponent({
  components: {
    BaseButton,
    BaseInput
    // BaseInputDate,
  },
  setup() {
    const goodsName = ref('');
    const goodsCode = ref('');
    const carType = ref('');
    const partNumber = ref('');
    const minimalAvailable = ref(0);
    const stockAvailable = ref(0);
    const purchasePrice = ref(0);

    async function sendCreateRequest(): Promise<void> {
      try {
        const response = await api.post<ICreateResponse>('/goods', {
          goodsName: goodsName.value,
          goodsCode: goodsCode.value,
          carType: carType.value,
          partNumber: partNumber.value,
          minimalAvailable: minimalAvailable.value,
          stockAvailable: stockAvailable.value,
          purchasePrice: purchasePrice.value
        });
        console.log(response);
      } catch (err) {
        console.log(err);
      }
    }

    return {
      goodsName,
      goodsCode,
      carType,
      partNumber,
      minimalAvailable,
      stockAvailable,
      purchasePrice,
      sendCreateRequest
    };
  }
});
</script>
