<template>
  <q-page class="q-mx-lg q-mt-xl">
    <h3 class="text-bold q-mt-sm">Tambah Barang</h3>
    <form @submit.prevent="create" class="row justify-start q-gutter-y-lg">
      <BaseInput
        v-model="Name"
        class="col-11"
        label="Nama Barang"
      >
      </BaseInput>

      <BaseInput
        v-model="Code"
        class="col-11"
        label="Kode Barang"
      />

      <BaseInput
        v-model="Category"
        class="col-11"
        label="Kategori Barang"
      />

      <BaseInput
        v-model="MinimalAvailable"
        class="col-11"
        label="Minimal Tersedia"
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
      Name: '' as string,
      Code: '' as string,
      Category: '' as string,
      MinimalAvailable: 0 as number,
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
