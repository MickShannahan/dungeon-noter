<template>
  <div class="container-fluid grid py-1 py-sm-3">
    <!-- SECTION notebook card -->
    <div class="grid-width">
      <section>
        <NotebookCard @formToggled="collapseForm = !collapseForm"/>
      </section>
    </div>
    <!-- SECTION entry form -->
    <section class="row">
      <EntryForm id="notebook-entry-form" :notebook="notebook" :collapse="collapseForm"/>
    </section>
    <!-- SECTION entries -->
    <section class="row">
      <EntryCard class="mt-3" v-for="entry in entries" :key="`entry-card-${entry.id}`" :entry="entry"/>
    </section>
    <ModalWrapper id="edit-notebook-modal" v-if="notebook">
      <div class="bg-secondary p-4 rounded">
        <p>Update {{ notebook.title }}</p>
        <NotebookForm :notebook="notebook"/>
      </div>
    </ModalWrapper>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, watchEffect } from 'vue';
import Pop from '../utils/Pop.js';
import { entriesService } from '../services/EntriesService.js';
import { useRoute } from 'vue-router';
import EntryCard from '../components/EntryCard.vue';
import NotebookCard from '../components/NotebookCard.vue';
import EntryForm from '../components/EntryForm.vue';
import { notebooksService } from '../services/NotebooksService.js';
import ModalWrapper from '../components/ModalWrapper.vue';
import NotebookForm from '../components/NotebookForm.vue';
export default {
    setup() {
        const route = useRoute();
        const collapseForm = ref(true)
        async function getNotebookEntries() {
            try {
                await entriesService.getNotebookEntries(route.params.notebookId);
            }
            catch (error) {
                Pop.error(error, '[Get Notebook Entries]');
            }
        }
        async function getNotebook(){
          try {
            await notebooksService.getNotebookById(route.params.notebookId)
          } catch (error) {
            Pop.error(error, '[Get Notebook]')
          }
        }
        watchEffect(() => {
          route.params.notebookId
          getNotebook()
          getNotebookEntries();
        });
        return {
          collapseForm,
            account: computed(() => AppState.account),
            notebook: computed(() => AppState.activeNotebook),
            entries: computed(() => AppState.notebookEntries)
        };
    },
    components: { EntryCard, NotebookCard, EntryForm, ModalWrapper, NotebookForm }
};
</script>


<style lang="scss" scoped>
.container-fluid{
  background-image: url(../assets/img/NotebookBg.png);
  background-size: 130%;
  background-position: 20%;
  background-attachment: fixed;
}
@media screen and (max-width: 1060px) {
  .container-fluid{
    background-size: cover;
    background-position: right;
  }
}
</style>
