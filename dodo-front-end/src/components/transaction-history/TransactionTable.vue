<template>
  <q-table
    grid
    :rows="rows"
    :filter="filter"
    v-model:pagination="modelPagination"
    hide-header
    @request="$emit('paging', $event.pagination)"
  >
    <template v-slot:top-right>
      <base-input-date
        borderless
        dense
        debounce="300"
        label="Purchase Date dari "
        v-model="purchaseDateFrom"
      />
      <base-input-date
        borderless
        dense
        debounce="300"
        label="Purchase Date sampai "
        v-model="purchaseDateTo"
      />
      <base-input-date
        borderless
        dense
        debounce="300"
        label="Receive Date dari "
        v-model="receiveDateFrom"
      />
      <base-input-date
        borderless
        dense
        debounce="300"
        label="Receive Date sampai "
        v-model="receiveDateTo"
      />
      <base-button
        label="Submit"
        @click="
          $emit('filter', {
            purchaseDateFrom,
            purchaseDateTo,
            receiveDateFrom,
            receiveDateTo
          })
        "
      />
      <base-button label="Clear" @click="clearFilter" />
    </template>
    <template v-slot:item="props">
      <div class="q-pa-xs col-12">
        <base-card
          :class="props.selected ? 'bg-grey-2' : ''"
          style="height: 90px"
        >
          <q-card-section horizontal class="row">
            <!-- TODO: Visual presentation of completed or not completed transaction -->
            <q-card-section class="col">
              <!-- TODO: Transaction title -->
              <p
                class="text-bold text-h5 q-pa-none q-ma-none text-indigo-8"
                v-if="props.row.transactionType === 'sell'"
              >
                Penjualan
              </p>
              <p
                class="text-bold text-h5 q-pa-none q-ma-none text-yellow-10"
                v-else
              >
                Pembelian
              </p>
              <div class="row">
                <p class="q-pr-md">Rp {{ props.row.totalPrice }}</p>
              </div>
            </q-card-section>

            <q-card-section class="text-right">
              <p class="text-overline q-ma-none" style="line-height: 15px">
                Dibuat:
                <strong>{{ formattedDate(props.row.createdDate) }}</strong>
              </p>
              <q-card-actions align="right">
                <!-- TODO: Delete transaction -->
                <base-button
                  label="Detail"
                  @click="$emit('get', props.row.id)"
                />
              </q-card-actions>
            </q-card-section>
          </q-card-section>
        </base-card>
      </div>
    </template>
  </q-table>
</template>

<script lang="ts">
// TODO: Set purchase date or receive date on calendar
import { defineComponent, ref, computed, PropType } from 'vue';
import { ITransactionHeader } from 'src/models/transaction';
import { IPageFilter } from 'src/models/requests.interface';
import BaseInputDate from 'components/ui/BaseInputDate.vue';
import BaseButton from 'components/ui/BaseButton.vue';
import BaseCard from 'components/ui/BaseCard.vue';
import { date } from 'quasar';

export default defineComponent({
  components: {
    BaseInputDate,
    BaseButton,
    BaseCard
  },
  emits: ['create', 'paging', 'get', 'filter'],
  props: {
    rows: {
      type: Array as PropType<ITransactionHeader[]>,
      required: false
    },
    pagination: {
      type: Object as PropType<IPageFilter>,
      required: true
    }
  },

  setup(props) {
    const filter = ref('');

    const requestPagination = ref<IPageFilter>({
      page: 1,
      rowsPerPage: 5
    });

    const modelPagination = computed({
      get(): Omit<IPageFilter, 'descending'> {
        return props.pagination;
      },
      set() {
        // Dummy code
        console.log();
      }
    });

    const purchaseDateFrom = ref('');
    const purchaseDateTo = ref('');
    const receiveDateFrom = ref('');
    const receiveDateTo = ref('');

    function clearFilter() {
      purchaseDateFrom.value = '';
      purchaseDateTo.value = '';
      receiveDateFrom.value = '';
      receiveDateTo.value = '';
    }

    function formattedDate(value: Date | string) {
      return date.formatDate(value, 'ddd D MMM, YYYY');
    }

    return {
      filter,
      formattedDate,
      receiveDateFrom,
      receiveDateTo,
      purchaseDateFrom,
      purchaseDateTo,
      requestPagination,
      clearFilter,
      modelPagination
    };
  }
});
</script>
