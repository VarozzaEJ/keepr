import { DatabaseItem } from "./DataBaseItem.js";
import { Profile } from "./Profile.js";

export class Keep extends DatabaseItem {
    constructor(data) {
        super(data)
        this.name = data.name
        this.description = data.description
        this.img = data.img
        this.views = data.views
        this.keptCount = data.keptCount
        this.creatorId = data.creatorId
        this.creator = new Profile(data.creator)
        this.vaultKeepId = data.vaultKeepId
    }

    get keepBackgroundImage() {
        return `url(${this.img})`
    }
}