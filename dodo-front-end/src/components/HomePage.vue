<template>
  <q-page>
    <base-card class="q-ma-lg q-py-lg q-px-md">
      <!-- TODO: Profit or fund amount -->
      <!-- TODO: Summary of today's income and expenses amount -->
      <div class="row justify-evenly">
        <p class="col-10 text-center text-bold text-h4 q-ma-md">
          Data Keuangan
        </p>
        <p class="col-10 text-left text-h6" v-if="gridCurrency.data[0]">
          Keuntungan: Rp {{ gridCurrency.data[0].profitAmount || null }}
        </p>
        <p class="col-10 text-left text-h6" v-if="gridCurrency.data[0]">
          Modal: Rp {{ gridCurrency.data[0].fundAmount || null }}
        </p>
      </div>
      <div class="row justify-around q-my-md">
        <base-button
          class="col-5 text-h6"
          @click="showAddDialog()"
          label="Tambah"
        />
        <base-button
          class="col-5 text-h6"
          @click="showConversionDialog()"
          label="Konversi"
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
        @click="createHeaderHandler('sell')"
      />
      <q-btn
        class="col-6 text-subtitle1 q-pa-sm text-weight-bold"
        label="Pembelian"
        style="height: 90px"
        @click="createHeaderHandler('purchase')"
      />
    </div>
  </q-page>
</template>

<script lang="ts">
import { defineComponent, onBeforeMount } from 'vue';
import BaseCard from 'components/ui/BaseCard.vue';
import { useRouter } from 'vue-router';
import BaseButton from 'components/ui/BaseButton.vue';
import CurrencyFormDialog from 'components/currency/CurrencyFormDialog.vue';
import CurrencyConversionFormDialog from './currency/CurrencyConversionFormDialog.vue';
import { ICurrency } from 'src/models/currency';
import { useQuasar } from 'quasar';
import { useCrudEntity } from 'src/models/crud';
import { ITransactionHeader } from 'src/models/transaction';

export default defineComponent({
  components: {
    BaseCard,
    BaseButton
  },
  setup() {
    const $q = useQuasar();
    const router = useRouter();

    const { grid: gridHeader, create: createHeader } =
      useCrudEntity<ITransactionHeader>('/transaction/header');

    async function createHeaderHandler(
      transactionType: 'sell' | 'purchase'
    ): Promise<void> {
      await createHeader({
        transactionType
      });
      if (gridHeader.value.data)
        await router.push(
          `/transaction/${transactionType}/${gridHeader.value.data[0].id || -1}`
        );
    }

    const {
      grid: gridCurrency,
      create: createCurrency,
      paging: pagingCurrency
    } = useCrudEntity<ICurrency>('/currency');

    function showAddDialog() {
      $q.dialog({
        component: CurrencyFormDialog,
        componentProps: {
          title: 'Perubahan Keuangan'
        }
      }).onOk(async (currency: ICurrency) => {
        await createCurrency(currency);
        await pagingCurrency({ rowsPerPage: 1 });
      });
    }

    function showConversionDialog() {
      if (gridCurrency.value.data)
        $q.dialog({
          component: CurrencyConversionFormDialog,
          componentProps: {
            title: 'Konversi keuntungan permodalan',
            fundAmount: gridCurrency.value.data[0].fundAmount || 0,
            profitAmount: gridCurrency.value.data[0].profitAmount || 0
          }
        }).onOk(async (currency: ICurrency) => {
          await createCurrency(currency);
          await pagingCurrency({ rowsPerPage: 1 });
        });
    }

    onBeforeMount(async () => await pagingCurrency({ rowsPerPage: 1 }));

    return {
      showAddDialog,
      showConversionDialog,
      createHeaderHandler,
      gridCurrency
    };
  }
});
</script>
