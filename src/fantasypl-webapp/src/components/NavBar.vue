<template>
    <!-- As a heading -->
    <nav class="navbar navbar-light bg-light justify-content-between">
        <span class=" navbar-brand mb-0 h1">FPL Enhanced</span>
        <span v-if="nextGameweek">
            Deadline GW{{ nextGameweek.id }}: {{ formattedDeadline(nextGameweek.deadline) }}
        </span>
    </nav>
</template>


<script setup lang="ts">
import { toRefs } from 'vue';
import GameWeek from '../types/GameWeek';
import moment from 'moment'

interface Props {
    nextGameweek: GameWeek | undefined
}

const props = defineProps<Props>()
const { nextGameweek } = toRefs(props)

const formattedDeadline = (deadline: Date) => {
    if (deadline) {
        return moment(String(deadline)).format('Do MMM hh:mm')
    }
}

</script>