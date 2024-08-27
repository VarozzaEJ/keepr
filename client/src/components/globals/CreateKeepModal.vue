<script setup>
import { keepsService } from '@/services/KeepsService.js';
import Pop from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { ref } from 'vue';


const editableKeepData = ref({
    name: '',
    img: '',
    description: ''
})


async function createKeep() {
    try {
        keepsService.createKeep(editableKeepData.value)
        Modal.getOrCreateInstance("#createKeepModal").hide()
        // editableKeepData.value = {
        //     title: '',
        //     img: '',
        //     description: ''
        // }
        Modal.getOrCreateInstance("#keepModal").show()
    }
    catch (error) {
        Pop.error(error);
    }
}
</script>


<template>


    <div class="modal  fade" id="createKeepModal" tabindex="-1" aria-labelledby="createKeepModalLabel"
        aria-hidden="true">
        <div class="modal-dialog ">
            <div class="modal-content no-round">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="createKeepModalLabel">Add Your Keep</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="createKeep()">
                        <div class="mb-3">
                            <input required v-model="editableKeepData.name" type="text" class="form-control"
                                placeholder="Title">
                        </div>
                        <div class="mb-3">
                            <input required v-model="editableKeepData.img" type="url" class="form-control"
                                placeholder="Image URL">
                        </div>
                        <div class="mb-5">
                            <textarea required v-model="editableKeepData.description" rows="5" class="form-control"
                                placeholder="Keep Description"></textarea>
                        </div>
                        <div class="d-flex justify-content-end">
                            <button class="btn btn-outline-dark text-end">Create</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.no-round {
    border-radius: 1%;
}

input[type=text] {
    border: none;
    border-bottom: 1px solid rgb(73, 73, 73);
    outline: none;
    border-radius: 0;
}

input[type=url] {
    border: none;
    border-bottom: 1px solid rgb(73, 73, 73);
    outline: none;
    border-radius: 0;
}

textarea {
    border: none;
    border-bottom: 1px solid rgb(73, 73, 73);
    outline: none;
    border-radius: 0;
}

input {
    outline: none;
    outline-width: 0;
}
</style>