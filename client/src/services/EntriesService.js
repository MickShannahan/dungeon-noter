import { AppState } from "../AppState.js"
import { Entry } from "../models/Entry.js"
import { api } from "./AxiosService.js"
import { imageUploadService } from "./ImageUploadService.js"

class EntriesService{

  async createEntry(entryData, entryImageFile){
    // REVIEW use imageUpload service to upload image, take url from response
    if(entryImageFile){
      const image = await imageUploadService.uploadImage(entryImageFile)
      entryData.img = image.url
    }
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

  async updateEntry(entryData, entryImageFile){
    if(entryData.img && entryImageFile){ // if there is an img and a new image, then we need to delete the old one
      const entry = AppState.accountEntries.find(e => e.id == entryData.id)
      await imageUploadService.deleteImage(entry.fileName)
    }
    if(entryImageFile){ // upload the new one
      const image = await imageUploadService.uploadImage(entryImageFile)
      entryData.img = image.url
    }

    const res = await api.put(`api/entries/${entryData.id}`, entryData)
    const update = new Entry(res.data)
    const aIndexToUpdate = AppState.accountEntries.findIndex(e => e.id == entryData.id)
    const bIndexToUpdate = AppState.notebookEntries.findIndex(e => e.id == entryData.id)

    if(aIndexToUpdate  != -1) AppState.accountEntries.splice(aIndexToUpdate, 1, update)
    if(bIndexToUpdate != -1) AppState.notebookEntries.splice(bIndexToUpdate, 1, update)
  }

  async deleteEntry(entryId){
    const entry = AppState.accountEntries.find(e => e.id == entryId)
    if(entry.img){
      await imageUploadService.deleteImage(entry.fileName)
    }
    const res = await api.delete(`api/entries/${entryId}`)

    const aIndexToRemove = AppState.accountEntries.findIndex(e => e.id == entryId)
    const bIndexToRemove = AppState.notebookEntries.findIndex(e => e.id == entryId)

    if(aIndexToRemove != -1) AppState.accountEntries.splice(aIndexToRemove, 1)
    if(bIndexToRemove != -1) AppState.notebookEntries.splice(bIndexToRemove, 1)
  }

}

export const entriesService = new EntriesService()
