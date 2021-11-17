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
      <p>Tanggal bayar: {{ transactionHeader?.purchaseDate }}</p>
      <p>Tanggal menerima barang: {{ transactionHeader?.receiveDate }}</p>
    </q-card-section>

    <q-card-section>
      <p class="text-h5 text-bold">Transaction Detail</p>
    </q-card-section>
  </base-card>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';
import { ITransactionHeader } from 'src/models/interfaces/transaction.interface';
import BaseCard from '../ui/BaseCard.vue';

export default defineComponent({
  components: { BaseCard },
  props: {
    transactionHeader: {
      type: Object as PropType<ITransactionHeader>,
      required: true
    }
  },
  setup(props) {
    console.log(props.transactionHeader);
  }
});
</script>
