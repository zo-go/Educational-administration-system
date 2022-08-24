export function getMenu(config) {
	// console.log(config);
	// console.log(JSON.parse(config.body))
	// 先判断用户是否存在
	// 判断账号和密码是否对应

	// return {
	// 	code: 20000,
	// 	data: {
	// 		menu: [
	// 			{
	// 				path: "/home",
	// 				name: "home",
	// 				children: null,
	// 				icon: "el-icon-s-home",
	// 				title: "首页",
	// 			},
	// 			{
	// 				path: "/college",
	// 				name: "college",
	// 				children: null,
	// 				icon: "el-icon-menu",
	// 				title: "学院",
	// 			},
	// 			{
	// 				path: "/student",
	// 				name: "student",
	// 				children: null,
	// 				icon: "el-icon-menu",
	// 				title: "学生管理",
	// 			},
	// 			{
	// 				path: "/teacher",
	// 				name: "teacher",
	// 				children: null,
	// 				icon: "el-icon-menu",
	// 				title: "教师管理",
	// 			},
	// 			{
	// 				path: "/counselor",
	// 				name: "counselor",
	// 				children: null,
	// 				icon: "el-icon-menu",
	// 				title: "辅导员",
	// 			},
	// 			{
	// 				path: "/administrator",
	// 				name: "administrator",
	// 				children: null,
	// 				icon: "el-icon-menu",
	// 				title: "管理员",
	// 			},
	// 			{
	// 				path: "/excel",
	// 				name: "excel",
	// 				children: null,
	// 				icon: "el-icon-menu",
	// 				title: "文件导入导出",
	// 			},
	// 		],
	// 		message: '获取成功'
	// 	}
	// }
	// 管理员
	if (config == '管理员') {
		return {
			code: 20000,
			data: {
				menu: [
					{
						path: "/home",
						name: "home",
						children: null,
						icon: "el-icon-s-home",
						title: "首页",
					},
					{
						path: "/college",
						name: "college",
						children: null,
						icon: "el-icon-menu",
						title: "学院",
					},
					{
						path: "/student",
						name: "student",
						children: null,
						icon: "el-icon-menu",
						title: "学生管理",
					},
					{
						path: "/teacher",
						name: "teacher",
						children: null,
						icon: "el-icon-menu",
						title: "教师管理",
					},
					{
						path: "/counselor",
						name: "counselor",
						children: null,
						icon: "el-icon-menu",
						title: "辅导员",
					},
					{
						path: "/administrator",
						name: "administrator",
						children: null,
						icon: "el-icon-menu",
						title: "管理员",
					},
				],
				message: '获取成功'
			}
		}
	} else if (config == '辅导员') {
		// 辅导员
		return {
			code: 20000,
			data: {
				menu: [
					{
						path: "/home",
						name: "home",
						children: null,
						icon: "el-icon-s-home",
						title: "首页",
					},
					{
						path: "/student",
						name: "student",
						children: null,
						icon: "el-icon-menu",
						title: "学生管理",
					},
					{
						path: "/counselor",
						name: "counselor",
						children: null,
						icon: "el-icon-menu",
						title: "辅导员",
					},
					{
						path: "/teacher",
						name: "teacher",
						children: null,
						icon: "el-icon-menu",
						title: "教师管理",
					},
				],
				message: '获取成功'
			}
		}
	} else if (config == '教师') {
		return {
			code: 20000,
			data: {
				menu: [
					{
						path: "/home",
						name: "home",
						children: null,
						icon: "el-icon-s-home",
						title: "首页",
					},
					{
						path: "/student",
						name: "student",
						children: null,
						icon: "el-icon-menu",
						title: "学生管理",
					},
					{
						path: "/teacher",
						name: "teacher",
						children: null,
						icon: "el-icon-menu",
						title: "教师管理",
					},

				],
				message: '获取成功'
			}
		}
	} else if (config == '学生') {
		return {
			code: 20000,
			data: {
				menu: [
					{
						path: "/home",
						name: "home",
						children: null,
						icon: "el-icon-s-home",
						title: "首页",
					},
					{
						path: "/student",
						name: "student",
						children: null,
						icon: "el-icon-menu",
						title: "学生管理",
					},

				],
				message: '获取成功'
			}
		}
	}
	else {
		return {
			code: 20000,
			data: {
				menu: [],
				message: '获取成功'
			}
		}
	}
}
