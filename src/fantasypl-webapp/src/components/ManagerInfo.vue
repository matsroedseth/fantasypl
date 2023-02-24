<template>
  <div v-if="standing.managerInfo" class="card" style="width: 18rem;">
    <div class="card-body">
      <h5 class="card-title">Manager Details</h5>
      <p>Name: {{ standing.managerInfo.firstName }} {{ standing.managerInfo.lastName }}</p>
      <p>Team Name: {{ standing.managerInfo.teamName }}</p>
      <p v-if="standing.activeChip">Active chip: {{ standing.activeChip }}</p>
      <p>Transfers this GW: {{ standing.teamInfo.transfers }}</p>
      <a v-on:click="closeCard" class="btn btn-warning">Close</a>
    </div>
  </div>
</template>

<script setup lang="ts">
import Standing from '../types/Standing';

interface Props {
  standing: Standing
}

const props = defineProps<Props>()

const closeCard = () => {
  emit('resetActiveManager')
}

const emit = defineEmits<{
  (e: 'resetActiveManager'): void
}>()
</script>