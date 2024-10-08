import { DatabaseItem } from "./DataBaseItem.js";


export class Profile extends DatabaseItem {

    constructor(data) {
        super(data);
        this.name = data.name;
        this.picture = data.picture;
        this.coverImg = data.coverImg ? data.coverImg : "https://images.unsplash.com/photo-1707007785777-9324ce8cf42d?q=80&w=3540&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
    }
    get trimmedName() {
        const lastIndexOfAt = this.name.lastIndexOf('@')
        return lastIndexOfAt > -1 ? this.name.substring(0, lastIndexOfAt) : this.name
    }
}