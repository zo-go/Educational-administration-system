import baseconfig from "@/config/baseconfig.js"

// 获取班级
export const getClass = (keyword) => {
	return new Promise((resolve, reject) => {
		uni.request({
			url: baseconfig + `/class?keyword=${keyword}`,
			method: 'GET',
			data: {},
			success: (res) => {
				resolve(res)
			}
		})
	})
}
// 获取班级
export const getClasses = () => {
	return new Promise((resolve, reject) => {
		uni.request({
			url: baseconfig + `/class`,
			method: 'GET',
			data: {},
			success: (res) => {
				resolve(res)
			}
		})
	})
}
