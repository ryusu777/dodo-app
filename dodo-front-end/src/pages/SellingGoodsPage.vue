<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
    <h3 class="text-bold q-mx-lg q-mt-sm">Menjual</h3>
    <q-table
      grid
      :rows="rows"
      :columns="goodsColumns"
      row-key="id"
      :filter="filter"
      hide-header
    >
      <template v-slot:top-right>
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
                <p class="text-bold text-h5 q-pa-none q-ma-none">
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
import { defineComponent, ref, onMounted, inject } from 'vue';
import { IGoods } from 'src/models/interfaces/goods.interface';
import { IPagination } from 'src/models/responses.interface';
import { api } from 'boot/axios';
import { IPageFilter } from 'src/models/requests.interface';
import { AxiosError, AxiosResponse } from 'axios';
import BaseInput from 'components/ui/BaseInput.vue';
import BaseButton from 'components/ui/BaseButton.vue';
import BaseCard from 'components/ui/BaseCard.vue';
import SellingGoodsBasketDialog from 'components/transaction/SellingGoodsBasketDialog.vue';
import { ITransactionHeader } from 'src/models/interfaces/transaction.interface';
import { QPopupProxy, useQuasar } from 'quasar';
import { useRouter } from 'vue-router';

export default defineComponent({
  props: {
    id: String
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

    const pagination = ref<IPageFilter>({
      page: 1,
      rowsPerPage: 5
    });

    const transactionHeader = ref<ITransactionHeader>();

    const rows = ref<IGoods[]>([]);

    onMounted(async () => {
      try {
        const response = await api.get<ITransactionHeader>(
          `/transaction/header/${props.id || 0}`
        );

        if (response.data.transactionType !== 'sell') {
          await $router.push('/not-found');
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
              ...pagination.value
            }
          }
        );
        if (response.data.data) rows.value = response.data.data;
      } catch (err) {
        notifyError?.(err);
      }
    });

    function showCart() {
      $q.dialog({
        component: SellingGoodsBasketDialog,
        componentProps: {
          transactionHeader: transactionHeader.value
        }
      });
    }

    async function sendAddDetail(goodsId: number) {
      try {
        await api.post('/transaction/detail', {
          goodsId,
          goodsTransactionHeaderId: transactionHeader.value?.id || 0,
          goodsAmount: amount.value,
          pricePerItem: sellPrice.value
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
      sellPrice,
      showCart,
      sendAddDetail,
      popupRef
    };
  }
});
</script>
