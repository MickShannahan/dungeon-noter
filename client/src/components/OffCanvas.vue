<template>

  <div class="notebooks-button fw-bold d-none  d-md-block" v-if="account.id">
    <button data-bs-toggle="offcanvas" data-bs-target="#notebooks-offcanvas">
      <svg xmlns="http://www.w3.org/2000/svg" width="274" height="60" viewBox="0 0 274 60" fill="none">
        <path d="M0 2C0 0.895432 0.89543 0 2 0H221.864H271.299C272.889 0 273.843 1.76595 272.972 3.09605L256.695 27.9396C256.271 28.587 256.258 29.421 256.662 30.0808L273.134 56.9549C273.95 58.2875 272.991 60 271.428 60H221.864H2.00001C0.895438 60 0 59.1046 0 58V2Z" fill="#2A47DE"/>
      </svg>
      <div class="fs-5 fw-bold text-light">
        <i class="mdi mdi-notebook">Notebooks</i>
      </div>
    </button>

    <RouterLink v-if="$route.name != 'Account'" class="mt-2" :to="{name: 'Account'}">
      <svg xmlns="http://www.w3.org/2000/svg" width="274" height="60" viewBox="0 0 274 60" fill="none">
        <path d="M0 2C0 0.895432 0.89543 0 2 0H221.864H271.299C272.889 0 273.843 1.76595 272.972 3.09605L256.695 27.9396C256.271 28.587 256.258 29.421 256.662 30.0808L273.134 56.9549C273.95 58.2875 272.991 60 271.428 60H221.864H2.00001C0.895438 60 0 59.1046 0 58V2Z" fill="#000"/>
      </svg>
      <div class="fs-5 fw-bold text-light">
        <i class="mdi mdi-note-text">All Entries</i>
      </div>
    </RouterLink>
  </div>

  <div
    class="offcanvas offcanvas-start bg-secondary"
    data-bs-scroll="true"
    tabindex="-1"
    id="notebooks-offcanvas"
    aria-labelledby="Enable both scrolling & backdrop"
  >
    <div class="offcanvas-header">
      <h5 class="offcanvas-title" id="Enable both scrolling & backdrop">
        Manage Notebooks
      </h5>
      <button
        type="button"
        class="btn-close"
        data-bs-dismiss="offcanvas"
        aria-label="Close"
      ></button>
    </div>
    <div class="offcanvas-body container-fluid">

        <NotebookForm/>

        <NotebookListItem v-for="notebook in notebooks" :key="`list-notebook-${notebook.id}`" :notebook="notebook"/>

    </div>
  </div>

</template>


<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import NotebookListItem from './NotebookListItem.vue';
import NotebookForm from './NotebookForm.vue';
import { Account } from '../models/Account.js';
const account = computed(()=> AppState.account)
const notebooks = computed(()=> AppState.notebooks)
</script>


<style lang="scss" scoped>
.notebooks-button{
  position: fixed;
  width: 200px;
  top: 8.5dvh;
  left: calc(-100px + 3dvw);
  button, a{
    display: grid;
    width: 100%;
    height: 60px;
    justify-items: end;
    align-items: center;
    padding-right: 2em;
    border: 0;
    background-color: transparent;
    position: relative;
    &:hover{
      transform: translateX(10px);
      transition: all .2s ease;
    }
    svg{
      position: absolute;
      top: 0;
      left: -75px;
      z-index: -1;
    }
  }
  a{
    font-size: 1.5rem;
    transform: scale(.75);
    left: -10px;
    &:hover{
    transform: scale(.75) translateX(10px);
    }
  }
}

.offcanvas{
  --bs-offcanvas-width: 450px
}
</style>
