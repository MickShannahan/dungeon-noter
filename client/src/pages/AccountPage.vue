<template>
<div class="container-fluid py-3">
<!-- SECTION filter bar desktop-->
<section class="grid-width mb-3 d-none d-md-grid">
  <div class="filter-bar d-flex bg-dark p-2 rounded shadow">
    <div class="w-75 d-flex align-items-end">
      <button class="btn btn-outline-light  px-4 me-2" @click="filterEntries = 'all'"><i class="mdi mdi-card-text-outline"></i> All</button>
      <button class="btn btn-outline-light px-4 me-2" @click="filterEntries = 'un-categorized'"><i class="mdi mdi-alert-circle-outline"></i> Un-categorized</button>
      <SearchBar class="d-none d-lg-block" @typingDone="searchFilter" @closed="searchFilter" width="30ch"/>
    </div>
    <div class="col d-flex justify-content-end">
      <button v-if="collapseForm" @click="collapseForm = false" class="btn btn-outline-light px-4"><i class="mdi mdi-plus-box"></i> New</button>
      <button v-else @click="collapseForm = true" class="btn btn-outline-light px-4"><i class="mdi mdi-close-box"></i> Close</button>
    </div>
  </div>
</section>
<!-- STUB filter bar Mobile -->
<section class="grid-width mb-3 d-block d-md-none ">
  <div class="filter-bar d-flex bg-dark p-2 rounded shadow flex-column">
    <div class="w-100 d-flex justify-content-end">
      <button class="btn btn-outline-light  px-3 me-1 w-50" @click="filterEntries = 'all'"><i class="mdi mdi-card-text-outline"></i> All</button>
      <button class="btn btn-outline-light px-3 w-50" @click="filterEntries = 'un-categorized'"><i class="mdi mdi-alert-circle-outline"></i> Un-categorized</button>
    </div>

    <div class="w-100 d-flex mt-2">
      <SearchBar class="w-100" @typingDone="searchFilter" @closed="searchFilter" width="100%"/>
    </div>
    <ModalWrapper id="create-entry-modal">
      <EntryForm id="modal-entry-form" :collapse="false"/>
    </ModalWrapper>
  </div>
</section>

  <!-- SECTION notebook -->
  <section  class="row justify-content-center">
    <EntryForm id="account-entry-form" @submitted="closeForm" :collapse="collapseForm"/>
  </section>

  <!-- SECTION entries -->
  <section class="row">
    <EntryCard class="mt-3" v-for="entry in entries" :key="`entry-card-${entry.id}`" :entry="entry"/>
  </section>
</div>

</template>

<script>
import { computed, onMounted, ref } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop.js';
import {entriesService} from '../services/EntriesService.js'
import EntryForm from '../components/EntryForm.vue';
import EntryCard from '../components/EntryCard.vue';
import { Collapse } from 'bootstrap';
import SearchBar from '../components/SearchBar.vue';
import { logger } from '../utils/Logger.js';
import ModalWrapper from '../components/ModalWrapper.vue';
export default {
    setup() {
      const collapseForm = ref(true)
      const filterEntries = ref('all')
      const searchEntries = ref(null)
        async function getAccountEntries() {
            try {
                await entriesService.getAccountEntries();
            }
            catch (error) {
                Pop.error(error, '[getting account entries]');
            }
        }
        onMounted(() => {
            getAccountEntries();
        });
        return {
          collapseForm,
          filterEntries,
            account: computed(() => AppState.account),
            entries: computed(() => {
              let entries
              if(filterEntries.value == 'all'){
                entries = AppState.accountEntries
              }
              if(filterEntries.value == 'un-categorized'){
                entries =  AppState.accountEntries.filter(e => !e.notebook)
              }
              if(searchEntries.value){
                entries = entries.filter(e => e.description.match(new RegExp(searchEntries.value, 'ig')))
              }
              return entries
            }),
            closeForm(){
              collapseForm.value = false
            },
            searchFilter(payload){
              searchEntries.value = payload
            }
        };
    },
    components: { EntryForm, EntryCard, SearchBar, ModalWrapper }
}
</script>

<style scoped>
.container-fluid{
  background-image: url(../assets/img/CityBg.png);
  background-attachment: fixed;
}

.floating-btn{
  position: fixed;
  bottom: .75em;
  right: .75em;
  z-index: 1;
}
</style>
