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
import axios from 'axios';
import { useQuasar } from 'quasar';

export default defineComponent({
  components: {
    GoodsForm
  },
  setup() {
    const $q = useQuasar();
    async function sendCreateRequest(goods: IGoods): Promise<void> {
      // TODO: Notify when successfully added and clear all input field
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
        if (axios.isAxiosError(err)) {
          const { response } = err;
          // eslint-disable-next-line
          response?.data.errors.forEach((element: string) => {
            $q.notify({
              message: element
            });
          });
        }
      }
    }

    // TODO: validateGoodsInfo()

    return {
      sendCreateRequest
    };
  }
});
</script>
