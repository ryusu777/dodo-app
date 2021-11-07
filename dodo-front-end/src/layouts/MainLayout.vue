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

        <q-toolbar-title>
         Dodo App
        </q-toolbar-title>
      </q-toolbar>
    </q-header>

    <q-drawer
      v-model="leftDrawerOpen"
      show-if-above
      bordered
    >
      <q-list>
        <q-item v-for="data in menu" :key="data.id"> 
          <q-item-section><a :href="data.url" class="text-black" style="text-decoration: none">{{ data.text }}</a></q-item-section>
        </q-item>
      </q-list>
    </q-drawer>

    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script lang="ts">



import { defineComponent, ref } from 'vue'

export default defineComponent({
  name: 'MainLayout',

  components: {
  },
  data(){
    return{
      menu: [
        {
          id: 1,
          url: '/#/create',
          text: 'Tambah Barang'
        },
        {
          id: 2,
          url: '/#/update',
          text: 'Ubah Barang'
        },
        {
          id: 3,
          url: '/#/view',
          text: 'Lihat Barang'
        },
      ],
    }
  },
  setup () {
    const leftDrawerOpen = ref(false)

    // const menu = [
    //   {
    //     id: 1,
    //     title: 'Tambah Barang',
    //     link: '/create'
    //   },
    //   {
    //     id: 2,
    //     title: 'Ubah Barang',
    //     link: '/update'
    //   },
    //   {
    //     id: 3,
    //     title: 'Lihat Barang',
    //     link: '/view'
    //   },
    // ];
    return {
      // menu,
      leftDrawerOpen,
      toggleLeftDrawer () {
        leftDrawerOpen.value = !leftDrawerOpen.value
      }
    }
  }
})
</script>
