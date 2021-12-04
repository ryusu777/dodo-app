<template>
  <q-page class="row items-center justify-evenly">
    <div>
      <base-button label="Penjualan" @click="sendSellTransactionHeader()" />
      <base-button label="Pembelian" @click="sendPurchaseTransactionHeader()" />
    </div>
  </q-page>
</template>

<script lang="ts">
import { defineComponent, inject } from 'vue';
import BaseButton from 'src/components/ui/BaseButton.vue';
import { ICreateResponse } from 'src/models/responses.interface';
import { api } from 'src/boot/axios';
import { AxiosError } from 'axios';
import { useRouter } from 'vue-router';

export default defineComponent({
  name: 'PageIndex',
  components: {
    BaseButton
  },
  setup() {
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
      sendPurchaseTransactionHeader
      // options: ['pilihan1', 'pilihan2', 'pilihan3']
    };
  }
});
</script>
