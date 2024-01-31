import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'
import { api } from './AxiosService.js'


class ImageUploadService{

  async uploadImage(file){
    try {
      const payload = new FormData()
      payload.append('file', file, file.name)
      const res = await api.post('https://cw-upload-demo.azurewebsites.net/api/imageUpload', payload)
      logger.log('🖼', res)
      return res.data
    } catch (error) {
      Pop.error(error, '[Upload image]')
    }
  }

  async deleteImage(fileName){
    try {
      const res = await api.delete(`https://cw-upload-demo.azurewebsites.net/api/deleteBlob?fileName=${fileName}`)
      logger.log(res.data)
    } catch (error) {
      Pop.error('[Delete Image]')
    }

  }

}

export const imageUploadService = new ImageUploadService()
