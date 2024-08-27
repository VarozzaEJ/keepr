<script setup>
import { computed, onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import { AppState } from '@/AppState.js';

const theme = ref(loadState('theme') || 'light')
const account = computed(() => AppState.account)

onMounted(() => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
})

function toggleTheme() {
  theme.value = theme.value == 'light' ? 'dark' : 'light'
  document.documentElement.setAttribute('data-bs-theme', theme.value)
  saveState('theme', theme.value)
}

</script>

<template>
  <nav class="navbar navbar-expand-sm shadow sticky-top navbar-dark bg-light px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <p class="fs-4 mb-0 bg-info rounded-pill px-3 text-dark">Home</p>
      </div>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          <div v-if="account" class="dropdown">
            <div data-bs-toggle="dropdown" role="button" class="btn dropdown-toggle text-dark lighten-30 selectable">
              <span class="fs-4 ">Create <i class="mdi mdi-menu-down"></i> </span>
            </div>
            <ul class="dropdown-menu">
              <li><span data-bs-toggle="modal" data-bs-target="#createKeepModal" class="dropdown-item">New Keep</span>
              </li>
              <li><span data-bs-toggle="modal" data-bs-target="#createVaultModal" class="dropdown-item">New Vault</span>
              </li>
            </ul>
          </div>
        </li>

      </ul>
      <div class="me-5">
        <img src="/Users/evanvarozza/source/codeworks/keepr/client/public/img/Keepr logo.png" height="64px" width="64px"
          alt="">
      </div>
      <!-- LOGIN COMPONENT HERE -->
      <!-- <div>
        <button class="btn text-light" @click="toggleTheme"
          :title="`Enable ${theme == 'light' ? 'dark' : 'light'} theme.`">
          <Icon :name="theme == 'light' ? 'weather-sunny' : 'weather-night'" />
        </button>
      </div> -->
      <Login />
    </div>
  </nav>
</template>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 576px) {
  nav {
    height: 64px;
  }
}

.shadow {
  border-bottom: 2px 2px black solid;
}
</style>
