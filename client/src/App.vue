<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view class="router-view pb-4 pb-md-3"/>
  </main>
  <OffCanvas/>
  <footer class="bg-dark d-block d-md-none">
    <div class="floating-btns w-100 px-2 py-1">
      <button v-if="$route.name == 'Account'" data-bs-toggle="modal" data-bs-target="#create-entry-modal" class="btn btn-outline-light bg-primary w-100"><i class="mdi mdi-plus-box"></i> New</button>
      <RouterLink :to="{name: 'Account'}" v-if="$route.name == 'Notebook'" class="btn btn-outline-light bg-primary w-100"><i class="mdi mdi-card-text"></i> Entries</RouterLink>
      <div></div>
      <button data-bs-toggle="offcanvas" data-bs-target="#notebooks-offcanvas" class="btn btn-outline-light bg-primary w-100">
        <i class="mdi mdi-notebook"></i> Notebooks
      </button>
    </div>
  </footer>
  <section class="bg-dark p-2 d-flex d-md-none justify-content-end">
    <Login/>
  </section>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import OffCanvas from './components/OffCanvas.vue'
import Login from './components/Login.vue'
import AskBarslo from './components/AskBarslo.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, OffCanvas, Login, AskBarslo }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root{
  --main-height: calc(100vh - 32px - 64px);
}

main{
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  .container-fluid{
    display: grid;
    height: 100%;
    flex-grow: 1;
    $flag-width: calc(100px + 3dvw);
    grid-template-columns: $flag-width minmax(auto, 150ch) 1fr;
    grid-auto-rows: max-content;
    &>*{
      grid-column: 2/3;

    }
  }
  @media screen and (max-width: 768px) {
  .container-fluid{
    grid-template-columns: 3px 1fr 3px;
  }
}
}



footer {
  position: sticky;
  bottom: 0;
  display: grid;
  place-content: center;
  height: 2em;
  .floating-btns{
    display: grid;
    place-content: center;
    grid-template-columns: 1fr 1fr 1fr;
    grid-template-rows: 1fr;
    position: absolute;
    top: -1em;
  }
}
</style>
