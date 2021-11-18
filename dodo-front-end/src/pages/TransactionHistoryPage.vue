<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
    <h3 class="text-bold q-mx-lg q-mt-sm">History Transaction</h3>
    <q-table grid :rows="rows" row-key="id" :filter="filter" hide-header>
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
      </template>
      <template v-slot:item="props">
        <div class="q-pa-xs col-12">
          <base-card
            :class="props.selected ? 'bg-grey-2' : ''"
            style="height: 90px"
          >
            <q-card-section horizontal class="row">
              <q-card-section class="col">
                <p class="text-bold text-h5 q-pa-none q-ma-none">
                  {{
                    props.row.transactionType === 'sell'
                      ? 'Pemasukan'
                      : 'Pengeluaran'
                  }}
                </p>
                <div class="row">
                  <p class="q-pr-md">Rp {{ props.row.totalPrice }}</p>
                </div>
              </q-card-section>

              <q-card-section class="text-right">
                <p class="text-overline q-ma-none" style="line-height: 15px">
                  {{ formattedDate(props.row.createdDate) }}
                </p>
                <q-card-actions align="right">
                  <base-button
                    label="Detail"
                    @click="showDetail(props.row.id)"
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
import { defineComponent, ref, onMounted, inject } from 'vue';
import { ITransactionHeader } from '../models/interfaces/transaction.interface';
import { IPagination } from 'src/models/responses.interface';
import { api } from 'boot/axios';
import { IPageFilter } from 'src/models/requests.interface';
import { AxiosError, AxiosResponse } from 'axios';
import { goodsColumns } from 'src/models/table-columns/goods-columns';
import BaseInputDate from 'components/ui/BaseInputDate.vue';
import BaseButton from 'components/ui/BaseButton.vue';
import BaseCard from 'components/ui/BaseCard.vue';
import { date, useQuasar } from 'quasar';
import TransactionHistoryDialog from 'src/components/transaction/TransactionHistoryDialog.vue';

export default defineComponent({
  components: {
    BaseInputDate,
    BaseButton,
    BaseCard
  },
  setup() {
    const filter = ref('');
    const notifyError: ((err: unknown | AxiosError) => void) | undefined =
      inject('notifyError');
    const $q = useQuasar();

    const pagination = ref<IPageFilter>({
      page: 1,
      rowsPerPage: 5
    });

    const purchaseDateFrom = ref('');
    const purchaseDateTo = ref('');
    const receiveDateFrom = ref('');
    const receiveDateTo = ref('');

    const transactionHeader = ref<ITransactionHeader>();

    const rows = ref<ITransactionHeader[]>([]);

    onMounted(async () => {
      try {
        const response: AxiosResponse<IPagination<ITransactionHeader>> =
          await api.get('/transaction/header', {
            params: {
              ...pagination.value
            }
          });

        if (response.data.data) rows.value = response.data.data;
      } catch (err) {
        notifyError?.(err);
      }
    });

    function formattedDate(value: Date | string) {
      return date.formatDate(value, 'ddd D MMM, YYYY');
    }

    function showDetail(id: number) {
      $q.dialog({
        component: TransactionHistoryDialog,
        componentProps: {
          headerId: id
        }
      });
    }

    // TODO: Transaction History Date Filter

    return {
      goodsColumns,
      rows,
      filter,
      transactionHeader,
      formattedDate,
      receiveDateFrom,
      receiveDateTo,
      purchaseDateFrom,
      purchaseDateTo,
      showDetail
    };
  }
});
</script>
