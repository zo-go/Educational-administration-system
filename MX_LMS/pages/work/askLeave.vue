<template>
	<!--请假页面 -->
	<view class="content">
		<view class="list">
			<uni-list>
				<view class="askLeave">
					<uni-list-item title="请假类型" clickable showArrow @click="open"
						:rightText="rightText? rightText : '请选择'">
					</uni-list-item>
					<uni-popup ref="popup" type="bottom" background-color="#fff">
						<text>请选择</text>
						<template>
							<view>
								<uni-data-checkbox v-model="leaveContent.leaveType" :localdata="range" @change="change"
									mode="list">
								</uni-data-checkbox>
							</view>
						</template>
					</uni-popup>
				</view>
			</uni-list>
			<view class="Time">
				<uni-list>
					<view class="leaveTime" style="height: 100rpx;">
						<text>选择时间:</text>
						<picker class="picker date" mode="date" :value="leaveContent.date"
							:start="leaveContent.startDate" :end="leaveContent.endDate" @change="bindDateChange">
							<view>{{ leaveContent.date }}</view>
						</picker>
						<picker class="picker" mode="time" :value="leaveContent.timeStart" start="09:01" end="21:01"
							@change="bindTimeStart">
							<view class="uni-input">{{ leaveContent.timeStart }}</view>
						</picker>
						-
						<picker class="picker" mode="time" :value="leaveContent.timeEnd" start="09:01" end="21:01"
							@change="bindTimeEnd">
							<view class="uni-input">{{ leaveContent.timeEnd }}</view>
						</picker>
					</view>
				</uni-list>
			</view>
			<view class="originally">
				<view>请假事由</view>
				<uni-easyinput type="textarea" v-model="leaveContent.value" placeholder="请输入内容"></uni-easyinput>
			</view>
		</view>
		<uni-list class="save">
			<uni-list-item title="申请请假" clickable @click="save">
			</uni-list-item>
		</uni-list>
	</view>
</template>

<script>
	export default {
		data() {
			const currentDate = this.getDate({
				format: true
			});
			return {
				leaveContent: {
					// 当天日期
					date: currentDate,
					// 请假开始时间
					timeStart: '14:00',
					// 请假结束时间
					timeEnd: '16:00',
					// 请假事由
					value: "",
					// 请假类型
					leaveType: '',
				},
				rightText: '',
				// 请假类型数据
				range: [{
					"value": 0,
					"text": "病假"
				}, {
					"value": 1,
					"text": "事假"
				}, {
					"value": 2,
					"text": "丧假"
				}, {
					"value": 3,
					"text": "调休"
				}]
			};
		},
		computed: {
			startDate() {
				return this.getDate('start');
			},
			endDate() {
				return this.getDate('end');
			}
		},

		methods: {
			open() {
				this.$refs.popup.open('bottom')
			},
			change(e) {
				this.rightText = e.detail.data.text
			},
			open2() {
				this.$refs.popup2.open('bottom')
			},

			bindPickerChange: function(e) {
				console.log('picker发送选择改变，携带值为', e.detail.value)
				this.setData({
					index: e.detail.value
				})
			},
			// 选择时间 日期
			bindDateChange: function(e) {
				this.date = e.target.value;
			},
			getDate(type) {
				const date = new Date();
				let year = date.getFullYear();
				let month = date.getMonth() + 1;
				let day = date.getDate();

				if (type === 'start') {
					year = year - 60;
				} else if (type === 'end') {
					year = year + 2;
				}
				month = month > 9 ? month : '0' + month;
				day = day > 9 ? day : '0' + day;
				return `${year}-${month}-${day}`;
			},
			// 开始时间
			bindTimeStart: function(e) {
				this.timeStart = e.target.value;
			},
			// 结束时间
			bindTimeEnd: function(e) {
				this.timeEnd = e.target.value;
			},
			save() {
				console.log(this.leaveContent);
			}
		}
	}
</script>

<style lang="scss" scoped>
	page {
		background-color: #f0f0f0;
	}

	.save {
		margin-top: 50rpx;
		display: flex;
		justify-content: center;
		align-items: center;
	}

	.Time {
		margin-top: 15rpx;
	}

	.leaveTime {
		padding-left: 10rpx;
		display: flex;
		align-items: center;
	}

	.originally {
		margin-top: 20rpx;
		background-color: #fff;
	}
</style>
