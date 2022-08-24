<template>
	<view class="container">

		<view class="banner-img">
			<!-- 轮播图组件 -->
			<swiper :indicator-dots="true" :autoplay="true" :interval="5000" :duration="1000" circular="">
				<block v-for="(item,index) in swipers" :key="index">
					<swiper-item>
						<view class="swiper-item" @tap="event(index)">
							<image :src="item.src" lazy-load style="height: 350upx; width:100%"></image>
						</view>
					</swiper-item>
				</block>
			</swiper>
		</view>

		<!-- <view class="banner-bottom-box" style="width: 100%; height:10rpx; border-top:0.35rem solid #003f8c"></view> -->
		<!-- #005AB5 -->
		<view class="banner-bottom-box" style="width: 100%; height:10rpx; border-bottom:0.45rem double #c1d8de"></view>
		<!-- <view class="banner-bottom-box" style="width: 77%; height:10rpx; border-bottom:0.15rem dashed #33b1ff"></view> -->

		<!-- 登录页面效果 -->
		<view class="login-box">

			<view>
				<uni-forms ref="form" :modelValue="formData" :rules="rules">
					<view class="input1 vs-row vs-align-center margin-b40">
						<image class="input-icon margin-r20" src="/static/account.png" mode=""></image>

						<uni-forms-item name="userName">
							<view style="margin-top: 45rpx;">
								<uni-easyinput type="text" v-model="formData.userName" placeholder="请输入学号"
									style="width: 535rpx;margin-top: 19.5rpx;border-top:0.09rem solid #d5d5d5;border-right:0.09rem solid #b9b9b9;border-bottom:0.09rem solid #b9b9b9;border-radius: 5%;">
								</uni-easyinput>
							</view>

						</uni-forms-item>
					</view>

					<view class="input2 vs-row vs-align-center margin-b40">
						<image class="input-icon margin-r20" src="/static/password.png" mode=""></image>
						<view style="margin-top: 45rpx;">
							<uni-forms-item name="passWord">
								<uni-easyinput type="password" v-model="formData.passWord" placeholder="请输入密码"
									style="width: 535rpx;margin-top: 19.5rpx;border-top:0.09rem solid #d5d5d5;border-right:0.09rem solid #b9b9b9;border-bottom:0.09rem solid #b9b9b9;border-radius: 5%;">
								</uni-easyinput>
							</uni-forms-item>
						</view>
					</view>
				</uni-forms>

				<!-- <view class="banner-bottom-box" style="width: 77%; height:10%;  border-top:0.1rem dashed #49b6ff">
				</view> -->

				<view class="login-btn">
					<!-- <navigator open-type="switchTab" url="/pages/index/index" @click="$emit('toProtocol')"> -->
					<button @click="submit" class="font-60"
						style="background-image: linear-gradient(to top, #92c3ee 0%, #e7f0fd 100%);color:lightslategrey;font-size: 1.2rem;text-align: center;">登
						录</button>

					<!-- </navigator> -->
				</view>
				<!-- 其他登录方式 -->
				<view class="other">
					<view class="vs-row vs-align-center margin-b40">
						<view class="separator vs-flex-item"></view>
						<text class="color-black-3 font-28">更多登录方式：</text>
						<view class="separator vs-flex-item"></view>
					</view>

					<view class="other-items vs-row vs-align-center vs-space-around">
						<image class="other-icon" src="/static/wechat.png" mode="" @click="wxLogin"></image>
						<image class="other-icon" src="/static/qq.png" mode=""></image>
						<image class="other-icon" src="/static/apple.png" mode=""></image>
					</view>
				</view>

				<view class="wave">
					<view class="wave1"></view>
					<view class="wave2"></view>
					<view class="wave3"></view>
					<view class="wave4"></view>
				</view>

				<!-- 用户协议提示 -->
				<view class="protocol" style="font-weight: 900;">
					登录注册即代表阅读并同意
					<br />
					<span @click="$emit('toProtocol')">用户服务协议</span>
				</view>

			</view>
		</view>
	</view>
</template>

