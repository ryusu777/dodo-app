<template>
  <q-page class="column items-left q-mt-xl q-px-sm">
    <h3 class="text-bold q-mx-lg q-mt-sm">
      {{ transactionType === 'sell' ? 'Menjual' : 'Membeli' }}
    </h3>
    <GoodsTable/>
  </q-page>
</template>

<script lang="ts">
import GoodsTable from 'components/goods-transaction/GoodsTable.vue';
import { defineComponent } from 'vue';

export default defineComponent ({
  components: {
    GoodsTable
  },
})
</script>
