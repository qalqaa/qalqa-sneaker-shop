<script setup>
import { useRouter } from 'vue-router'
import DrawerHead from '@/components/DrawerHead.vue'

const router = useRouter()

const goBack = () => {
  router.push('/')
}
</script>

<template>
  <DrawerHead :back-to-func="goBack" :title="'Профиль'" />
</template>