<script>
	import {
		Wxauth
	} from '@/api/api.js'
	import {
		getStudent,
		login,
		getUserInfo
	} from '@/api/studentApi.js';
	import {
		getWxUser
	} from '@/api/api.js'
	import store from '@/store/index.js'

	import {
		getClass
	} from '@/api/classApi.js'
	export default {
		data() {
			return {
				cur: 0,
				userInfo: {
					StudentName: "",
					password: ""
				},
				swipers: [{
						src: "/static/apple.png"
					},
					// {
					// 	src: "/static/md_door.jpg"
					// },
					// {
					// 	src: "/static/md_success_library.jpg"
					// },
					// {
					// 	src: "/static/md_wyl.jpg"
					// }
				],
				// 表单数据
				formData: {
					userName: '',
					passWord: ''
				},
				rules: {
					// 对userName字段进行必填验证
					// userName: {
					// 	rules: [{
					// 			required: true,
					// 			errorMessage: '请输入用户名',
					// 		},
					// 		{
					// 			minLength: 3,
					// 			maxLength: 20,
					// 			errorMessage: '用户名长度在 {minLength} 到 {maxLength} 个字符',
					// 		}
					// 	]
					// },
					// 对passWord字段进行必填验证
					// passWord: {
					// 	rules: [{
					// 		required: true,
					// 		errorMessage: '请输入密码',
					// 	}]
					// }
				},
				userInfo: []
			}
		},
		methods: {
			getUser() {
				return new Promise((resolve, reject) => {
					uni.getUserProfile({
						lang: 'zh_CN',
						desc: '用户登录', // 声明获取用户个人信息后的用途，后续会展示在弹窗中，
						success: (res) => {
							// console.log(res, 'resss')
							resolve(res.userInfo)
						},
						fail: (err) => {
							console.log("111");
							reject(err)
						}
					})
				})
			},

			getLogin() {
				return new Promise((resolve, reject) => {
					uni.login({
						success(res) {
							// console.log(res, 'res')
							resolve(res)
						},
						fail: (err) => {
							// console.log(err, 'logoer')
							reject(err)
						}
					})
				})
			},

			wxLogin() {
				let that = this;
				uni.getProvider({
					service: 'oauth',
					success: function(res) {
						//支持微信、qq和微博等
						if (~res.provider.indexOf('weixin')) {
							let userInfo = that.getUser();
							let loginRes = that.getLogin();


							Promise.all([userInfo, loginRes]).then((res) => {
								// console.log(res);
								let userInfo = res[0];
								let loginRes = res[1];
								// console.log(loginRes);
								store.commit('setAvatar', userInfo.avatarUrl)
								// console.log(loginRes);
								// wx接口路径
								// let url = 'https://api.weixin.qq.com/sns/jscode2session?appid = ' +
								// 'wx250f9dda5d5aab2f' +
								// '&secret=' + '851fa63d9c32acf88304b6d53d634e99' + '&js_code=' +
								// loginRes.code +
								// 	'&grant_type=authorization_code';

								Wxauth(loginRes.code).then(res => {
									console.log(res);
									var openid = res.data.openid
									store.commit('setOpenId', res.data.openid)
									getWxUser(openid).then(res => {
										console.log(res
											.data.data[0]);
										if (res.data.data[0]) {
											getUserInfo(res
												.data.data[0]
												.studentInfo
												.studentId).then(
												res => {
													console.log(res);
													store.commit(
														'updataClassInfo',
														res.data[0]
														.classInfo)
													// console.log(store.state
													// 	.userInfo);
												})

											store.commit(
												'updataStudentInfo',
												res
												.data.data[0]
												.studentInfo)
											store.commit('updataUserInfo',
												res
												.data
												.data[0].userInfo)
											// console.log(store.state
											// 	.userInfo.userInfo);
											var data = {
												username: store.state
													.userInfo.userInfo
													.userInfo.userName,
												password: store.state
													.userInfo.userInfo
													.userInfo.passWord
											}
											// console.log(data);
											login(data).then(res => {
												// console.log(res);
												store.commit(
													'setToken',
													res.data
													.token)
											})

											uni.switchTab({
												url: '/pages/index/index'
											})
										} else {
											uni.navigateTo({
												url: '/pages/home/userInfo'
											})
										}
									})
								})
								// let url = 'https://api.weixin.qq.com/sns/jscode2session?appid=' +
								// 	'wx250f9dda5d5aab2f' + '&secret=' +
								// 	'851fa63d9c32acf88304b6d53d634e99' + '&js_code=' + loginRes.code +
								// 	'&grant_type=authorization_code';

								// uni.request({
								// 	url: url, // 请求路径
								// 	method: 'GET', //请求方式
								// 	success: res => {
								// 		//响应成功
								// 		//这里就获取到了openid了
								// 		// console.info(res.data);
								// 		// console.info(res.data.openid);
								// 		// uni.setStorage({
								// 		// 	key: 'user',
								// 		// 	data: res.data.openid
								// 		// })

								// 		var session_key = res.data.session_key;
								// 		// console.log(session_key);
								// 		var openid = res.data.openid;
								// 		// let data = Object.assign(res.data, userInfo);
								// 		store.commit('setOpenId', openid)
								// 		store.commit('setToken', session_key)

								// 		console.log(openid);
								// 		getWxUser(openid).then(res => {
								// 			console.log(res
								// 				.data.data[0]);
								// 			if (res.data.data[0]) {
								// 				getUserInfo(res
								// 					.data.data[0]
								// 					.studentInfo
								// 					.studentId).then(
								// 					res => {
								// 						console.log(res);
								// 						store.commit(
								// 							'updataClassInfo',
								// 							res.data[0]
								// 							.classInfo)
								// 						// console.log(store.state
								// 						// 	.userInfo);
								// 					})

								// 				store.commit(
								// 					'updataStudentInfo',
								// 					res
								// 					.data.data[0]
								// 					.studentInfo)
								// 				store.commit('updataUserInfo',
								// 					res
								// 					.data
								// 					.data[0].userInfo)
								// 				// console.log(store.state
								// 				// 	.userInfo.userInfo);
								// 				var data = {
								// 					username: store.state
								// 						.userInfo.userInfo
								// 						.userInfo.userName,
								// 					password: store.state
								// 						.userInfo.userInfo
								// 						.userInfo.passWord
								// 				}
								// 				// console.log(data);
								// 				login(data).then(res => {
								// 					// console.log(res);
								// 					store.commit(
								// 						'setToken',
								// 						res.data
								// 						.token)
								// 				})

								// 				uni.switchTab({
								// 					url: '/pages/index/index'
								// 				})
								// 			} else {
								// 				uni.navigateTo({
								// 					url: '/pages/home/userInfo'
								// 				})
								// 			}
								// 		})
								// 	},
								// 	fail: err => {
								// 		console.log(err);
								// 	} //失败
								// });
							})

						}
					},
					fail: function(err) {
						uni.hideLoading();
						uni.showToast({
							icon: 'none',
							title: err
						})
					}
				})
			},
			event(index) {
				console.log("点击了 index:" + index)
			},
			submit() {
				// console.log("a");
				this.$refs.form.validate().then(res => {
					login(res).then(res => {
						if (res.data.code == 200) {
							store.commit("setToken", res.data.token)
							store.commit("updataUserInfo", this.formData);
							// console.log(store.state.userInfo.userInfo.userName);
							var keyword = store.state.userInfo.userInfo.userName
							getUserInfo(keyword).then(res => {
								store.commit("updataStudentInfo", res.data[0]
									.studentInfo)
								store.commit("updataClassInfo", res.data[0].classInfo)
								store.commit("updataAcademyInfo", res.data[0]
									.academyInfo)
								store.commit("updataSpecializedInfo", res.data[0]
									.specializedInfo)
								console.log(store.state);
							}).then(() => {
								uni.switchTab({
									url: '/pages/index/index'
								}).then(() => {
									uni.showToast({
										title: '登录成功'
									})
								})

							})
						} else {
							uni.showModal({
								content: "学号或密码错误",
								showCancel: false
							})
						}
					})
				}).catch(err => {
					console.log('表单错误信息：', err);
				})
			}
		},
		created() {}
	}
