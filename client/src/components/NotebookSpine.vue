<template>
  <div class="vertical-text no-select" >
    <span class="fs-5">
      <RouterLink v-if="notebook" :to="{name: 'Notebook', params: {notebookId: notebook.id}}" :class="{'text-light': notebook, 'text-dark': !notebook}">
        <i :class="`fs-4 mt-1 mb-2 mdi ${notebook.icon}`" :style="`color: ${notebook.color}`" ></i><span>{{ notebook.title }}</span>
      </RouterLink>
      <div v-else class="p-1">
        <i class="mdi mdi-circle-outline mb-2 mt-1"></i>
        Un-categorized
      </div>
      </span>
  </div>
</template>


<script setup>
import { Notebook } from '../models/Notebook.js';

const props = defineProps({notebook: Notebook})

</script>


<style lang="scss" scoped>
.vertical-text{
  writing-mode: vertical-lr;
  text-orientation: mixed;
}
</style>
