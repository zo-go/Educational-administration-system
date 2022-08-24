<template>
	<view class="class-table">
		<view class="tabel-container">
			<uni-table stripe>
				<!-- 表头 -->
				<!-- <uni-thead> -->
				<uni-tr>
					<uni-th width="5" align="center"></uni-th>
					<uni-th width="5" align="center">第一节课</uni-th>
					<uni-th width="5" align="center">第二节课</uni-th>
					<uni-th width="5" align="center">第三节课</uni-th>
					<uni-th width="5" align="center">第四节课</uni-th>
					<uni-th width="5" align="center">第五节课</uni-th>
					<uni-th width="5" align="center">第六节课</uni-th>
					<uni-th width="5" align="center">第七节课</uni-th>
					<uni-th width="5" align="center">第八节课</uni-th>
					<!-- <uni-th width="5" align="center">周六</uni-th>
					<uni-th width="5" align="center">周日</uni-th> -->
				</uni-tr>
				<!-- </uni-thead> -->
				<!-- 表格数据 -->
				<uni-tr v-for="(item,index) in classTableData.courses" :key="index">
					<uni-td style="font-size:12px;background:#d4f7fd;word-wrap: break-word;
					word-break: break-all;">{{changeCharacter(index)}}</uni-td>
					<uni-td v-for="(innerItem,idx) in item" :key="idx">
						<view class="div" style @click="toScanDetail(innerItem)">
							{{innerItem}}
						</view>
					</uni-td>
				</uni-tr>
			</uni-table>
		</view>
	</view>
</template>

<script>
	import {
		getCourse
	} from '@/api/courseApi.js'
	var that;
	export default {
		props: {
			classTableData: {
				type: Object
			}
		},
		computed: {
			// 将数字转换成汉字
			changeCharacter(num) {
				return function(num) {
					var digArr = [1, 2, 3, 4, 5, 6, 7, 8, 9];
					var characterArr = [
						"周一",
						"周二",
						"周三",
						"周四",
						"周五",
						"周六",
						"周日",
					];
					return characterArr[num];
				};
			}
		},
		created() {
			getCourse().then(res => {
				console.log(res);
			})
		},
		methods: {
			// 查看该课程的相关详情
			toScanDetail(item, idx) {
				var con =
					`课程名称：${item.lessonsName}\n上课时间：${item.lessonsTime}\n上课地点：${item.lessonsAddress}\n授课老师：${item.lessonsTeacher}\n课程课时：${item.lessonsRemark}`;
				if (item.lessonsName) {
					uni.showModal({
						content: con,
						showCancel: false
					});
				}
			}
		}
	};
</script>
<style lang="scss" scoped>
	.uni-group {
		display: flex;
		align-items: center;
	}

	.uni-table-th {
		background: #d4f7fd;
		border: solid #fff 1rpx;
	}

	.uni-thead {}

	.uni-table-td {
		padding: 0;
	}

	.div {
		background: #a5d16d;
		width: 100%;
		height: 100%;
		color: #efefef;
		border-radius: 10px;
		// padding: 5px;
		box-sizing: border-box;
	}
</style>

