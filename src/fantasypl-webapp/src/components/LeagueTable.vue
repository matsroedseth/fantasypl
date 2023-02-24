<template>
    <h3 v-if="leagueInfo">League name: {{ leagueInfo.name }}</h3>
    <table v-if="standings?.length" class="table table-hover">
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
            <TableRow v-for="(standing) in standings" :key="standing.managerInfo.id" :currentRank="standing.currentRank"
                :manager="standing.managerInfo" v-on:click="setActiveManager(standing.managerInfo.id)" />
        </tbody>
    </table>
</template>
  
<script setup lang="ts">
import { toRefs } from 'vue';
import LeagueInfo from '../types/LeagueInfo'
import Manager from '../types/Manager';
import Standing from '../types/Standing';
import TableRow from './TableRow.vue';

interface Props {
    leagueInfo: LeagueInfo | undefined,
    standings: Standing[] | undefined
}
const props = defineProps<Props>();
const { leagueInfo, standings } = toRefs(props)

const setActiveManager = (manager: number | null): void => {
    if (manager) {
        emit('activeManagerUpdate', manager);
    }
};

const emit = defineEmits<{
    (e: 'activeManagerUpdate', id: number): void
}>()

</script>