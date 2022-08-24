<template>
	<view>
		<view class="list">
			<uni-forms ref='form' :modelValue="userInfo" :rules="rules">
				<uni-forms-item label="学号" name="userName">
					<uni-easyinput v-model="userInfo.userName" placeholder="请输入学号"></uni-easyinput>
				</uni-forms-item>
				<!-- <uni-forms-item label="密码" name="passWord">
					<uni-easyinput v-model="userInfo.passWord" placeholder="请输入密码"></uni-easyinput>
				</uni-forms-item> -->
			</uni-forms>
			<button @click="submit">保存</button>
		</view>
	</view>
</template>

<script>
	import store from '@/store/index.js'
	import {
		updateUser,
		getWxUser
	} from '@/api/api.js'
	import {
		getRole
	} from '@/api/roleApi.js'
	export default {
		data() {
			return {
				userInfo: {
					userName: "",
				},
				// 班级信息
				classes: [],
				// 学院信息
				academyInfo: {},
				// 数据规则
				rules: {
					// 对name字段进行必填验证
					studentName: {
						rules: [{
							required: true,
							errorMessage: '请输入姓名',
						}]
					},
					userName: {
						rules: [{
							required: true,
							errorMessage: '请输入学号',
						}]
					},
					passWord: {
						rules: [{
							required: true,
							errorMessage: '请输入密码',
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
							errorMessage: '请输入班级',
						}]
					},
				},
			}
		},
		methods: {
			// 绑定学生
			submit() {
				this.$refs.form.validate().then(res => {
					// console.log(store.state.userInfo);
					// this.userInfo.openId = store.state.userInfo.openId
					// console.log(this.userInfo.userName);

					getWxUser(this.userInfo.userName).then(res => {
						console.log(res);
						// let id = res.data.data[0].studentInfo.id
						var data = {}
						data.id = res.data.data[0].userInfo.userInfo.id
						data.userName = this.userInfo.userName
						data.passWord = res.data.data[0].userInfo.userInfo.passWord
						data.roleId = res.data.data[0].userInfo.roleInfo.id
						data.openId = store.state.userInfo.openId
						console.log(data);
						updateUser(data.id, data).then(res => {
							console.log(res);
							if (res.data.code == 200) {
								uni.showModal({
									title: "绑定成功",
									content: "请重新登录",
									showCancel: false
								})
								uni.navigateTo({
									url: '/pages/login/login'
								})
							}
						})
						// if (!res.data.data[0].userInfo.userInfo.openId) {
						// 	updateUser(id,data).then(res => {
						// 		console.log(data);
						// 		if (res.data.code == 200) {
						// 			uni.showModal({
						// 				title: "绑定成功",
						// 				showCancel: false
						// 			})
						// 		}
						// 	})
						// }else{
						// 	console.log("11");
						// }
					})
					// updateUser(this.userInfo).then(res => {
					// 	console.log(res);
					// })

				}).catch(err => {
					console.log('表单错误信息：', err);
				})
			},

		},
		beforeMount() {
			
			console.log(store.state.userInfo);
			if (!store.state.userInfo.studentInfo.studentId) {
				uni.showModal({
					content: "请绑定学号",
					showCancel: false
				})
			}else{
				this.userInfo.userName = store.state.userInfo.studentInfo.studentId
			}
			getRole('学生').then(res => {
				this.userInfo.id = res.data[0].id
			})	
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
		padding-left: 20rpx;
	}

	.save {
		margin-top: 20rpx;
		display: flex;
		align-items: center;
	}
</style>
