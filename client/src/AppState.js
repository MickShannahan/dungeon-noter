import { reactive } from 'vue'
import { Entry } from './models/Entry.js'
import { Notebook } from './models/Notebook.js'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},

  /** @type {Entry[]} */
  accountEntries: [],
  /** @type {Entry[]} */
  notebookEntries: [],
  /** @type {Notebook[]} */
  notebooks: [],
  /** @type {Notebook} */
  activeNotebook: null,
  /**  @type {Entry} */
  activeEntry: null
})
