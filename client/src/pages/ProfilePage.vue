<script setup>
import { AppState } from '@/AppState.js';
import VaultCard from '@/components/globals/VaultCard.vue';
import { accountService } from '@/services/AccountService.js';
import Pop from '@/utils/Pop.js';
import { computed, onMounted, watch, watchEffect } from 'vue';
import { useRoute } from 'vue-router';

const profile = computed(() => AppState.activeProfile)
const route = useRoute()
const publicVaults = computed(() => AppState.publicProfileVaults)
const vaults = computed(() => AppState.profileVaults)
const keeps = computed(() => AppState.profileKeeps)
const account = computed(() => AppState.account)


watch(() => route.params.profileId, () => {
    setActiveProfile(route.params.profileId)
    getVaultsByCreatorId(route.params.profileId)
    getKeepsByProfileId(route.params.profileId)
}, { immediate: true })




async function getVaultsByCreatorId(profileId) {
    try {
        accountService.getVaultsByCreatorId(profileId)
    }
    catch (error) {
        Pop.error(error);
    }
}

async function getKeepsByProfileId(accountId) {
    try {
        accountService.getKeepsByCreatorId(accountId)
    }
    catch (error) {
        Pop.error(error);
    }
}

async function setActiveProfile(creatorId) {
    try {
        accountService.setActiveProfile(creatorId)
    }
    catch (error) {
        Pop.error(error);
    }
}
</script>


