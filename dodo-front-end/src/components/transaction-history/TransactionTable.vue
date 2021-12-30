<template>
    <q-table
        grid
        :rows="rows"
        :filter="filter"
        hide-header
        @request="$emit('getAll', requestPagination)"
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
            <base-button label="Submit" @click="sendHeaderFilter" />
            <base-button label="Clear" @click="clearFilter" />
        </template>
        <template v-slot:item="props">
            <div class="q-pa-xs col-12">
                <base-card
                    :class="props.selected ? 'bg-grey-2' : ''"
                    style="height: 90px"
                >
                    <q-card-section horizontal class="row">
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
                                {{ formattedDate(props.row.createdDate) }}
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
import { defineComponent, ref, onMounted, inject, PropType } from 'vue';
import { ITransactionHeader } from 'src/models/transaction';
import { IPagination } from 'src/models/responses.interface';
import { api } from 'boot/axios';
import { IPageFilter } from 'src/models/requests.interface';
import { AxiosError } from 'axios';
import BaseInputDate from 'components/ui/BaseInputDate.vue';
import BaseButton from 'components/ui/BaseButton.vue';
import BaseCard from 'components/ui/BaseCard.vue';
import { date, useQuasar } from 'quasar';


export default defineComponent({
  components: {
    BaseInputDate,
    BaseButton,
    BaseCard
  },

  emits: ['create', 'getAll', 'get'],
  props: {
    rows: {
      type: Array as PropType<ITransactionHeader[]>,
      required: false
    }
  },

  setup() {
    const filter = ref('');
    // const notifyError: ((err: unknown | AxiosError) => void) | undefined =
    //   inject('notifyError');

    const requestPagination = ref<IPageFilter>({
      page: 1,
      rowsPerPage: 5
    });

    // const purchaseDateFrom = ref('');
    // const purchaseDateTo = ref('');
    // const receiveDateFrom = ref('');
    // const receiveDateTo = ref('');

    const transactionHeader = ref<ITransactionHeader>();

    // const rows = ref<ITransactionHeader[]>([]);


    // async function clearFilter() {
    //   purchaseDateFrom.value = '';
    //   purchaseDateTo.value = '';
    //   receiveDateFrom.value = '';
    //   receiveDateTo.value = '';
    //   await sendGetHeaders();
    // }
    // async function sendHeaderFilter() {
    //   try {
    //     const response = await api.post<IPagination<ITransactionHeader>>(
    //       '/transaction/header-filter',
    //       {
    //         purchaseDateFrom: purchaseDateFrom.value || null,
    //         purchaseDateTo: purchaseDateTo.value || null,
    //         receiveDateFrom: receiveDateFrom.value || null,
    //         receiveDateTo: receiveDateTo.value || null
    //       },
    //       {
    //         params: {
    //           ...requestPagination.value
    //         }
    //       }
    //     );

    //     if (response.data.data) {
    //       rows.value = response.data.data;
    //       requestPagination.value.rowsNumber = response.data.rowsNumber;
    //       requestPagination.value.page = response.data.pageNumber;
    //       requestPagination.value.rowsPerPage = response.data.itemPerPage;
    //     }
    //   } catch (err) {
    //     notifyError?.(err);
    //   }
    // }
    // onMounted(async () => await sendGetHeaders());

    function formattedDate(value: Date | string) {
      return date.formatDate(value, 'ddd D MMM, YYYY');
    }

    // function showDetail(
    //   id: number,
    //   transactionType: string,
    //   transactionIsDone: boolean
    // ) {
    //   $q.dialog({
    //     component: TransactionDetailDialog,
    //     componentProps: {
    //       headerId: id,
    //       transactionType,
    //       transactionIsDone
    //     }
    //   });
    // }

    return {
      // rows,
      filter,
      transactionHeader,
      formattedDate,
    //   receiveDateFrom,
    //   receiveDateTo,
    //   purchaseDateFrom,
    //   purchaseDateTo,
      // showDetail,
      requestPagination,
      // handleRequest,
      // sendHeaderFilter,
      // clearFilter
    };
  }
});
</script>

