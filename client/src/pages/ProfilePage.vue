<script setup>
import { AppState } from '@/AppState.js';
import VaultCard from '@/components/globals/VaultCard.vue';
import { accountService } from '@/services/AccountService.js';
import Pop from '@/utils/Pop.js';
import { computed, onMounted, watch } from 'vue';
import { useRoute } from 'vue-router';

const profile = computed(() => AppState.activeProfile)
const route = useRoute()
const vaults = computed(() => AppState.profileVaults)


watch(() => route.params.profileId, () => {
    getVaultsByCreatorId(route.params.profileId)
    setActiveProfile(route.params.profileId)
}, { immediate: true })


async function getVaultsByCreatorId(profileId) {
    try {
        accountService.getVaultsByCreatorId(profileId)
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
        <div class="row" v-if="profile">
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
                        <div class="d-flex justify-content-center">
                            <span class="me-2">{{ vaults.length }} Vaults</span>
                            <span>|</span>
                            <span class="ms-2"> 14 Keeps</span>
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
                <div v-for="vault in vaults" :key="vault.id" class="col-md-3">
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
        <div v-else>
            <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
        </div>
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