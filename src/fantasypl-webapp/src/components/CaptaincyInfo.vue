<template>
  <div v-if="captaincyPicks?.length" class="card p-3" style="width: 18rem;">
    <h4 class="card-title">
      Captaincy picks
    </h4>
    <table class="invisible-table">
      <tbody>
        <tr v-for="(captain) in captaincyPicks">
          <th>{{
            captain.pickedByPercentage }}% </th>
          <th>{{ captain.player.firstName }} {{ captain.player.lastName }}</th>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import { toRefs } from 'vue';
import { CaptaincyPick } from '../types/LeagueWithStandings';

interface Props {
  captaincyPicks: CaptaincyPick[]
}

const props = defineProps<Props>()
const { captaincyPicks } = toRefs(props)
</script>

<style>
.invisible-table {
  padding: 10px;
  border: none;
}
</style>