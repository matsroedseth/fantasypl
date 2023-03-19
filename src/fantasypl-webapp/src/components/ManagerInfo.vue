<template>
  <div v-if="standing" class="modal fade show managerinfo" tabindex="-1" aria-labelledby="exampleModalLabel"
    aria-modal="true" role="dialog" style="display:block">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class=" modal-header">
          <h5 class=" modal-title">Team: {{ standing.managerInfo.teamName }}</h5>
        </div>
        <div class="modal-body">
          <table v-if="standing.managerInfo" class="row invisible-table">
            <h4>Manager info</h4>
            <tbody>
              <tr>
                <th>Manager:</th>
                <th>{{ standing.managerInfo.firstName }} {{ standing.managerInfo.lastName }}</th>
              </tr>
              <tr>
                <th>Mini league rank:</th>
                <th>{{ standing.currentRank }}</th>
              </tr>
              <tr>
                <th>Overall rank:</th>
                <th>{{ standing.managerInfo.overallRank }}</th>
              </tr>
              <tr v-if="standing.activeChip">
                <th>Active chip:</th>
                <th>{{ standing.activeChip }}</th>
              </tr>
              <tr v-if="!isWildCard(standing.activeChip)">
                <th>Transfers this GW:</th>
                <th>{{ standing.teamInfo.transfers }}</th>
              </tr>
              <tr v-if="!isWildCard(standing.activeChip)">
                <th>Transfer cost this GW:</th>
                <th>{{ standing.teamInfo.transferCost }}</th>
              </tr>
            </tbody>
          </table>
          <ManagerPicks v-if="standing.players?.length" :players="standing.players" />
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
import { Chip } from '../types/Enums';
import Standing from '../types/Standing';
import ManagerPicks from './ManagerPicks.vue';

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

<style>
.modal-header {
  background-color: #166d37c3;
  color: #fff;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px 20px;
}

.modal-footer {
  color: #fff;
  display: flex;
  align-items: end;
  padding: 10px 20px;
}
</style>