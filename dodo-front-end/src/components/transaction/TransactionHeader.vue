<template>
  <base-card>
    <q-card-section>
      <p class="text-h5 text-bold">
        {{
          transactionHeader?.transactionType === 'sell'
            ? 'Pemasukan'
            : 'Pengeluaran'
        }}
      </p>
      <p class="text-bold">
        Total Harga: Rp. {{ transactionHeader?.totalPrice }}
      </p>
      <p>Vendor: {{ transactionHeader?.vendor }}</p>
      <p>
        Tanggal bayar: <br />
        <strong>{{ formattedDate(transactionHeader?.purchaseDate) }}</strong>
      </p>
      <p>
        Tanggal menerima barang: <br />
        <strong>{{ formattedDate(transactionHeader?.receiveDate) }}</strong>
      </p>
    </q-card-section>

    <q-card-section>
      <p class="text-h5 text-bold">Transaction Detail</p>
      <selling-goods-basket
        v-if="transactionHeader"
        :transaction-header="transactionHeader"
      ></selling-goods-basket>
    </q-card-section>
  </base-card>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from 'vue';
import { ITransactionHeader } from 'src/models/interfaces/transaction.interface';
import BaseCard from '../ui/BaseCard.vue';
import { api } from 'src/boot/axios';
import axios, { AxiosError } from 'axios';
import { date, useQuasar } from 'quasar';
import SellingGoodsBasket from './SellingGoodsBasket.vue';

export default defineComponent({
  components: { BaseCard, SellingGoodsBasket },
  props: {
    headerId: {
      type: [Number, String],
      required: true
    }
  },
  setup(props) {
    const transactionHeader = ref<ITransactionHeader>();
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

    onMounted(async () => {
      try {
        const response = await api.get<ITransactionHeader>(
          `/transaction/header/${props.headerId}`
        );

        transactionHeader.value = response.data;
      } catch (err) {
        notifyError?.(err);
      }
    });

    function formattedDate(value: Date | undefined) {
      if (value) return date.formatDate(value, 'dddd, D MMMM YYYY');
      return undefined;
    }

    return {
      transactionHeader,
      formattedDate
    };
  }
});
</script>
