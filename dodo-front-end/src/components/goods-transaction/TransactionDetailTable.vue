<template>
  <q-table grid :rows="rows" row-key="id" hide-pagination>
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
                  @click="$emit('delete', props.row.id)"
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
                        @click="
                          $emit('update', {
                            id: props.row.id,
                            goodsAmount: goodsAmountRequest,
                            pricePerItem: pricePerItemRequest
                          })
                        "
                        v-close-popup="1"
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
import { defineComponent, ref, PropType } from 'vue';
import BaseButton from 'components/ui/BaseButton.vue';
import BaseCard from 'components/ui/BaseCard.vue';
import BaseInput from 'components/ui/BaseInput.vue';
import { ITransactionDetail } from 'src/models/transaction';
import { QPopupProxy } from 'quasar';

export default defineComponent({
  props: {
    rows: {
      type: Array as PropType<ITransactionDetail[]>,
      required: true
    },
    editable: Boolean
  },
  emits: ['update', 'delete'],
  components: {
    BaseButton,
    BaseCard,
    BaseInput
  },
  setup() {
    const popupRef = ref<InstanceType<typeof QPopupProxy>>();
    const goodsAmountRequest = ref<number>();
    const pricePerItemRequest = ref<number>();

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
      goodsAmountRequest,
      pricePerItemRequest,
      clearInput,
      setInput
    };
  }
});
</script>
