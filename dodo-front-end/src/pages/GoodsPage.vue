<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
    <h3 class="text-bold q-mx-lg q-mt-sm">Daftar Barang</h3>
    <!-- TODO: Pagination -->
    <q-table
      grid
      :rows="rows"
      :columns="goodsColumns"
      row-key="id"
      v-model:filter="filter"
      v-model:pagination="pagination"
      hide-header
    >
      <template v-slot:top-right>
        <base-button
          label="Tambah Barang"
          @click="showAddDialog()"
          class="q-mr-md"
        />
        <base-input
          borderless
          dense
          debounce="300"
          v-model="filter"
          placeholder="Search"
        >
          <template v-slot:append>
            <q-icon name="search" />
          </template>
        </base-input>
      </template>
      <template v-slot:item="props">
        <div class="q-pa-xs col-12">
          <base-card
            :class="props.selected ? 'bg-grey-2' : ''"
            style="height: 100px"
          >
            <q-card-section horizontal class="row">
              <q-card-section class="col">
                <p
                  class="text-overline q-pa-none q-ma-none"
                  style="line-height: 15px"
                >
                  {{ props.row.goodsCode }}
                </p>
                <p class="text-bold text-h5 q-pa-none q-ma-none">
                  {{ props.row.goodsName }}
                </p>
                <div class="row">
                  <p class="q-pr-md">{{ props.row.partNumber }}</p>
                  |
                  <p class="q-pl-md">{{ props.row.carType }}</p>
                </div>
              </q-card-section>

              <q-card-section class="text-right">
                <p class="text-overline q-ma-none" style="line-height: 15px">
                  Stok: {{ props.row.stockAvailable }}
                </p>
                <p
                  class="text-overline q-ma-none self-end"
                  style="line-height: 15px"
                >
                  Rp {{ props.row.purchasePrice }}
                </p>
                <q-card-actions align="right">
                  <base-button
                    @click="sendDeleteRequest(props.row.id)"
                    icon="delete"
                  />
                  <base-button
                    icon="edit"
                    @click="showUpdateDialog(props.row)"
                  />
                </q-card-actions>
              </q-card-section>
            </q-card-section>
          </base-card>
        </div>
      </template>
    </q-table>
  </q-page>
</template>

<script lang="ts">
import { defineComponent, ref, inject, watch } from 'vue';
import { IGoods } from 'src/models/interfaces/goods.interface';
import { ICreateResponse, IPagination } from 'src/models/responses.interface';
import { api } from 'boot/axios';
import { IPageFilter } from 'src/models/requests.interface';
import { AxiosError, AxiosResponse } from 'axios';
import { useQuasar } from 'quasar';
import { goodsColumns } from 'src/models/table-columns/goods-columns';
import GoodsFormDialog from 'components/goods/GoodsFormDialog.vue';
import BaseDialog from 'components/ui/BaseDialog.vue';
import BaseInput from 'components/ui/BaseInput.vue';
import BaseButton from 'components/ui/BaseButton.vue';
import BaseCard from 'components/ui/BaseCard.vue';

export default defineComponent({
  components: {
    BaseInput,
    BaseButton,
    BaseCard
  },
  setup() {
    const $q = useQuasar();
    const filter = ref('');
    const notifyError: ((err: unknown | AxiosError) => void) | undefined =
      inject('notifyError');

    const pagination = ref<IPageFilter>({
      page: 1,
      rowsPerPage: 5
    });

    const rows = ref<IGoods[]>([]);

    watch(
      () => pagination.value,
      async () => {
        await sendGetGoods();
      }
    );

    watch(
      () => filter.value,
      async () => {
        await sendGetGoods();
      }
    );

    async function sendGetGoods() {
      try {
        const response: AxiosResponse<IPagination<IGoods>> = await api.get(
          '/goods',
          {
            params: {
              ...pagination.value,
              searchText: filter.value
            }
          }
        );

        if (response.data.data) rows.value = response.data.data;
      } catch (err) {
        notifyError?.(err);
      }
    }

    function sendDeleteRequest(id: number) {
      $q.dialog({
        component: BaseDialog,
        componentProps: {
          title: 'Hapus barang',
          body: 'Yakin ingin menghapus barang?'
        }
      }).onOk(async () => {
        try {
          await api.delete(`/goods/${id}`);

          rows.value.splice(
            rows.value.findIndex((item) => item.id == id),
            1
          );
        } catch (err) {
          notifyError?.(err);
        }
      });
    }

    async function sendUpdateRequest(goods: IGoods) {
      try {
        await api.put(`/goods/${goods.id || -1}`, goods);

        rows.value[rows.value.findIndex((item) => item.id == goods.id)] = goods;
      } catch (err) {
        notifyError?.(err);
      }
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
      } catch (err) {
        notifyError?.(err);
      }
    }

    function showUpdateDialog(goods: IGoods) {
      $q.dialog({
        component: GoodsFormDialog,
        componentProps: {
          goods,
          title: 'Ubah data barang'
        }
      }).onOk(async (goods: IGoods) => {
        await sendUpdateRequest(goods);
      });
    }

    function showAddDialog() {
      $q.dialog({
        component: GoodsFormDialog,
        componentProps: {
          title: 'Tambah Barang'
        }
      }).onOk(async (goods: IGoods) => {
        await sendCreateRequest(goods);
      });
    }

    return {
      goodsColumns,
      rows,
      filter,
      sendDeleteRequest,
      sendGetGoods,
      showUpdateDialog,
      showAddDialog,
      pagination
    };
  }
});
</script>
