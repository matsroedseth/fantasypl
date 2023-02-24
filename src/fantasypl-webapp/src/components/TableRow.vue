<template>
    <tr>
        <th scope="row">{{ currentRank }}</th>
        <td>{{ manager.teamName }}</td>
        <td>{{ manager.firstName }} {{ manager.lastName }}</td>
        <td>{{ manager.gameWeekPoints }}</td>
        <td>{{ manager.overallPoints }}</td>
    </tr>
</template>
  
<script setup lang="ts">
import { toRefs } from 'vue';
import Manager from '../types/Manager';
type Props = {
    currentRank: number,
    manager: Manager
}
const props = defineProps<Props>();
const { manager } = toRefs(props)

</script>
  