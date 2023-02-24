<template>
  <div v-if="manager">
    <h2>Manager Details</h2>
    <p>ID: {{ manager.id }}</p>
    <p>Name: {{ manager.firstName }} {{ manager.lastName }}</p>
    <p>Team Name: {{ manager.teamName }}</p>
  </div>
</template>

<script setup lang="ts">
import Manager from '../types/Manager'

interface Props {
  manager: Manager
}
defineProps<Props>()
</script>