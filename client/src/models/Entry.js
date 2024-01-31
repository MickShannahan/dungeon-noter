import { Notebook } from "./Notebook.js"

export class Entry{
  constructor(data){
    this.id = data.id
    /** @type {string} */
    this.description = data.description
    this.img = data.img
    this.notebookId = data.notebookId
    this.notebook = data.notebook ? new Notebook(data.notebook) : null
    this.updatedAt = new Date(data.updatedAt)
  }

  get wordCount(){
    return this.description.split(' ').length
  }

  get charCount(){
    return this.description.length
  }

  get updatedTimeStamp(){
    return this.updatedAt.toLocaleString('en-US', {weekday: 'short', month:'2-digit', year: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit'}).replace(/\//g, '.')
  }

  get shortUpdate(){
    return this.updatedAt.toLocaleString('en-US', { month:'2-digit', year: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit'}).replace(/\//g, '.')
  }

  get fileName(){
    return this.img.slice(this.img.lastIndexOf('/')+1)
  }
}


