
import Cookie from 'js-cookie'

export default {
    state: {
        token: "",
        userInfo: {},
        roleName: "",
        menu: [],
    },
    getters: {
    },
    mutations: {
        setToken(state, newToken) {
            state.token = newToken
        },
        updataUserInfo(state, payload) {
            state.userInfo = payload
        },

        setMenu(state, val) {
            state.menu = val
            // Cookie.set('menu', JSON.stringify(val))
            localStorage.setItem('menu', JSON.stringify(val))
        },
        // 角色
        setRoleName(state, val) {
            state.roleName = val
            // Cookie.set('menu', JSON.stringify(val))
            localStorage.setItem('roleName', JSON.stringify(val))
        },
        clearMenu(state) {
            state.menu = []
            // Cookie.remove('menu')
            localStorage.removeItem('menu')
        },
        addMenu(state, router) {
            // !Cookie.get('menu')
            // localStorage.getItem('meun')
            if (! localStorage.getItem('meun')) {
                return
            }
            const menu = JSON.parse(localStorage.getItem('meun'))
            state.menu = menu
            const menuArray = []
            menu.forEach(item => {
                if (item.children) {
                    item.children = item.children.map(item => {
                        item.component = () => import(`../views/${item.url}`)
                        return
                    })
                    menuArray.push(...item.children)
                } else {
                    item.component = () => import(`../views/${item.url}`)
                    menuArray.push(item)
                }
                // console.log(menu);
            })
            menuArray.forEach(item => {
                router.addRoute('Main', item)
            })
        }
    },
}