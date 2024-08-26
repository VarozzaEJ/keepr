<script setup>
import { AppState } from '@/AppState.js';
import { Vault } from '@/models/Vault.js';
import { computed } from 'vue';

const props = defineProps({ vaultProp: { type: Vault, required: true } })
const account = computed(() => AppState.account)
</script>


<template>
    <main v-if="account.id != vaultProp.creatorId && !vaultProp.isPrivate" role="button"
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