<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/globals/KeepCard.vue';
import { keepsService } from '@/services/KeepsService.js';
import Pop from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';


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
  </div>
</template>

<style scoped lang="scss">
@media screen and (min-width: 769px) {
  .masonry {
    columns: 228px;
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
    columns: 150px;
    column-gap: 1em;
    column-fill: balance;
    break-inside: avoid;
  }
}
</style>
