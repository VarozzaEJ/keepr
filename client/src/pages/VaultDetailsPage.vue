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
import { waveform } from 'ldrs';

waveform.register()

const vault = computed(() => AppState.activeVault)
const vaultKeeps = computed(() => AppState.vaultKeeps)
const route = useRoute()
const account = computed(() => AppState.account)

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

async function deleteVault() {
    try {
        const wantsToDelete = await Pop.confirm("Are you sure?")
        if (!wantsToDelete) return
        vaultsService.deleteVault(route.params.vaultId)
        router.push({ name: 'Home' })
    }
    catch (error) {
        Pop.error(error);
    }
}
</script>


<template>
    <div v-if="vault" class="container mt-2">
        <div class="row d-flex justify-content-center">
            <div class="col-md-8 col-11  ">
                <div v-if="vault.creatorId == account?.id" class="d-flex justify-content-end">
                    <i @click="deleteVault()" role="button" title="Delete Vault?"
                        class="mdi mdi-close-circle text-danger text-end fs-1"></i>
                </div>
                <img :src="vault.img" alt="" class="img-fluid vault-bg-img">
                <div class="d-flex justify-content-center relative">
                    <div class="d-block text-light text-shadow">
                        <p class="fw-bold text-capitalize fs-2 mb-1 text-center">{{ vault.name }}</p>
                        <p class="fw-bold fs-5 mb-0 text-center">by: {{ vault.creator.name }}</p>
                    </div>
                </div>
            </div>
        </div>
        <div class="row d-flex justify-content-center mb-3">
            <div class="col-md-3 col-4">
                <div class="bg-info rounded-pill p-2">
                    <p v-if="vaultKeeps.length > 1 || vaultKeeps.length == 0" class="fs-3 text-center mb-0">{{
                        vaultKeeps.length }} Keeps</p>
                    <p v-if="vaultKeeps.length == 1" class="fs-3 text-center mb-0">{{ vaultKeeps.length }} Keep
                    </p>
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
        <div class="d-flex justify-content-center align-items-center mt-4"><span class="large-text">Loading<l-waveform
                    class="ms-5" l-waveform size="83" stroke="3.5" speed="1" color="black">
                </l-waveform></span>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.large-text {
    font-size: 70px;
}

.vault-bg-img {
    // background-image: v-bind('vault.vaultBackgroundImage');
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

    i {
        position: relative;
        top: 30px;
        left: 20px;
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

    i {
        position: relative;
        top: 20px;
        left: 10px;
    }
}
</style>