<template>
    <Search @search-update="fetchLeagueInfoWithStandings" />
    <div v-if="isSearching" class="d-flex justify-content-center m-5">
        <div class="spinner-border ml-auto" role="status" aria-hidden="true"></div>
    </div>
    <LeagueTable v-else :leagueInfo="leagueInfoRef" :standings="standingsRef" @active-manager-update="setActiveManager" />
    <ManagerInfo v-if="activeStandingRef" :standing="activeStandingRef" @reset-active-manager="resetActiveManager" />
</template>
  
<script setup lang="ts">
import { ref } from 'vue';
import FantasyApi from '../services/FantasyApi';
import LeagueInfo from '../types/LeagueInfo';
import ResponseData from '../types/ResponseData';
import Standing from '../types/Standing';
import LeagueTable from './LeagueTable.vue'
import Search from './Search.vue';
import ManagerInfo from './ManagerInfo.vue';
import LeagueWithStandings from '../types/LeagueWithStandings';

const leagueInfoRef = ref<LeagueInfo | null>(null);
const standingsRef = ref<Standing[]>([]);
let isSearching = ref(false);
let activeStandingRef = ref<Standing | null>(null);

const fetchLeagueInfoWithStandings = (leagueId: number): void => {
    isSearching.value = true;
    try {
        FantasyApi.getLeagueInfoWithStandings(leagueId)
            .then((response: LeagueWithStandings) => {
                leagueInfoRef.value = response.league;
                standingsRef.value = response.standing;
                isSearching.value = false;
            });
    }
    catch (error) {
        isSearching.value = false;
        console.error(error);
    }
}

const setActiveManager = (managerId: number): void => {
    activeStandingRef.value = standingsRef.value.filter(item => item.managerInfo.id === managerId)[0];
}

const resetActiveManager = (): void => {
    activeStandingRef.value = null;
}
</script>
  