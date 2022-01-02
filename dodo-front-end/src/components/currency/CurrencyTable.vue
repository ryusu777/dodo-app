<template>
  <q-table
    grid
    @request="$emit('paging', $event.pagination)"
    :rows="rows"
    v-model:pagination="modelPagination"
  >
    <template v-slot:top-right>
      <!-- TODO: Convert profit to fund -->
      <base-button label="Tambah" @click="showAddDialog" class="q-mr-md" />
    </template>
    <template v-slot:item="props">
      <div class="q-pa-xs col-12">
        <base-card :class="props.selected ? 'bg-grey-2' : ''">
          <q-card-section horizontal class="row">
            <q-card-section class="col q-pt-sm">
              <p
                class="text-bold text-h5 q-pa-none q-ma-none text-yellow-10"
                v-if="props.row.changingAmount <= 0"
              >
                Pengeluaran
              </p>
              <p
                class="text-bold text-h5 q-pa-none q-ma-none text-indigo-8"
                v-if="props.row.changingAmount > 0"
              >
                Pemasukkan
              </p>
              <div class="column">
                <p
                  class="q-mb-none q-mt-none"
                  v-if="props.row.changingAmount < 0"
                >
                  <b>Changing: </b>Rp{{ -1 * props.row.changingAmount }}
                </p>
                <p
                  class="q-mb-none q-mt-none"
                  v-if="props.row.changingAmount > 0"
                >
                  <b>Changing: </b>Rp{{ props.row.changingAmount }}
                </p>
                <p class="q-my-none">
                  <b>Deskripsi: </b>{{ props.row.changeDescription }}
                </p>
              </div>
            </q-card-section>

            <q-card-section class="text-right">
              <p class="text-overline q-ma-none" style="line-height: 15px">
                {{ formattedDate(props.row.dateOfChange) }}
              </p>
              <p class="q-mb-none">
                <b>Currency: </b>Rp{{ props.row.currencyAmount }}
              </p>
              <base-button
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
import { date, useQuasar } from 'quasar';
import CurrencyFormDialog from 'components/currency/CurrencyFormDialog.vue';
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

  setup(props, { emit }) {
    const $q = useQuasar();
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

    function showAddDialog() {
      $q.dialog({
        component: CurrencyFormDialog,
        componentProps: {
          title: 'Tambah Daftar'
        }
      }).onOk((currency: ICurrency) => {
        emit('create', currency);
      });
    }

    function formattedDate(value: Date | undefined) {
      if (value) return date.formatDate(value, 'dddd, D MMMM YYYY');
      return undefined;
    }

    return {
      filter,
      showAddDialog,
      formattedDate,
      modelPagination
    };
  }
});
</script>
