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
