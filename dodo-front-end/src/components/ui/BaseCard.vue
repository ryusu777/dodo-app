<template>
  <q-card class="my-card">
    <slot></slot>
  </q-card>
</template>

<style lang="scss" scoped>
.my-card {
  min-width: 100px;
  min-height: 100px;
  border-radius: 30px;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.25);
}
</style>
