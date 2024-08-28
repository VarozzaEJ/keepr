<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/globals/KeepCard.vue';
import { keepsService } from '@/services/KeepsService.js';
import Pop from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { waveform } from 'ldrs'

waveform.register()

const keeps = computed(() => AppState.keeps)
const account = computed(() => AppState.account)


onMounted(() => {
  getAllKeeps()
})



async function getAllKeeps() {
  try {
    await keepsService.getAllKeeps()
  }
  catch (error) {
    Pop.error(error);
  }
}
</script>

<template>
  <div class="container-fluid full-vh">
    <div class="row mt-3 full-vh">
      <div class="col-md-12 mobile-masonry  masonry">
        <div class="break-inside" v-for="keep in keeps" :key="keep.id">
          <KeepCard :keepProp='keep' />
        </div>
      </div>
    </div>
    <div class="d-flex justify-content-center align-items-center mt-4"><span class="large-text">Loading<l-waveform
          class="ms-5" l-waveform size="83" stroke="3.5" speed="1" color="black">
        </l-waveform></span>
    </div>
  </div>
</template>

<style scoped lang="scss">
.large-text {
  font-size: 70px;
}

@media screen and (min-width: 769px) {
  .masonry {
    columns: 4;
    column-gap: 1em;
    column-fill: balance;
  }
}

.full-vh {}

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
