<template>
  <div class="home flex-grow-1 d-flex flex-column align-items-center justify-content-center bg-secondary">
    <router-link :to="{ name:'Boards' }" class="nav-link">
      <button type="button" class="btn btn-primary" title="Go to your boards">
        Go to Your Boards
      </button>
    </router-link>
  </div>
</template>

<script>
export default {
  name: 'Home'
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
</style>
