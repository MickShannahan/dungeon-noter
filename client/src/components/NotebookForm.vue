<template>

  <form class="row mb-4" @submit.prevent="handleSubmit()">
    <div class="mb-2 col-12">
      <label for="create-notebook-title">Title</label>
      <input v-model="notebookData.title" type="text" class="form-control" minlength="3" maxlength="25" required>
    </div>
    <div class="mb-2 col-7">
      <label for="create-notebook-icon">Icon</label>
      <div class="dropdown">
        <button class="btn form-control btn-light border dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
          <i class="mdi" :class="notebookData.icon"></i> Icon
        </button>
        <input v-if="!notebookData.icon" class="custom-check" required  type="checkbox" @invalid="(e)=>setCustomMessage(e, 'Please select an icon')" title="please select an icon">
        <ul class="dropdown-menu">
          <li class="p-1 fs-5"><i v-for="option in options" class="mdi p-1 selectable rounded" :class="option" @click="notebookData.icon = option"></i></li>
        </ul>
      </div>
    </div>
    <div class="mb-2 col-4">
      <label for="create-notebook-color">Color</label>
      <input v-model="notebookData.color" type="color" minlength="3" maxlength="25" required class="form-control form-control-color">
    </div>
    <div class="mb-2 col-12">
      <label for="create-notebook-cover-image">Cover Image</label>
      <input v-model="notebookData.coverImg" type="text" class="form-control" minlength="3" maxlength="500" required>
    </div>
    <div class="col-12 text-end">
      <button v-if="!notebook" class="btn btn-primary w-50"> <i class="mdi mdi-plus-box"></i> Create</button>
      <button v-else class="btn btn-success w-50"> <i class="mdi mdi-floppy"></i> Update</button>
    </div>

  </form>
</template>


<script setup>
import {ref, watch} from 'vue'
import Pop from '../utils/Pop.js';
import { notebooksService } from '../services/NotebooksService.js';
import { Notebook } from '../models/Notebook.js';
import { logger } from '../utils/Logger.js';

const props = defineProps({notebook: Notebook})

function setCustomMessage(e, message){
  e.target.setCustomValidity(message)
}
  const notebookData = ref({})
  const options = [
  'mdi-sword',
  'mdi-shield',
  'mdi-axe-battle',
  'mdi-bow-arrow',
  'mdi-skull-crossbones',
  'mdi-ghost',
  'mdi-treasure-chest',
  'mdi-diamond',
  'mdi-anvil',
  'mdi-fire',
  'mdi-chess-rook',
  'mdi-chess-king',
  'mdi-chess-knight',
  'mdi-chess-queen',
  'mdi-cards',
  'mdi-barley',
  'mdi-flask',
  'mdi-bandage',
  'mdi-home',
  'mdi-map',
  'mdi-map-marker',
  'mdi-pin',
  'mdi-city',
  'mdi-account-group',
  'mdi-bowl-mix',
  'mdi-cross-celtic',
  'mdi-shield-crown',
  'mdi-shield-sword',
  'mdi-shield-cross',
  ]

async function createNotebook(){
  try {
    await notebooksService.createNotebook(notebookData.value)
    notebookData.value = {}
  } catch (error) {
    Pop.error(error, '[Create Notebook]')
  }
}
async function updateNotebook(){
  try {
    await notebooksService.updateNotebook(notebookData.value)
  } catch (error) {
    Pop.error(error, '[UPDATE NOTEBOOK]')
  }
}

watch(props, ()=>{
 if(props.notebook) setForm()
}, {immediate: true})

function setForm(){
  notebookData.value = {...props.notebook}
}
function handleSubmit(){
  if(props.notebook){
    updateNotebook()
  } else {
    createNotebook()
  }
}
</script>


<style lang="scss" scoped>
.custom-check{
  opacity: 0;
  pointer-events: none;
  position: relative;
  top: -15px;
  left: 20px;
  margin-bottom: -1em;
}
</style>
