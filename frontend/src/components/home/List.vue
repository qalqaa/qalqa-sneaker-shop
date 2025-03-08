<script setup>
import Card from '../card/Card.vue'

defineProps({
  items: Array,
  isFavorites: Boolean
})
</script>
<template>
  <div class="grid min-[450px]:grid-col-1 sm:grid-cols-2 md:grid-cols-3 gap-5" v-auto-animate>
    <Card
      v-for="item in items"
      :key="item.id"
      :id="item.id"
      :title="item.title"
      :img-url="item.imageUrl"
      :price="item.price"
      :isFavorite="item.isFavorite"
      :isAdded="item.isAdded"
    />
  </div>
</template>
