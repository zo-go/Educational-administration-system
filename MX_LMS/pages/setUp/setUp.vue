<template>
	<view>
		<view class="list">
			<view class="message">
				<uni-list>
					<uni-list-item title="消息通知" note="设置接收消息" showArrow="true" clickable @click="onClick">
					</uni-list-item>
				</uni-list>
			</view>
			<view class="voicePrompt">
				<uni-list>
					<uni-list-item title="声音提示" note="通过声音提醒您收到新消息" switchChecked="true" :show-switch="true"
						@switchChange="isVoicePrompt">
					</uni-list-item>
				</uni-list>
			</view>
			<view class="vibration">
				<uni-list>
					<uni-list-item title="震动提示" note="通过震动提醒您收到新消息" switchChecked="true" :show-switch="true"
						@switchChange="isVibration">
					</uni-list-item>
				</uni-list>
			</view>
			<view class="recentlyUsed">
				<uni-list>
					<uni-list-item title="最近使用" note="关闭后应用大厅将不会显示最近使用栏目" switchChecked="true" :show-switch="true"
						@switchChange="isRecentlyUsed">
					</uni-list-item>
				</uni-list>
			</view>
		</view>
		<view>
			<view>
				<uni-list class="logout">
					<uni-list-item title="退出登录" clickable @click="logout">
					</uni-list-item>
				</uni-list>
			</view>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {

			}
		},
		methods: {
			// 消息通知
			onClick(e) {
				// var main = plus.android.runtimeMainActivity();  
				// var Intent = plus.android.importClass("android.content.Intent");
				// var mIntent = new Intent('android.settings.WIRELESS_SETTINGS');  
				// main.startActivity(mIntent); 

				console.log(e.value)
			},
			// 声音提示
			isVoicePrompt(e) {
				console.log(e.value)
			},
			// 震动提示
			isVibration() {
				console.log("a");
			},
			// 最近使用
			isRecentlyUsed() {
				console.log("v");
			},
			logout() {
				wx.clearStorage()
				uni.navigateTo({
					url: "/pages/login/login"
				})
			}
		}
	}
</script>

<style lang="scss" scoped>
	page {
		background-color: #f0f0f0;
	}

	.message {
		// padding: 20rpx;
		background-color: #ffffff;

	}

	p {
		margin-top: 8rpx;
		color: #cbcbcb;
		font-size: 13px;
	}

	.logout {
		margin-top: 20rpx;
		display: flex;
		align-items: center;
	}
</style>
