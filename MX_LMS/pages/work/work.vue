<template>
	<view class="f-box">
		<view>
			<uni-card title="我的应用" note="Tips">
				<uni-grid :column="3" :showBorder="false" :square="false">
					<uni-grid-item>
						<view class="function" @click="goSchedule">
							<image src="@/static/Schedule.png"></image>
						</view>
						<text class="a">课表查看</text>
					</uni-grid-item>
					<uni-grid-item>
						<view class="function" @click="leaveApplication">
							<image src="@/static/leaveApplication.png"></image>
						</view>
						<text class="a">请假申请</text>
					</uni-grid-item>
					<uni-grid-item>
						<view class="function" @click="resultInquiry">
							<image src="@/static/resultInquiry.png"></image>
						</view>
						<text class="a">成绩查询</text>
					</uni-grid-item>
				</uni-grid>
			</uni-card>
			<uni-card title="所有应用" note="Tips">
				<uni-grid :column="3" :showBorder="false" :square="false">
					<uni-grid-item>
						<view class="function" @click="goSchedule">
							<image src="@/static/Schedule.png"></image>
						</view>
						<text class="a">课表查看</text>
					</uni-grid-item>
					<uni-grid-item>
						<view class="function" @click="leaveApplication">
							<image src="@/static/leaveApplication.png"></image>
						</view>
						<text class="a">请假申请</text>
					</uni-grid-item>
					<uni-grid-item>
						<view class="function" @click="resultInquiry">
							<image src="@/static/resultInquiry.png"></image>
						</view>
						<text class="a">成绩查询</text>
					</uni-grid-item>
					<uni-grid-item>
						<view class="function" @click="outstandingWorks">
							<image src="@/static/logo.png"></image>
						</view>
						<text class="a">优秀作品</text>
					</uni-grid-item>
					<uni-grid-item>
						<view class="function" @click="problemFeedback">
							<image src="@/static/logo.png"></image>
						</view>
						<text class="a">问卷调查</text>
					</uni-grid-item>
					<uni-grid-item>
						<view class="function" @click="transferApplication">
							<image src="@/static/transferApplication.png"></image>
							</p>
						</view>
						<text class="a">调课申请</text>
					</uni-grid-item>
				</uni-grid>
			</uni-card>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {}
		},
		methods: {
			// 去往课程表
			goSchedule() {
				uni.switchTab({
					url: "/pages/course/course"
				})
			},
			// 请假申请
			leaveApplication() {
				uni.navigateTo({
					url: "/pages/work/askLeave"
				})
			},
			// 成绩查询
			resultInquiry() {
				uni.navigateTo({
					url: "/pages/work/resultsQuery"
				})
			},
			// 优秀作品
			outstandingWorks() {
				uni.navigateTo({
					url: "/pages/home/personalWorks"
				})
			},
			// 问卷调查
			problemFeedback() {
				uni.navigateTo({
					url: "/pages/work/teacherScoreList"
				})
			},
			// 调课申请
			transferApplication() {
				uni.switchTab({
					url: "/pages/index/index"
				})
			},
		}
	}
</script>

<style scoped>
	.f-box {
		height: 100%;
		width: 100%;
		background-image: linear-gradient(to bottom, #ffffff 0%, #c6dffd 100%);
	}

	.a {
		background-image: linear-gradient(to top, #dfe9f3 0%, white 100%);
		border-radius: 8%;
		display: flex;
		justify-content: center;
		align-items: center;
	}

	.function {
		background-image: linear-gradient(120deg, #fdfbfb 0%, #ebedee 100%);
		display: flex;
		justify-content: center;
		align-items: center;
		height: 150rpx;
	}

	image {
		height: 100rpx;
		width: 100rpx;
		border-radius: 50%;
	}

	.uni-card {
		font-weight: 900;
		/* background-color: #f2f3f3; */
	}
</style>
