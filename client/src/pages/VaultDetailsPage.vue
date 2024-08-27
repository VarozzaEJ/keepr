<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/globals/KeepCard.vue';
import VaultedKeepCard from '@/components/globals/VaultedKeepCard.vue';
import { router } from '@/router.js';
import { vaultKeepService } from '@/services/VaultKeepService.js';
import { vaultsService } from '@/services/VaultsService.js';
import Pop from '@/utils/Pop.js';
import { computed, watch } from 'vue';
import { useRoute } from 'vue-router';

const vault = computed(() => AppState.activeVault)
const vaultKeeps = computed(() => AppState.vaultKeeps)
const route = useRoute()

watch(() => route.params.vaultId, () => {
    getVaultById(route.params.vaultId)
    vaultKeepService.getKeepsForPublicVault(route.params.vaultId)

}, { immediate: true })

async function getVaultById(vaultId) {
    try {
        await vaultsService.getVaultById(vaultId)
    }
    catch (error) {
        Pop.error(error);
        if (error.response.data.includes('🅇')) {
            router.push({ name: 'Home' })
        }
    }
}
</script>


<template>
    <div v-if="vault" class="container">
        <div class="row d-flex justify-content-center">
            <div class="col-md-8 col-11  ">
                <img :src="vault.img" alt="" class="img-fluid vault-bg-img">
                <div class="d-flex justify-content-center relative">
                    <div class="d-block text-light text-shadow">
                        <p class="fw-bold text-capitalize fs-2 mb-1 text-center">{{ vault.name }}</p>
                        <p class="fw-bold fs-5 mb-0 text-center">by: {{ vault.creator.name }}</p>
                    </div>
                </div>
            </div>
        </div>
        <div class="row d-flex justify-content-center my-3">
            <div class="col-md-2 col-4">
                <div class="bg-info rounded-pill p-1">
                    <p v-if="vaultKeeps.length > 1 || vaultKeeps.length == 0" class="fs-3 text-center mb-0">{{
                        vaultKeeps.length }} Keeps</p>
                    <p v-if="vaultKeeps.length == 1" class="fs-3 text-center mb-0">{{ vaultKeeps.length }} Keep</p>
                </div>
            </div>
        </div>
        <div class="row ">
            <div class="col-md-12 mobile-masonry  masonry">
                <div class="break-inside" v-for="keep in vaultKeeps" :key="keep.id">
                    <VaultedKeepCard :keepProp='keep' />
                </div>
            </div>
        </div>
    </div>
    <div v-else class="container">
        <div class="row d-flex justify-content-center">
            <div class="col-4 d-flex justify-content-center">
                <p class="text-center fs-1">Loading... <i class="mdi mdi-loading mdi-spin"></i></p>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.vault-bg-img {
    // background-image: v-bind('vault.vaultBackgroundImage');
    height: 100%;
    border-radius: 2%;
    background-position: center;
    object-fit: contain;
    width: 100%;

}

.text-shadow {
    text-shadow: 2px 2px black;
}

.absolute {
    position: absolute;
}

.relative {
    position: relative;
    top: -13dvh;
}

@media screen and (min-width: 769px) {
    .masonry {
        columns: 4;
        column-gap: 1em;
        column-fill: balance;
    }
}


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