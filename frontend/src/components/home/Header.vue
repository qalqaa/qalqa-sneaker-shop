<script setup>
const emit = defineEmits(['openCart'])

defineProps({
  totalPrice: Number
})
</script>

<template>
  <header class="flex justify-between border-b border-zinc-500 px-4 sm:px-8 py-3">
    <router-link to="/"
      ><div class="flex items-center">
        <img src="/src/assets/imgs/logo.png" alt="Logo" class="w-10 mr-4" />
        <div>
          <h1 class="c-accent font-bold accent-shadow-text text-2xl">q{\}qa Sneakers</h1>
          <p class="md:block hidden">Магазин лучших кросовок</p>
        </div>
      </div></router-link
    >

    <ul class="flex items-center gap-5">
      <li
        @click="() => emit('openCart')"
        class="flex items-center gap-3 cursor-pointer hover-accent"
      >
        <img src="/src/assets/imgs/cart.svg" alt="Cart" />
        <b class="md:block hidden">{{ totalPrice }} ₽</b>
      </li>
      <router-link to="/favorites">
        <li class="flex items-center gap-3 cursor-pointer hover-accent">
          <img src=" /src/assets/imgs/heart.svg" alt="Cart" />
          <span class="md:block hidden">Избранное</span>
        </li>
      </router-link>
      <router-link to="/profile">
        <li class="flex items-center gap-3 cursor-pointer hover-accent">
          <img src=" /src/assets/imgs/profile.svg" alt="Cart" />
          <span class="md:block hidden">Профиль</span>
        </li>
      </router-link>
    </ul>
  </header>
</template>
