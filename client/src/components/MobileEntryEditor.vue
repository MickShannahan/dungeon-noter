<template>
  <div class="editor p-1 gap-1 bg-secondary">
    <div class="text-end  close-btn"><button class="btn fs-2" @click="closeModal()"><i class="mdi mdi-close text-danger"></i></button></div>
    <div class="cover-img rounded" :style="{backgroundImage: `url(${entryData.img})`}">
      <img :src="entryData.img" alt="" class="rounded border">
    </div>
    <div class="input-group">
      <span class="input-group-text"><i class="mdi mdi-image"></i></span>
      <input v-model="entryData.img" type="text" class="form-control">
    </div>
    <section class="description">
      <MdEditor v-model="entryData.description" input-box-width="100%" :footers="[]" :auto-focus="true" :scroll-auto="true" :preview="false" :toolbars="toolbar" class="form-control w-100 h-100" language="en-US"/>
    </section>
    <section class="input-group">
      <span class="input-group-text">
        <i class="mdi mdi-notebook"></i>
      </span>
      <select v-model="entryData.notebookId" name="" id="" class="form-control">
        <option value="" selected disabled>Select Notebook</option>
        <option value="null">None</option>
        <option v-for="notebook in notebooks" :key="`form-select-${notebook.id}`" :value="notebook.id">{{ notebook.title }}</option>
      </select>
    </section>
    <button class="btn btn-outline-light bg-primary" @click="saveEntry()">Save</button>

  </div>
</template>


<script setup>
import { AppState } from '../AppState';
import { computed, ref, onMounted, watch } from 'vue';
import {MdEditor} from 'md-editor-v3'
import Pop from '../utils/Pop.js';
import { entriesService } from '../services/EntriesService.js';
import { Modal } from 'bootstrap';
const entry = computed(()=>AppState.activeEntry)
const notebooks = computed(()=> AppState.notebooks)
const entryData = ref('')
watch(entry, ()=>{
  if(entry){
    entryData.value = {...entry.value}
  }
})

async function saveEntry(){
  try {
    await entriesService.updateEntry(entryData.value)
    Modal.getOrCreateInstance('#entry-mobile-editor')?.hide()
  } catch (error) {
    Pop.error(error)
  }
}

function closeModal(){
  AppState.activeEntry = {}
  Modal.getOrCreateInstance('#entry-mobile-editor')?.hide()
}

const toolbar = ['bold', 'underline','italic', 'strikeThrough', 'title', 'link', 'image', 'code', 'codeRow', 'quote', 'orderedList', 'unorderedList', 'table']
</script>


<style lang="scss" scoped>
.mdi-close{
  position: relative;
  text-shadow: 0px 0px 3px black;
  z-index: 1000;
}
.editor{
  position: relative;
  display: flex;
  flex-direction: column;
  height: 100%;
  .close-btn{
    position: relative;
    margin-bottom: -3em;
  }
}
.cover-img{
  background-position: center;
  background-size: cover;
  img{
    width: 100%;
    height: 7.5em;
    object-fit: contain;
    object-position: center;
    backdrop-filter: blur(20px);
  }
}

.description{
  flex-grow: 1;
}
</style>
