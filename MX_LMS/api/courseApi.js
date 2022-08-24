import baseconfig from "@/config/baseconfig.js"

// 获取课程
export function getCourse() {
	return new Promise((resolve, reject) => {
		uni.request({
			url: baseconfig + "/course",
			method: 'GET',
			success: (res) => {
				resolve(res.data)
			}
		})
	})
}
// 获取课表
export function getCurriculum(className) {
	return new Promise((resolve, reject) => {
		uni.request({
			url: baseconfig + `/curriculum/class/${className}`,
			method: 'GET',
			success: (res) => {
				resolve(res.data)
			}
		})
	})
}

// 获取课表
export const reqFindTeacherLesson = () => {
	return {
		"msg": "查询成功",
		"status": 1,
		"info": [
			// 第一节课
			[{
					"id": 1,
					"classId": 2,
					"lessonsTime": "8:00-9:40",
					"lessonsName": "计算机",
					"lessonsAddress": "二教302",
					"lessonsTeacher": "吴老师",
					"lessonsRemark": "1-17周",
					"lessonsNumber": "一",
					"weekday": "星期一"
				},
				{
					"id": 19,
					"classId": 2,
					"lessonsTime": "8:00-9:40",
					"lessonsName": "编译原理",
					"lessonsAddress": "二教302",
					"lessonsTeacher": "吴老师",
					"lessonsRemark": "1-5,8-12周",
					"lessonsNumber": "一",
					"weekday": "星期二"
				}, {
					"id": 19,
					"classId": 2,
					"lessonsTime": "8:00-9:40",
					"lessonsName": "编译原理",
					"lessonsAddress": "二教302",
					"lessonsTeacher": "吴老师",
					"lessonsRemark": "1-5,8-12周",
					"lessonsNumber": "一",
					"weekday": "星期三"
				},
				{
					"id": 19,
					"classId": 2,
					"lessonsTime": "8:00-9:40",
					"lessonsName": "编译原理",
					"lessonsAddress": "二教302",
					"lessonsTeacher": "吴老师",
					"lessonsRemark": "1-5,8-12周",
					"lessonsNumber": "一",
					"weekday": "星期三"
				},
				{
					"id": 19,
					"classId": 2,
					"lessonsTime": "8:00-9:40",
					"lessonsName": "编译原理",
					"lessonsAddress": "二教302",
					"lessonsTeacher": "吴老师",
					"lessonsRemark": "1-5,8-12周",
					"lessonsNumber": "一",
					"weekday": "星期三"
				}
			],
			// 第二节课
			[{
				"id": 19,
				"classId": 2,
				"lessonsTime": "8:00-9:40",
				"lessonsName": "编译原理",
				"lessonsAddress": "二教302",
				"lessonsTeacher": "吴老师",
				"lessonsRemark": "1-5,8-12周",
				"lessonsNumber": "一",
				"weekday": "星期二"
			}, {}, {}],
			[{
				"id": 19,
				"classId": 2,
				"lessonsTime": "8:00-9:40",
				"lessonsName": "编译原理",
				"lessonsAddress": "二教302",
				"lessonsTeacher": "吴老师",
				"lessonsRemark": "1-5,8-12周",
				"lessonsNumber": "一",
				"weekday": "星期二"
			}, {}, {}],
			[{
				"id": 19,
				"classId": 2,
				"lessonsTime": "8:00-9:40",
				"lessonsName": "编译原理",
				"lessonsAddress": "二教302",
				"lessonsTeacher": "吴老师",
				"lessonsRemark": "1-5,8-12周",
				"lessonsNumber": "一",
				"weekday": "星期二"
			}, {}, {}],
			[{}, {}, {}],
			[{}, {}, {}],
			[{}, {}, {}],
			[{}, {}, {}],
		]
	}

}
