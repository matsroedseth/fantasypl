<template>
    <div>
        <h3 v-if="leagueWithStandings.league">League name: {{ leagueWithStandings.league.name }}</h3>
    </div>
    <div class="container">
        <LeagueTable class="table-a" :leagueInfo="leagueWithStandings?.league" :standings="leagueWithStandings.standing"
            :currentGameWeek="currentGameWeek" />
        <div class="stats">
            <CaptaincyInfo class="element-a" :captaincyPicks="leagueWithStandings.captaincyPicks" />
            <CaptaincyInfo class="element-b" :captaincyPicks="leagueWithStandings.captaincyPicks" />
            <CaptaincyInfo class="element-c" :captaincyPicks="leagueWithStandings.captaincyPicks" />
        </div>

    </div>
</template>
  
<script setup lang="ts">
import { toRefs } from 'vue';
import LeagueTable from './LeagueTable.vue'
import { GameWeek } from '../types/GameWeek';
import CaptaincyInfo from './CaptaincyInfo.vue';
import LeagueWithStandings, { CaptaincyPick } from '../types/LeagueWithStandings';

interface Props {
    currentGameWeek: GameWeek | null
    leagueWithStandings: LeagueWithStandings
}
const props = defineProps<Props>();
const { currentGameWeek, leagueWithStandings } = toRefs(props)
</script>
  
<style>
.container {
    display: flex;
    flex-wrap: wrap;
    align-items: center;
    justify-content: space-between;

    max-width: 400px;
    margin: 0 auto;
    padding: 10px 0;
}

.table-a {
    flex-basis: 70%;
}

.stats {
    display: flex;
    flex-basis: 25%;
    flex-wrap: wrap;
}

.element-a,
.element-b,
.element-c {
    flex-basis: 100%;
    border: 2rem;
}

@media only screen and (max-width: 1200px) {
    .container {
        flex-direction: column;
    }

    .table-a {
        flex-basis: 100%;
    }

    .stats {
        flex-basis: 100%;
        justify-content: space-between;
    }

    .element-a,
    .element-b,
    .element-c {
        flex-basis: 25%;
        margin: 2rem;
    }
}
</style>