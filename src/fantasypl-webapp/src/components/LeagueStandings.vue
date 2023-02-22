<template>
    <div>
        <label for="league-id">League ID:</label>
        <input type="number" id="league-id" v-model="leagueId" @keydown.enter="fetchData" placeholder="e.g. 1232133">
        <button @click="fetchData">Fetch Data</button>
    </div>

    <h3 v-if="leagueInfo">League name: {{ leagueInfo.name }}</h3>
    <ul class="list-group">
        <li class="list-group-item" v-for="(standing, index) in standings">
            {{ standing.currentRank }} - {{ standing.manager.teamName }} - {{ standing.manager.gameWeekPoints }}
        </li>
    </ul>
</template>
  
<script lang="ts">
import { defineComponent } from 'vue';
import FantasyApi from '../services/FantasyApi';
import LeagueInfo from '../types/LeagueInfo'
import ResponseData from '../types/ResponseData';
import Standing from '../types/Standing';

export default defineComponent({
    data() {
        return {
            leagueId: {} as number,
            leagueInfo: null as LeagueInfo | null,
            standings: [] as Standing[]
        };
    },
    methods: {
        async fetchData() {
            try {
                FantasyApi.getLeague(this.leagueId)
                    .then((response: ResponseData) => {
                        console.log(response.data);
                        this.leagueInfo = response.data.league
                        this.standings = response.data.standing
                    })
            } catch (error) {
                console.error(error);
            }
        },
    },
});
</script>
  
<style scoped></style>
  