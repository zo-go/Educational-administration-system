import App from './App'
import store from './store'
import Vue from 'vue'


import {
	getUser
} from '@/api/api.js'

Vue.prototype.$getUser = getUser

Vue.config.productionTip = false

App.mpType = 'app'
const app = new Vue({
	store,
	...App
})
app.$mount()

// #ifdef VUE3
import {
	createSSRApp
} from 'vue'
export function createApp() {
	const app = createSSRApp(App)
	return {
		app
	}
}
// #endif
