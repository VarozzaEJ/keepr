<script setup>
import { AppState } from '@/AppState.js';
import { Keep } from '@/models/Keep.js';
import { VaultKeepKeep } from '@/models/VaultKeep.js';
import { accountService } from '@/services/AccountService.js';
import { keepsService } from '@/services/KeepsService.js';
import { vaultKeepService } from '@/services/VaultKeepService.js';
import Pop from '@/utils/Pop.js';
import { computed } from 'vue';


const props = defineProps({ keepProp: { type: VaultKeepKeep, required: true } })
const account = computed(() => AppState.account)
const activeProfile = computed(() => AppState.activeProfile)

async function setActiveKeep(keepId) {
    try {
        keepsService.setActiveKeep(keepId)
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

async function deleteVaultKeep(vaultKeepId, keepId) {
    try {
        const wantsToDelete = await Pop.confirm("Are you sure?")
        if (!wantsToDelete) return
        vaultKeepService.deleteVaultKeep(vaultKeepId, keepId)
        Pop.success("Success")
    }
    catch (error) {
        Pop.error(error);
    }
}

</script>


<template>
    <main class="container-fluid my-md-1 my-2 keep-bg-img dynamic-width text-dark">
        <div class="d-flex justify-content-end"><i @click="deleteVaultKeep(keepProp.vaultKeepId, keepProp.id)"
                v-if="account?.id == activeProfile.id" role="button" class="mdi mdi-close-circle text-danger"></i>
        </div>
        <div class="row ">
            <div class="col-12">
                <img role="button" @click="setActiveKeep(keepProp.id)" data-bs-toggle="modal"
                    data-bs-target="#keepModal" class="img-fluid rounded img" :src="keepProp.img"
                    :alt="`A Keep made by ${keepProp.creator.name}`">
            </div>
            <div class="d-flex relative desktop-text mobile-text justify-content-between">
                <span class=" ms-2 text-light text-shadow">{{ keepProp.name }}</span>
                <router-link :to="{ name: 'Profile', params: { profileId: keepProp.creatorId } }">
                    <img @click="setActiveProfile(keepProp.creatorId)" role='button' class="creator-picture me-1"
                        :src="keepProp.creator.picture" :alt="keepProp.creator.name">
                </router-link>
            </div>
        </div>
    </main>
</template>


<style lang="scss" scoped>
.img {
    height: 100%;
}

.keep-bg-img {
    aspect-ratio: 1/1;
    width: 100%;
    height: 100%;
}

i {
    position: relative;
    top: 15px;
    left: 7px;
}

@media screen and (max-width: 768px) {
    .mobile-text {
        font-size: 16px;
        position: relative;
        top: -50px;
    }
}

@media screen and (min-width: 769px) {
    .desktop-text {
        font-size: 20px;
        position: relative;
        top: -40px;
    }
}

.text-shadow {
    text-shadow: 2px 2px black;

}

.relative {}

.creator-picture {
    aspect-ratio: 1/1;
    width: 30px;
    height: 30px;
    border-radius: 50%;
}
</style>