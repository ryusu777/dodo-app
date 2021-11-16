<template>
  <!-- TODO: Grid display of item -->
  <q-table
    grid
    :rows="transactionHeader?.goodsTransactionDetails"
    :columns="goodsColumns"
    row-key="id"
    hide-header
  >
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
                {{ props.row.theGoods.goodsCode }}
              </p>
              <p class="text-bold text-h7 q-pa-none q-ma-none">
                {{ props.row.theGoods.goodsName }}
              </p>
              <p class="text-caption q-pa-none q-ma-none">
                {{ props.row.theGoods.partNumber }}
              </p>
              <p class="text-caption q-pa-none q-ma-none">
                {{ props.row.theGoods.carType }}
              </p>
              
            </q-card-section>

            <q-card-section class="text-right">
              <p class="text-overline q-ma-none" style="line-height: 15px">
                Stok: {{ props.row.theGoods.stockAvailable }}
              </p>
              <p
                class="text-overline q-ma-none self-end"
                style="line-height: 15px"
              >
                Rp {{ props.row.theGoods.purchasePrice }}
              </p>
              <q-card-actions align="right">
                <base-button icon="delete" @click="removeGoods(props.row.id)" />
                <base-button icon="edit">
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
                        @click="sendUpdateDetail(props.row.id)"
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
  <div class="row justify-end q-mb-md">
    <base-button label="Lakukan Transaksi" />
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, PropType } from 'vue';
import { api } from 'boot/axios';
import { goodsColumns } from 'src/models/table-columns/goods-columns';
import BaseButton from 'components/ui/BaseButton.vue';
import BaseCard from 'components/ui/BaseCard.vue';
import BaseDialog from 'components/ui/BaseDialog.vue';
import BaseInput from'components/ui/BaseInput.vue'
import { ITransactionHeader, ITransactionDetail } from 'src/models/interfaces/transaction.interface';
import axios, { AxiosError } from 'axios';
import { QPopupProxy, useQuasar } from 'quasar';

export default defineComponent({
  emits: ['deletedDetail'],
  props: {
    transactionHeader: Object as PropType<ITransactionHeader>
  },
  components: {
    BaseButton,
    BaseCard,
    BaseInput
  },
  setup(props) {
    console.log(props.transactionHeader);
    const $q = useQuasar();
    const popupRef = ref<InstanceType<typeof QPopupProxy>>();
    const rows = ref<ITransactionDetail[]>([]);
    const amount = ref(Number);
    const sellPrice = ref(Number);

    function notifyError(err: unknown | AxiosError): void {
      if (axios.isAxiosError(err)) {
        const { response } = err;
        // eslint-disable-next-line
        response?.data.errors.forEach((element: string) => {
          $q.notify({
            message: element
          });
        });
      }
    }

    function removeDetail(id: number) {
      $q.dialog({
        component: BaseDialog,
        componentProps: {
          title: 'Hapus barang',
          body: 'Yakin ingin menghapus barang?'
        }
      }).onOk(async () => {
        try {
          await api.delete(`/transaction/detail/${id}`);

        } catch (err) {
          notifyError(err);
        }
      });
    }

    async function sendUpdateDetail(id: number) {
      try {
        await api.put(`/transaction/detail/${id}`);
        // TODO: Update Function
      } catch (err) {
        notifyError?.(err);
      }
    }

    // TODO: "Lakukan Transaksi" Function

    return {
      goodsColumns,
      popupRef,
      rows,
      amount,
      sellPrice,
      removeGoods: removeDetail,
      sendUpdateDetail,
    };
  }
});
</script>
