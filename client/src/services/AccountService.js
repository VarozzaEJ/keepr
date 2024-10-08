import { Profile } from '@/models/Profile.js'
import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { Vault } from '@/models/Vault.js'
import { Keep } from '@/models/Keep.js'

class AccountService {
  async getMyAccountVaults(profileId) {
    const response = await api.get('account/vaults')
    const profileVaults = response.data.map(vaultPOJO => new Vault(vaultPOJO))
    AppState.profileVaults = profileVaults
  }
  async updateAccount(accountData) {
    const response = await api.put('/account/update', accountData)
    AppState.activeProfile = new Profile(response.data)
  }
  async getVaultsByCreatorId(profileId) {
    AppState.publicProfileVaults = []
    AppState.profileVaults = []
    const response = await api.get(`api/profiles/${profileId}/vaults`)
    const publicProfileVaults = response.data
      .filter(vault => vault.isPrivate == false)
      .map(vaultPOJO => new Vault(vaultPOJO))
    AppState.publicProfileVaults = publicProfileVaults
  }

  async getKeepsByCreatorId(profileId) {
    AppState.profileKeeps = []
    const response = await api.get(`api/profiles/${profileId}/keeps`)
    const keeps = response.data.map(keepPOJO => new Keep(keepPOJO))
    AppState.profileKeeps = keeps
  }
  async setActiveProfile(profileId) {
    AppState.activeProfile = null
    const response = await api.get(`api/profiles/${profileId}`)
    const profile = new Profile(response.data)
    AppState.activeProfile = profile
  }
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
}

export const accountService = new AccountService()
