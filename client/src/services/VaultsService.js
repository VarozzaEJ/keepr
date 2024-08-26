import { Vault } from "@/models/Vault.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";

class VaultsService {
    async setActiveVault(vaultId) {
        AppState.activeVault = null
        const vault = await this.getVaultById(vaultId)
        AppState.activeVault = vault
    }


    async getVaultById(vaultId) {
        AppState.activeVault = null
        const response = await api.get(`api/vaults/${vaultId}`)
        const vault = new Vault(response.data)
        return vault
    }
}

export const vaultsService = new VaultsService()