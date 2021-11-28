<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
    <h3 class="text-bold q-mx-lg q-mt-sm">Daily Expenses</h3>
    <q-table
      grid
      :rows="rows"
      row-key="id"
      v-model:filter="filter"
      v-model:pagination="requestPagination"
      @request="handleRequest"
    >
      <template v-slot:top-right>
        <base-button
          label="Tambah"
          @click="showAddDialog()"
          class="q-mr-md"
        />
      </template>
      <template v-slot:item="props">
        <div class="q-pa-xs col-12">
          <base-card
            :class="props.selected ? 'bg-grey-2' : ''"
            style="height: 100px"
          >
            <q-card-section horizontal class="row">
              <q-card-section class="col q-pt-sm">
                <p class="text-bold text-h5 q-pa-none q-ma-none text-yellow-10" v-if="props.row.changingAmount < 0">
                  Pengeluaran
                </p>
                <p class="text-bold text-h5 q-pa-none q-ma-none text-indigo-8" v-if="props.row.changingAmount > 0">
                  Pemasukkan
                </p>
                <div class="row">
                  <p class="q-mb-none q-mt-none" v-if="props.row.changingAmount < 0"><b>Changing: </b>Rp{{ (-1 * props.row.changingAmount) }}</p>
                  <p class="q-mb-none q-mt-none" v-if="props.row.changingAmount > 0"><b>Changing: </b>Rp{{ props.row.changingAmount}}</p>
                  <p class="q-mt-none"><b>Deskripsi: </b>{{ props.row.changeDescription }}</p>
                </div>
              </q-card-section>

              <q-card-section class="text-right">
                <p class="text-overline q-ma-none" style="line-height: 15px">
                  {{ formattedDate(props.row.dateOfChange) }}
                </p>
                <p class="q-mb-none"><b>Currency: </b>Rp{{ props.row.currencyAmount}}</p>
                <base-button 
                  label="Detail"
                  v-if="props.row.transactionHeaderId !== null"
                  @click="showDetail(
                    props.row.id,
                  )"
                />
              </q-card-section>

            </q-card-section>
          </base-card>
        </div>
      </template>
    </q-table>
  </q-page>
</template>

<script lang="ts">
import { defineComponent, ref, inject, onMounted } from 'vue';
import { ICurrency } from 'src/models/interfaces/currency.interface';
import { ICreateResponse, IPagination } from 'src/models/responses.interface';
import { api } from 'boot/axios';
import { IPageFilter } from 'src/models/requests.interface';
import { AxiosError, AxiosResponse } from 'axios';
import { date, useQuasar } from 'quasar';
import CurrencyFormDialog from 'components/currency/CurrencyFormDialog.vue';
import BaseButton from 'components/ui/BaseButton.vue';
import BaseCard from 'components/ui/BaseCard.vue';
import TransactionHistoryDialog from 'src/components/transaction/TransactionHistoryDialog.vue';

export default defineComponent({
  components: {
    BaseButton,
    BaseCard
  },
  setup() {
    const $q = useQuasar();
    const filter = ref('');
    const notifyError: ((err: unknown | AxiosError) => void) | undefined =
      inject('notifyError');

    const requestPagination = ref<IPageFilter>({
      page: 1,
      rowsPerPage: 5
    });

    const rows = ref<ICurrency[]>([]);

    onMounted(async () => await sendGetCurrency());

    async function handleRequest({ pagination }: { pagination: IPageFilter }) {
      requestPagination.value = pagination;
      await sendGetCurrency();
    }

    async function sendGetCurrency() {
      try {
        const response: AxiosResponse<IPagination<ICurrency>> = await api.get(
          '/currency',
          {
            params: {
              ...requestPagination.value,
              searchText: filter.value
            }
          }
        );

        if (response.data.data) {
          rows.value = response.data.data;
          requestPagination.value.rowsNumber = response.data.rowsNumber;
          requestPagination.value.page = response.data.pageNumber;
          requestPagination.value.searchText = response.data.searchText;
          requestPagination.value.rowsPerPage = response.data.itemPerPage;
        }
      } catch (err) {
        notifyError?.(err);
      }
    }

    async function sendCreateRequest(currency: ICurrency): Promise<void> {
      try {
        const response = await api.post<ICreateResponse>('/currency', {
          changingAmount: currency.changingAmount,
          changeDescription: currency.changeDescription,
        });
        rows.value.push({
          id: response.data.id,
          changingAmount: currency.changingAmount,
          changeDescription: currency.changeDescription,
        });
      } catch (err) {
        notifyError?.(err);
      }
    }

    function showAddDialog() {
      $q.dialog({
        component: CurrencyFormDialog,
        componentProps: {
          title: 'Tambah Daftar'
        }
      }).onOk(async (currency: ICurrency) => {
        await sendCreateRequest(currency);
      });
    }

    function formattedDate(value: Date | undefined) {
      if (value) return date.formatDate(value, 'dddd, D MMMM YYYY');
      return undefined;
    }

    function showDetail(
      id: number,
    ) {
      $q.dialog({
        component: TransactionHistoryDialog,
        componentProps: {
          headerId: id,
        }
      });
    }

    return {
      rows,
      filter,
      sendGetCurrency,
      showAddDialog,
      requestPagination,
      handleRequest,
      formattedDate,
      showDetail
    };
  },
});
</script>
