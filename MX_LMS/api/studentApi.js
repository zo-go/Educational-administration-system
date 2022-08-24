import baseconfig from "@/config/baseconfig.js"

// 登录并获得token
export function login(data) {
	return new Promise((resolve, reject) => {
		uni.request({
			url: baseconfig + "/login",
			method: 'POST',
			data: data,
			success: (res) => {
				resolve(res)
			}
		})
	})
}

// 获取学生信息
export const getStudent = () => {
	return new Promise((resolve, reject) => {
		uni.request({
			url: baseconfig + "/student",
			method: 'GET',
			data: {},
			success: (res) => {
				resolve(res.data)
			}
		})
	})
}
// 获取个人信息
export function getUserInfo(keyword) {
	return new Promise((resolve, reject) => {
		uni.request({
			url: baseconfig + `/student?keyword=${keyword}`,
			method: 'GET',
			success: (res) => {
				resolve(res.data)
			}
		})
	})
}

// 添加学生信息
export function addStudent(data) {
	return new Promise((resolve, reject) => {
		uni.request({
			url: baseconfig + `/student`,
			method: 'POST',
			data: data,
			success: (res) => {
				resolve(res.data)
			}
		})
	})
}

// 更新学生信息
export function updateStudent(id, data) {
	return new Promise((resolve, reject) => {
		uni.request({
			url: baseconfig + `/student/${id}`,
			method: 'PUT',
			data: data,
			success: (res) => {
				resolve(res.data)
			}
		})
	})
}
