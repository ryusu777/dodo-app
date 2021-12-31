<template>
  <router-view />
</template>

<script lang="ts">
// REFACTOR: Notify error uses axios interceptors
// REFACTOR: CRUD function as module
// REFACTOR: Pagination on pages
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'App'
});
</script>
