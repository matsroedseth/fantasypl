<template>
  <NavBar @search-update="fetchLeagueInfoWithStandings" :nextGameweek="gameWeekDataRef?.next" />
  <div class="container">
    <div v-if="isSearching" class="d-flex justify-content-center m-5">
      <div class="spinner-border ml-auto" role="status" aria-hidden="true"></div>
    </div>
    <div v-else>
      <Fantasy v-if="gameWeekDataRef?.current && leagueWithStandingsRef" :currentGameWeek="gameWeekDataRef.current"
        :leagueWithStandings="leagueWithStandingsRef" />
      <div v-else>Search for a league using the input field.</div>
    </div>
  </div>
</template>

<script setup lang="ts">
import Fantasy from './components/Fantasy.vue';
import NavBar from './components/NavBar.vue';
import { onMounted, ref } from 'vue'
import FantasyApi from './services/FantasyApi';
import { GameWeekData } from './types/GameWeek';
import LeagueWithStandings from './types/LeagueWithStandings';



const gameWeekDataRef = ref<GameWeekData>();
const leagueWithStandingsRef = ref<LeagueWithStandings | null>(null);
const isSearching = ref(false);

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

const fetchLeagueInfoWithStandings = async (leagueId: number): Promise<void> => {
  isSearching.value = true;
  try {
    await FantasyApi.getLeagueInfoWithStandings(leagueId)
      .then((response: LeagueWithStandings) => {
        if (leagueWithStandingsRef) {
          leagueWithStandingsRef.value = response
        }
        isSearching.value = false;
      });
  }
  catch (error) {
    isSearching.value = false;
    console.error(error);
  }
}

onMounted(() => {
  fetchGameWeekData()
})
</script>