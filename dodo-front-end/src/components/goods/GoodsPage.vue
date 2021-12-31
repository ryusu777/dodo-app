<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
    <h3 class="text-bold q-mx-lg q-mt-sm">Daftar Barang</h3>
    <goods-table
      :rows="rows"
      row-key="id"
      @get-all="sendGetGoods"
      @create="sendCreateRequest"
      @update="sendUpdateRequest"
      @delete="sendDeleteRequest"
    />
  </q-page>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue';
import GoodsTable from 'components/goods/GoodsTable.vue';
import { IGoods } from 'src/models/goods';
import { AxiosResponse } from 'axios';
import { api } from 'src/boot/axios';
import { useQuasar } from 'quasar';
import { IPagination, ICreateResponse } from 'src/models/responses.interface';
import { IPageFilter } from 'src/models/requests.interface';
import BaseDialog from 'components/ui/BaseDialog.vue';
export default defineComponent({
  components: {
    GoodsTable
  },
  setup() {
    const $q = useQuasar();
    const rows = ref<IGoods[]>([]);
    const filter = ref('');

    onMounted(async () => await sendGetGoods({ page: 1, rowsPerPage: 5 }));

    async function sendGetGoods(requestPagination: IPageFilter) {
      try {
        const response: AxiosResponse<IPagination<IGoods>> = await api.get(
          '/goods',
          {
            params: {
              ...requestPagination,
              searchText: filter.value
            }
          }
        );

        if (response.data.data) {
          rows.value = response.data.data;
          requestPagination.rowsNumber = response.data.rowsNumber;
          requestPagination.page = response.data.pageNumber;
          requestPagination.searchText = response.data.searchText;
          requestPagination.rowsPerPage = response.data.itemPerPage;
        }
      } catch {}
    }

    function sendDeleteRequest(id: number) {
      $q.dialog({
        component: BaseDialog,
        componentProps: {
          title: 'Hapus barang',
          body: 'Yakin ingin menghapus barang?',
          okLabel: 'Hapus',
          cancelLabel: 'Tidak'
        }
      }).onOk(async () => {
        try {
          await api.delete(`/goods/${id}`);

          rows.value.splice(
            rows.value.findIndex((item) => item.id == id),
            1
          );
        } catch {}
      });
    }

    async function sendUpdateRequest(goods: IGoods) {
      try {
        await api.put(`/goods/${goods.id || -1}`, goods);
        rows.value[rows.value.findIndex((item) => item.id == goods.id)] = goods;
      } catch {}
    }

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
        rows.value.push({
          id: response.data.id,
          goodsName: goods.goodsName,
          goodsCode: goods.goodsCode,
          carType: goods.carType,
          partNumber: goods.partNumber,
          minimalAvailable: goods.minimalAvailable,
          stockAvailable: goods.stockAvailable,
          purchasePrice: goods.purchasePrice
        });
      } catch {}
    }

    return {
      rows,
      sendGetGoods,
      sendDeleteRequest,
      sendCreateRequest,
      sendUpdateRequest
    };
  }
});
</script>
