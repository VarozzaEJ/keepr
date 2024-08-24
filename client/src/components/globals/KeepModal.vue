<script setup>
import { AppState } from '@/AppState.js';
import { computed } from 'vue';


const keep = computed(() => AppState.activeKeep)

</script>


<template>
    <div class="modal modal-lg fade" id="keepModal" tabindex="-1" aria-labelledby="exampleModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content" v-if="keep">
                <!-- <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Modal title</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div> -->
                <div class="modal-body p-0">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-6 p-0">
                                <img class="img img-fluid mobile-height" :src="keep.img" alt="">
                            </div>
                            <div class="col-md-6 d-flex mobile-height">
                                <div class="container d-flex flex-column justify-content-between">
                                    <div class="row d-flex justify-content-center">
                                        <div class="col-4 mt-4 d-flex justify-content-between">
                                            <span class="fs-4"><i class="mdi mdi-eye me-2"></i>0</span>
                                            <span class="fs-4"><i class="mdi mdi-alpha-k-box-outline me-2"></i>0</span>
                                        </div>
                                    </div>
                                    <div class="row d-flex justify-content-center">
                                        <div class="col-md-8 d-flex justify-content-center">
                                            <span class="fw-bold fs-2">{{ keep.name }}</span>
                                        </div>
                                        <div class="col-12 d-flex justify-content-center">
                                            <span class="fs-6 text-center">{{ keep.description }}</span>
                                        </div>
                                    </div>
                                    <div class="row d-flex justify-content-md-around justify-content-between">
                                        <div class="col-4 mb-4 d-flex align-items-center justify-content-between">
                                            <span class="fs-5 d-flex border-bottom me-1 selectable">Plants <i
                                                    class="mdi mdi-menu-down"></i></span>
                                            <button class="btn btn-success rounded text-light">save</button>
                                        </div>
                                        <div
                                            class="col-md-6 col-4 mb-4 me-md-0 me-2 ps-md-5 ps-0 d-flex align-items-center">
                                            <img class="img-fluid creator-picture " :src="keep.creator.picture"
                                                :alt="keep.creator.name">
                                            <span class=" ms-2 fs-5">{{ keep.creator.name
                                                }}</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.img {
    border-top-left-radius: 1%;
    border-bottom-left-radius: 1%;
    border-top-right-radius: 1%;
    border-bottom-right-radius: 1%;
    max-height: 100%;
    max-width: 100%;
    height: 600px;
    aspect-ratio: 1/1;
    object-fit: cover;
    object-position: center;
}

.creator-picture {
    aspect-ratio: 1/1;
    height: 50px;
    width: 50px;
    border-radius: 50%;
    object-position: center;
    object-fit: cover;
}

@media screen and (max-width: 768px) {
    .mobile-height {
        height: 450px;
        width: 100%;
    }
}
</style>