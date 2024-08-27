<script setup>
import { AppState } from '@/AppState.js';
import { accountService } from '@/services/AccountService.js';
import Pop from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { computed, ref } from 'vue';

const account = computed(() => AppState.account)

const editableAccountData = ref({
    name: '',
    // TODO pre place your account information so you don't have to put the url in every time, also, get a previewed picture
    picture: '',
    coverImg: '',

})

async function updateAccount() {
    try {
        accountService.updateAccount(editableAccountData.value)
        Modal.getOrCreateInstance('#editAccountModal').hide()
        Pop.success("Success")
    }
    catch (error) {
        Pop.error(error);
    }
}
</script>


<template>
    <div>

        <div class="modal fade" id="editAccountModal" tabindex="-1" aria-labelledby="editAccountModalLabel"
            aria-hidden="true">
            <div class="modal-dialog">
                <div v-if="account" class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="editAccountModalLabel">Modal title</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <form @submit.prevent="updateAccount()">
                            <div class="mb-3">
                                <label for="name" class="form-label">Account Name</label>
                                <input v-model="editableAccountData.name" type="text" class="form-control" id="name"
                                    required maxlength="100">
                            </div>
                            <div class="mb-3">
                                <label class="form-label">Image Preview</label>
                                <div class="d-flex justify-content-center">
                                    <div v-if="!editableAccountData.picture"
                                        class="bg-subtle rounded img-preview d-flex justify-content-center align-items-center">
                                        <i class="mdi mdi-image display-1"></i>
                                    </div>
                                    <div v-else>
                                        <img class="rounded img-preview preview-img" :src="editableAccountData.picture"
                                            :alt="`${editableAccountData.picture}`">
                                    </div>
                                </div>
                            </div>
                            <div class="mb-3">
                                <label for="picture" class="form-label">Account Picture</label>
                                <input v-model="editableAccountData.picture" type="url" class="form-control"
                                    id="picture" required maxlength="500">
                            </div>
                            <div class="mb-3">
                                <label for="coverImg" class="form-label">Account CoverImg</label>
                                <input v-model="editableAccountData.coverImg" type="url" class="form-control"
                                    id="coverImg" maxlength="500">
                            </div>

                            <button type="submit" class="btn btn-primary">Submit</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.preview-img {
    width: 300px;
    height: 300px;
}

.img-preview {
    height: 25vh;
    width: 25vh;
    border: 2px dashed black;
    color: black;
}
</style>