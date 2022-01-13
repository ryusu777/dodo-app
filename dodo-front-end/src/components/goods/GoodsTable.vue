<template>
  <q-table
    grid
    :rows="rows"
    v-model:pagination="modelPagination"
    @request="$emit('paging', $event.pagination)"
  >
    <template v-slot:top-right>
      <slot name="top-right"></slot>
      <base-button
        v-if="!transactionType"
        :label="sortByStock ? 'Kembalikan ke semula' : 'Stok terkecil'"
        @click="sortByStock = !sortByStock"
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
                Stok: {{ props.row.stockAvailable }}/{{
                  props.row.minimalAvailable
                }}
              </p>
              <p
                class="text-overline q-ma-none self-end"
                style="line-height: 15px"
              >
                Rp {{ props.row.purchasePrice }}
              </p>
              <q-card-actions v-if="!transactionType" align="right">
                <base-button
                  @click="$emit('delete', props.row.id)"
                  icon="delete"
                />
                <base-button
                  icon="edit"
                  @click="$emit('update-request', props.row)"
                />
              </q-card-actions>
              <q-card-actions v-else align="right">
                <base-button label="Tambah" />
                <q-popup-proxy :breakpoint="100">
                  <base-card class="q-pa-sm">
                    <base-input
                      v-model="props.row.transactionAmount"
                      class="col-12 q-my-sm"
                      label="Jumlah Barang"
                      type="number"
                    />
                    <base-input
                      v-if="transactionType == 'sell'"
                      v-model="props.row.sellPrice"
                      class="col-12 q-my-sm"
                      label="Harga Jual"
                      type="number"
                    />
                    <base-button
                      label="Submit"
                      :disabled="
                        !(
                          props.row.transactionAmount &&
                          (transactionType === 'sell'
                            ? props.row.sellPrice
                            : true)
                        )
                      "
                      @click="
                        $emit('add-to-cart', {
                          goodsId: props.row.id,
                          goodsAmount: props.row.transactionAmount,
                          pricePerItem:
                            transactionType === 'sell'
                              ? props.row.sellPrice
                              : props.row.purchasePrice
                        })
                      "
                      v-close-popup="1"
                    />
                  </base-card>
                </q-popup-proxy>
              </q-card-actions>
            </q-card-section>
          </q-card-section>
        </base-card>
      </div>
    </template>
  </q-table>
</template>

<script lang="ts">
import { defineComponent, ref, PropType, computed, watch } from 'vue';
import { IGoods } from 'src/models/goods';
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
  emits: ['paging', 'update-request', 'delete', 'search', 'add-to-cart'],
  props: {
    rows: {
      type: Array as PropType<IGoods[]>,
      required: false
    },
    pagination: {
      type: Object as PropType<IPageFilter>,
      required: true
    },
    transactionType: String
  },
  setup(props, { emit }) {
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

    watch(
      () => sortByStock.value,
      () => {
        emit('paging', {
          ...props.pagination,
          sortBy: sortByStock.value ? 'StockAvailable' : null,
          descending: sortByStock.value ? 'ASC' : null
        });
      }
    );

    return {
      filter,
      sortByStock,
      modelPagination
    };
  }
});
</script>
