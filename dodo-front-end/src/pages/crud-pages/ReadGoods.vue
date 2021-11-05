<template>
  <q-page class="column items-left q-mt-xl">
    <h3 class="text-bold q-mx-lg q-mt-sm">Daftar Barang</h3>
    <q-table
      grid
      :rows="data.rows"
      :columns="data.columns"
      row-key="id"
      :filter="filter"
      hide-header
    >
      <template v-slot:top-right>
        <base-input borderless dense debounce="300" v-model="filter" placeholder="Search">
          <template v-slot:append>
            <q-icon name="search" />
          </template>
        </base-input>
      </template>
      <template v-slot:item="props">
        <div class="q-pa-xs col-11"
        >
          <q-card :class="props.selected ? 'bg-grey-2' : ''">
            <q-card-section align="right">
              <base-button icon="delete" />
            </q-card-section>
            <q-separator />
            <q-list dense>
              <q-item v-for="col in props.cols.filter(col => col.name !== 'desc')" :key="col.name">
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
import { defineComponent, ref, reactive } from 'vue';
import BaseInput from 'components/ui/BaseInput.vue';
import BaseButton from 'src/components/ui/BaseButton.vue';
// import { api } from 'boot/axios';

export default defineComponent({
  components: {
    BaseInput,
    BaseButton,
  },
  setup() {
    // const Name = ref('');
    // const Code = ref('');
    // const Category = ref('');
    // const MinimalAvailable = ref(0);
    const filter = ref('');
    const selected = ref([]);
    const data = reactive({
      rows: [
        {
          id: 1,
          Name: 'Frozen Yogurt',
          Code: '159',
          Category: '6.0',
          MinimalAvailable : 24
        },
        {
          id: 2,
          Name: 'Ice cream sandwich',
          Code: '159',
          Category: '6.0',
          MinimalAvailable : 24
        },
        {
          id: 3,
          Name: 'Frozen Yogurt',
          Code: '159',
          Category: '6.0',
          MinimalAvailable : 24
        },
      ],
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
          name: 'Name',
          align: 'left',
          label: 'Nama Barang',
          field: 'Name',
        },
        {
          name: 'Code',
          align: 'left',
          label: 'Kode Barang',
          field: 'Code',
        },
        {
          name: 'Category',
          align: 'left',
          label: 'Kategori Barang',
          field: 'Category',
        },
        {
          name: 'MinimalAvailable',
          align: 'left',
          label: 'Minimal Tersedia',
          field: 'MinimalAvailable',
        },
      ],
    });

    // const tanggalMasuk = ref<null | Date>(null);

    // function postGoods() {
    //   const response = api.post('/goods', {
    //     Name: 'string',
    //     Code: 'string',
    //     Category: 'string',
    //     MinimalAvailable: 0
    //   });
    //   console.log(response);
    // }

    // function getGoods() {
    //   const response = api.get('/goods');
    //   console.log(response);
    // }

    return {
      data,
      filter,
      selected,
      // Name,
      // Code,
      // Category,
      // MinimalAvailable,
      // columns,
      // rows,
      // tanggalMasuk,
      // getGoods,
    }
  }
});
</script>
