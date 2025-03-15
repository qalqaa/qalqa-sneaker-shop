import axios from 'axios'

export const API_URL = 'https://qalqa.tw1.ru'

const api = axios.create({
  withCredentials: true,
  baseURL: API_URL
})

api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token')
    if (token) {
      config.headers.Authorization = `Bearer ${token}`
    }
    return config
  },
  (error) => {
    return Promise.reject(error)
  }
)

api.interceptors.response.use(
  (response) => {
    return response
  },
  (error) => {
    if (error.response.status === 401) {
      // Логика обработки ошибки авторизации (например, перенаправление на страницу логина)
      console.error('Неавторизован! Пожалуйста, войдите в систему.')
      // Можно удалить токен из localStorage и перенаправить пользователя на страницу входа
      localStorage.removeItem('token')
    }
    return Promise.reject(error)
  }
)

export default api
