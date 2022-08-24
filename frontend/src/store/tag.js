import Cookie from 'js-cookie'

export default {
    state: {
        isCollapse: false,
        tagList: [
            {
                path: '/home',
                name: 'home',
                title: '首页',
                icon: 'home'
            },
        ],
        // menu: [],
        currentMenu: null,
    },
    mutations: {
        collapseMenu(state) {
            state.isCollapse = !state.isCollapse
        },
        selectMenu(state, val) {
            if (val.name !== 'home') {
                state.currentMenu = val
                const result = state.tagList.findIndex(item => item.name === val.name)
                if (result === -1) {
                    state.tagList.push(val)
                } else {
                    state.currentMenu = null
                }
            }
        },
        closeTag(state, val) {
            const result = state.tagList.findIndex(item => item.name == val.name)
            state.tagList.splice(result, 1)
        }
        
    }
}