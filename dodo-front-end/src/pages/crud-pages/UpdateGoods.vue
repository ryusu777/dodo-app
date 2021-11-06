<template>
  <q-page class="q-mx-lg q-mt-xl">
    <h3 class="text-bold q-mt-sm">Ubah Barang</h3>
    <form @submit.prevent="update" class="row justify-start q-gutter-y-lg">
      <BaseInput
        v-model="GoodsName"
        class="col-11"
        label="Nama Barang"
      />

      <BaseInput
        v-model="GoodsCode"
        class="col-11"
        label="Kode Barang"
      />

      <BaseInput
        v-model="CarType"
        class="col-11"
        label="Kategori Barang"
      />

      <BaseInput
        v-model="PartNumber"
        class="col-11"
        label="Part Number"
      />

      <BaseInput
        v-model="MinimalAvailable"
        class="col-11"
        label="Minimal Tersedia"
        type="number"
      />

      <BaseInput
        v-model="StockAvailable"
        class="col-11"
        label="Stok Tersedia"
        type="number"
      />

      <BaseInput
        v-model="PurchasePrice"
        class="col-11"
        label="Harga Beli"
        type="number"
      />

      <!-- <BaseInputDate
        v-model="tanggalMasuk"
      >
      <b>Tanggal Masuk</b>
      </BaseInputDate> -->

      <base-button
        class="col-2 q-mt-md"
        type="submit"
      >
        Submit
      </base-button>
    </form>
  </q-page>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import BaseButton from 'components/ui/BaseButton.vue';
import BaseInput from 'components/ui/BaseInput.vue';
// import BaseInputDate from 'components/ui/BaseInputDate.vue';
// import { EUserActions } from 'src/store/user/types/enumeration';

export default defineComponent({
  components: {
    BaseButton,
    BaseInput,
    // BaseInputDate,
  },
  data() {
    return {
      GoodsName: '' as string,
      GoodsCode: '' as string,
      CarType: '' as string,
      PartNumber: '' as string,
      MinimalAvailable: 0 as number,
      StockAvailable: 0 as number,
      PurchasePrice: 0 as number,
      // tanggalMasuk: null as null|Date,
    };
  },
//   methods: {
//     async signIn() {
//       if (!this.userName || !this.password) {
//         this.$q.notify({ message: 'Please enter valid data' })
//         return;
//       }
//       if (
//         !await this.$store.dispatch(EUserActions.login, {
//           userName: this.userName,
//           password: this.password,
//         })
//       ) {
//         this.$q.notify({
//           message: 'Incorrect username or password..',
//         });
//       } else {
//         await this.$router.replace('/');
//       }
//     },
//   },
//   watch: {
//     async isAuth(value: boolean) {
//       if (value) {
//         await this.$router.push('/');
//       } else {
//         await this.$router.push('/login');
//       }
//     },
//   },
});
</script>
