import { inject, provide, ref } from 'vue'

const ErrorHandlerSymbol = Symbol('ErrorHandler')

export function useErrorHandlerProvider() {
  const error = ref()

  function handleError(err) {
    let errorMessage = ''

    if (err.response) {
      switch (err.response.status) {
        case 400:
          errorMessage = 'Ошибка 400: Неверный запрос'
          break
        case 401:
          errorMessage = 'Ошибка 401: Неавторизован'
          break
        case 404:
          errorMessage = 'Ошибка 404: Не найдено'
          break
        case 500:
          errorMessage = 'Ошибка 500: Внутренняя ошибка сервера'
          break
        default:
          errorMessage = `Ошибка ${err.response.status}: ${err.response.data.message}`
      }
    } else if (err.request) {
      errorMessage = 'Ошибка: Сервер не ответил'
    } else {
      errorMessage = `Ошибка: ${err.message}`
    }

    error.value = errorMessage

    setTimeout(() => {
      error.value = null
    }, 5000)
  }

  provide(ErrorHandlerSymbol, { error, handleError })
  if (!error.value) {
    return { error, handleError }
  }
}

export function useErrorHandler() {
  const errorHandler = inject(ErrorHandlerSymbol)
  if (!errorHandler) {
    throw new Error('useErrorHandler must be used within a ErrorHandlerProvider')
  }
  return errorHandler
}
