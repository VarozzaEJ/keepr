<script setup>
import { AppState } from '@/AppState.js';
import { Keep } from '@/models/Keep.js';
import { keepsService } from '@/services/KeepsService.js';
import Pop from '@/utils/Pop.js';
import { computed } from 'vue';


const props = defineProps({ keepProp: { type: Keep, required: true } })
const account = computed(() => AppState.account)

async function setActiveKeep(keepId) {
    try {
        keepsService.setActiveKeep(keepId)
    }
    catch (error) {
        Pop.error(error);
    }
}

async function deleteKeep(keepId) {
    try {
        const wantsToDelete = await Pop.confirm("Are you sure?")
        if (!wantsToDelete) return
        keepsService.deleteKeep(keepId)
        Pop.success("Deleted Keep")
    }
    catch (error) {
        Pop.error(error);
    }
}
</script>


<template>
    <main class="container-fluid my-md-5 my-2 keep-bg-img dynamic-width text-dark">
        <div class="d-flex justify-content-end"><i @click="deleteKeep(keepProp.id)"
                v-if="account?.id == keepProp.creatorId" role="button" class="mdi mdi-close-circle text-danger"></i>
        </div>
        <div class="row " @click="setActiveKeep(keepProp.id)" data-bs-toggle="modal" data-bs-target="#keepModal">
            <div class="col-12">
                <img class="img-fluid rounded" :src="keepProp.img" :alt="`A Keep made by ${keepProp.creator.name}`">
            </div>
            <div class="d-flex relative desktop-text mobile-text justify-content-between">
                <span class=" ms-2 text-light text-shadow">{{ keepProp.name }}</span>
                <img class="creator-picture me-1" :src="keepProp.creator.picture" :alt="keepProp.creator.name">
            </div>
        </div>
    </main>
</template>


<style lang="scss" scoped>
.keep-bg-img {
    aspect-ratio: 1/1;
    width: 100%
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