import { createRouter, createWebHashHistory } from 'vue-router'
import { authGuard, authSettled } from '@bcwdev/auth0provider-client'

function loadPage(page) {
  return () => import(`./pages/${page}.vue`)
}

const routes = [
  {
    path: '/',
    name: 'Home',
    component: loadPage('HomePage'),
    beforeEnter: authSettled
  },
  {
    path: '/:profileId',
    name: 'Profile',
    component: loadPage('ProfilePage'),
    beforeEnter: authSettled
  },
  {
    path: '/vault/:vaultId',
    name: 'Vault Details',
    component: loadPage('VaultDetailsPage'),
    beforeEnter: authSettled
  },
]

export const router = createRouter({
  linkActiveClass: 'router-link-active',
  linkExactActiveClass: 'router-link-exact-active',
  history: createWebHashHistory(),
  routes
})
