<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col text-center text-dark">
        <h1 class="">
          Your Boards
        </h1>
      </div>
    </div>
    <div class="row mb-5 mr-4">
      <!---CREATE BOARD--->
      <form @submit.prevent="createBoard">
        <div class="form-row align-items-center">
          <div class="col-auto">
            <label class="sr-only" for="inlineFormInput">Add New Board</label>
            <input type="text"
                   class="form-control"
                   id="inlineFormInput"
                   placeholder="Add New Board"
                   v-model="state.newBoard.title"
            >
          </div>
          <div class="col">
            <button type="submit" class="btn btn-success" title="Add New Board">
              Add Board
            </button>
          </div>
        </div>
      </form>
    </div>
    <div class="row">
      <BoardComponent v-for="board in state.boards" :key="board.id" :board-prop="board" />
    </div>
  </div>
</template>

<!------JS CODE STARTS HERE ------->
<script>
import BoardComponent from '../components/BoardsComponent'
import { reactive, computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { boardsService } from '../services/BoardsService'
import Notification from '../utils/Notification'

export default {
  name: 'Boards',

  setup() {
    const state = reactive({
      boards: computed(() => AppState.boards),
      newBoard: {}
    })
    onMounted(async() => {
      try {
        await boardsService.getAllBoards()
      } catch (error) {
        Notification.toast('Error:' + error, 'error')
      }
    })
    return {
      state,
      async createBoard() {
        try {
          await boardsService.createBoard(state.newBoard)
          state.newBoard = {}
          await boardsService.getAllBoards()
          Notification.toast('Board Created!', 'success')
        } catch (error) {
          Notification.toast('Error:' + error, 'error')
        }
      }
    }
  },
  components: {
    BoardComponent
  }
}
