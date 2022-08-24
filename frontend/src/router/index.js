import Vue from 'vue'
import VueRouter from 'vue-router'
import routes from './routes'
import store from '@/store'

Vue.use(VueRouter)
const originalPush = VueRouter.prototype.push;
VueRouter.prototype.push = function push(location) {
  return originalPush.call(this, location).catch((err) => err)
}

const router = new VueRouter({
  routes,
  // mode: 'history',
})

// 路由守卫
router.beforeEach((to, from, next) => {
  const token = store.state.userInfo.token
  // console.log("token:" + token);
  if (!token && to.name !== 'login') {
    next({ name: 'login' })
  } else {
    next()
  }
})
export default router
