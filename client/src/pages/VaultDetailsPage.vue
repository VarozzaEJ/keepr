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
                    <button @click="deleteVault()" title=" Delete Vault?" class="bin-button">
                        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 39 7" class="bin-top">
                            <line stroke-width="4" stroke="white" y2="5" x2="39" y1="5"></line>
                            <line stroke-width="3" stroke="white" y2="1.5" x2="26.0357" y1="1.5" x1="12"></line>
                        </svg>
                        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 33 39" class="bin-bottom">
                            <mask fill="white" id="path-1-inside-1_8_19">
                                <path d="M0 0H33V35C33 37.2091 31.2091 39 29 39H4C1.79086 39 0 37.2091 0 35V0Z"></path>
                            </mask>
                            <path mask="url(#path-1-inside-1_8_19)" fill="white"
                                d="M0 0H33H0ZM37 35C37 39.4183 33.4183 43 29 43H4C-0.418278 43 -4 39.4183 -4 35H4H29H37ZM4 43C-0.418278 43 -4 39.4183 -4 35V0H4V35V43ZM37 0V35C37 39.4183 33.4183 43 29 43V35V0H37Z">
                            </path>
                            <path stroke-width="4" stroke="white" d="M12 6L12 29"></path>
                            <path stroke-width="4" stroke="white" d="M21 6V29"></path>
                        </svg>
                        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 89 80" class="garbage">
                            <path fill="white"
                                d="M20.5 10.5L37.5 15.5L42.5 11.5L51.5 12.5L68.75 0L72 11.5L79.5 12.5H88.5L87 22L68.75 31.5L75.5066 25L86 26L87 35.5L77.5 48L70.5 49.5L80 50L77.5 71.5L63.5 58.5L53.5 68.5L65.5 70.5L45.5 73L35.5 79.5L28 67L16 63L12 51.5L0 48L16 25L22.5 17L20.5 10.5Z">
                            </path>
                        </svg>
                    </button>
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

.bin-button {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    width: 50px;
    height: 50px;
    border-radius: 50%;
    background-color: rgb(255, 95, 95);
    cursor: pointer;
    border: 2px solid rgb(255, 201, 201);
    transition-duration: 0.3s;
    position: relative;
    top: 20px;
    left: 20px;
    overflow: hidden;
}

.bin-bottom {
    width: 15px;
    z-index: 2;
}

.bin-top {
    width: 17px;
    transform-origin: right;
    transition-duration: 0.3s;
    z-index: 2;
}

.bin-button:hover .bin-top {
    transform: rotate(45deg);
}

.bin-button:hover {
    background-color: rgb(255, 0, 0);
}

.bin-button:active {
    transform: scale(0.9);
}

.garbage {
    position: absolute;
    width: 14px;
    height: auto;
    z-index: 1;
    opacity: 0;
    transition: all 0.3s;
}

.bin-button:hover .garbage {
    animation: throw 0.4s linear;
}

@keyframes throw {
    from {
        transform: translate(-400%, -700%);
        opacity: 0;
    }

    to {
        transform: translate(0%, 0%);
        opacity: 1;
    }
}
</style>