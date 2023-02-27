<template>
    <tr>
        <th scope="row">{{ player.firstName }} {{ player.lastName }}</th>
        <td>{{ player.isCaptain ? "X" : "" }}</td>
        <td>{{ player.isViceCaptain ? "X" : "" }}</td>
        <td>{{ player.multiplier > 0 ? "" : "Benched" }}</td>
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

</script>
  