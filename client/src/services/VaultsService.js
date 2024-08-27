import { Vault } from "@/models/Vault.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";

class VaultsService {
    async createVault(vaultData) {
        const response = await api.post('api/vaults', vaultData)
        const newVault = new Vault(response.data)
        if (newVault.isPrivate == false) {
            AppState.profileVaults.push(newVault)
        } else if (newVault.isPrivate == true) {
            AppState.publicProfileVaults.push(newVault)
        }
        return newVault
    }
    async setActiveVault(vault) {
        AppState.activeVault = null
        AppState.activeVault = vault
    }


    async getVaultById(vaultId) {
        AppState.activeVault = null
        const response = await api.get(`api/vaults/${vaultId}`)
        const vault = new Vault(response.data)
        AppState.activeVault = vault
    }
}

export const vaultsService = new VaultsService()