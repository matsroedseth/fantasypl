<template>
    <!-- As a heading -->
    <header>
        <h1>FPL Enhanced</h1>
        <span v-if="nextGameweek">
            Next deadline (GW{{ nextGameweek.id }}): {{ formattedDeadline(nextGameweek.deadline) }}
        </span>
    </header>
</template>


<script setup lang="ts">
import { toRefs } from 'vue';
import { GameWeek } from '../types/GameWeek';
import moment from 'moment'

interface Props {
    nextGameweek: GameWeek | undefined
}

const props = defineProps<Props>()
const { nextGameweek } = toRefs(props)

const formattedDeadline = (deadline: Date) => {
    if (deadline) {
        console.log(deadline)
        return moment(String(deadline)).format('Do MMM H:mm')
    }
}

</script>

<style>
header {
    background-color: #333;
    color: #fff;
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 10px 20px;
}
</style>