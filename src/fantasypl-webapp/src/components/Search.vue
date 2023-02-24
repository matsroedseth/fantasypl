<template>
    <div>
        <input type="number" v-model="leagueId" id="league-id" @input="onSearch(leagueId)"
            placeholder="Enter your leagueId">
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';

let debounceId = 0;
let leagueId = ref<number>(0)

const onSearch = (searchTerm: number): void => {
    if (searchTerm > 1000) {
        clearTimeout(debounceId);
        debounceId = setTimeout(() => {
            emit('searchUpdate', searchTerm);
        }, 500);
    }
};

const emit = defineEmits<{
    (e: 'searchUpdate', id: number): void
}>()
</script>