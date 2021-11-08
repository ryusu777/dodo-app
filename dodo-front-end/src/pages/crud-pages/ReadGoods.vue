<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
    <!-- TODO: Create Goods with dialog -->
    <h3 class="text-bold q-mx-lg q-mt-sm">Daftar Barang</h3>
    <q-table
      grid
      :rows="rows"
      :columns="data.columns"
      row-key="id"
      :filter="filter"
      hide-header
    >
      <template v-slot:top-right>
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
import { defineComponent, ref, reactive, onMounted } from 'vue';
import BaseInput from 'components/ui/BaseInput.vue';
import BaseButton from 'src/components/ui/BaseButton.vue';
import BaseCard from 'src/components/ui/BaseCard.vue';
import { IGoods } from 'src/domain/goods.interface';
import { IPagination } from 'src/domain/responses.interface';
import { api } from 'src/boot/axios';
import axios from 'axios';
import { IPageFilter } from 'src/domain/requests.interface';
import { AxiosResponse } from 'axios';
import { useQuasar } from 'quasar';
import GoodsFormDialog from 'src/components/goods/GoodsFormDialog.vue';
import BaseDialog from 'src/components/ui/BaseDialog.vue';

export default defineComponent({
  components: {
    BaseInput,
    BaseButton,
    BaseCard
  },
  setup() {
    const $q = useQuasar();
    const filter = ref('');
    const data = reactive({
      columns: [
        {
          name: 'id',
          required: true,
          label: 'ID',
          field: 'id',
          align: 'left',
          sortable: true
        },
        {
          name: 'goodsName',
          align: 'left',
          label: 'Nama Barang',
          field: 'goodsName'
        },
        {
          name: 'goodsCode',
          align: 'left',
          label: 'Kode Barang',
          field: 'goodsCode'
        },
        {
          name: 'carType',
          align: 'left',
          label: 'Kategori Barang',
          field: 'carType'
        },
        {
          name: 'partNumber',
          align: 'left',
          label: 'Part Number',
          field: 'partNumber'
        },
        {
          name: 'minimalAvailable',
          align: 'left',
          label: 'Minimal Tersedia',
          field: 'minimalAvailable'
        },
        {
          name: 'stockAvailable',
          align: 'left',
          label: 'Stok Tersedia',
          field: 'stockAvailable'
        },
        {
          name: 'purchasePrice',
          align: 'left',
          label: 'Harga Beli',
          field: 'purchasePrice'
        }
      ]
    });

    const pagination = ref<IPageFilter>({
      page: 1,
      rowsPerPage: 5
    });

    const rows = ref<IGoods[]>([]);

    onMounted(async () => {
      try {
        const response: AxiosResponse<IPagination<IGoods>> = await api.get(
          '/goods',
          {
            params: {
              ...pagination.value
            }
          }
        );

        if (response.data.data) rows.value = response.data.data;
      } catch (err) {
        console.log(err);
      }
    });

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
      });
    }

    async function sendUpdateRequest(goods: IGoods) {
      try {
        await api.put(`/goods/${goods.id || -1}`, goods);

        rows.value[rows.value.findIndex((item) => item.id == goods.id)] = goods;
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

    function showUpdateDialog(goods: IGoods) {
      $q.dialog({
        component: GoodsFormDialog,
        componentProps: {
          goods
        }
      }).onOk(async (goods: IGoods) => {
        await sendUpdateRequest(goods);
      });
    }
    return {
      data,
      rows,
      filter,
      sendDeleteRequest,
      showUpdateDialog
    };
  }
});
</script>
