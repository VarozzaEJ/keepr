import { DatabaseItem } from "./DataBaseItem.js";
import { Profile } from "./Profile.js";

export class Keep extends DatabaseItem {
    constructor(data) {
        super(data)
        this.name = data.name
        this.description = data.description
        this.img = data.img
        this.views = data.views
        this.kept = data.kept
        this.creatorId = data.creatorId
        this.creator = new Profile(data.creator)
    }

    get keepBackgroundImage() {
        return `url(${this.img})`
    }
}