<script setup>
import { AppState } from '@/AppState.js';
import { accountService } from '@/services/AccountService.js';
import { vaultKeepService } from '@/services/VaultKeepService.js';
import Pop from '@/utils/Pop.js';
import { computed, onMounted, ref, watch } from 'vue';


const keep = computed(() => AppState.activeKeep)
const profileVaults = computed(() => AppState.profileVaults)
const account = computed(() => AppState.account)

watch(() => AppState.account, () => {
    accountService.getVaultsByCreatorId(AppState.account.id)
})


const editableVaultData = ref({
    vaultId: 0,
    keepId: null
})

async function createVaultKeep() {
    try {
        editableVaultData.value.keepId = keep.value.id
        const response = await vaultKeepService.createVaultKeep(editableVaultData.value)
        editableVaultData.value = {
            vaultId: 0,
            keepId: null
        }
        Pop.success("Success")
    }
    catch (error) {
        Pop.error(error);
    }
}
</script>


<template>
    <div class="modal modal-xl fade" id="keepModal" tabindex="-1" aria-labelledby="exampleModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content" v-if="keep">
                <div class="modal-body p-0">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-6 p-0">
                                <img class="img img-fluid mobile-height" :src="keep.img" alt="">
                            </div>
                            <div class="col-md-6 d-flex mobile-height">
                                <div class="container d-flex flex-column justify-content-between">
                                    <div class="row d-flex justify-content-center">
                                        <div class="col-4 mt-4 d-flex justify-content-between">
                                            <span class="fs-4"><i class="mdi mdi-eye me-2"></i>0</span>
                                            <span class="fs-4"><i class="mdi mdi-alpha-k-box-outline me-2"></i>0</span>
                                        </div>
                                    </div>
                                    <div class="row d-flex justify-content-center">
                                        <div class="col-lg-8 col-12 d-flex justify-content-center">
                                            <span class="fw-bold fs-2 text-center">{{ keep.name }}</span>
                                        </div>
                                        <div class="col-12 d-flex justify-content-center">
                                            <span class="fs-6 text-center">{{ keep.description }}</span>
                                        </div>
                                    </div>
                                    <div
                                        class="row d-flex justify-content-md-around justify-content-md-between justify-content-around">
                                        <div
                                            class="col-lg-7 col-12 mb-4 d-flex ps-0 align-items-center justify-content-center">
                                            <form @submit.prevent="createVaultKeep()" v-if="account" class="d-flex">
                                                <select v-model="editableVaultData.vaultId"
                                                    class="form-select form-control no-round-end pe-0"
                                                    aria-label="Select a vault to add the picture to">
                                                    <option selected value=0>Select a Vault</option>
                                                    <option v-for="vault in profileVaults" :key="vault.id"
                                                        :value="vault.id">{{ vault.name }}</option>
                                                </select>
                                                <button
                                                    class="btn btn-success rounded text-light text-center no-round-start">save</button>
                                            </form>
                                        </div>
                                        <div
                                            class="col-lg-5 col-12 mb-4 me-md-0 me-2 ps-md-2 justify-content-center ps-0 d-flex align-items-center">
                                            <img class="img-fluid creator-picture " :src="keep.creator.picture"
                                                :alt="keep.creator.name">
                                            <span class=" ms-2 fs-5 me-2">{{ keep.creator.trimmedName
                                                }}</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.img {
    border-top-left-radius: 1%;
    border-bottom-left-radius: 1%;
    border-top-right-radius: 1%;
    border-bottom-right-radius: 1%;
    max-height: 100%;
    max-width: 100%;
    height: 600px;
    aspect-ratio: 1/1;
    object-fit: cover;
    object-position: center;
}

.creator-picture {
    aspect-ratio: 1/1;
    height: 50px;
    width: 50px;
    border-radius: 50%;
    object-position: center;
    object-fit: cover;
}

@media screen and (max-width: 768px) {
    .mobile-height {
        height: 450px;
        width: 100%;
    }
}

.no-round-end {
    border-top-right-radius: 0;
    border-bottom-right-radius: 0;
}

.no-round-start {
    border-top-left-radius: 0;
    border-bottom-left-radius: 0;
}

form {
    width: 50dvw;
}
</style>