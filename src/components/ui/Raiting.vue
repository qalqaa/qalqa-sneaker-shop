<template>
  <div class="rating">
    <svg
      v-for="star in 5"
      :key="star"
      :class="{ filled: star <= rating, hovered: star <= hoverRating }"
      class="star"
      viewBox="0 0 24 24"
      @mouseover="setHoverRating(star)"
      @mouseleave="setHoverRating(0)"
      @click="setRating(star)"
      xmlns="http://www.w3.org/2000/svg"
    >
      <path
        d="M3.612 15.443c-.386.198-.824-.149-.746-.592l.83-4.73L.173 6.765c-.329-.314-.158-.888.283-.95l4.898-.696L7.538.792c.197-.39.73-.39.927 0l2.184 4.327 4.898.696c.441.062.612.636.282.95l-3.522 3.356.83 4.73c.078.443-.36.79-.746.592L8 13.187l-4.389 2.256z"
      />
    </svg>
  </div>
</template>

<script>
export default {
  data() {
    return {
      rating: 0,
      hoverRating: 0
    }
  },
  methods: {
    setRating(star) {
      this.rating = star
    },
    setHoverRating(star) {
      this.hoverRating = star
    }
  }
}
</script>

<style scoped>
.rating {
  display: flex;
}

.star {
  transition: all 0.2s ease;
  width: 40px;
  height: 40px;
  cursor: pointer;
  fill: transparent;
  stroke: var(--vt-c-black);
  stroke-width: 1.2;
}

.star.filled {
  fill: var(--color-accent);
}

.star.hovered {
  fill: var(--color-accent-hl);
}
</style>
