<template>
  <div :id="id" class="collapse">
    <form  class="entry-grid rounded" @submit.prevent="createEntry()">
      <section class="spine">
        <!-- no spine on form -->
      </section>
      <section class="entry-image p-1">
        <img :src="entryData.img || placeHolder" alt="" class="rounded">
      </section>
      <section class="image-upload p-1">
        <input v-model="entryData.img" type="text" class="form-control" placeholder="Image Url">
      </section>
      <section class="entry-description p-1">
        <textarea v-model="entryData.description" class="form-control w-100 h-100"></textarea>
      </section>
      <section class="entry-menu p-1 d-flex justify-content-end">

        <select v-if="!notebook" v-model="entryData.notebookId" name="" id="" class="form-control w-75 me-1">
        <option value="" selected disabled>Select Notebook</option>
        <option value="null">None</option>
        <option v-for="notebook in notebooks" :key="`form-select-${notebook.id}`" :value="notebook.id">{{ notebook.title }}</option>
      </select>
      <div v-else class="form-control bg-transparent w-75 me-1">
        <i :class="`mdi ${notebook.icon}`" :style="{color: notebook.color}"></i>
        <span>
          {{ notebook.title }}
        </span>
      </div>
      <button class="btn btn-primary w-25"><span class="d-none d-sm-inline">Create</span> <i class="mdi mdi-plus-box"></i></button>

    </section>
  </form>
</div>
</template>


<script setup>
import {ref, computed, onMounted, watch, watchEffect} from 'vue'
import placeHolder from '../assets/img/FormPlaceholder.png'
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { entriesService } from '../services/EntriesService.js';
import { Notebook } from '../models/Notebook.js';
import { logger } from '../utils/Logger.js';
import { Collapse } from 'bootstrap';

const props = defineProps({notebook: Notebook, collapse: Boolean, id: String})
const emit = defineEmits(['submitted'])
const entryData = ref({})
const notebooks = computed(()=> AppState.notebooks)

watchEffect(()=>{
  logger.log('watch', props.collapse)
  let colElm = document.getElementById(props.id)
  if(!colElm) return
  let collapse = new Collapse(colElm, {hide:true})
  if(props.collapse){
    collapse.hide()
  } else {
    collapse.show()
  }
})

function resetForm(){
  entryData.value = {
    notebookId: props.notebook ? props.notebook.id : ''
  }
}
  onMounted(()=>{
    resetForm()
    if(!props.collapse){
      document.getElementById(props.id).classList.remove('collapse')
    }
  })

  async function createEntry(){
    try {
      if(props.notebook) entryData.value.notebookId = props.notebook.id
      await entriesService.createEntry(entryData.value)
      resetForm()
      emit('submitted')
    } catch (error) {
      Pop.error(error, '[CREATE ENTRY]')
    }
  }

</script>


<style lang="scss" scoped>

</style>
