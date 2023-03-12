<template>
    <tr>
        <th scope="row">{{ player.firstName }} {{ player.lastName }} {{ player.isCaptain ? "(C)" : player.isViceCaptain ?
            "(VC)" : "" }} {{ player.multiplier > 0 ? "" : "(Benched)" }}</th>
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
  