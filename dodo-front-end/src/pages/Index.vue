<template>
  <q-page>
    <base-card class="q-ma-lg q-py-lg q-px-md">
      <!-- TODO: Profit or fund amount -->
      <!-- TODO: Summary of today's income and expenses amount -->
      <div class="row justify-evenly">
        <p class="col-10 text-center text-bold text-h4 q-ma-md">
          Data Keuangan
        </p>
        <p class="col-5 text-left text-h6">Keuangan sekarang:</p>
        <p class="col-5 text-right text-h6">Rp {{ currencyAmount }}</p>
      </div>
      <div class="row justify-end q-my-md">
        <base-button
          class="col-5 text-h6 self-end"
          @click="showAddDialog()"
          label="Tambah"
        />
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
import { defineComponent, ref, inject, onMounted } from 'vue';
import BaseCard from 'src/components/ui/BaseCard.vue';
import { ICreateResponse, IPagination } from 'src/models/responses.interface';
import { api } from 'src/boot/axios';
import { AxiosError, AxiosResponse } from 'axios';
import { useRouter } from 'vue-router';
import BaseButton from 'src/components/ui/BaseButton.vue';
import CurrencyFormDialog from 'components/currency/CurrencyFormDialog.vue';
import { ICurrency } from 'src/models/interfaces/currency.interface';
import { useQuasar } from 'quasar';

export default defineComponent({
  name: 'PageIndex',
  components: {
    BaseCard,
    BaseButton
  },
  setup() {
    const $q = useQuasar();
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

    function showAddDialog() {
      $q.dialog({
        component: CurrencyFormDialog,
        componentProps: {
          title: 'Tambah Daftar'
        }
      }).onOk(async (currency: ICurrency) => {
        await sendCreateRequest(currency);
      });
    }

    async function sendCreateRequest(currency: ICurrency): Promise<void> {
      try {
        await api.post<ICreateResponse>('/currency', {
          changingAmount: currency.changingAmount,
          changeDescription: currency.changeDescription
        });
      } catch (err) {
        notifyError?.(err);
      }

      await sendGetLastCurrency();
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

    const currencyAmount = ref<number>();
    async function sendGetLastCurrency() {
      try {
        const response: AxiosResponse<IPagination<ICurrency>> = await api.get(
          '/currency',
          {
            params: {
              rowsPerPage: 1
            }
          }
        );

        if (response.data.data) {
          currencyAmount.value = response.data.data[0].currencyAmount;
        }
      } catch (err) {
        notifyError?.(err);
      }
    }

    onMounted(async () => await sendGetLastCurrency());

    return {
      showAddDialog,
      sendSellTransactionHeader,
      sendPurchaseTransactionHeader,
      currencyAmount,
      $router
    };
  }
});
</script>
