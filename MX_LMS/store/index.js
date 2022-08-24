import Vue from 'vue'
import Vuex from 'vuex'
import userInfo from './userInfo'
// import createPersistedState from 'vuex-persistedstate'

Vue.use(Vuex)

export default new Vuex.Store({
	// plugins: [createPersistedState()],
	modules: {
		userInfo
	}
})
