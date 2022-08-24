// import request from "./request.js"
import baseconfig from "@/config/baseconfig.js"
import store from '@/store/index.js'

// 获取问卷
export const getQuestionnaire = (data) => {
	return new Promise((resolve, reject) => {
		uni.request({
			url: baseconfig +
				`/questionnaire?pageIndex=${data.pageIndex}&&pageSize=${data.pageSize}`,
			method: 'GET',
			data: data,
			success: (res) => {
				resolve(res)
			}
		})
	})
}

//提交问卷
export const addQuestionnaireRecord = (data) => {
	return new Promise((resolve, reject) => {
		uni.request({
			url: baseconfig + `/questionnaireres`,
			header: {
				'Authorization': "Bearer " + store.state.userInfo.token.accessToken
			},
			method: 'POST',
			data: data,
			success: (res) => {
				resolve(res)
			}
		})
	})
}

export const getQuestionnaireRecord = (data) => {
	return new Promise((resolve, reject) => {
		uni.request({
			url: baseconfig + `/questionnaire/${data}`,
			method: 'GET',
			data: data,
			success: (res) => {
				resolve(res)
			}
		})
	})
}

// export function getQuestionnaireRecord(data) {
// 	return new Promise((resolve, reject) => {
// 			uni.request({
// 				url: baseconfig + `/questionnaireres`,
// 				method: 'POST',
// 				data: data,
// 				success: (res) => {
// 					resolve(res)
// 				}
// 			})
// 		}
// 	})


export function getQuestionnaireRecordaa(data) {
	return new Promise((resolve, reject) => {
		uni.request({
			url: baseconfig + `/questionnaireres+${data}`,
			method: 'GET',
			data: data,
			success: (res) => {
				resolve(res)
			}
		})
	})
}


// export const addQuestionnaireRecord = (data) => {
// 	return new Promise((resolve, reject) => {
// 		uni.request({
// 			url: baseconfig + `/questionnaireres`,
// 			method: 'POST',
// 			data: data,
// 			success: (res) => {
// 				resolve(res)
// 			}
// 		})
// 	})
// }

// export function getQuestionnaire(data) {
// 	return request.get(`/questionnaire?pageIndex=${data.pageIndex}&&pageSize=${data.pageSize}`)
// }

// export function getQuestionnaireRecord(data) {
// 	return request.get(`/questionnaire/${data}`)
// }

// export function addQuestionnaireRecord(data) {
// 	return request.post(`/questionnaireres`, data)
// }
