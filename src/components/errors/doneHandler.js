import { inject, provide, ref } from 'vue'

const DoneHandlerSymbol = Symbol('DoneHandler')

export function useDoneHandlerProvider() {
  const done = ref(null)
  function handleDone(message) {
    done.value = message
  }
  provide(DoneHandlerSymbol, { done, handleDone })
  return { done, handleDone }
}

export function useDoneHandler() {
  const doneHandler = inject(DoneHandlerSymbol)
  return doneHandler
}
