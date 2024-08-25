import { Profile } from '@/models/Profile.js'
import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { Vault } from '@/models/Vault.js'

class AccountService {
  async getVaultsByCreatorId(profileId) {
    AppState.profileVaults = []
    const response = await api.get(`account/${profileId}/vaults`)
    const profileVaults = response.data.map(vaultPOJO => new Vault(vaultPOJO))
    AppState.profileVaults = profileVaults
  }

  async getKeepsByCreatorId(profileId) {
    const response = await api.get(`account/${profileId}/keeps`)
    logger.log(response.data)
  }
  async setActiveProfile(creatorId) {
    AppState.activeProfile = null
    const response = await api.get(`account/${creatorId}`)
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
