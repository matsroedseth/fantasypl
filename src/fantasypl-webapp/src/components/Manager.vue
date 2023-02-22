<template>
  <div>
    <label for="manager-id">Manager ID:</label>
    <input type="text" id="manager-id" v-model="managerId">
    <button @click="fetchData">Fetch Data</button>
  </div>
  <div v-if="manager">
    <h2>Manager Details</h2>
    <p>ID: {{ manager.id }}</p>
    <p>Name: {{ manager.firstName }} {{ manager.lastName }}</p>
    <p>Team Name: {{ manager.teamName }}</p>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import FantasyApi from '../services/FantasyApi';
import ResponseData from '../types/ResponseData'
import Manager from '../types/Manager'

export default defineComponent({
  data() {
    return {
      managerId: '' as String,
      manager: {} as Manager
    };
  },
  methods: {
    async fetchData() {
      try {
        FantasyApi.getManager(this.managerId)
          .then((response: ResponseData) => {
            console.log(response.data);
            this.manager = response.data
          })
      } catch (error) {
        console.error(error);
      }
    },
  },
});
</script>

<style scoped>
.read-the-docs {
  color: #888;
}
</style>
