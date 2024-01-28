import { AppState } from "../AppState.js"
import { Entry } from "../models/Entry.js"
import { api } from "./AxiosService.js"

class EntriesService{

  async createEntry(entryData){
    // TODO upload image?
    const res = await api.post('api/entries', entryData)
    const entry = new Entry(res.data)
    AppState.accountEntries.unshift(entry)
    AppState.notebookEntries.unshift(entry)
  }

  async getAccountEntries(){
    const res = await api.get('api/entries')
    const entries = res.data.map(e => new Entry(e))
    AppState.accountEntries = entries
  }

  async getNotebookEntries(notebookId){
    AppState.notebookEntries = []
    const res = await api.get(`api/notebooks/${notebookId}/entries`)
    const entries = res.data.map(e => new Entry(e))
    AppState.notebookEntries = entries
  }

  async updateEntry(entryData){
    // TODO update image?
    const res = await api.put(`api/entries/${entryData.id}`, entryData)
    const update = new Entry(res.data)
    const aIndexToUpdate = AppState.accountEntries.findIndex(e => e.id == entryData.id)
    const bIndexToUpdate = AppState.notebookEntries.findIndex(e => e.id == entryData.id)

    if(aIndexToUpdate  != -1) AppState.accountEntries.splice(aIndexToUpdate, 1, update)
    if(bIndexToUpdate != -1) AppState.notebookEntries.splice(bIndexToUpdate, 1, update)
  }

  async deleteEntry(entryId){
    // TODO delete image?
    const res = await api.delete(`api/entries/${entryId}`)

    const aIndexToRemove = AppState.accountEntries.findIndex(e => e.id == entryId)
    const bIndexToRemove = AppState.notebookEntries.findIndex(e => e.id == entryId)

    if(aIndexToRemove != -1) AppState.accountEntries.splice(aIndexToRemove, 1)
    if(bIndexToRemove != -1) AppState.notebookEntries.splice(bIndexToRemove, 1)
  }

}

export const entriesService = new EntriesService()
