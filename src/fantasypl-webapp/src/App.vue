<template>
  <NavBar :nextGameweek="nextGameWeekRef" />
  <div class="container">
    <Fantasy />
  </div>
</template>

<script setup lang="ts">
import Fantasy from './components/Fantasy.vue';
import NavBar from './components/NavBar.vue';
import { onMounted, ref } from 'vue'
import FantasyApi from './services/FantasyApi';
import GameWeek from './types/GameWeek';



let nextGameWeekRef = ref<GameWeek>();

const fetchNextGameWeek = async (): Promise<void> => {
  try {
    await FantasyApi.getNextGameWeek()
      .then((response: GameWeek) => {
        nextGameWeekRef.value = response;
      });
  }
  catch (error) {
    console.error(error);
  }
}

onMounted(() => {
  fetchNextGameWeek()
})
</script>