<template>
  <q-page class="q-mx-lg q-mt-xl">
    <h3 class="text-bold q-mt-sm">Tambah Barang</h3>
    <goods-form @submit="sendCreateRequest"></goods-form>
  </q-page>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { api } from 'boot/axios';
import { ICreateResponse } from 'src/domain/responses.interface';
import GoodsForm from 'src/components/goods/GoodsForm.vue';
import { IGoods } from 'src/domain/goods.interface';

export default defineComponent({
  components: {
    GoodsForm
  },
  setup() {
    async function sendCreateRequest(goods: IGoods): Promise<void> {
      try {
        const response = await api.post<ICreateResponse>('/goods', {
          goodsName: goods.goodsName,
          goodsCode: goods.goodsCode,
          carType: goods.carType,
          partNumber: goods.partNumber,
          minimalAvailable: goods.minimalAvailable,
          stockAvailable: goods.stockAvailable,
          purchasePrice: goods.purchasePrice
        });
        console.log(response);
      } catch (err) {
        console.log(err);
      }
    }

    // TODO: validateGoodsInfo()

    return {
      sendCreateRequest
    };
  }
});
</script>
