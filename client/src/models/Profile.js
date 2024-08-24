import { DatabaseItem } from "./DataBaseItem.js";


export class Profile extends DatabaseItem {

    constructor(data) {
        super(data);
        this.name = data.name;
        this.picture = data.picture;
    }
}