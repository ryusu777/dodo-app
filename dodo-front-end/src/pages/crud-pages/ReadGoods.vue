<template>
  <q-page class="column items-left q-mt-xl">
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
        <div class="q-pa-xs col-11">
          <q-card :class="props.selected ? 'bg-grey-2' : ''">
            <q-card-section align="right">
              <base-button
                @click="sendDeleteRequest(props.row.id)"
                icon="delete"
              />
            </q-card-section>
            <q-separator />
            <q-list dense class="q-pa-sm">
              <q-item v-for="col in props.cols" :key="col.name">
                <q-item-section>
                  <q-item-label>{{ col.label }}</q-item-label>
                </q-item-section>
                <q-item-section side>
                  <q-item-label caption>{{ col.value }}</q-item-label>
                </q-item-section>
              </q-item>
            </q-list>
          </q-card>
        </div>
      </template>
    </q-table>
  </q-page>
</template>

<script lang="ts">
import { defineComponent, ref, reactive, onMounted } from 'vue';
import BaseInput from 'components/ui/BaseInput.vue';
import BaseButton from 'src/components/ui/BaseButton.vue';
import { IGoods } from 'src/domain/goods.interface';
import { IPagination } from 'src/domain/responses.interface';
import { api } from 'src/boot/axios';
import { IPageFilter } from 'src/domain/requests.interface';
import { AxiosResponse } from 'axios';
// import { api } from 'boot/axios';

export default defineComponent({
  components: {
    BaseInput,
    BaseButton
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
