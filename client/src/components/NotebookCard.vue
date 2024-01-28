<template  >
  <div data-bs-theme="light" class="notebook-grid mb-1 bg-dark" v-if="notebook" :style="`--notebook:${notebook?.color};`">
    <section class="cover-img">
      <img :src="notebook.coverImg" class="" :alt="`cover image for ${notebook.title}`">
    </section>
    <section class="icon img-shadow pe-4">
      <i class="mdi fs-1" :class="notebook.icon" :style="{color: notebook.color}"></i>
    </section>
    <section class="banner" >
        <svg xmlns="http://www.w3.org/2000/svg" width="62" height="81" viewBox="0 0 62 81" fill="none">
          <path d="M62 79.0001L62 2C62 0.895432 61.1046 -7.8281e-08 60 -1.74846e-07L4.9487 3.31594e-05C3.15022 3.30022e-05 2.26562 2.18881 3.55908 3.43841L38.4089 37.1069C39.1946 37.8659 39.2258 39.115 38.479 39.9124L3.15338 77.633C1.9569 78.9106 2.8628 81.0001 4.61317 81.0001L60 81.0001C61.1046 81.0001 62 80.1046 62 79.0001Z" :fill="notebook.color"/>
        </svg>
      </section>
      <h2 class="banner-text text-center ps-2">{{ notebook.title }}</h2>
      <h4 class="creator ps-4 p-2 text-light">created by {{ notebook.creator.name }}</h4>
      <div class="timestamps ps-4 p-2 text-secondary">
        <p>Created {{ notebook.createdTimeStamp }}</p>
        <p>Updated {{ notebook.updatedTimeStamp }}</p>
      </div>
      <div class="entry-count pe-4 text-secondary">
        <p class="mb-0">Entries {{ notebook.entryCount }}</p>
      </div>
      <div v-if="user.id == notebook.creatorId" class="menu-buttons row w-100 gap-1 pb-2 pb-md-4 px-2 px-md-4">
        <button class="btn btn-outline-light col py-2 rounded-sm" data-bs-toggle="modal" data-bs-target="#edit-notebook-modal">edit</button>
        <button class="btn btn-outline-light col py-2 rounded-sm" @click="deleteNotebook()">delete</button>
        <button class="btn btn-outline-light bg-primary col py-2 rounded-sm" @click="emit('formToggled')"><i class="mdi mdi-plus-box"></i> new</button>
      </div>
  </div>
</template>


<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { notebooksService } from '../services/NotebooksService.js';
import { useRouter } from 'vue-router';
import { AuthService } from '../services/AuthService.js';
  const emit = defineEmits(['formToggled'])
  const notebook = computed(()=> AppState.activeNotebook)
  const user = computed(()=> AppState.user)
  const router = useRouter()

  async function deleteNotebook(){
    try {
      if(await Pop.confirm("Delete this notebook?", "all of it's entries will be un-categorized")){
        await notebooksService.deleteNotebook(AppState.activeNotebook.id)
        router.push({name: 'Account'})
      }
    } catch (error) {
      Pop.error(error, '[DELETE NOTEBOOK]')
    }
  }
</script>


<style lang="scss" scoped>

.notebook-grid{
  display: grid;
  grid-template-columns: 3fr .5fr 4fr;
  grid-template-rows: 18dvh 2.5rem 2.5rem 3rem 4rem;
  place-content: center;
  border-radius: 8px;
  overflow: hidden;
  .cover-img{
    grid-column: 1/4;
    grid-row: 1/3;
    img{
      width: 100%;
      height: 100%;
      object-fit: cover;
      object-position: top;
    }
  }
  .icon{
    grid-column: 3/4;
    grid-row: 1/2;
    place-self: end;
  }
  .banner{
    grid-column: 3/4;
    grid-row: 2/4;
    position: relative;
    svg{
      position: absolute;
      height: 100%;
      left: -40px;
    }
    &::after{
      content: '';
      position: absolute;
      width: 100%;
      height: 100%;
      top: 0;
      background-color: var(--notebook);
    }
  }
  .banner-text{
    position: relative;
    place-self: center;
    grid-column: 3/4;
    grid-row: 2/4;
    background-color: (--notebook);
  }
  .creator{
    grid-column: 1/2;
    grid-row: 3/4;
  }
  .timestamps{
    grid-column: 1/2;
    grid-row: 4/6;
    align-self: center;
    p{
      margin-bottom: .25em;
    }
  }
  .entry-count{
    grid-column: 3/4;
    grid-row: 4/5;
    align-self: center;
    justify-self: end;
  }
  .menu-buttons{
    grid-column: 3/4;
    grid-row: 5/6;
    place-self: center;
  }
}
.img-shadow{
  filter: drop-shadow(0px 0px 3px rgba(0, 0, 0, 0.432));
}
@media screen and (max-width: 835px) {
  .notebook-grid{
    grid-template-rows: 15dvh 1.5rem 1.75rem 1.75em 3rem 5rem;
    .banner, .banner-text{
      grid-column: 2/4;
    }
    .creator{
      grid-column: 1/4;
      grid-row: 4/5;
      align-self: baseline;
    }
    .entry-count{
      align-self: baseline;
    }
    .timestamps{
      grid-column: 1/4;
      grid-row: 5/6;
      align-self: baseline;
      p{
        margin-bottom: .15em;
      }
    }
    .menu-buttons{
      width: 100%;
      grid-column: 1/4;
      grid-row: 6/7;
      align-self: end;
      display: flex;
      justify-content: space-around;
    }
  }
}
</style>
