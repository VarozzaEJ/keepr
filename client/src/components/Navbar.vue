<script setup>
import { computed, onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import { AppState } from '@/AppState.js';
import { useRoute } from 'vue-router';
import { accountService } from '@/services/AccountService.js';

const theme = ref(loadState('theme') || 'light')
const account = computed(() => AppState.account)

onMounted(() => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
})

const route = useRoute()

function toggleTheme() {
  theme.value = theme.value == 'light' ? 'dark' : 'light'
  document.documentElement.setAttribute('data-bs-theme', theme.value)
  saveState('theme', theme.value)
}

</script>

<template>

  <nav class="navbar nav navbar-expand shadow sticky-top navbar-dark bg-light px-3">
    <router-link @click="accountService.getMyAccountVaults" class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <!-- <div class="d-flex flex-column align-items-center">
        <p title="Go Home?" class="fs-4 mb-0 bg-info rounded-pill px-3 text-dark">Home</p>
      </div> -->
      <ul class="ul">
        <li class="li">
          <button class="button">
            <p class="p">Home</p>
          </button>
        </li>
      </ul>
    </router-link>
    <button class="navbar-toggler text-dark" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon text-dark"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <label title="Open Menu" v-if="route.name == 'Home'" class="popup">
          <input type="checkbox">
          <div class="burger" tabindex="0">
            <span></span>
            <span></span>
            <span></span>
          </div>
          <nav class="popup-window">
            <legend>Create</legend>
            <ul>
              <li>
                <button data-bs-toggle="modal" data-bs-target="#createKeepModal">
                  <svg stroke-linejoin="round" stroke-linecap="round" stroke-width="2" stroke="currentColor" fill="none"
                    viewBox="0 0 24 24" height="14" width="14" xmlns="http://www.w3.org/2000/svg">
                    <polygon points="16 3 21 8 8 21 3 21 3 16 16 3"></polygon>
                  </svg>
                  <span>New Keep</span>
                </button>
              </li>
              <li>
                <button data-bs-toggle="modal" data-bs-target="#createVaultModal">
                  <svg stroke-linejoin="round" stroke-linecap="round" stroke-width="2" stroke="currentColor" fill="none"
                    viewBox="0 0 24 24" height="14" width="14" xmlns="http://www.w3.org/2000/svg">
                    <polygon points="16 3 21 8 8 21 3 21 3 16 16 3"></polygon>
                  </svg>
                  <span>New Vault</span>
                </button>
              </li>

            </ul>
          </nav>
        </label>


      </ul>
      <div class="me-md-5 me-2">
        <img title="Our Logo" src="@/assets/img/Keeprlogo.png" height="64px" width="64px" alt="">
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

@media screen and (min-width: 576px) {}

.nav {
  height: 70px;
}

.shadow {
  border-bottom: 2px 2px black solid;
}

/* From Uiverse.io by asgardOP */
.ul {
  width: fit-content;
  height: fit-content;
  background-color: transparent;
  list-style: none;
  margin-top: 2.5rem;
}

.li {
  margin-bottom: 0;
}

.button {
  background-color: transparent;
  font-family: sans-serif;
  color: rgb(2, 2, 2);
  border: none;
  font-size: 22px;
  font-weight: 700;
  padding: 10px 30px;
  cursor: pointer;
  position: relative;
  padding-left: 11px;
  text-align: center;
  transition: 0.1s;
  z-index: 1;
}

.p {
  z-index: 2;
  position: relative;
}

.button:hover {
  color: rgb(5, 5, 5);
  text-align: center;
}

.button:hover::before {
  transform: rotate(0deg);
  width: 100%;
  height: 40px;
  top: 2px;
  border-radius: 3px;
  background-color: rgb(145, 116, 173);
}

.button::before {
  content: "";
  border-radius: 1px;
  position: absolute;
  width: 6px;
  height: 6px;
  background-color: rgb(145, 116, 173);
  left: -10px;
  top: 19px;
  cursor: pointer;
  transform: rotate(225deg);
  transition: 0.3s;
  z-index: -1;
}

.button:active::before {
  background-color: rgb(120, 24, 210);
}

/* From Uiverse.io by Galahhad */
/* The design is inspired from the mockapi.io */

.popup {
  --burger-line-width: 1.125em;
  --burger-line-height: 0.125em;
  --burger-offset: 0.625em;
  --burger-bg: rgba(0, 0, 0, .15);
  --burger-color: #333;
  --burger-line-border-radius: 0.1875em;
  --burger-diameter: 2.125em;
  --burger-btn-border-radius: calc(var(--burger-diameter) / 2);
  --burger-line-transition: .3s;
  --burger-transition: all .1s ease-in-out;
  --burger-hover-scale: 1.1;
  --burger-active-scale: .95;
  --burger-enable-outline-color: var(--burger-bg);
  --burger-enable-outline-width: 0.125em;
  --burger-enable-outline-offset: var(--burger-enable-outline-width);
  /* nav */
  --nav-padding-x: 0.25em;
  --nav-padding-y: 0.625em;
  --nav-border-radius: 0.375em;
  --nav-border-color: #ccc;
  --nav-border-width: 0.0625em;
  --nav-shadow-color: rgba(0, 0, 0, .2);
  --nav-shadow-width: 0 1px 5px;
  --nav-bg: #eee;
  --nav-font-family: Menlo, Roboto Mono, monospace;
  --nav-default-scale: .8;
  --nav-active-scale: 1;
  --nav-position-left: 0;
  --nav-position-right: unset;
  /* if you want to change sides just switch one property */
  /* from properties to "unset" and the other to 0 */
  /* title */
  --nav-title-size: 0.625em;
  --nav-title-color: #777;
  --nav-title-padding-x: 1rem;
  --nav-title-padding-y: 0.25em;
  /* nav button */
  --nav-button-padding-x: 1rem;
  --nav-button-padding-y: 0.375em;
  --nav-button-border-radius: 0.375em;
  --nav-button-font-size: 12px;
  --nav-button-hover-bg: #6495ed;
  --nav-button-hover-text-color: #fff;
  --nav-button-distance: 0.875em;
  /* underline */
  --underline-border-width: 0.0625em;
  --underline-border-color: #ccc;
  --underline-margin-y: 0.3125em;
}

