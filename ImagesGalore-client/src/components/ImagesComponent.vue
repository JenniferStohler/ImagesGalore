<script>
import { reactive, computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import Notification from '../utils/Notification'
import { imagesService } from '../services/ImagesService'
import { logger } from '../utils/Logger'

export default {
  name: 'Image',
  props: {
    imageProp: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      images: computed(() => AppState.activeImage),
      activeImage: computed(() => AppState.activeImage)
    })
    onMounted(async() => {
      try {
        await imagesService.getAllImages(props.imageProp.id)
      } catch (error) {
        Notification.toast('Error:' + error, 'error')
      }
    })
    return {
      state,
      async createImage() {
        try {
          await imagesService.createImage(state.newImage)
          state.newImage = {}
          Notification.toast('Image Create!', 'success')
        } catch (error) {
          logger.log(error)
          Notification.toast('Error:' + error, 'error')
        }
      }
    }
  }
}
</script>
