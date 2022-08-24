export default {
	state: {
		token: '',
		// 头像
		avatar: "",
		// 用户信息
		userInfo: {},
		// 学生信息
		studentInfo: {},
		// 班级信息
		classInfo: {},
		// 学院信息
		academyInfo: {},
		// 专业信息
		specializedInfo: {},
		// openId
		openId: '',
		// 成绩
		results: []
	},
	mutations: {
		setToken(state, newToken) {
			state.token = newToken
			wx.setStorage({
				key: 'token',
				data: newToken
			})
		},
		setResults(state, payload) {
			state.results = payload
			wx.setStorage({
				key: 'results',
				data: payload
			})
		},
		setOpenId(state, payload) {
			state.openId = payload
			wx.setStorage({
				key: 'openId',
				data: payload
			})
		},
		setAvatar(state, payload) {
			state.avatar = payload
			wx.setStorage({
				key: 'avatar',
				data: payload
			})
		},
		updataUserInfo(state, payload) {
			state.userInfo = payload
			wx.setStorage({
				key: 'userInfo',
				data: payload
			})
		},
		updataStudentInfo(state, payload) {
			state.studentInfo = payload
			wx.setStorage({
				key: 'studentInfo',
				data: payload
			})
		},
		updataClassInfo(state, payload) {
			state.classInfo = payload
			wx.setStorage({
				key: 'classInfo',
				data: payload
			})
		},
		updataAcademyInfo(state, payload) {
			state.academyInfo = payload
			wx.setStorage({
				key: 'academyInfo',
				data: payload
			})
		},
		updataSpecializedInfo(state, payload) {
			state.specializedInfo = payload
			wx.setStorage({
				key: 'specializedInfo',
				data: payload
			})
		},
	}
}
