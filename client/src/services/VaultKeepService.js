import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { VaultKeep, VaultKeepKeep } from "@/models/VaultKeep.js"
import { AppState } from "@/AppState.js"
import { Keep } from "@/models/Keep.js"
import { keepsService } from "./KeepsService.js"

class VaultKeepService {
    async deleteVaultKeep(vaultKeepId, keepId) {
        const keepIndex = AppState.vaultKeeps.findIndex(keep => keep.id == keepId)
        AppState.vaultKeeps.splice(keepIndex, 1)
        const response = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
        logger.log(response.data)
    }
    async createVaultKeep(vaultKeepData) {
        const response = await api.post('api/vaultkeeps', vaultKeepData)
    }
    async getKeepsForPublicVault(vaultId) {

        const response = await api.get(`api/vaults/${vaultId}/keeps`)
        const vaultKeeps = response.data.map(vaultKeepPOJO => new VaultKeepKeep(vaultKeepPOJO))
        AppState.vaultKeeps = vaultKeeps
    }


}

export const vaultKeepService = new VaultKeepService()