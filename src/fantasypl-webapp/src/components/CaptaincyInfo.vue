<template>
  <div class="container">
    <div class="row">
      <div v-if="captaincyPicks?.length" class="card" style="width: 18rem;">
        <table class="table table-bordered table-sm">
          <thead>
            Captains
          </thead>
          <tbody class="table-group-divider">
            <tr v-for="(captain) in captaincyPicks">
              <th scope="row">{{
                captain.pickedByPercentage }}% - {{ captain.player.firstName }} {{ captain.player.lastName }}</th>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
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