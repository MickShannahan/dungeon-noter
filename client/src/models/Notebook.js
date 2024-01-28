export class Notebook{
  constructor(data){
    this.id = data.id
    this.title = data.title
    this.coverImg = data.coverImg
    this.icon = data.icon
    this.color = data.color
    this.entryCount = data.entryCount
    this.creatorId = data.creatorId
    this.creator = data.creator
    this.createdAt = new Date(data.createdAt)
    this.updatedAt = new Date(data.updatedAt)
  }

  get updatedTimeStamp(){
    return this.updatedAt.toLocaleString('en-US', {weekday: 'long', month:'short', year: 'numeric', day: 'numeric', hour: '2-digit', minute: '2-digit'}).replace(/\//g, '.')
  }
  get createdTimeStamp(){
    return this.createdAt.toLocaleString('en-US', {weekday: 'long', month:'short', year: 'numeric', day: 'numeric'}).replace(/\//g, '.')
  }
}
