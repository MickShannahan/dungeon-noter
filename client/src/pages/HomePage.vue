<template>
  <div class="container-fluid grid">
    <section class="home-card bg-dark rounded m-2 m-md-5">
      <section class="cover-img">
        <img src="https://www.enworld.org/attachments/dd-tashas-1604041136985-jpg.128081/" alt="image of Tasha"
          class="bg-dark">
      </section>
      <section class="d-flex justify-content-center">
        <div class="banner bg-primary">
          <svg xmlns="http://www.w3.org/2000/svg" width="62" height="81" viewBox="0 0 62 81" fill="none">
            <path
              d="M62 79.0001L62 2C62 0.895432 61.1046 -7.8281e-08 60 -1.74846e-07L4.9487 3.31594e-05C3.15022 3.30022e-05 2.26562 2.18881 3.55908 3.43841L38.4089 37.1069C39.1946 37.8659 39.2258 39.115 38.479 39.9124L3.15338 77.633C1.9569 78.9106 2.8628 81.0001 4.61317 81.0001L60 81.0001C61.1046 81.0001 62 80.1046 62 79.0001Z"
              fill="var(--bs-primary)" />
          </svg>
          <img src="../assets/img/LogoLight.png" alt="">
          <svg class="flip" xmlns="http://www.w3.org/2000/svg" width="62" height="81" viewBox="0 0 62 81" fill="none">
            <path
              d="M62 79.0001L62 2C62 0.895432 61.1046 -7.8281e-08 60 -1.74846e-07L4.9487 3.31594e-05C3.15022 3.30022e-05 2.26562 2.18881 3.55908 3.43841L38.4089 37.1069C39.1946 37.8659 39.2258 39.115 38.479 39.9124L3.15338 77.633C1.9569 78.9106 2.8628 81.0001 4.61317 81.0001L60 81.0001C61.1046 81.0001 62 80.1046 62 79.0001Z"
              fill="var(--bs-primary)" />
          </svg>
        </div>
      </section>
      <section class="card-bottom bg-dark p-3 text-light">
        <h4 class="fw-bold">Cloud Upload<i class="mdi mdi-circle-small"></i>Markdown Editor<i
            class="mdi mdi-circle-small"></i>Note Taker</h4>
        <p class="p-2 text-secondary text-start">This is a demo application for a note taking app. Currently themed for
          taking notes for the worlds greatest role-playing game. Part of the note taking process is to upload images.
          Files are posted to an <kbd class="text-primary">Azure</kbd> cloud function, and then stored in <kbd
            class="text-primary">Azure</kbd> blob storage. To Demo the app, log in and go to the <RouterLink
            to="Account">Account Page</RouterLink>. To see how to set up image uploading for yourself, head to the
          <RouterLink to="Build">Build Page</RouterLink>.
        </p>
      </section>
    </section>
  </div>
</template>

<script>
import { computed, watch } from 'vue'
import { AppState } from '../AppState.js';
import { useRouter } from 'vue-router';
import { logger } from '../utils/Logger.js';
export default {
  setup() {
    const router = useRouter()
    const account = computed(() => AppState.account)
    watch(account, () => {
      logger.log('account loaded')
      router.push({ name: 'Account' })
    })
    return {

    }
  }
}
</script>

<style scoped lang="scss">
.home-card {
  text-align: center;
  user-select: none;
  max-width: 100ch;
  place-self: center;
  filter: drop-shadow(0px 2px 5px rgba(0, 0, 0, 0.5));

  .cover-img {
    width: 100%;
    position: relative;

    img {
      border-top-right-radius: 8px;
      border-top-left-radius: 8px;
      width: 100%;
      height: 250px;
      object-fit: cover;
    }

    &::after {
      position: absolute;
      content: '';
      left: 0;
      width: 100%;
      height: 100%;
      outline: 2px #ffffff66 inset;
      outline-offset: -7px;
    }
  }

  .card-bottom {
    max-width: 65ch;
    border-bottom-left-radius: 8px;
    border-bottom-right-radius: 8px;
  }

  .banner {
    --banner-end: -55px;
    background-color: var(--bs-dark);
    display: flex;
    position: relative;
    width: 100%;
    height: 75px;
    align-items: center;
    justify-content: center;

    img {
      height: 80%;
    }

    svg {
      position: absolute;
      height: 100%;
      left: var(--banner-end);
      top: 0;

      &.flip {
        transform: scale(-1);
        right: var(--banner-end);
        left: unset;
      }
    }
  }

  @media screen and (max-width: 768px) {
    .cover-img {
      margin-bottom: -25px;
    }

    .banner {
      --banner-end: -45px;
      justify-content: center;
      width: 60%;
      height: 50px;
    }
  }
}

.container-fluid {
  width: 100dvw;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-grow: 1;
  background-image: url(../assets/img/homeBg.png);
  background-size: cover;
  background-position: center;
}
</style>
