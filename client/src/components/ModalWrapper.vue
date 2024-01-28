<template>
 <!-- Modal -->
 <div
  class="modal fade"
  id="modalId"
  tabindex="-1"
  role="dialog"
  aria-labelledby="modalTitleId"
  aria-hidden="true"
 >

  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
          <slot>

          </slot>
    </div>
  </div>
 </div>

</template>


<script setup>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';

</script>


<style lang="scss" scoped>

</style>
