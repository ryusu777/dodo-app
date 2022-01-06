<template>
  <q-table
    grid
    @request="$emit('paging', $event.pagination)"
    :rows="rows"
    v-model:pagination="modelPagination"
  >
    <template v-slot:item="props">
      <div class="q-pa-xs col-12">
        <base-card :class="props.selected ? 'bg-grey-2' : ''">
          <q-card-section horizontal class="row">
            <q-card-section class="col q-pt-sm">
              <p
                class="text-bold text-h5 q-pa-none q-ma-none text-green-8"
                v-if="
                  (props.row.changingProfitAmount < 0 &&
                    props.row.changingFundAmount > 0) ||
                  (props.row.changingProfitAmount > 0 &&
                    props.row.changingFundAmount < 0)
                "
              >
                Konversi
              </p>
              <p
                class="text-bold text-h5 q-pa-none q-ma-none text-yellow-10"
                v-else-if="props.row.changingProfitAmount < 0"
              >
                Pengeluaran
              </p>
              <p
                class="text-bold text-h5 q-pa-none q-ma-none text-indigo-8"
                v-else-if="
                  props.row.changingProfitAmount > 0 &&
                  props.row.changingFundAmount > 0
                "
              >
                Penjualan barang
              </p>
              <p
                class="text-bold text-h5 q-pa-none q-ma-none text-indigo-8"
                v-else-if="props.row.changingProfitAmount > 0"
              >
                Pemasukan keuntungan
              </p>
              <p
                class="text-bold text-h5 q-pa-none q-ma-none text-yellow-10"
                v-else-if="props.row.changingFundAmount < 0"
              >
                Restock barang
              </p>
              <p
                class="text-bold text-h5 q-pa-none q-ma-none text-indigo-8"
                v-else-if="props.row.changingFundAmount > 0"
              >
                Pemasukan permodalan
              </p>
              <div class="column">
                <p
                  class="q-pa-none q-ma-none"
                  v-if="props.row.changingProfitAmount"
                >
                  <b>Perubahan keuntungan: </b
                  >{{ props.row.changingProfitAmount }}
                </p>
                <p
                  class="q-pa-none q-ma-none"
                  v-if="props.row.changingFundAmount"
                >
                  <b>Perubahan permodalan: </b
                  >{{ props.row.changingFundAmount }}
                </p>
                <p class="q-my-none" v-if="!props.row.transactionHeaderId">
                  <b>Deskripsi: </b>{{ props.row.changeDescription }}
                </p>
              </div>
            </q-card-section>

            <q-card-section class="text-right">
              <p class="text-overline q-ma-none" style="line-height: 15px">
                {{ formattedDate(props.row.dateOfChange) }}
              </p>
              <base-button
                class="q-mt-sm"
                label="Detail"
                v-if="props.row.transactionHeaderId !== null"
                @click="$emit('get', props.row.transactionHeaderId)"
              />
            </q-card-section>
          </q-card-section>
        </base-card>
      </div>
    </template>
  </q-table>
</template>

<script lang="ts">
import { defineComponent, ref, PropType, computed } from 'vue';
import { ICurrency } from 'src/models/currency';
import { IPageFilter } from 'src/models/requests.interface';
import { date } from 'quasar';
import BaseButton from 'components/ui/BaseButton.vue';
import BaseCard from 'components/ui/BaseCard.vue';

export default defineComponent({
  components: {
    BaseButton,
    BaseCard
  },
  emits: ['create', 'get', 'paging'],
  props: {
    rows: {
      type: Array as PropType<ICurrency[]>,
      required: false
    },
    pagination: {
      type: Object as PropType<IPageFilter>,
      required: true
    }
  },

  setup(props) {
    const filter = ref('');

    const modelPagination = computed({
      get(): Omit<IPageFilter, 'descending'> {
        return props.pagination;
      },
      set() {
        // Dummy code
        console.log();
      }
    });

    function formattedDate(value: Date | undefined) {
      if (value) return date.formatDate(value, 'dddd, D MMMM YYYY');
      return undefined;
    }

    return {
      filter,
      formattedDate,
      modelPagination
    };
  }
});
</script>
