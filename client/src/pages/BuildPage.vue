<script setup>
import { onMounted, ref, watch } from 'vue'
import { marked } from 'marked';
import readme from '/uploadReadme.md'
import { logger } from '../utils/Logger.js';
const markdownContent = ref('')
const navlinks = ref([])
const markdown = ref(null)
onMounted(async () => {
  const res = await fetch(readme)
  let content = await res.text()
  logger.log(content)
  content.replace(new RegExp('assets/', 'ig'), '../assets/')
  markdownContent.value = await marked.parse(content)
})

watch(markdownContent, () => {
  setTimeout(createLinks, 200)
})

function createLinks() {
  let headers = markdown.value.querySelectorAll('h2, h3')
  let headersArray = Array.from(headers)
  for (let i = 0; i < headersArray.length; i++) {
    let h2 = headersArray[i]
    h2.id = h2.innerText.replace(/ /ig, '-')
    logger.log(h2)
    let next = headersArray.findIndex((h, j) => h.nodeName == 'H2' && j > i)
    const childLinks = []
    headersArray.splice(i + 1, next - i - 1).forEach(c => {
      c.id = c.innerText.replace(/ |\.|\!|\?/ig, '-')
      c.classList.add('mdi', 'mdi-circle-small')
      childLinks.push({ href: "#" + c.id, title: c.innerText })
    })
    logger.log(childLinks)
    navlinks.value.push({ href: '#' + h2.id, title: h2.innerText, childLinks })
  }

}
function scrollTo(id) {
  document.querySelector(id).scrollIntoView()
}

</script>

<template>
  <div class="container-fluid no-grid">
    <section class="row">
      <div class="col-3 pt-3 ps-3 d-none d-sm-block">
        <div class="sticky-top row bg-dark rounded shadow p-1 nav-box">
          <div class="col-12 mb-2" v-for="link in navlinks" :key="link">
            <RouterLink class="selectable rounded" :to="link.href" @click="scrollTo(link.href)">
              <span class="mdi mdi-link f-mono">{{ link.title }}</span>
            </RouterLink>
            <div v-if="$route.fullPath.includes(link.href)" class="ms-2" v-for="child in link.childLinks" :key="child">
              <a class="selectable rounded" @click="scrollTo(child.href)">
                <span class="mdi mdi-link f-mono text-secondary">{{ child.title }}</span>
              </a>
            </div>
          </div>
          <div class="col-12">
            <a class="text-info"
              href="https://github.com/MickShannahan/cloud-upload-demo/blob/main/src/functions/imageUpload.mjs"
              target="_blank"><i class="mdi mdi-github"></i>Cloud Function</a>
          </div>
          <div class="col-12">
            <a class="text-info"
              href="https://github.com/MickShannahan/dungeon_noter/blob/main/client/src/services/ImageUploadService.js"
              target="_blank"><i class="mdi mdi-github"></i>Client Upload</a>
          </div>
        </div>
      </div>
      <div class="col-9 d-flex justify-content-center pt-3">
        <div ref="markdown" class="markdown" v-html="markdownContent">
        </div>
      </div>
    </section>
  </div>
</template>

<style lang="scss">
main {
  background-image: url(../assets/img/buildBg.png);
  object-fit: cover;
  object-position: right;
  background-attachment: fixed;
}

.nav-box {
  font-size: 14px;
}

.markdown {
  background-color: var(--bs-dark);
  color: var(--bs-secondary);
  padding: 1em 2em;
  border-radius: 8px;
  font-size: 16px;
  max-width: 90ch;

  img {
    border-radius: 5px;
    max-width: 100%;
    object-fit: contain;
    margin-bottom: .25em;
  }

  h1 {
    border-bottom: 1px solid var(--bs-primary);
    padding-bottom: .25em;
    margin-bottom: 1em;
    text-align: center;
  }

  h2:not(:first-of-type) {
    // border-left: 3px solid var(--bs-secondary);
    border-top: 1px grey dotted;
    padding-top: 1em;
  }

  p {
    padding-left: .5em;
    font-family: var(--f-inter);
    color: var(--bs-light);
  }

  blockquote {
    border: 1px dashed var(--bs-info);
    padding: .5em;
    border-radius: 8px;
    font-size: 14px;

  }

  em {
    color: var(--bs-info) !important;
  }
}
</style>
