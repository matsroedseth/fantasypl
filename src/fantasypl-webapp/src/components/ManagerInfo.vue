<template>
  <div class="container">
    <div class="row">
      <div v-if="standing.managerInfo" class="card col-sm-1" style="width: 18rem;">
        <div class="card-body">
          <h5 class="card-title">Team: {{ standing.managerInfo.teamName }}</h5>
          <p>Manager: {{ standing.managerInfo.firstName }} {{ standing.managerInfo.lastName }}</p>
          <p>Mini league rank: {{ standing.currentRank }}</p>
          <p>Overall rank: {{ standing.managerInfo.overallRank }}</p>
          <p v-if="standing.activeChip">Active chip: {{ standing.activeChip }}</p>
          <div v-if="!isWildCard(standing.activeChip)">
            <p v-if="standing.teamInfo.transfers > 0">Transfers this GW: {{ standing.teamInfo.transfers }}</p>
            <p v-if="standing.teamInfo.transfers > 0">Transfer cost this GW: {{ standing.teamInfo.transferCost }}</p>
          </div>
          <a v-on:click="closeCard" class="btn btn-warning">Close</a>
        </div>
      </div>

      <table v-if="standing.players?.length" class="col table table-bordered table-sm">
        <thead>
          <tr>
            <th scope="col">Name</th>
            <th scope="col">
              <div data-toggle="tooltip" data-placement="right" title="Captain">(C)</div>
            </th>
            <th scope="col">
              <div data-toggle="tooltip" data-placement="right" title="Vice captain">(VC)</div>
            </th>
            <th scope="col">Role</th>
          </tr>
        </thead>
        <tbody class="table-group-divider">
          <PlayerRow v-for="(player) in standing.players" :key="player.id" :player="player"
            :manager="standing.managerInfo" />
        </tbody>
      </table>
    </div>
  </div>
</template>

<script setup lang="ts">
import { toRefs } from 'vue';
import Standing, { Chip } from '../types/Standing';
import PlayerRow from './PlayerRow.vue';

interface Props {
  standing: Standing
}

const props = defineProps<Props>()
const { standing } = toRefs(props)

const isWildCard = (activeChip: Chip | null): boolean => {
  return activeChip === Chip.Wildcard
}

const closeCard = () => {
  emit('resetActiveManager')
}

const emit = defineEmits<{
  (e: 'resetActiveManager'): void
}>()
</script>