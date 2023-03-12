<template>
    <h3 v-if="leagueInfo">League name: {{ leagueInfo.name }}</h3>
    <table v-if="standings?.length" class="table table-hover">
        <thead>
            <tr>
                <th scope="col">Rank</th>
                <th scope="col">Team</th>
                <th scope="col">Manager</th>
                <th scope="col">
                    <div data-toggle="tooltip" data-placement="right" title="Live points">Live</div>
                </th>
                <th scope="col">
                    <div data-toggle="tooltip" data-placement="right" title="Gameweek points">GW</div>
                </th>
                <th scope="col">
                    <div data-toggle="tooltip" data-placement="right" title="Total points">Tot</div>
                </th>
                <th scope="col">Captain</th>
                <th scope="col">Chip</th>
            </tr>
        </thead>
        <tbody class="table-group-divider">
            <TableRow v-for="(standing) in standings" :key="standing.managerInfo.id" :currentRank="standing.currentRank"
                :manager="standing.managerInfo" :team="standing.players" :activeChip="standing.activeChip"
                v-on:click="setActiveManager(standing.managerInfo.id)" :livePoints="liveDataRef" />
        </tbody>
    </table>
</template>
  
<script setup lang="ts">
import { onBeforeUnmount, onMounted, ref, toRefs } from 'vue';
import FantasyApi from '../services/FantasyApi';
import LeagueInfo from '../types/LeagueInfo'
import LiveData from '../types/LiveData';
import Standing from '../types/Standing';
import TableRow from './TableRow.vue';

interface Props {
    leagueInfo: LeagueInfo | null,
    standings: Standing[]
}
const props = defineProps<Props>();
const { leagueInfo, standings } = toRefs(props)
const isFetchingLivePointsRef = ref(false);
const liveDataRef = ref<LiveData[]>([]);
let intervalId: number | null = null;

const setActiveManager = (manager: number | null): void => {
    if (manager) {
        emit('activeManagerUpdate', manager);
    }
};

const fetchLivePoints = (): void => {
    try {
        if (leagueInfo.value && standings.value) {
            FantasyApi.getLivePoints(leagueInfo.value.id)
                .then((response: LiveData[]) => {
                    liveDataRef.value = response;
                });
        }
    }
    catch (error) {
        console.error(error);
    }
}

onMounted(() => {
    fetchLivePoints();
    // set up periodic data fetching
    intervalId = setInterval(() => {
        fetchLivePoints();
    }, 10000); // fetch data every 5 minutes
});

onBeforeUnmount(() => {
    // clear the interval when the component is destroyed
    if (intervalId) {
        clearInterval(intervalId);
        intervalId = null;
    }
});

const emit = defineEmits<{
    (e: 'activeManagerUpdate', id: number): void
}>()
</script>