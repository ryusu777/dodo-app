<template>
  <q-btn class="button" unelevated no-caps push>
    <slot></slot>
  </q-btn>
</template>

<style lang="scss" scoped>
.button {
  font-family: inherit;
  border: solid 2px transparent;
  border-radius: 10px;
  color: primary;
  background-color: primary;
}
</style>
