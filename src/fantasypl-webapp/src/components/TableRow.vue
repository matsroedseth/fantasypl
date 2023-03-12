<template>
    <tr>
        <th scope="row">{{ currentRank }}</th>
        <td>{{ manager.teamName }}</td>
        <td>{{ manager.firstName }} {{ manager.lastName }}</td>
        <td>{{ live(livePoints) }}</td>
        <td>{{ manager.gameWeekPoints }}</td>
        <td>{{ manager.overallPoints }}</td>
        <td>{{ captain(team).lastName }}</td>
        <td>{{ activeChip != null ? activeChip : "none" }}</td>
    </tr>
</template>
  
<script setup lang="ts">
import { toRefs } from 'vue';
import LiveData from '../types/LiveData';
import Manager from '../types/Manager';
import Player from '../types/Player';
import { Chip } from '../types/Standing';
type Props = {
    currentRank: number,
    livePoints: LiveData[],
    manager: Manager,
    team: Player[],
    activeChip: Chip | null
}
const props = defineProps<Props>();

const { currentRank, livePoints, manager, team, activeChip } = toRefs(props)

const captain = (players: Player[]): Player => {
    return players.filter(p => p.isCaptain)[0];
}

const live = (liveData: LiveData[]): number | null => {
    return liveData.length ? liveData.filter(ld => ld.managerId == manager.value.id)[0].points : null;
}

</script>
  