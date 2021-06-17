<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col text-center text-dark">
        <h1 class="">
          New Images? Add them to your board.
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
            <button type="submit" class="btn btn-success" title="Click to Add New Board">
              Add Board
            </button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>
