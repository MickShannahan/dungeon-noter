<template>
<div class="container-fluid">
  <section class="home">
    <div class="bg-secondary p-3 rounded shadow">
      <img src="../assets/img/LogoDark.png" alt="">
    </div>
  </section>
</div>
</template>

<script>
import {computed, watch} from 'vue'
import { AppState } from '../AppState.js';
import { useRouter } from 'vue-router';
import { logger } from '../utils/Logger.js';
export default {
  setup() {
    const router = useRouter()
    const account = computed(()=> AppState.account)
    watch(account, ()=>{
      logger.log('account loaded')
        router.push({name: 'Account'})
    })
    return {

    }
  }
}
</script>

<style scoped lang="scss">
.home {
  height: 100%;
  display: grid;
  place-content: center;
  text-align: center;
  user-select: none;
}

.container-fluid{
  flex-grow: 1;
  background-image: url(../assets/img/HomeBg.png);
  background-size: cover;
  background-position: center;
}
</style>
