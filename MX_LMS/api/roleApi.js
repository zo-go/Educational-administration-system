import baseconfig from "@/config/baseconfig.js"

// 获取角色
export function getRole(keyword)  {
	return new Promise((resolve, reject) => {
		uni.request({
			url: baseconfig + `/role?keyword=${keyword}`,
			method: 'GET',
			data: {},
			success: (res) => {
				resolve(res.data)
			}
		})
	})
}
