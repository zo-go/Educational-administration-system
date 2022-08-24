import baseconfig from "@/config/baseconfig.js"

// 获取成绩(可根据姓名查询)
export function getScore(keyword) {
	return new Promise((resolve, reject) => {
		uni.request({
			url: baseconfig + `/score?keyword=${keyword}`,
			method: 'GET',
			success: (res) => {
				resolve(res)
			}
		})
	})
}
