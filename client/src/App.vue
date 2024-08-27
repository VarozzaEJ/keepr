<script setup>
import Navbar from './components/Navbar.vue';
import { AppState } from './AppState.js';
import KeepModal from './components/globals/KeepModal.vue';
import CreateKeepModal from './components/globals/CreateKeepModal.vue';
import { computed } from 'vue';
import CreateVaultModal from './components/globals/CreateVaultModal.vue';

const screenHeight = computed(() => screenX) //maybe whats needed for moving the nav to the bottom of the screen on mobile
</script>

<template>
  <header class="sticky-top">
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <KeepModal />
  <CreateKeepModal />
  <CreateVaultModal />
</template>

<style lang="scss">
@import '@/assets/scss/main.scss';

:root {
  --main-height: calc(100vh - 32px - 64px);
}

footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
