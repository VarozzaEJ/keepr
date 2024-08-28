<script setup>
import { AppState } from '@/AppState.js';
import { Keep } from '@/models/Keep.js';
import { accountService } from '@/services/AccountService.js';
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

async function setActiveProfile(creatorId) {
    try {
        accountService.setActiveProfile(creatorId)
    }
    catch (error) {
        Pop.error(error);
    }
}

function setAltImg(event) {
    event.target.src = "https://images.unsplash.com/photo-1663465376645-aca0b7c8227a?q=80&w=3560&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
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
    <main class="container-fluid my-md-1 my-2 keep-bg-img dynamic-width text-dark">
        <div class="d-flex justify-content-end"><i @click="deleteKeep(keepProp.id)"
                v-if="account?.id == keepProp.creatorId" role="button" class="mdi mdi-close-circle text-danger"></i>
        </div>
        <div class="row ">
            <div class="col-12">
                <img @error="setAltImg" role="button" @click="setActiveKeep(keepProp.id)" data-bs-toggle="modal"
                    data-bs-target="#keepModal" class="img-fluid rounded img" :src="keepProp.img" alt="">
            </div>
            <div class="d-flex relative desktop-text mobile-text justify-content-between">
                <span class=" ms-2 text-light text-shadow">{{ keepProp.name }}</span>
                <router-link :to="{ name: 'Profile', params: { profileId: keepProp.creatorId } }">
                    <img title="Navigate to Profile Page" @click="setActiveProfile(keepProp.creatorId)" role='button'
                        class="creator-picture me-1" :src="keepProp.creator.picture" :alt="keepProp.creator.name">
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