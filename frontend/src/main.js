import Vue from 'vue'
import App from './App.vue'

import router from './router'
import store from './store'

import 'default-passive-events'
// 引入elmentui 相关内容
import axios from 'axios'
import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
// 引入全局配置样式
import '@/assets/global.less'
// 注册组件
// 引入富文本框
import VueQuillEditor from 'vue-quill-editor'
// import marked from 'marked'

// 引入日期时间工具包
import './utils/dateHelper'

// require styles
import 'quill/dist/quill.core.css'
import 'quill/dist/quill.snow.css'
import 'quill/dist/quill.bubble.css'

Vue.use(VueQuillEditor),

  Vue.prototype.$http = axios
// axios.defaults.headers.get['Content-Type'] = 'application/json;charset=UTF-8';
Vue.config.productionTip = false
Vue.use(ElementUI);
new Vue({
  router,
  store,
  render: (h) => h(App)
}).$mount('#app')
