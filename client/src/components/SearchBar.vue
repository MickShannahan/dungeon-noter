<template>
  <div class="search-bar">
    <div class="input-group">
      <button v-if="!open" @click="open = true" class="btn btn-outline-light px-4" :class="{rounded: !open}" data-bs-toggle="collapse" data-bs-target="#search-collapse"><i class="mdi mdi-magnify"></i></button>
      <button v-else @click="close" class="btn btn-outline-light px-4" data-bs-toggle="collapse" data-bs-target="#search-collapse"><i class="mdi mdi-close"></i></button>
      <div class="collapse collapse-horizontal flex-grow-1" id="search-collapse">
        <input v-model="searchField" @keyup="()=> startInterval()" class="form-control bg-dark text-light h-100" type="text" name="search-entries" id="search-entries" :style="`width: ${width}`">
        <div v-if="timer > 0 && timer < 50" class="loader"> <i class="mdi mdi-loading mdi-spin text-light fs-5"></i></div>
      </div>
    </div>
  </div>
</template>


<script setup>
import {ref} from 'vue'
import { logger } from '../utils/Logger.js';
const open = ref(false)
const props = defineProps({width: {type: String, default: '50ch'}})
const emit = defineEmits(['typingDone', 'closed'])
const delay = 50
const timer = ref(delay)
const interval = ref(null)

const searchField = ref('')

function countDown(){
  timer.value--
  if(timer.value <= 0){
    emit('typingDone', searchField.value)
    interval.value = clearInterval(interval.value)
    timer.value = delay
  }
}

function startInterval(){
  timer.value = delay
  interval.value = clearInterval(interval.value)
  interval.value = setInterval(countDown, 10)
}

function close(){
  open.value = false
  searchField.value = ''
  interval.value = clearInterval(interval.value)
  emit('closed', null)
}


</script>


<style lang="scss" scoped>
.search-bar{
  display: inline-block;
}
input{
  border-top-left-radius: 0px;
  border-bottom-left-radius: 0px;
}
.collapse{
  position: relative;
}
.loader{
  position: absolute;
  right: 5px;
  top: 0px;
}
</style>
