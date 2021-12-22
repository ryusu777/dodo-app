<template>
  <router-view />
</template>

<script lang="ts">
// REFACTOR: Notify error uses axios interceptors
// REFACTOR: Page, forms, and component folder structure
// REFACTOR: Separate table with page component
// REFACTOR: CRUD function as module
import { useQuasar } from 'quasar';
import axios, { AxiosError } from 'axios';
import { defineComponent, provide } from 'vue';

export default defineComponent({
  name: 'App',
  setup() {
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

    provide('notifyError', notifyError);
  }
});
</script>
