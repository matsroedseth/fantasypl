<template>
    <Search @search-update="fetchLeagueInfoWithStandings" />
    <div v-if="isSearching" class="d-flex justify-content-center m-5">
        <div class="spinner-border ml-auto" role="status" aria-hidden="true"></div>
    </div>
    <LeagueTable v-else :leagueInfo="leagueInfoRef" :standings="standingsRef" :currentGameWeek="currentGameWeek" />
    <CaptaincyInfo v-if="captaincyPicksRef" :captaincyPicks="captaincyPicksRef" />
</template>
  
<script setup lang="ts">
import { ref, toRefs } from 'vue';
import FantasyApi from '../services/FantasyApi';
import LeagueInfo from '../types/LeagueInfo';
import Standing from '../types/Standing';
import LeagueTable from './LeagueTable.vue'
import Search from './Search.vue';
import LeagueWithStandings, { CaptaincyPick } from '../types/LeagueWithStandings';
import { GameWeek } from '../types/GameWeek';
import CaptaincyInfo from './CaptaincyInfo.vue';

const leagueInfoRef = ref<LeagueInfo | null>(null);
const standingsRef = ref<Standing[]>([]);
const captaincyPicksRef = ref<CaptaincyPick[]>([]);
let isSearching = ref(false);

interface Props {
    currentGameWeek: GameWeek | null
}
const props = defineProps<Props>();
const { currentGameWeek } = toRefs(props)

const fetchLeagueInfoWithStandings = async (leagueId: number): Promise<void> => {
    isSearching.value = true;
    try {
        await FantasyApi.getLeagueInfoWithStandings(leagueId)
            .then((response: LeagueWithStandings) => {
                leagueInfoRef.value = response.league;
                standingsRef.value = response.standing;
                captaincyPicksRef.value = response.captaincyPicks;
                isSearching.value = false;
            });
    }
    catch (error) {
        isSearching.value = false;
        console.error(error);
    }
}
</script>
  