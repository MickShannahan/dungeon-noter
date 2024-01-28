import { AppState } from "../AppState.js"
import { Notebook } from "../models/Notebook.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"



class NotebooksService{

  async createNotebook(notebookData){
    // TODO upload image
    const res = await api.post('api/notebooks', notebookData)
    const notebook = new Notebook(res.data)
    AppState.notebooks.push(notebook)
    return notebook
  }

  async getAccountNotebooks(){
    const res = await api.get('api/notebooks')
    const notebooks = res.data.map(n => new Notebook(n))
    AppState.notebooks = notebooks
    logger.table('notebooks', notebooks)
  }

  async getNotebookById(notebookId){
    AppState.activeNotebook = null
    const res = await api.get(`api/notebooks/${notebookId}`)
    const notebook = new Notebook(res.data)
    AppState.activeNotebook = notebook
  }

  async updateNotebook(notebookData){
    // TODO new image?
    const res = await api.put(`api/notebooks/${notebookData.id}`, notebookData)
    const update =new Notebook(res.data)
    AppState.activeNotebook = update
  }

  async deleteNotebook(notebookId){
    //TODO delete image?
    const res = await api.delete(`api/notebooks/${notebookId}`)
    AppState.activeNotebook = null
    const indexToRemove = AppState.notebooks.findIndex(n => n.id == notebookId)
    AppState.notebooks.splice(indexToRemove, 1)
  }
}


export const notebooksService = new NotebooksService()
