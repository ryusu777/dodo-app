<template>
  <router-view />
</template>

<script lang="ts">
// TODO: Filtering functionality for all tables
/* 
  TODO: Loading state of data
  HomePage: Loading current currency data
  Pages: Fetching tables data
*/
// REFACTOR: Entity that extends cruds
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'App'
});
</script>