</script>

<style lang="scss" scoped>
	.other {
		width: 100%;
		// position: absolute;
		margin-top: 50rpx;
		z-index: 100;
		// background-color: #2CD8D5;

		&-items {
			padding: 0 200rpx;
		}

		&-icon {
			width: 65rpx;
			height: 65rpx;
			margin-left: 50rpx;
		}
	}

	.wave {
		// border: 0.2rem double #186cda;
		// position: relative;
		// border: 1px solid silver;
		// 颜色节点均匀分布的径向渐变：
		// background-image: radial-gradient(#fff, rgba($color:#32D74B, $alpha:0.6));
		background-image: radial-gradient(#fff);
		width: 725rpx;
		height: 625rpx;
		border-radius: 50%;
		line-height: 100rpx;
		margin: 0 auto;
		font-size: 28rpx;
		text-align: center;
		// overflow: hidden;
		animation: water-wave linear infinite;
	}

	.wave1 {
		position: absolute;
		top: 80%;
		left: -100%;
		background-image: linear-gradient(-225deg, #2CD8D5 0%, #C5C1FF 56%, #FFBAC3 100%);
		opacity: .7;
		width: 300%;
		height: 300%;
		border-radius: 50%;
		animation: inherit;
		animation-duration: 12s;
	}

	.wave2 {
		position: absolute;
		top: 80%;
		left: -100%;
		background-image: linear-gradient(-225deg, #756d93 0%, #3584A7 51%, #30D2BE 100%);
		opacity: .7;
		width: 300%;
		height: 300%;
		border-radius: 45%;
		animation: inherit;
		animation-duration: 25s;
	}

	.wave3 {
		position: absolute;
		top: 80%;
		left: -65%;
		background-image: linear-gradient(to top, #37ecba 0%, #72afd3 100%);
		opacity: .3;
		width: 200%;
		height: 200%;
		border-radius: 40%;
		animation: inherit;
		animation-duration: 15s;
	}

	@keyframes water-wave {
		0% {
			transform: rotate(0deg);
		}

		50% {
			transform: rotate(-180deg);
		}

		100% {
			transform: rotate(360deg);
		}
	}

	.container {
		background-image: linear-gradient(to top, #accbee 0%, #e7f0fd 0%);
	}

	.banner-img {
		background-color: #2986de;
		width: 100%;
	}

	.login-box {
		background-image: linear-gradient(to top, #accbee 0%, #e7f0fd 80%);
		// margin-top: 10%;
		padding-top: 1.5%;
	}

	.input {

		&-icon {
			width: 30rpx;
			height: 38rpx;
			margin-right: 5%;
		}
	}

	.input1,
	.input2 {
		// border-left: 8rpx double #7d9bff;
		display: flex;
		// justify-content: center;
		align-items: center;
		border-left: 15rpx double #ffffff;
		border-bottom: 7.5rpx solid #ffffff;
		background-image: linear-gradient(45deg, #c0d3f7 0%, #c8e0f7 50%, #e4efe9 100%);
		margin: 20rpx;
		margin-bottom: 6.5% !important;
		width: 640rpx;
		height: 110rpx;
		padding-left: 25rpx !important;
		padding-right: 30rpx !important;
		padding: 2rpx;
		border-radius: 2%;
		// border: 0.1rem solid #333333;
		display: flex;
		align-items: center;
	}

	.protocol {
		position: fixed;
		bottom: 5%;
		font-size: 1em;
		color: #333333;
		margin-left: 25%;
		text-align: center;

		span {
			color: #2986de;
		}
	}

	.login-btn {
		// background-image: linear-gradient(to top, #cfd9df 0%, #e2ebf0 100%);
		background-color: #9bd6e7;
		// background-image: linear-gradient(to top, #accbee 0%, #e7f0fd 100%);
		// position: absolute;
		width: 50%;
		margin-top: 3% !important;
		margin-left: 25%;
		font-size: 5rem;
		color: #242424;
		text-align: center;
		border: 0.25rem double #98bdde;
		border-radius: 5%;
	}

	.bg {
		position: relative;
		width: 0rpx;
		height: 400rpx;
	}

	.tab {
		position: absolute;
		top: 250rpx;
		left: 80rpx;
		right: 440rpx;
		height: 120rpx;
		padding: 0 50rpx;
		background-color: #b4ffcc;
		border-top-left-radius: 30rpx;
		border-top-right-radius: 30rpx;
		font-size: 2em;
		border-radius: 10%;


		&-bg {
			position: absolute;
			right: -50rpx;
			width: 900rpx;
			height: 280rpx;
		}
	}

	.tab1 {
		position: absolute;
		top: 250rpx;
		left: 400rpx;
		right: 110rpx;
		height: 120rpx;
		padding: 0 50rpx;
		background-color: #b4ffcc;
		border-top-left-radius: 30rpx;
		border-top-right-radius: 30rpx;
		font-size: 2em;
		border-radius: 10%;
	}

	.tab2 {
		position: absolute;
		top: 250rpx;
		left: 400rpx;
		right: 110rpx;
		height: 120rpx;
		padding: 0 50rpx;
		background-color: #b4ffcc;
		border-top-left-radius: 30rpx;
		border-top-right-radius: 30rpx;
		font-size: 2em;
		border-radius: 10%;
	}

	.line {
		width: 20rpx;
		height: 0rpx;
	}

	.color-black-3 {
		color: #ffffff;
		margin-left: 300rpx;
	}

	.other-items {
		margin-top: 25rpx;
		// background-color: #ffffff;
	}
</style>
