import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Keep } from "@/models/Keep.js"

class KeepsService {
    async deleteKeep(keepId) {
        await api.delete(`api/keeps/${keepId}`)
        const foundKeep = AppState.keeps.findIndex(keep => keep.id == keepId)
        AppState.keeps.splice(foundKeep, 1)
    }
    async createKeep(editableKeepData) {
        AppState.activeKeep = null
        const response = await api.post('api/keeps', editableKeepData)
        const newKeep = new Keep(response.data)
        AppState.activeKeep = newKeep
        AppState.keeps.push(newKeep)
    }
    async setActiveKeep(keepId) {
        AppState.activeKeep = null
        const keep = await this.getKeepById(keepId)
        AppState.activeKeep = keep
    }

    async getAllKeeps() {
        const response = await api.get('api/keeps')
        const keeps = response.data.map(keepPOJO => new Keep(keepPOJO))
        AppState.keeps = keeps
    }

    async getKeepById(keepId) {
        const response = await api.get(`api/keeps/${keepId}`)
        const keep = new Keep(response.data)
        return keep
    }

}

export const keepsService = new KeepsService()