<template>
  <!-- TODO: Grid display of item -->
  <q-table
    grid
    :rows="transactionHeader?.goodsTransactionDetails"
    :columns="transactionDetailColumns"
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
                {{ props.row.id }}
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
                <base-button icon="remove" @click="removeGoods(props.row.id)" />
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
import { defineComponent, PropType } from 'vue';
import { api } from 'boot/axios';
import { transactionDetailColumns } from 'src/models/table-columns/transaction-detail-columns';
import BaseButton from 'components/ui/BaseButton.vue';
import BaseCard from 'components/ui/BaseCard.vue';
import { ITransactionHeader } from 'src/models/interfaces/transaction.interface';
import axios, { AxiosError } from 'axios';
import { useQuasar } from 'quasar';

export default defineComponent({
  emits: ['deletedDetail'],
  props: {
    transactionHeader: Object as PropType<ITransactionHeader>
  },
  components: {
    BaseButton,
    BaseCard
  },
  setup(props) {
    console.log(props.transactionHeader);
    const $q = useQuasar();
    function notifyError(err: unknown | AxiosError) {
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

    // TODO: Removes detail data after OK
    async function removeDetail(id: number) {
      try {
        await api.delete(`/transaction/detail/${id}`);
      } catch (err) {
        notifyError(err);
      }
    }

    return {
      transactionDetailColumns,
      removeGoods: removeDetail
    };
  }
});
</script>
