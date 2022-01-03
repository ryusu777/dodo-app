<template>
  <q-table
    grid
    :rows="rows"
    v-model:pagination="modelPagination"
    @request="$emit('paging', $event.pagination)"
  >
    <template v-slot:top-right>
      <base-button
        :label="sortByStock ? 'Kembalikan ke semula' : 'Stok terkecil'"
        @click="sortByStock = !sortByStock"
      />
      <base-button
        label="Tambah Barang"
        @click="showAddDialog"
        class="q-mr-md"
      />
      <base-input
        borderless
        dense
        debounce="300"
        :model-value="modelPagination.searchText"
        @update:model-value="$emit('search', $event)"
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
              <p
                class="text-bold text-h5 q-pa-none q-ma-none"
                :class="{
                  'text-yellow-10':
                    props.row.minimalAvailable > props.row.stockAvailable
                }"
              >
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
                  @click="$emit('delete', props.row.id)"
                  icon="delete"
                />
                <base-button icon="edit" @click="showUpdateDialog(props.row)" />
              </q-card-actions>
            </q-card-section>
          </q-card-section>
        </base-card>
      </div>
    </template>
  </q-table>
</template>

<script lang="ts">
import { defineComponent, ref, PropType, computed } from 'vue';
import { IGoods } from 'src/models/goods';
import { useQuasar } from 'quasar';
import GoodsFormDialog from 'components/goods/GoodsFormDialog.vue';
import BaseInput from 'components/ui/BaseInput.vue';
import BaseButton from 'components/ui/BaseButton.vue';
import BaseCard from 'components/ui/BaseCard.vue';
import { IPageFilter } from 'src/models/requests.interface';

export default defineComponent({
  components: {
    BaseInput,
    BaseButton,
    BaseCard
  },
  emits: ['create', 'paging', 'update', 'delete', 'search'],
  props: {
    rows: {
      type: Array as PropType<IGoods[]>,
      required: false
    },
    pagination: {
      type: Object as PropType<IPageFilter>,
      required: true
    }
  },
  setup(props, { emit }) {
    const $q = useQuasar();
    const filter = ref('');

    const modelPagination = computed({
      get(): Omit<IPageFilter, 'descending'> {
        return props.pagination;
      },
      set() {
        console.log();
      }
    });

    const sortByStock = ref(false);

    function showAddDialog() {
      $q.dialog({
        component: GoodsFormDialog,
        componentProps: {
          title: 'Tambah Barang'
        }
      }).onOk((goods: IGoods) => {
        emit('create', goods);
      });
    }

    function showUpdateDialog(goods: IGoods) {
      $q.dialog({
        component: GoodsFormDialog,
        componentProps: {
          goods,
          title: 'Ubah data barang'
        }
      }).onOk((goods: IGoods) => {
        emit('update', goods);
      });
    }

    return {
      filter,
      sortByStock,
      modelPagination,
      showAddDialog,
      showUpdateDialog
    };
  }
});
</script>
