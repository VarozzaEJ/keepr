<script setup>
import { AppState } from '@/AppState.js';
import { router } from '@/router.js';
import { accountService } from '@/services/AccountService.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { computed, ref } from 'vue';


const editableVaultData = ref({
    name: '',
    img: '',
    description: '',
    isPrivate: false
})
const account = computed(() => AppState.account)

async function createVault() {
    try {
        debugger
        const newVault = await vaultsService.createVault(editableVaultData.value)
        // Modal.getOrCreateInstance('#createVaultModal').hide()
        Pop.success("Success")
        accountService.getVaultsByCreatorId(account.value.id)
        // router.push({ name: 'Vault Details', params: { vaultId: newVault.id } })

    }
    catch (error) {
        Pop.error(error);
    }
}
</script>


<template>
    <main>

        <div class="modal  fade" id="createVaultModal" tabindex="-1" aria-labelledby="createVaultModalLabel"
            aria-hidden="true">
            <div class="modal-dialog ">
                <div class="modal-content no-round">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="createVaultModalLabel">Add Your Vault</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <form @submit.prevent="createVault()">
                            <div class="mb-3 form-floating">
                                <input minlength="2" v-model="editableVaultData.name" type="text" class="form-control"
                                    id="title" maxlength="50">
                                <label for="title">Title</label>
                            </div>
                            <div class="mb-3 form-floating">
                                <input minlength="2" v-model="editableVaultData.img" type="url" class="form-control"
                                    id="Img" maxlength="3000">
                                <label for="Img">Image Url</label>
                            </div>
                            <div class="mb-5 form-floating">
                                <textarea minlength="2" v-model="editableVaultData.description" rows="5"
                                    style="height: 100px" class="form-control" placeholder=""
                                    maxlength="1000"></textarea>
                                <label for="Vault Description">Description</label>
                            </div>
                            <div class="mb-3 form-check">
                                <input v-model="editableVaultData.isPrivate" type="checkbox" class="form-check-input"
                                    id="isPrivate">
                                <label class="form-check-label" for="isPrivate">Make This Vault Private?</label>
                            </div>
                            <div class="d-flex justify-content-end">
                                <button class="btn btn-outline-dark text-end">Create</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </main>
</template>


<style lang="scss" scoped>
main {
    outline: none;
}

.no-round {
    border-radius: 1%;
}

input[type=text] {
    border: none;
    border-bottom: 1px solid rgb(73, 73, 73);
    outline: none;
    border-radius: 0;
}

input[type=url] {
    border: none;
    border-bottom: 1px solid rgb(73, 73, 73);
    outline: none;
    border-radius: 0;
}

textarea {
    border: none;
    border-bottom: 1px solid rgb(73, 73, 73);
    outline: none;
    border-radius: 0;
}

input {
    outline: none;
    outline-width: 0;
}

label {
    outline: none;
}

input:not(:placeholder-shown):invalid {
    outline: none;
}

input:not(:placeholder-shown):invalid {
    outline: none;
}

input:is(:placeholder-shown):valid {
    outline: none;
}

input:not(:placeholder-shown):valid {
    outline: none;
}

textarea:not(:placeholder-shown):valid {
    outline: none;
}

textarea:not(:placeholder-shown):invalid {
    outline: none;
}
</style>