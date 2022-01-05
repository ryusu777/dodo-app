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

      <q-field label="Total Harga" stack-label>
        Rp. {{ transactionHeader?.totalPrice || 0 }}
      </q-field>

      <q-field label="Vendor" stack-label>
        {{ transactionHeader?.vendor }}
      </q-field>

      <q-field label="Tanggal Bayar" stack-label>
        <strong
          :class="
            transactionHeader?.purchaseDate === null
              ? 'text-negative'
              : 'text-positive'
          "
        >
          {{ formattedDate(transactionHeader?.purchaseDate) || 'Belum bayar' }}
        </strong>
      </q-field>

      <q-field label="Tanggal Terima" stack-label>
        <strong
          :class="
            transactionHeader?.receiveDate === null
              ? 'text-negative'
              : 'text-positive'
          "
        >
          {{
            formattedDate(transactionHeader?.receiveDate) || 'Belum diterima'
          }}
        </strong>
      </q-field>
    </q-card-section>

    <q-card-section>
      <p class="text-h5 text-bold">Daftar Barang</p>
      <transaction-detail-table
        v-if="transactionHeader?.goodsTransactionDetails"
        :rows="transactionHeader.goodsTransactionDetails"
      ></transaction-detail-table>
    </q-card-section>
  </base-card>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';
import { ITransactionHeader } from 'src/models/transaction';
import BaseCard from '../ui/BaseCard.vue';
import { date } from 'quasar';
import TransactionDetailTable from '../goods-transaction/TransactionDetailTable.vue';

export default defineComponent({
  components: { BaseCard, TransactionDetailTable },
  props: {
    transactionHeader: Object as PropType<ITransactionHeader>
  },
  setup() {
    function formattedDate(value: Date | string | undefined) {
      if (value) return date.formatDate(value, 'dddd, D MMMM YYYY');
      return undefined;
    }

    return {
      formattedDate
    };
  }
});
</script>
