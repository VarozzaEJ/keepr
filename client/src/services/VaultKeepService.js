import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { VaultKeepKeep } from "@/models/VaultKeep.js"
import { AppState } from "@/AppState.js"
import { Keep } from "@/models/Keep.js"

class VaultKeepService {
    async getKeepsForPublicVault(vaultId) {
        const response = await api.get(`api/vaults/${vaultId}/keeps`)
        const vaultKeeps = response.data.map(vaultKeepPOJO => new Keep(vaultKeepPOJO))
        AppState.vaultKeeps = vaultKeeps
    }


}

export const vaultKeepService = new VaultKeepService()