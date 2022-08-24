<template>
	<view>
		<view class="classInfo" :data="classInfo">
			<view class="classname">
				<h2>{{classInfo.className}}</h2>
			</view>
			<view class="list">
				<uni-list>
					<view class="classCounselor">
						<uni-list-item title="辅导员" :rightText="classInfo.classCounselor">
						</uni-list-item>
					</view>
					<view class="classNumber">
						<uni-list-item title="班级人数" :rightText="classInfo.classNumber">
						</uni-list-item>
					</view>
					<view class="classMembers">
						<uni-collapse>
							<uni-collapse-item title="班级成员">
								<uni-list>
									<uni-list-item :title="item.name" v-for="item in studentsInfo" :key="item.name">
									</uni-list-item>
								</uni-list>
							</uni-collapse-item>
						</uni-collapse>
					</view>
				</uni-list>
			</view>
		</view>

	</view>
</template>

<script>
	import {
		getClass
	} from '@/api/classApi.js'
	import {
		getStudent
	} from '@/api/studentApi.js'
	import store from '@/store/index.js'
	export default {
		data() {
			return {
				classInfo: {
					className: '软件一班',
					classNumber: '45',
					classCounselor: '王老师',
				},
				studentsInfo: [{
					name: '张三',
				}, {
					name: '李四',
				}, {
					name: '王五',
				}, ]
			}
		},
		methods: {

		},
		beforeMount() {
			// console.log(store.state.userInfo);
			// this.classInfo = store.state.userInfo.classInfo
			// console.log(this.classInfo);
			// getClass(this.classInfo.className).then(res => {
			// 	// console.log(res);
			// })
			// getStudent().then(res => {
			// 	console.log(res);
			// })
		}
	}
</script>

<style lang="scss" scoped>
	page {
		background-color: #f0f0f0;
	}

	.list {
		margin-top: 50rpx;
	}

	.classname {
		display: flex;
		justify-content: center;
	}
</style>
