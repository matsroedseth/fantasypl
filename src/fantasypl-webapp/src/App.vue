<template>
  <NavBar :nextGameweek="gameWeekDataRef?.next" />
  <div class="container">
    <Fantasy v-if="gameWeekDataRef?.current" :currentGameWeek="gameWeekDataRef.current" />
  </div>
</template>

<script setup lang="ts">
import Fantasy from './components/Fantasy.vue';
import NavBar from './components/NavBar.vue';
import { onMounted, ref } from 'vue'
import FantasyApi from './services/FantasyApi';
import { GameWeekData } from './types/GameWeek';



let gameWeekDataRef = ref<GameWeekData>();

const fetchGameWeekData = async (): Promise<void> => {
  try {
    await FantasyApi.getGameWeekData()
      .then((response: GameWeekData) => {
        gameWeekDataRef.value = response;
      });
  }
  catch (error) {
    console.error(error);
  }
}

onMounted(() => {
  fetchGameWeekData()
})
</script>