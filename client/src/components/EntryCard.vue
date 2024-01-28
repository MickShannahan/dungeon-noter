<template>
<div class="entry-grid">

  <section class="entry-image p-1">
    <img :src="entryData.img" :alt="entry.img" class="rounded">
  </section>

  <section class="image-upload p-1">
    <input :disabled="!needsSave" v-model="entryData.img" type="text" class="form-control" placeholder="Image Url" @change="needsSave = true">
  </section>

  <section class="entry-description p-1" :class="{'entry-description-full-width': !entryData.img}">
    <textarea :disabled="!needsSave" v-model="entryData.description" class="form-control w-100 h-100" @keypress="needsSave = true"></textarea>
  </section>

  <section class="spine m-1 m-sm-0" :class="{'bg-black': entry.notebook, 'bg-light': !entry.notebook}">
    <NotebookSpine  :notebook="entry.notebook"/>
  </section>

  <section v-if="!needsSave" class="entry-menu p-1 d-flex align-items-baseline">
    <div class="w-75 d-none d-sm-block">
      Last updated - {{ entry.updatedTimeStamp }}
    </div>
    <div class="w-75 d-block d-sm-none">
       <i class="mdi mdi-update me-2"></i>{{ entry.shortUpdate }}
    </div>
    <div class="w-25 text-end">
      <button class="btn btn-light border w-100 w-sm-50" @click="needsSave = true"><i class="mdi mdi-dots-horizontal"></i></button>
    </div>
  </section>
  <section v-else class="entry-menu p-1 d-flex align-items-baseline">
    <div class="w-50 me-1">
      <select v-model="entryData.notebookId" name="" id="" class="form-control">
        <option value="" selected disabled>Select Notebook</option>
        <option value="null">None</option>
        <option v-for="notebook in notebooks" :key="`form-select-${notebook.id}`" :value="notebook.id">{{ notebook.title }}</option>
      </select>
    </div>
    <div class="w-50 text-end">
      <button class="btn btn-light bg-transparent w-25" @click="deleteEntry"><i class="mdi mdi-delete text-danger"></i></button>
      <button class="btn btn-light bg-transparent w-25" @click="cancel">cancel</button>
      <button class="w-50 btn btn-primary" @click="updateEntry">
        Save
      </button>
    </div>
  </section>

</div>

</template>


<script setup>
import { ref, onMounted, computed, watchEffect } from 'vue';
import { Entry } from '../models/Entry.js';
import { AppState } from '../AppState.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { entriesService } from '../services/EntriesService.js';
import NotebookSpine from './NotebookSpine.vue';
const props = defineProps({entry: {type: Entry,  required: true}})
const needsSave = ref(false)
const entryData = ref({})
const notebooks = computed(()=> AppState.notebooks)
watchEffect(()=>{
  entryData.value = {...props.entry}
})

async function updateEntry(){
  try {
    await entriesService.updateEntry(entryData.value)
    needsSave.value = false
  } catch (error) {
    Pop.error(error, '[UPDATE ENTRY]')
  }
}

function cancel(){
  entryData.value = {...props.entry}
  needsSave.value = false
}

async function deleteEntry(){
  try {
    if(await Pop.confirm("are you sure you want to delete this?")) {
      await entriesService.deleteEntry(props.entry.id)
      Pop.toast('Entry Deleted', 'info', 'bottom')
    }
  } catch (error) {
    Pop.error(error, '[DELETE ENTRY]')
  }
}


</script>


<style lang="scss" scoped>

</style>
