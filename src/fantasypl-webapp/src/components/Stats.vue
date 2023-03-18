<template>
    <div class="card-deck">
        <CaptaincyInfo class="card" :captaincyPicks="captaincyPicks" />
        <CaptaincyInfo class="card" :captaincyPicks="captaincyPicks" />
        <CaptaincyInfo class="card" :captaincyPicks="captaincyPicks" />
    </div>
</template>
  
<script setup lang="ts">
import { toRefs } from 'vue';
import { CaptaincyPick } from '../types/LeagueWithStandings';
import CaptaincyInfo from './CaptaincyInfo.vue';

interface Props {
    captaincyPicks: CaptaincyPick[]
}

const props = defineProps<Props>()
const { captaincyPicks } = toRefs(props)
</script>
  
<style></style>