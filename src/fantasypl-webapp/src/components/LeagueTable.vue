<template>
    <div>
        <h3 v-if="leagueInfo">League name: {{ leagueInfo.name }}</h3>
        <table v-if="standings?.length" class="table table-hover">
            <thead>
                <tr>
                    <th scope="col">Rank</th>
                    <th scope="col">Team</th>
                    <th scope="col">Manager</th>
                    <th scope="col">
                        <span>
                            <div data-toggle="tooltip" data-placement="right" title="Live points">Live
                                <div v-if="!currentGameWeek?.finished" class="live-dot" />
                            </div>
                        </span>
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
        <ManagerInfo v-if="activeStandingRef" :standing="activeStandingRef" @reset-active-manager="resetActiveManager" />
    </div>
</template>
  
<script setup lang="ts">
import { onBeforeUnmount, onMounted, ref, toRefs } from 'vue';
import FantasyApi from '../services/FantasyApi';
import { GameWeek } from '../types/GameWeek';
import LeagueInfo from '../types/LeagueInfo'
import LiveData from '../types/LiveData';
import Standing from '../types/Standing';
import TableRow from './TableRow.vue';
import ManagerInfo from './ManagerInfo.vue';

interface Props {
    leagueInfo: LeagueInfo | null,
    standings: Standing[],
    currentGameWeek: GameWeek | null
}
const props = defineProps<Props>();
const { leagueInfo, standings } = toRefs(props)
const liveDataRef = ref<LiveData[]>([]);
const activeStandingRef = ref<Standing | null>(null);
let intervalId: number | null = null;

const setActiveManager = (managerId: number | null): void => {
    if (managerId) {
        activeStandingRef.value = standings.value.filter(item => item.managerInfo.id === managerId)[0];
    }
};

const fetchLivePoints = async (): Promise<void> => {
    try {
        if (leagueInfo.value && standings.value) {
            await FantasyApi.getLivePoints(leagueInfo.value.id)
                .then((response: LiveData[]) => {
                    liveDataRef.value = response;
                });
        }
    }
    catch (error) {
        console.error(error);
    }
}

const resetActiveManager = (): void => {
    activeStandingRef.value = null;
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
</script>

<style>
.live-dot {
    height: .5rem;
    position: relative;
    width: .5rem;

    border-radius: 100%;
    background-color: royalBlue;
}

.live-dot::after {
    content: '';
    display: block;
    height: 100%;
    position: absolute;
    width: 100%;

    border-radius: 100%;

    background-color: inherit;

    animation: 4s ease-out signal infinite;
    opacity: .5;
}

@keyframes signal {

    20%,
    100% {
        opacity: 0;
        transform: scale(4);
    }
}
</style>