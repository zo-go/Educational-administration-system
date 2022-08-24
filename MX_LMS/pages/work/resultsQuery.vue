<template>
	<view class="list">
		<view>
			<uni-data-select v-model="value" :localdata="range" @change="change" placeholder="请选择年级">
			</uni-data-select>
		</view>

		<view class="results">
			<uni-grid :column="2" :showBorder="false" :square="false">
				<uni-grid-item v-for="item in studeentResults" :key='item.scoreInfo.id'>
					<uni-card :title="item.courseInfo.courseName" :isFull="true">
						<p>学分：1</p>
						<p>获得学分：1</p>
						<p>绩点: 2.0</p>
						<p>最终成绩: {{item.scoreInfo.score}}</p>
					</uni-card>
				</uni-grid-item>
			</uni-grid>
		</view>


	</view>
</template>

<script>
	import {
		getScore
	} from '@/api/score.js'
	import store from '@/store/index.js'
	export default {
		data() {
			return {
				value: '第一学期',
				// 成绩
				studeentResults: [],
				aaa: [],
				bbb: [],
				ccc: [],
				ddd: [],
				eee: [],
				fff: [],

				range: [{
						text: '大一第一学期',
						value: '第一学期'
					},
					{
						text: '大一第二学期',
						value: '第二学期'
					}, {
						text: '大二第一学期',
						value: '第三学期'
					}, {
						text: '大二第二学期',
						value: '第四学期'
					}, {
						text: '大三第一学期',
						value: '第五学期'
					}, {
						text: '大三第二学期',
						value: '第六学期'
					},
				],
				isshow: true,
			}
		},
		beforeMount() {
			// console.log(this.studeentResults);
			// console.log(store.state.userInfo.studentInfo.studentId);
			getScore(store.state.userInfo.studentInfo.studentId).then(res => {
				for (var i = 0; i < res.data.data.length; i++) {
					if (res.data.data[i].termInfo.termName == '第一学期') {
						this.aaa.push(res.data.data[i])
					}
					if (res.data.data[i].termInfo.termName == '第二学期') {
						this.bbb.push(res.data.data[i])
					}
					if (res.data.data[i].termInfo.termName == '第三学期') {
						this.ccc.push(res.data.data[i])
					}
					if (res.data.data[i].termInfo.termName == '第四学期') {
						this.ddd.push(res.data.data[i])
					}
					if (res.data.data[i].termInfo.termName == '第五学期') {
						this.eee.push(res.data.data[i])
					}
					if (res.data.data[i].termInfo.termName == '第六学期') {
						this.fff.push(res.data.data[i])
					}
				}
				this.studeentResults = this.aaa
			})

		},
		methods: {
			change(e) {
				console.log(e);
				if (e == '第一学期') {
					this.studeentResults = this.aaa
					console.log(this.studeentResults);
				}
				if (e == '第二学期') {
					this.studeentResults = this.bbb
					console.log(this.studeentResults);
				}
				if (e == '第三学期') {
					this.studeentResults = this.ccc
					console.log(this.studeentResults);
				}
				if (e == '第四学期') {
					this.studeentResults = this.ddd
					console.log(this.studeentResults);
				}
				if (e == '第五学期') {
					this.studeentResults = this.eee
					console.log(this.studeentResults);
				}
				if (e == '第六学期') {
					this.studeentResults = this.fff
					console.log(this.studeentResults);
				}
			},
		},
	}
</script>

<style lang="scss" scoped>
	text {
		font-size: 12px;
	}

	.results {
		margin-top: 20rpx;
		// padding-left: 10rpx;
	}

	/deep/.uni-grid-wrap {
		padding-left: 36rpx
	}

	/deep/.uni-select__input-placeholder,
	/deep/.uni-select__input-text {
		display: flex;
		justify-content: center;
	}

	/deep/.uni-scroll-view {
		padding-left: 100rpx
	}

	/deep/.uni-card--shadow {
		width: 260rpx;
		background-color: aqua;
	}
</style>
