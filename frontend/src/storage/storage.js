import { createStore } from 'vuex'

const store = createStore({
  state() {
    return {
      data: {
        favorite: false,
        favoriteid: null
      }
    }
  },
  mutations: {
    setFavorite(state, payload) {
      state.data.favorite = payload
    },
    setFavoriteId(state, payload) {
      state.data.favoriteid = payload
    }
  }
})

export default store
