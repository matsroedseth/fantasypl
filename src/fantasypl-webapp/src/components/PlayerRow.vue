<template>
    <tr>
        <th>{{ player.name }}</th>
        <th>{{ player.isCaptain ? "(C)" : player.isViceCaptain ?
            "(VC)" : "" }}</th>
    </tr>
</template>
  
<script setup lang="ts">
import { toRefs } from 'vue';
import Player from '../types/Player';
type Props = {
    player: Player
}
const props = defineProps<Props>();

const { player } = toRefs(props)

const isOnBench = (player: Player): boolean => {
    return player.multiplier > 0;
}
</script>
<style></style>