<template>
  <section class="row bg-light mb-2 shadow rounded p-2 align-items-baseline">
    <div class="col-2 col-md-1 p-2 bg-dark rounded text-center"><i class="mdi fs-5" :class="notebook.icon" :style="{color: notebook.color}"></i></div>
    <div class="col-10 col-md-7">{{ notebook.title }}</div>
    <div class="col-12 col-md-4 text-end pe-0">{{ notebook.entryCount }} entries
      <RouterLink class="btn selectable rounded-pill px-2 text-end" :to="{name: 'Notebook', params: {notebookId: notebook.id}}">
        <i class="mdi mdi-book-arrow-right"></i>
      </RouterLink >
    </div>
  </section>
</template>


<script setup>
import { Notebook } from '../models/Notebook.js';

const props = defineProps({notebook: Notebook})
</script>


<style lang="scss" scoped>

</style>
