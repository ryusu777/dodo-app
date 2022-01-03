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
      <transaction-detail-form
        v-if="transactionHeader"
        :transaction-header="transactionHeader"
      ></transaction-detail-form>
    </q-card-section>
  </base-card>
</template>

<script lang="ts">
// REFACTOR: Move request to page component
import { defineComponent, PropType } from 'vue';
import { ITransactionHeader } from 'src/models/transaction';
import BaseCard from '../ui/BaseCard.vue';
import { date } from 'quasar';
import TransactionDetailForm from '../goods-transaction/TransactionDetailForm.vue';

export default defineComponent({
  components: { BaseCard, TransactionDetailForm },
  props: {
    transactionHeader: Object as PropType<ITransactionHeader>
  },
  setup() {
    function formattedDate(value: Date | undefined) {
      if (value) return date.formatDate(value, 'dddd, D MMMM YYYY');
      return undefined;
    }

    return {
      formattedDate
    };
  }
});
</script>
