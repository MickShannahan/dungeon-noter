<template>
  <div class="wrapper">
    <section class="chat bubble">

    </section>
    <section class="barlso">
      <img src="../assets/img/AdviceOslo.png" class="img-fluid" alt="">
    </section>
  </div>
</template>


<script setup>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';

</script>


<style lang="scss" scoped>
.wrapper{
  position: fixed;
  top: 5vh;
  right: .5em;
}
</style>
