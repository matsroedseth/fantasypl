<template>
    <Search @search-update="fetchLeagueInfoWithStandings" />
    <LeagueTable :league-info="leagueInfo" :standings="standings" />
</template>
  
<script setup lang="ts">
import { ref } from 'vue';
import FantasyApi from '../services/FantasyApi';
import LeagueInfo from '../types/LeagueInfo';
import Manager from '../types/Manager';
import ResponseData from '../types/ResponseData';
import Standing from '../types/Standing';
import LeagueTable from './LeagueTable.vue'
import Search from './Search.vue';

let leagueInfo = ref<LeagueInfo>();
let standings = ref<Standing[]>();
let manager = ref<Manager>();

const fetchLeagueInfoWithStandings = (leagueId: number): void => {
    try {
        FantasyApi.getLeagueInfoWithStandings(leagueId)
            .then((response: ResponseData) => {
                console.log(response.data);
                leagueInfo = response.data.league;
                standings = response.data.standing;
            });
    }
    catch (error) {
        console.error(error);
    }
}

const fetchManagerInfo = (managerId: number): void => {
    try {
        FantasyApi.getManager(managerId)
            .then((response: ResponseData) => {
                console.log(response.data);
                manager = response.data;
            });
    }
    catch (error) {
        console.error(error);
    }
}
</script>
  