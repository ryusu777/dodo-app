<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated>
      <q-toolbar>
        <q-btn
          flat
          dense
          round
          icon="menu"
          aria-label="Menu"
          @click="toggleLeftDrawer"
        />

        <q-toolbar-title> Dodo App </q-toolbar-title>
      </q-toolbar>
    </q-header>

    <q-drawer v-model="leftDrawerOpen" show-if-above bordered>
      <q-list class="q-mt-xl">
        <q-item v-for="data in menu" :key="data.id">
          <q-item-section>
            <a
              :href="data.url"
              class="text-black text-h5 text-bold"
              style="text-decoration: none"
            >
              {{ data.text }}
            </a>
          </q-item-section>
        </q-item>
      </q-list>
    </q-drawer>

    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';

const menu = [
  {
    id: 1,
    url: '/goods',
    text: 'Lihat Barang'
  },
  {
    id: 2,
    url: '/selling-goods',
    text: 'Menjual Barang'
  },
  {
    id: 3,
    url: '/transaction-history',
    text: 'Histori transaksi'
  }
];
export default defineComponent({
  name: 'MainLayout',

  components: {},
  setup() {
    const leftDrawerOpen = ref(false);

    return {
      menu,
      leftDrawerOpen,
      toggleLeftDrawer() {
        leftDrawerOpen.value = !leftDrawerOpen.value;
      }
    };
  }
});
</script>
