<template>
  <div class="col">
    <div class="card" style="">
      <div class="card-body bg-primary">
        <router-link :to="{name:'Board', params: {id: boardProp.id}}">
          <h3 class="card-title text-dark">
            {{ boardProp.title }}
          </h3>
        </router-link>
      </div>
    </div>
  </div>
</template>
<!------ CODE GOES HERE ------>
<script>
import { reactive, computed } from 'vue'
import { AppState } from '../AppState'
import { boardsService } from '../services/BoardsService'
import Notification from '../utils/Notification'

export default {
  name: 'Board',
  props: {
    boardProp: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      boards: computed(() => AppState.boards),
      newBoardName: {}
    })
    return {
      state,
      async deleteBoard(id) {
        try {
          if (await Notification.confrimAction('Are you sure you want to remove this board?')) {
            await boardsService.deleteBoard(id)
            await boardsService.getAllBoards()
            Notification.toast('Successfully Deleted!', 'success')
          }
        } catch (error) {
          Notification.toast('Error:' + error, 'error')
        }
      },
      async editBoard() {
        try {
          await boardsService.editBoard(state.newBoardName, props.boardProp.id)
          state.newBoardName = {}
          Notification.toast('Board Edited!', 'success')
        } catch (error) {
          Notification.toast('Error:' + error, 'error')
        }
      }
    }
  }
}

</script>
