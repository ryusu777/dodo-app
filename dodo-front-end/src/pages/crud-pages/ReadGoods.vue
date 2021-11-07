<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
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
                <q-card-action>
                  <base-button
                    class="q-mt-sm"
                    @click="sendDeleteRequest(props.row.id)"
                    icon="delete"
                  />
                  <base-button class="q-mt-sm" icon="edit" />
                </q-card-action>
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
import { IPageFilter } from 'src/domain/requests.interface';
import { AxiosResponse } from 'axios';
// import { api } from 'boot/axios';

export default defineComponent({
  components: {
    BaseInput,
    BaseButton,
    BaseCard
  },
  setup() {
    // const Name = ref('');
    // const Code = ref('');
    // const Category = ref('');
    // const MinimalAvailable = ref(0);
    const filter = ref('');
    const selected = ref([]);
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

    async function sendDeleteRequest(id: number): Promise<void> {
      try {
        await api.delete(`/goods/${id}`);

        rows.value.splice(
          rows.value.findIndex((item) => item.id == id),
          1
        );
      } catch (err) {
        console.log(err);
      }
    }
    // function getGoods() {
    //   const response = api.get('/goods');
    //   console.log(response);
    // }

    return {
      data,
      rows,
      filter,
      sendDeleteRequest,
      selected
    };
  }
});
</script>
