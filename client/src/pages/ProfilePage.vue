<script setup>
import { AppState } from '@/AppState.js';
import EditAccountModal from '@/components/globals/EditAccountModal.vue';
import KeepCard from '@/components/globals/KeepCard.vue';
import VaultCard from '@/components/globals/VaultCard.vue';
import { accountService } from '@/services/AccountService.js';
import Pop from '@/utils/Pop.js';
import { computed, onMounted, watch, watchEffect } from 'vue';
import { useRoute } from 'vue-router';
import { waveform } from 'ldrs'

waveform.register()


const profile = computed(() => AppState.activeProfile)
const route = useRoute()
const publicVaults = computed(() => AppState.publicProfileVaults)
const vaults = computed(() => AppState.profileVaults)
const keeps = computed(() => AppState.profileKeeps)
const account = computed(() => AppState.account)


watch([
    () => route.params.profileId,
    // () => AppState.account
], () => {
    setActiveProfile(route.params.profileId)
    getVaultsByCreatorId(route.params.profileId)
    accountService.getMyAccountVaults()
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
    <div class="container w-md-75">
        <div v-if="profile && account" class="row">
            <div class="col-12">
                <div class=" h-50">
                    <img class="img-fluid img " :src="profile.coverImg"
                        :alt="`A picture of ${profile.name}'s cover image'`" :title="`${profile.name}'s cover image`">

                </div>
            </div>

            <div class="row d-flex justify-content-end">
                <div class="col-md-2 col-5 d-flex justify-content-center">
                    <img class="img-fluid creator-picture" :src="profile.picture" :title="`${profile.name}`"
                        :alt="`A picture of ${profile.name}`">

                </div>
                <div class="col-md-5 col-3">
                    <div class="dropdown-center flex-grow-1 d-flex justify-content-end">
                        <p v-if="account.id == profile.id" title="Open Account Dropdown?"
                            class="mdi mdi-dots-horizontal fs-1" role="button" data-bs-toggle="dropdown"
                            aria-expanded="false">

                        </p>
                        <div class="dropdown-menu ">
                            <div class="container">
                                <div class="row">
                                    <div class="col-12 d-flex justify-content-center">
                                        <button data-bs-toggle="modal" data-bs-target="#editAccountModal"
                                            class="btn  text-center mb-1 btn-success"><i class="mdi mdi-file"></i>Edit
                                            Account</button>
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
                        <div v-else-if="account.id != profile.id" class="d-flex justify-content-center">
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
                <div v-if="account.id == profile.id" class="row d-flex justify-content-center">
                    <div v-for="vault in vaults" :key="vault.id" class="col-xl-3 col-md-6 col-sm-9">
                        <VaultCard :vaultProp="vault" />
                    </div>
                </div>

                <div v-else-if="account.id != profile.id" class="row d-flex justify-content-center">
                    <div v-for="vault in publicVaults" :key="vault.id" class="col-xl-3 col-md-6 col-sm-9">
                        <VaultCard :vaultProp="vault" />
                    </div>
                </div>
            </div>

            <div class="col-12">
                <div class="row">
                    <span class="fw-bold fs-2">Keeps</span>
                </div>
                <div class="row mt-3 full-vh">
                    <div class="col-md-12 mobile-masonry  masonry">
                        <div class="break-inside" v-for="keep in keeps" :key="keep.id">
                            <KeepCard :keepProp='keep' />
                        </div>
                    </div>
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
                <div class="col-md-2 col-3 d-flex justify-content-center">
                    <img class="img-fluid creator-picture me-md-0 me-3" :src="profile.picture"
                        :alt="`A picture of ${profile.name}`">

                </div>
                <div class="col-md-5 col-4">

                </div>
                <div class="col-12 d-flex  justify-content-center">
                    <div class="row d-flex flex-column">
                        <div class="d-flex justify-content-center">
                            <span class="fw-bold fs-2">{{ profile.name }}</span>
                        </div>
                        <div class="d-flex justify-content-center">
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
                    <VaultCard :vaultProp="vault" />
                </div>


            </div>

            <div class="col-12">
                <div class="row">
                    <span class="fw-bold fs-2">Keeps</span>
                </div>
                <div class="row mt-3 full-vh">
                    <div class="col-md-12 mobile-masonry  masonry">
                        <div class="break-inside" v-for="keep in keeps" :key="keep.id">
                            <KeepCard :keepProp='keep' />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="d-flex justify-content-center align-items-center mt-4"><span class="large-text">Loading<l-waveform
                    class="ms-5" l-waveform size="83" stroke="3.5" speed="1" color="black">
                </l-waveform></span>
        </div>
    </div>
    <div v-if="account">
        <EditAccountModal />
    </div>
</template>


<style lang="scss" scoped>
.large-text {
    font-size: 70px;
}

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

@media screen and (min-width: 769px) {
    .masonry {
        columns: 4;
        column-gap: 1em;
        column-fill: balance;
    }
}

.full-vh {}

.break-inside {
    break-inside: avoid;
}

@media screen and (max-width: 768px) {
    .mobile-masonry {
        columns: 2;
        column-gap: 1em;
        column-fill: balance;
        break-inside: avoid;
    }
}
</style>