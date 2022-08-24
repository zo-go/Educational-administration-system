import baseconfig from "@/config/baseconfig.js"

// 获取用户
export const getUsers = () => {
	return new Promise((resolve, reject) => {
		uni.request({
			url: baseconfig + "/user",
			method: 'GET',
			data: {},
			success: (res) => {
				resolve(res)
			}
		})
	})
}

// 更新用户
export const updateUser = (id, data) => {
	return new Promise((resolve, reject) => {
		uni.request({
			url: baseconfig + `/user/${id}`,
			method: 'PUT',
			data: data,
			success: (res) => {
				resolve(res)
			}
		})
	})
}

// 查找用户
export const getWxUser = (keyword) => {
	return new Promise((resolve, reject) => {
		uni.request({
			url: baseconfig + `/user?keyword=${keyword}`,
			method: 'GET',
			success: (res) => {
				resolve(res)
			}
		})
	})
}

// 查找用户
export const Wxauth = (code) => {
	return new Promise((resolve, reject) => {
		uni.request({
			url: baseconfig + `/wxauth`,
			method: 'GET',
			data: {
				json_code: code
			},
			success: (res) => {
				resolve(res)
			}
		})
	})
}
