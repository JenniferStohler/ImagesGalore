<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col text-center mt-5 mb-6 text-dark">
        <h1 class="">
          {{ state.board.title }}
        </h1>
      </div>
    </div>
  </div>
</template>
<!----------------------------------------------------------------------------------------------->
<script>
import { reactive, computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { boardsService } from '../services/BoardsService'
import Notification from '../utils/Notification'
import { useRoute } from 'vue-router'
import ImagesComponent from '../components/ImagesComponent.vue'
import { imagesService } from '../services/ImagesService'

export default {
  name: 'Board',

  setup() {
    const route = useRoute()
    const state = reactive({
      board: computed(() => AppState.activeBoard),
      images: computed(() => AppState.images)
    })

    onMounted(async() => {
      try {
        await boardsService.getBoardById(route.params.id)
        await imagesService.getAllImages(route.params.id)
      } catch (error) {
        Notification.toast('Error' + error, 'error')
      }
    })
    return {
      state,
      async createImage() {
        try {
          state.newImage.boardId = route.params.id
          await imagesService.createImage(state.newImage)
          state.newImage = {}
          await imagesService.getAllImages(route.params.id)
          Notification.toast('Image Created!', 'success')
        } catch (error) {
          Notification.toast('Error' + error, 'error')
        }
      }
    }
  },
  components: {
    ImagesComponent
  }
}
</script>
