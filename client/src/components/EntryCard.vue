<template>
<div class="entry-grid">

  <section class="entry-image p-1">
    <img :src="entryData.img" :alt="entry.img" class="rounded">
  </section>

  <section class="image-upload p-1">
    <input :disabled="!needsSave" v-model="entryData.img" type="text" class="form-control" placeholder="Image Url" @change="needsSave = true">
  </section>

  <section class="entry-description p-1" :class="{'entry-description-full-width': !entryData.img}">
    <div v-if="!needsSave" :id="`entry-${entry.id}-body`" class="description form-control w-100 h-100" v-html="markdown" ></div>
    <!-- <textarea v-else   v-model="entryData.description"  ></textarea> -->
      <MdEditor v-else v-model="entryData.description" input-box-width="100%" :footers="[]" :auto-focus="true" :scroll-auto="true" :preview="false" :toolbars="toolbar" class="form-control w-100 h-100" language="en-US"/>
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
      <button class="btn btn-light border w-100 w-sm-50 d-none d-sm-inline" @click="needsSave = true"><i class="mdi mdi-dots-horizontal"></i></button>
      <button class="btn btn-light border w-100 w-sm-50 d-sm-none d-inline" @click="openMobileEdit"><i class="mdi mdi-dots-horizontal"></i></button>
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
    <div class="w-50 text-end d-flex">
      <button class="btn btn-light bg-transparent col" @click="deleteEntry"><i class="mdi mdi-delete text-danger"></i></button>
      <button class="btn btn-light bg-transparent col" @click="cancel">cancel</button>
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
import {MdEditor} from 'md-editor-v3'
import NotebookSpine from './NotebookSpine.vue';
import { marked } from 'marked';
import { Modal } from 'bootstrap';
const props = defineProps({entry: {type: Entry,  required: true}})
const needsSave = ref(false)
const entryData = ref({})
const notebooks = computed(()=> AppState.notebooks)
const markdown = ref('')
watchEffect(()=>{
  entryData.value = {...props.entry}
  markdown.value = marked.parse(props.entry.description)
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

function openMobileEdit(){
  logger.log(props.entry)
  AppState.activeEntry = props.entry
  Modal.getOrCreateInstance('#entry-mobile-editor')?.show()
}

const toolbar = ['bold', 'underline','italic', 'strikeThrough', 'title', 'link', 'image', 'code', 'codeRow', 'quote', 'orderedList', 'unorderedList', 'table']
</script>


<style lang="scss" scoped>

</style>
