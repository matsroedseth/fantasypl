<template>
  <div v-if="standing" class="modal fade show" tabindex="-1" aria-labelledby="exampleModalLabel" aria-modal="true"
    role="dialog" style="display:block">
    <div class="modal-dialog modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLongTitle">Team: {{ standing.managerInfo.teamName }}</h5>
        </div>
        <div class="modal-body">
          <div class="row">
            <div v-if="standing.managerInfo" class="card col" style="width: 18rem;">
              <div class="card-body">
                <p>Manager: {{ standing.managerInfo.firstName }} {{ standing.managerInfo.lastName }}</p>
                <p>Mini league rank: {{ standing.currentRank }}</p>
                <p>Overall rank: {{ standing.managerInfo.overallRank }}</p>
                <p v-if="standing.activeChip">Active chip: {{ standing.activeChip }}</p>
                <div v-if="!isWildCard(standing.activeChip)">
                  <p v-if="standing.teamInfo.transfers > 0">Transfers this GW: {{ standing.teamInfo.transfers }}</p>
                  <p v-if="standing.teamInfo.transfers > 0">Transfer cost this GW: {{ standing.teamInfo.transferCost }}
                  </p>
                </div>
              </div>
            </div>
            <table v-if="standing.players?.length" class="col table table-bordered table-sm">
              <thead>
                <tr>
                  <th scope="col">Name</th>
                </tr>
              </thead>
              <tbody class="table-group-divider">
                <PlayerRow v-for="(player) in standing.players" :key="player.id" :player="player"
                  :manager="standing.managerInfo" />
              </tbody>
            </table>
          </div>
        </div>
        <div class="modal-footer">
          <a v-on:click="closeCard" class="btn btn-secondary" data-dismiss="modal">Close</a>
        </div>
      </div>
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