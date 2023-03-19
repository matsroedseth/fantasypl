<template>
    <table v-if="players?.length" class="row invisible-table">
        <h4>Team</h4>
        <tbody class="">
            <PlayerRow v-for="(player) in isPlayingGoalkeeper(players)" :key="player.id" :player="player" />
            <PlayerRow v-for="(player) in isPlayingDefender(players)" :key="player.id" :player="player" />
            <PlayerRow v-for="(player) in isPlayingMidfielder(players)" :key="player.id" :player="player" />
            <PlayerRow v-for="(player) in isPlayingAttacker(players)" :key="player.id" :player="player" />
            ---
            <PlayerRow v-for="(player) in isOnBench(players)" :key="player.id" :player="player" />
        </tbody>
    </table>
</template>
  
<script setup lang="ts">
import { toRefs } from 'vue';
import { Position } from '../types/Enums';
import Player from '../types/Player';
import PlayerRow from './PlayerRow.vue';
type Props = {
    players: Player[]
}
const props = defineProps<Props>();

const { players } = toRefs(props)

const isOnBench = (players: Player[]): Player[] => {
    return players.filter(p => p.position > 11);
}

const isPlayingGoalkeeper = (players: Player[]): Player[] => {
    return players.filter(p => p.elementType == Position.Goalkeeper && p.position <= 11);
}
const isPlayingDefender = (players: Player[]): Player[] => {
    return players.filter(p => p.elementType == Position.Defender && p.position <= 11);
}
const isPlayingMidfielder = (players: Player[]): Player[] => {
    return players.filter(p => p.elementType == Position.Midfielder && p.position <= 11);
}
const isPlayingAttacker = (players: Player[]): Player[] => {
    return players.filter(p => p.elementType == Position.Attacker && p.position <= 11);
}

</script>
<style>
.invisible-table {
    padding: 10px;
    border: none;
}

th {
    font-weight: normal;
}
</style>