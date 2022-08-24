<template>
	<view>
		<view class="userInfo" @click="goUserInfo">
			<image class="avatar" :src="avatar"></image>
			<view class="userName">
				<text>{{studentInfo.studentName}}</text>
			</view>
		</view>
		<view class="list">
			<view class="a1" @click="goCourse">
				<image src="@/static/aigei.png"></image>
				<view>
					<text>课程</text>
				</view>
			</view>
			<view class="a1" @click="goClassInfo">
				<image src="@/static/aigei.png"></image>
				<view>
					<text>班级信息</text>
				</view>
			</view>
			<view class="a1" @click="goOutstandingWorks">
				<image src="@/static/aigei.png"></image>
				<view>
					<text>我的作品</text>
				</view>
			</view>
			<view class="a1">
				<image src="@/static/aigei.png"></image>
				<view>
					<text>小组</text>
				</view>
			</view>
			<view class="a1">
				<image src="@/static/aigei.png"></image>
				<view>
					<text>书架</text>
				</view>
			</view>
			<view class="a1" @click="gopersonalLnformation">
				<image src="@/static/aigei.png"></image>
				<view>
					<text>个人中心</text>
				</view>
			</view>
		</view>

		<view class="setUp">
			<view class="a1" @click="goSetUp">
				<image src="@/static/setUp.png"></image>
				<view>
					<text>设置</text>
				</view>
			</view>
		</view>
	</view>
</template>

<script>
	import {
		getUser
	} from '@/api/studentApi.js'
	import store from '@/store/index.js'
	export default {
		data() {
			return {
				// 头像
				avatar:"",
				userInfo: [],
				studentInfo: []
			}
		},
		beforeMount() {
			this.avatar = store.state.userInfo.avatar
			console.log(store.state.userInfo.token);
			if (!store.state.userInfo.token.accessToken) {
				uni.showModal({
					content: "请先登录",
					showCancel: false
				});
				uni.navigateTo({
					url: '/pages/login/login'
				})
			}
			this.userInfo = store.state.userInfo.userInfo
			this.studentInfo = store.state.userInfo.studentInfo

			// console.log(store.state.userInfo.studentInfo);
			// console.log(this.studentInfo);
		},
		methods: {
			// 去往用户详情页
			goUserInfo() {
				uni.navigateTo({
					url: "/pages/home/userInfo"
				})
			},
			// 去往课程页
			goCourse() {
				uni.switchTab({
					url: "/pages/course/course"
				})
			},
			// 班级页
			goClassInfo() {
				uni.navigateTo({
					url: "/pages/home/classInfo"
				})
			},
			// 我的作品
			goOutstandingWorks() {
				uni.navigateTo({
					url: "/pages/home/personalWorks"
				})
			},
			// 设置页
			goSetUp() {
				uni.navigateTo({
					url: "/pages/setUp/setUp"
				})
			},
			// 个人信息
			gopersonalLnformation() {
				uni.navigateTo({
					url: "/pages/home/personalLnformation"
				})
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

	a {
		text-decoration: none;
		color: #000000;
	}

	.router-link-active {
		text-decoration: none;
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

	.setUp {
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
			// margin-bottom: 30rpx;
		}
	}
</style>
