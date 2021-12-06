<template>
  <q-page>
    <base-card class="q-ma-lg q-py-lg q-px-md">
      <div class="row justify-evenly">
        <p class="col-10 text-center text-bold text-h4 q-ma-md">
          Data Keuangan
        </p>
        <p class="col-5 text-left text-h6">Keuangan sekarang:</p>
        <p class="col-5 text-right text-h6">Rp50000</p>
      </div>
      <div class="row justify-end q-my-md">
        <base-button class="col-5 text-h6 self-end" label="Tambah" />
      </div>
    </base-card>
    <div class="row items-center justify-between q-ma-lg">
      <q-btn
        class="col-4 q-my-md q-pa-md text-subtitle1 text-weight-bold"
        label="Daftar Barang"
        @click="$router.push('/goods')"
        style="height: 90px"
      />
      <q-btn
        class="col-4 q-my-md q-pa-md text-subtitle1 text-weight-bold"
        label="Histori Transaksi"
        @click="$router.push('/transaction-history')"
        style="height: 90px"
      />
      <q-btn
        class="col-4 q-my-md q-pa-md text-subtitle1 text-weight-bold"
        label="Data Keuangan"
        @click="$router.push('/currency')"
        style="height: 90px"
      />
      <q-btn
        class="col-6 text-subtitle1 q-pa-sm text-weight-bold"
        label="Penjualan"
        style="height: 90px"
        @click="sendSellTransactionHeader()"
      />
      <q-btn
        class="col-6 text-subtitle1 q-pa-sm text-weight-bold"
        label="Pembelian"
        style="height: 90px"
        @click="sendPurchaseTransactionHeader()"
      />
    </div>
  </q-page>
</template>

<script lang="ts">
import { defineComponent, inject } from 'vue';
import BaseCard from 'src/components/ui/BaseCard.vue';
import { ICreateResponse } from 'src/models/responses.interface';
import { api } from 'src/boot/axios';
import { AxiosError } from 'axios';
import { useRouter } from 'vue-router';
import BaseButton from 'src/components/ui/BaseButton.vue';

export default defineComponent({
  name: 'PageIndex',
  components: {
    BaseCard,
    BaseButton
  },
  setup() {
    BaseButton;
    const $router = useRouter();
    const notifyError: ((err: unknown | AxiosError) => void) | undefined =
      inject('notifyError');

    async function sendSellTransactionHeader(): Promise<void> {
      try {
        const response = await api.post<ICreateResponse>(
          '/transaction/header',
          {
            transactionType: 'sell'
          }
        );

        await $router.push(`/transaction/sell/${response.data.id || 0}`);
      } catch (err) {
        notifyError?.(err);
      }
    }

    async function sendPurchaseTransactionHeader(): Promise<void> {
      try {
        const response = await api.post<ICreateResponse>(
          '/transaction/header',
          {
            transactionType: 'purchase'
          }
        );

        await $router.push(`/transaction/purchase/${response.data.id || 0}`);
      } catch (err) {
        notifyError?.(err);
      }
    }

    return {
      sendSellTransactionHeader,
      sendPurchaseTransactionHeader,
      $router
    };
  }
});
</script>
