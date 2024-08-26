<script setup>
import { AppState } from '@/AppState.js';
import { computed } from 'vue';

const vault = computed(() => AppState.activeVault)
</script>


<template>
    <div class="modal fade" id="vaultModal" tabindex="-1" aria-labelledby="vaultModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div v-if="vault" class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="vaultModalLabel">Modal title</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <img :src="vault.img" :alt="`The cover image for ${vault.name}`" class="img-fluid">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                </div>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped></style>