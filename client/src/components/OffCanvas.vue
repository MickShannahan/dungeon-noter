<template>

  <div class="notebooks-button fw-bold d-none  d-md-block" v-if="account.id">
    <button data-bs-toggle="offcanvas" data-bs-target="#notebooks-offcanvas" class="bg-primary">
      <svg xmlns="http://www.w3.org/2000/svg" width="62" height="81" viewBox="0 0 62 81" fill="none">
        <path d="M0 2.00004L0 79.0001C0 80.1047 0.89543 81.0001 2 81.0001L57.0513 81.0001C58.8498 81.0001 59.7344 78.8113 58.4409 77.5617L23.5911 43.8932C22.8055 43.1342 22.7742 41.8851 23.521 41.0877L58.8466 3.36711C60.0431 2.08952 59.1372 0 57.3868 2.96138e-06L2 3.85167e-05C0.89543 3.92258e-05 0 0.895471 0 2.00004Z" fill="var(--bs-primary)"/>
      </svg>
      <div class="fs-5 fw-bold text-light bg-primary position-relative">
        <i class="mdi mdi-notebook ps-4 ps-lg-1" title="manage notebooks"><span class="d-none d-lg-inline">Notebooks</span></i>
      </div>
    </button>

    <RouterLink v-if="$route.name != 'Account'" :to="{name: 'Account'}" class="bg-dark mt-2">
      <svg xmlns="http://www.w3.org/2000/svg" width="62" height="81" viewBox="0 0 62 81" fill="none">
        <path d="M0 2.00004L0 79.0001C0 80.1047 0.89543 81.0001 2 81.0001L57.0513 81.0001C58.8498 81.0001 59.7344 78.8113 58.4409 77.5617L23.5911 43.8932C22.8055 43.1342 22.7742 41.8851 23.521 41.0877L58.8466 3.36711C60.0431 2.08952 59.1372 0 57.3868 2.96138e-06L2 3.85167e-05C0.89543 3.92258e-05 0 0.895471 0 2.00004Z" fill="var(--bs-dark)"/>
      </svg>
      <div class="fs-5 fw-bold text-light bg-dark position-relative">
        <i class="mdi mdi-note-text ps-4 ps-lg-1" title="got back to entries"><span class="d-none d-lg-inline">Entries</span></i>
      </div>
    </RouterLink >

    <!-- <RouterLink >
      <svg xmlns="http://www.w3.org/2000/svg" width="274" height="60" viewBox="0 0 274 60" fill="none">
        <path d="M0 2C0 0.895432 0.89543 0 2 0H221.864H271.299C272.889 0 273.843 1.76595 272.972 3.09605L256.695 27.9396C256.271 28.587 256.258 29.421 256.662 30.0808L273.134 56.9549C273.95 58.2875 272.991 60 271.428 60H221.864H2.00001C0.895438 60 0 59.1046 0 58V2Z" fill="#000"/>
      </svg>
      <div class="fs-5 fw-bold text-light">
        <i class="mdi mdi-note-text" title="back to all entries"><span class="d-none d-lg-inline">All Entries</span></i>
      </div>
    </RouterLink> -->
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
  top: 8.5dvh;
  left: calc(-85px + 5dvw);
  button, a{
    padding-left: 2em;
    width: 100%;
    height: 60px;
    text-align: end;
    border: 0;
    background-color: transparent;
    position: relative;
    &:hover{
      transform: translateX(10px);
      transition: all .2s ease;
    }
    svg{
      position: absolute;
      height: 100%;
      top: 0;
      right: -38px
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
