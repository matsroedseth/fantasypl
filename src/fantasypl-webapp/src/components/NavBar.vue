<template>
    <header>
        <h1>FPL Enhanced</h1>
        <span style="display: flex; align-items: center;" v-if="nextGameweek">
            <Search @search-update="onSearch" />
            Next deadline (GW{{ nextGameweek.id }}): {{ formattedDeadline(nextGameweek.deadline) }}
        </span>
    </header>
</template>


<script setup lang="ts">
import { toRefs } from 'vue';
import { GameWeek } from '../types/GameWeek';
import moment from 'moment'
import Search from './Search.vue';

interface Props {
    nextGameweek: GameWeek | undefined
}

const props = defineProps<Props>()
const { nextGameweek } = toRefs(props)

const formattedDeadline = (deadline: Date) => {
    if (deadline) {
        return moment(String(deadline)).format('Do MMM H:mm')
    }
}
const onSearch = (searchTerm: number): void => {
    emit('searchUpdate', searchTerm);
};

const emit = defineEmits<{
    (e: 'searchUpdate', id: number): void
}>()

</script>

<style>
header {
    background-color: #166d38;
    color: #fff;
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 10px 20px;
}
</style>