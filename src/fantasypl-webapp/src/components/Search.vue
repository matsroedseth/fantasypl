<template>
    <div class="search-container">
        <input type="number" v-model="leagueId" id="league-id" @input="onSearch(leagueId)"
            placeholder="Enter your leagueId">
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';

let debounceId = 0;
let leagueId = ref<number | null>(null)

const onSearch = (searchTerm: number | null): void => {
    if (searchTerm && searchTerm > 1000) {
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

<style scoped>
/* Chrome, Safari, Edge, Opera */
input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
    -webkit-appearance: none;
    margin: 0;
}

/* Firefox */
input[type=number] {
    -moz-appearance: textfield;
    appearance: textfield;
}

.search-container {
    padding: 6px;
    display: flex;
    align-items: center;
}

input[type=number] {
    padding: 6px;
    border-radius: 4px;
    margin-right: 10px;
    font-size: 16px;
}
</style>