<template>
    <div class="container w-75">
        <div v-if="profile && account" class="row">
            <div class="col-12">
                <div class=" h-50">
                    <img class="img-fluid img " :src="profile.coverImg"
                        :alt="`A picture of ${profile.name}'s cover image'`" :title="`${profile.name}`">

                </div>
            </div>

            <div class="row d-flex justify-content-end">
                <div class="col-2 d-flex justify-content-center">
                    <img class="img-fluid creator-picture" :src="profile.picture" :alt="`A picture of ${profile.name}`">

                </div>
                <div class="col-5">
                    <div class="dropdown-center flex-grow-1 d-flex justify-content-end">
                        <p class="mdi mdi-dots-horizontal fs-1" href="#" role="button" data-bs-toggle="dropdown"
                            aria-expanded="false">

                        </p>
                        <div class="dropdown-menu ">
                            <div class="container">
                                <div class="row">
                                    <div class="col-12 d-flex justify-content-center">
                                        <button class="btn w-75 d-flex justify-content-center mb-2 btn-danger"><i
                                                class="mdi mdi-delete-forever">Delete</i></button>
                                    </div>
                                    <div class="col-12 d-flex justify-content-center">
                                        <button class="btn w-75 text-center mb-1 btn-success"><i
                                                class="mdi mdi-file"></i>Edit</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-12 d-flex  justify-content-center">
                    <div class="row d-flex flex-column">
                        <div class="d-flex justify-content-center">
                            <span class="fw-bold fs-2">{{ profile.name }}</span>
                        </div>
                        <div v-if="account.id == profile.id" class="d-flex justify-content-center">
                            <span v-if="vaults.length > 1 || vaults.length == 0" class="me-2">{{ vaults.length }}
                                Vaults</span>
                            <span v-else-if="vaults.length == 1" class="me-2">{{ vaults.length }} Vault</span>
                            <span>|</span>
                            <span v-if="keeps.length > 1" class="ms-2"> {{ keeps.length }} Keeps</span>
                            <span v-else class="ms-2"> {{ keeps.length }} Keep</span>
                        </div>
                        <div v-else-if="account.id != profile.id" class="d-flex justify-content-center text-danger">
                            <span v-if="publicVaults.length > 1 || publicVaults.length == 0" class="me-2">{{
                                publicVaults.length }} Vaults</span>
                            <span v-else-if="publicVaults.length == 1" class="me-2">{{ publicVaults.length }}
                                Vault</span>
                            <span>|</span>
                            <span v-if="keeps.length > 1 || keeps.length == 0" class="ms-2"> {{ keeps.length }}
                                Keeps</span>
                            <span v-else class="ms-2"> {{ keeps.length }} Keep</span>
                        </div>
                    </div>
                </div>
                <div class="col-12">
                    <div class="row">
                        <span class="fw-bold fs-2">Vaults</span>
                    </div>
                </div>
            </div>
            <div>

                <div v-if="account.id != profile.id || !account" class="row">
                    <div v-for="vault in publicVaults" :key="vault.id" class="col-md-3">
                        <!-- Vault cards go here -->
                        <VaultCard :vaultProp="vault" />
                    </div>
                </div>
                <div v-else-if="account.id == profile.id" class="row">
                    <div v-for="vault in vaults" :key="vault.id" class="col-md-3">
                        <VaultCard :vaultProp="vault" />
                    </div>
                </div>
            </div>

            <div class="col-12">
                <div class="row">
                    <span class="fw-bold fs-2">Keeps</span>
                </div>
            </div>

        </div>
        <div v-else-if="profile && !account">
            <div class="col-12">
                <div class=" h-50">
                    <img class="img-fluid img " :src="profile.coverImg"
                        :alt="`A picture of ${profile.name}'s cover image'`" :title="`${profile.name}`">

                </div>
            </div>

            <div class="row d-flex justify-content-end">
                <div class="col-2 d-flex justify-content-center">
                    <img class="img-fluid creator-picture" :src="profile.picture" :alt="`A picture of ${profile.name}`">

                </div>
                <div class="col-5">
                    <div class="dropdown-center flex-grow-1 d-flex justify-content-end">
                        <p class="mdi mdi-dots-horizontal fs-1" href="#" role="button" data-bs-toggle="dropdown"
                            aria-expanded="false">

                        </p>
                        <div class="dropdown-menu ">
                            <div class="container">
                                <div class="row">
                                    <div class="col-12 d-flex justify-content-center">
                                        <button class="btn w-75 d-flex justify-content-center mb-2 btn-danger"><i
                                                class="mdi mdi-delete-forever">Delete</i></button>
                                    </div>
                                    <div class="col-12 d-flex justify-content-center">
                                        <button class="btn w-75 text-center mb-1 btn-success"><i
                                                class="mdi mdi-file"></i>Edit</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-12 d-flex  justify-content-center">
                    <div class="row d-flex flex-column">
                        <div class="d-flex justify-content-center">
                            <span class="fw-bold fs-2">{{ profile.name }}</span>
                        </div>
                        <!-- <div v-if="account.id == profile.id" class="d-flex justify-content-center">
                            <span v-if="vaults.length > 1 || vaults.length == 0" class="me-2">{{ vaults.length }}
                                Vaults</span>
                            <span v-else-if="vaults.length == 1" class="me-2">{{ vaults.length }} Vault</span>
                            <span>|</span>
                            <span v-if="keeps.length > 1" class="ms-2"> {{ keeps.length }} Keeps</span>
                            <span v-else class="ms-2"> {{ keeps.length }} Keep</span>
                        </div> -->
                        <div class="d-flex justify-content-center text-danger">
                            <span v-if="publicVaults.length > 1 || publicVaults.length == 0" class="me-2">{{
                                publicVaults.length }} Vaults</span>
                            <span v-else-if="publicVaults.length == 1" class="me-2">{{ publicVaults.length }}
                                Vault</span>
                            <span>|</span>
                            <span v-if="keeps.length > 1 || keeps.length == 0" class="ms-2"> {{ keeps.length }}
                                Keeps</span>
                            <span v-else class="ms-2"> {{ keeps.length }} Keep</span>
                        </div>
                    </div>
                </div>
                <div class="col-12">
                    <div class="row">
                        <span class="fw-bold fs-2">Vaults</span>
                    </div>
                </div>
            </div>
            <div class="row">


                <div v-for="vault in publicVaults" :key="vault.id" class="col-md-3">
                    <!-- Vault cards go here -->
                    <VaultCard :vaultProp="vault" />
                </div>


            </div>

            <div class="col-12">
                <div class="row">
                    <span class="fw-bold fs-2">Keeps</span>
                </div>
            </div>
        </div>
        <div v-else><span class="fs-1">Loading ... <i class="mdi mdi-loading mdi-spin"></i></span></div>
    </div>
</template>


<style lang="scss" scoped>
.img {
    max-width: 100%;
    width: 100%;
    height: 250px;
}

.creator-picture {
    aspect-ratio: 1/1;
    border-radius: 50%;
    height: 75px;
    width: 75px;
    position: relative;
    top: -30px;
}
</style>