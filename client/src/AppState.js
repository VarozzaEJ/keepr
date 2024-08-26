import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  /**@type {import('@bcwdev/auth0provider-client').Identity} */
  identity: null,
  /** @type {import('./models/Account.js').Account} user info from the database*/
  account: null,

  /** @type {import('./models/Keep.js').Keep[]} user info from the database*/
  keeps: [],

  /** @type {import('./models/Keep.js').Keep} user info from the database*/
  activeKeep: null,

  /** @type {import('./models/Vault.js').Vault} user info from the database*/
  activeVault: null,

  /** @type {import('./models/Vault.js').Vault[]} user info from the database*/
  profileVaults: [],

  /** @type {import('./models/Vault.js').Vault[]} user info from the database*/
  publicProfileVaults: [],

  /** @type {import('./models/Profile.js').Profile} user info from the database*/
  activeProfile: null,

  /** @type {import('./models/Keep.js').Keep[]} user info from the database*/
  profileKeeps: [],

  /** @type {import('./models/VaultKeep.js').VaultKeepKeep[]} user info from the database*/
  vaultKeeps: [],
})

