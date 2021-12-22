<template>
  <q-table
    grid
    :rows="modelHeader?.goodsTransactionDetails"
    row-key="id"
    hide-header
    hide-pagination
  >
    <template v-slot:item="props">
      <div class="q-pa-xs col-12">
        <base-card
          :class="props.selected ? 'bg-grey-2' : ''"
          :style="{ height: editable ? '100px' : '75px' }"
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
            </q-card-section>

            <q-card-section class="text-right">
              <p class="text-overline q-ma-none" style="line-height: 15px">
                Jumlah: {{ props.row.goodsAmount }}
              </p>
              <p
                class="text-overline q-ma-none self-end"
                style="line-height: 15px"
              >
                Rp {{ props.row.pricePerItem }}
              </p>
              <q-card-actions align="right">
                <base-button
                  v-if="editable"
                  icon="delete"
                  @click="removeGoods(props.row.id)"
                />
                <base-button v-if="editable" icon="edit">
                  <q-popup-proxy
                    :breakpoint="100"
                    ref="popupRef"
                    @hide="clearInput"
                    @show="
                      setInput(props.row.goodsAmount, props.row.pricePerItem)
                    "
                  >
                    <base-card class="q-pa-sm">
                      <base-input
                        v-model="goodsAmountRequest"
                        class="col-12 q-my-sm"
                        label="Jumlah Barang"
                        type="number"
                      />
                      <base-input
                        v-model="pricePerItemRequest"
                        class="col-12 q-my-sm"
                        label="Harga Jual"
                        type="number"
                      />
                      <base-button
                        label="Submit"
                        @click="sendUpdateDetail(props.row.id, props.row)"
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
</template>

<script lang="ts">
// TODO: Receive or purchase/sell button
import { defineComponent, ref, PropType } from 'vue';
import { api } from 'boot/axios';
import BaseButton from 'components/ui/BaseButton.vue';
import BaseCard from 'components/ui/BaseCard.vue';
import BaseDialog from 'components/ui/BaseDialog.vue';
import BaseInput from 'components/ui/BaseInput.vue';
import {
  ITransactionHeader,
  ITransactionDetail
} from 'src/models/interfaces/transaction.interface';
import axios, { AxiosError } from 'axios';
import { LooseDictionary, QPopupProxy, useQuasar } from 'quasar';

export default defineComponent({
  props: {
    transactionHeader: {
      type: Object as PropType<ITransactionHeader>,
      required: true
    },
    editable: Boolean
  },
  components: {
    BaseButton,
    BaseCard,
    BaseInput
  },
  setup(props) {
    const $q = useQuasar();
    const popupRef = ref<InstanceType<typeof QPopupProxy>>();
    const rows = ref<ITransactionDetail[]>([]);
    const goodsAmountRequest = ref<number>();
    const pricePerItemRequest = ref<number>();
    const modelHeader = ref(props.transactionHeader);

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
          body: 'Yakin ingin menghapus barang?',
          okLabel: 'Hapus',
          cancelLabel: 'Tidak'
        }
      }).onOk(async () => {
        try {
          await api.delete(`/transaction/detail/${id}`);
          modelHeader.value?.goodsTransactionDetails.splice(
            modelHeader.value?.goodsTransactionDetails.findIndex(
              (item) => item.id === id
            ),
            1
          );
          $q.notify({
            message: 'Berhasil menghapus barang'
          });
        } catch (err) {
          notifyError(err);
        }
      });
    }

    async function sendUpdateDetail(id: number, detail: LooseDictionary) {
      try {
        await api.put(`/transaction/detail/${id}`, {
          // eslint-disable-next-line @typescript-eslint/no-unsafe-assignment
          id: detail.id || 0,
          goodsAmount: goodsAmountRequest.value,
          pricePerItem: pricePerItemRequest.value
        });

        $q.notify({
          message: 'Berhasil mengubah data'
        });

        detail.goodsAmount = goodsAmountRequest.value;
        detail.pricePerItem = pricePerItemRequest.value;

        popupRef.value?.hide();
      } catch (err) {
        notifyError?.(err);
      }
    }

    function clearInput() {
      goodsAmountRequest.value = 0;
      pricePerItemRequest.value = 0;
    }

    function setInput(goodsAmount: number, pricePerItem: number) {
      goodsAmountRequest.value = goodsAmount;
      pricePerItemRequest.value = pricePerItem;
    }

    return {
      popupRef,
      rows,
      goodsAmountRequest,
      pricePerItemRequest,
      removeGoods: removeDetail,
      sendUpdateDetail,
      clearInput,
      setInput,
      modelHeader
    };
  }
});
</script>
