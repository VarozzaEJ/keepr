import { DatabaseItem } from "./DataBaseItem.js";
import { Keep } from "./Keep.js";
import { Profile } from "./Profile.js";

export class VaultKeep extends DatabaseItem {
    constructor(data) {
        super(data)
        this.keepId = data.keepId
        this.vaultId = data.vaultId
        this.creatorId = data.creatorId
        this.creator = new Profile(data.creator)
    }
}

export class VaultKeepKeep extends Keep {
    constructor(data) {
        super(data)
        this.keepId = data.keepId
        this.vaultId = data.vaultId
    }
}