/* popup settings 👆 */

.popup {
  display: inline-block;
  text-rendering: optimizeLegibility;
  position: relative;
}

.popup input {
  display: none;
}

.burger {
  display: flex;
  position: relative;
  align-items: center;
  justify-content: center;
  background: var(--burger-bg);
  width: var(--burger-diameter);
  height: var(--burger-diameter);
  border-radius: var(--burger-btn-border-radius);
  border: none;
  cursor: pointer;
  overflow: hidden;
  transition: var(--burger-transition);
  outline: var(--burger-enable-outline-width) solid transparent;
  outline-offset: 0;
}

.burger span {
  height: var(--burger-line-height);
  width: var(--burger-line-width);
  background: var(--burger-color);
  border-radius: var(--burger-line-border-radius);
  position: absolute;
  transition: var(--burger-line-transition);
}

.burger span:nth-child(1) {
  top: var(--burger-offset);
}

.burger span:nth-child(2) {
  bottom: var(--burger-offset);
}

.burger span:nth-child(3) {
  top: 50%;
  transform: translateY(-50%);
}

.popup-window {
  transform: scale(var(--nav-default-scale));
  visibility: hidden;
  opacity: 0;
  position: absolute;
  padding: var(--nav-padding-y) var(--nav-padding-x);
  background: var(--nav-bg);
  font-family: var(--nav-font-family);
  color: var(--nav-text-color);
  border-radius: var(--nav-border-radius);
  box-shadow: var(--nav-shadow-width) var(--nav-shadow-color);
  border: var(--nav-border-width) solid var(--nav-border-color);
  top: calc(var(--burger-diameter) + var(--burger-enable-outline-width) + var(--burger-enable-outline-offset));
  left: var(--nav-position-left);
  right: var(--nav-position-right);
  transition: var(--burger-transition);
}

.popup-window legend {
  padding: var(--nav-title-padding-y) var(--nav-title-padding-x);
  margin: 0;
  color: var(--nav-title-color);
  font-size: var(--nav-title-size);
  text-transform: uppercase;
}

.popup-window ul {
  margin: 0;
  padding: 0;
  list-style-type: none;
}

.popup-window ul button {
  outline: none;
  width: 100%;
  border: none;
  background: none;
  display: flex;
  align-items: center;
  color: var(--burger-color);
  font-size: var(--nav-button-font-size);
  padding: var(--nav-button-padding-y) var(--nav-button-padding-x);
  white-space: nowrap;
  border-radius: var(--nav-button-border-radius);
  cursor: pointer;
  column-gap: var(--nav-button-distance);
}

.popup-window ul li:nth-child(1) svg,
.popup-window ul li:nth-child(2) svg {
  color: cornflowerblue;
}

.popup-window ul li:nth-child(4) svg,
.popup-window ul li:nth-child(5) svg {
  color: rgb(153, 153, 153);
}

.popup-window ul li:nth-child(7) svg {
  color: red;
}

.popup-window hr {
  margin: var(--underline-margin-y) 0;
  border: none;
  border-bottom: var(--underline-border-width) solid var(--underline-border-color);
}

/* actions */

.popup-window ul button:hover,
.popup-window ul button:focus-visible,
.popup-window ul button:hover svg,
.popup-window ul button:focus-visible svg {
  color: var(--nav-button-hover-text-color);
  background: var(--nav-button-hover-bg);
}

.burger:hover {
  transform: scale(var(--burger-hover-scale));
}

.burger:active {
  transform: scale(var(--burger-active-scale));
}

.burger:focus:not(:hover) {
  outline-color: var(--burger-enable-outline-color);
  outline-offset: var(--burger-enable-outline-offset);
}

.popup input:checked+.burger span:nth-child(1) {
  top: 50%;
  transform: translateY(-50%) rotate(45deg);
}

.popup input:checked+.burger span:nth-child(2) {
  bottom: 50%;
  transform: translateY(50%) rotate(-45deg);
}

.popup input:checked+.burger span:nth-child(3) {
  transform: translateX(calc(var(--burger-diameter) * -1 - var(--burger-line-width)));
}

.popup input:checked~nav {
  transform: scale(var(--nav-active-scale));
  visibility: visible;
  opacity: 1;
}
</style>
