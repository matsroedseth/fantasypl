<template>
    <h3 v-if="leagueInfo">League name: {{ leagueInfo.name }}</h3>
    <table v-if="leagueInfo" class="table table-striped">
        <thead>
            <tr>
                <th scope="col">Rank</th>
                <th scope="col">Team</th>
                <th scope="col">Manager</th>
                <th scope="col">
                    <div data-toggle="tooltip" data-placement="right" title="Gameweek points">GW</div>
                </th>
                <th scope="col">
                    <div data-toggle="tooltip" data-placement="right" title="Total points">Tot</div>
                </th>
            </tr>
        </thead>
        <tbody class="table-group-divider">
            <TableRow v-for="(standing) in standings" :key="standing.manager.id" :current-rank="standing.currentRank"
                :manager="standing.manager" />
        </tbody>
    </table>
</template>
  
<script setup lang="ts">
import LeagueInfo from '../types/LeagueInfo'
import Standing from '../types/Standing';
import TableRow from './TableRow.vue';

type Props = {
    leagueInfo: LeagueInfo | undefined,
    standings: Standing[] | undefined
}
defineProps<Props>();
</script>