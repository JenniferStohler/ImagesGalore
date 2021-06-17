import { AppState } from '../AppState'
import { api } from './AxiosService'

class BoardsService {
  async getAllBoards() {
    const res = await api.get('api/boards')
    AppState.boards = res.data
  }

  async getBoardbyId(id) {
    const res = await api.get('api/boards' + id)
    AppState.activeBoardard = res.data
  }

  async createBoard(data) {
    await api.post('api/boards/', data)
  }

  async editBoard(data, id) {
    await api.put('api/boards/' + id, data)
  }

  async deleteBoard(id) {
    await api.delete('api/boards/' + id)
  }
}

export const boardsService = new BoardsService()
