<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
    <h3 class="text-bold q-mx-lg q-mt-sm">
      {{ transactionType === 'sell' ? 'Menjual' : 'Membeli' }}
    </h3>
    <q-table grid :rows="rows" row-key="id"
      v-model:filter="filter"
      v-model:pagination="requestPagination"
      @request="handleRequest" hide-header>
      <template v-slot:top-right>
        <base-button
          :label="sortByStok ? 'Kembalikan ke semula' : 'Urutkan stok'"
          @click="sortByStok = !sortByStok"
          v-if="transactionType === 'purchase'"
        />
        <base-button icon="shopping_cart" @click="showCart" class="q-mr-md" />
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
                  :class="{'text-yellow-10': props.row.minimalAvailable > props.row.stockAvailable}"
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
                  <base-button icon="add">
                    <q-popup-proxy :breakpoint="100" ref="popupRef">
                      <base-card class="q-pa-sm">
                        <base-input
                          v-model="amount"
                          class="col-12 q-my-sm"
                          label="Jumlah Barang"
                          type="number"
                        />
                        <base-input
                          v-if="transactionType === 'sell'"
                          v-model="sellPrice"
                          class="col-12 q-my-sm"
                          label="Harga Jual"
                          type="number"
                        />
                        <base-button
                          label="Submit"
                          @click="sendAddDetail(props.row.id)"
                        />
                      </base-card>
                    </q-popup-proxy>
                  </base-button>
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
import { defineComponent, ref, onMounted, inject, PropType, watch } from 'vue';
import { IGoods } from 'src/models/goods';
import { IPagination } from 'src/models/responses.interface';
import { api } from 'boot/axios';
import { IPageFilter } from 'src/models/requests.interface';
import { AxiosError, AxiosResponse } from 'axios';
import BaseInput from 'components/ui/BaseInput.vue';
import BaseButton from 'components/ui/BaseButton.vue';
import BaseCard from 'components/ui/BaseCard.vue';
import TransactionDetailFormDialog from 'components/goods-transaction/TransactionDetailFormDialog.vue';
import { ITransactionHeader } from 'src/models/transaction';
import { QPopupProxy, useQuasar } from 'quasar';
import { useRouter } from 'vue-router';
import BaseDialog from 'components/ui/BaseDialog.vue';

export default defineComponent({
  props: {
    id: {
      type: String,
      required: true
    },
    transactionType: {
      type: String as PropType<'sell' | 'purchase'>,
      required: true
    }
  },
  components: {
    BaseInput,
    BaseButton,
    BaseCard
  },
  setup(props) {
    const $q = useQuasar();
    const $router = useRouter();
    const filter = ref('');
    const amount = ref(0);
    const sellPrice = ref(0);
    const popupRef = ref<InstanceType<typeof QPopupProxy>>();
    const notifyError: ((err: unknown | AxiosError) => void) | undefined =
      inject('notifyError');

    const requestPagination = ref<IPageFilter>({
      page: 1,
      rowsPerPage: 5
    });

    async function handleRequest({ pagination }: { pagination: IPageFilter }) {
      requestPagination.value = pagination;
      await sendGetHeaders();
    }

    const transactionHeader = ref<ITransactionHeader>();

    const rows = ref<IGoods[]>([]);

    const sortByStok = ref(false);

    watch(
      () => sortByStok.value,
      async () => await sendGetHeaders()
    );

    async function sendGetHeaders() {
      try {
        const response = await api.get<ITransactionHeader>(
          `/transaction/header/${props.id || 0}`
        );

        if (response.data.transactionType !== props.transactionType) {
          await $router.push('/not-found');
        }

        if (
          response.data.purchaseDate !== null ||
          response.data.receiveDate !== null
        ) {
          $q.dialog({
            component: BaseDialog,
            componentProps: {
              title: 'Pemberitahuan',
              body: 'Transaksi ini telah selesai dilakukan',
              okLabel: 'Kembali'
            }
          }).onDismiss(async () => {
            await $router.push('/');
          });
        }

        transactionHeader.value = response.data;
      } catch (err) {
        await $router.push('/not-found');
      }

      try {
        const response: AxiosResponse<IPagination<IGoods>> = await api.get(
          '/goods',
          {
            params: {
              ...requestPagination.value,
              searchText: filter.value,
              sortBy: sortByStok.value ? 'StockAvailable' : null,
              descending: sortByStok.value ? 'ASC' : null
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

    onMounted(async () => {
      await sendGetHeaders();
    });

    function showCart() {
      $q.dialog({
        component: TransactionDetailFormDialog,
        componentProps: {
          transactionHeader: transactionHeader.value,
          editable: true
        }
      });
    }

    async function sendAddDetail(goodsId: number) {
      try {
        await api.post('/transaction/detail', {
          goodsId,
          goodsTransactionHeaderId: transactionHeader.value?.id || 0,
          goodsAmount: amount.value,
          pricePerItem:
            props.transactionType == 'sell'
              ? sellPrice.value
              : rows.value.find((item) => item.id == goodsId)?.purchasePrice
        });

        $q.notify({
          message: 'Berhasil menambahkan barang'
        });
        amount.value = 0;
        sellPrice.value = 0;
        popupRef.value?.hide();
      } catch (err) {
        notifyError?.(err);
      }

      try {
        const response = await api.get<ITransactionHeader>(
          `/transaction/header/${transactionHeader.value?.id || 0}`
        );

        transactionHeader.value = response.data;
      } catch (err) {
        notifyError?.(err);
      }
    }

    return {
      rows,
      filter,
      amount,
      sortByStok,
      sellPrice,
      showCart,
      sendAddDetail,
      popupRef,
      requestPagination,
      handleRequest,
    };
  }
});
</script>
