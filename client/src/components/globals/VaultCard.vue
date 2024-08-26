<script setup>
import { AppState } from '@/AppState.js';
import { Vault } from '@/models/Vault.js';
import { vaultKeepService } from '@/services/VaultKeepService.js';
import Pop from '@/utils/Pop.js';
import { computed } from 'vue';

const props = defineProps({ vaultProp: { type: Vault, required: true } })
const account = computed(() => AppState.account)
const vaultKeeps = computed(() => AppState.vaultKeeps)


async function getKeepsForPublicVault(vaultId) {
    try {
        vaultKeepService.getKeepsForPublicVault(vaultId)
    }
    catch (error) {
        Pop.error(error);
    }
}

async function setActiveVault(vaultId) {
    try {
        vaultsS
    }
    catch (error) {
        Pop.error(error);
    }
}
</script>


<template>
    <main @click="setActiveVault(vaultProp) && getKeepsForPublicVault(vaultProp.id)" role="button"
        class="container-fluid vault-bg-img mb-3">
        <div class="row">
            <div class="col-12 absolute d-flex justify-content-between">
                <span class="fs-5 text-light text-shadow">{{ vaultProp.name }}</span>
                <span v-if="vaultProp.isPrivate" class="fs-5 text-light text-shadow"><i class="mdi mdi-lock"></i></span>
            </div>
        </div>
    </main>
</template>


<style lang="scss" scoped>
.vault-bg-img {
    background-image: v-bind('$props.vaultProp.vaultBackgroundImage');
    height: 250px;
    border-radius: 2%;
    background-position: center;
    object-fit: contain;
}

.absolute {
    position: relative;
    top: 220px;
}

.text-shadow {
    text-shadow: 1px 1px black;
}
</style>