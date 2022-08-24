<template>
	<view>
		<view class="userInfo">
			<image class="avatar" :src="avatar"></image>
			<view class="userName">
				<text>{{userInfo.studentName}}</text>
			</view>
		</view>
		<view class="list">
			<uni-forms ref='form' :modelValue="userInfo" :rules="rules">
				<uni-forms-item label="姓名" name="studentName">
					<uni-easyinput v-model="userInfo.studentName" placeholder="请输入姓名"></uni-easyinput>
				</uni-forms-item>
				<uni-forms-item label="身份证号" name="idNumber">
					<uni-easyinput v-model="userInfo.idNumber" placeholder="请输入身份证"></uni-easyinput>
				</uni-forms-item>
				<uni-forms-item label="入学时间" name="enrollmentTime">
					<uni-easyinput v-model="userInfo.enrollmentTime" placeholder="请输入入学时间"></uni-easyinput>
				</uni-forms-item>
				<uni-forms-item label="性别" name="sex">
					<uni-data-checkbox v-model="userInfo.sex" :localdata="range" placeholder="请选择性别" @change="change">
					</uni-data-checkbox>
				</uni-forms-item>
				<uni-forms-item label="班级" name="classId">
					<uni-data-checkbox v-model="userInfo.classId" :localdata="classes" placeholder="请选择班级"
						@change="classChange">
					</uni-data-checkbox>
				</uni-forms-item>

			</uni-forms>
			<button @click="submit">保存</button>
		</view>
	</view>
</template>

<script>
	import {
		addStudent,
		getUserInfo,
		updateStudent
	} from '@/api/studentApi.js'
	import {
		getClasses
	} from '@/api/classApi.js'
	import store from '@/store/index.js'
	export default {
		data() {
			return {
				avatar: "",
				userInfo: {
					studentName: "",
					sex: 0,
					idNumber: "",
					enrollmentTime: "",
					classId: 0
				},
				value: "男",
				range: [{
					"value": "男",
					"text": "男"
				}, {
					"value": "女",
					"text": "女"
				}],
				classInfo: "",
				classes: [],
				rules: {
					// 对name字段进行必填验证
					studentName: {
						rules: [{
							required: true,
							errorMessage: '请输入姓名',
						}]
					},
					sex: {
						rules: [{
							required: true,
							errorMessage: '请选择性别',
						}]
					},
					idNumber: {
						rules: [{
							required: true,
							errorMessage: '请输入身份证',
						}]
					},
					enrollmentTime: {
						rules: [{
							required: true,
							errorMessage: '请输入入学年份',
						}]
					},
					classId: {
						rules: [{
							required: true,
							errorMessage: '请选择班级',
						}]
					},
				},
			}
		},
		created() {
			this.avatar = store.state.userInfo.avatar
			// console.log(store.state.userInfo.studentInfo.studentId);
			getClasses().then(res => {
				console.log(res);
				res.data.data.forEach(item => {
					var data = {}
					data.text = item.classinfo.className
					data.value = item.classinfo.id
					// console.log(data);
					this.classes.push(data)
				})
			})
			let id = store.state.userInfo.studentInfo.studentId
			getUserInfo(id).then(res => {
				console.log(res);
				this.userInfo.studentName = res.data[0].studentInfo.studentName
				this.userInfo.sex = res.data[0].studentInfo.sex
				this.userInfo.idNumber = res.data[0].studentInfo.idNumber
				this.userInfo.enrollmentTime = res.data[0].studentInfo.enrollmentTime
				this.userInfo.classId = res.data[0].classInfo.id
				console.log(this.userInfo);
			})

		},
		methods: {
			submit() {
				this.$refs.form.validate().then(res => {
					// console.log(res);
					var id = store.state.userInfo.studentInfo.id
					// console.log(id);
					updateStudent(id,res).then(res => {
						console.log(res);
					})
				}).catch(err => {
					console.log('表单错误信息：', err);
				})
			},
			change(e) {
				console.log(e);
			},
			classChange(e) {
				console.log(e);
			}
		}
	}
</script>

<style lang="scss" scoped>
	page {
		background-color: #f0f0f0;
	}

	.userInfo {
		display: flex;
		padding: 20rpx;
		background-color: #ffffff;
		align-items: flex-start;

		.avatar {
			width: 100rpx;
			height: 100rpx;
			border-radius: 50%;
		}

		.userName {
			margin-top: 25rpx;
			margin-left: 40rpx;
		}
	}

	.list {
		margin-top: 15rpx;
		// display: flex;
		padding: 25rpx;
		background-color: #ffffff;
		// align-items: center;

		image {
			width: 60rpx;
			height: 60rpx;
		}

		text {
			margin-left: 25rpx;
		}

		.a1 {
			display: flex;
			align-items: center;
			margin-bottom: 30rpx;
		}
	}
</style>
