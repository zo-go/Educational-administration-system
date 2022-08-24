<template>
	<!-- 未填写的教师问卷列表 -->
	<view>
		<view v-for="(item,i) in tableData" :key="i">
			<uni-card class="content" :title="item.questionnaireTheme" @click="ToTeacherSystem(item.id,item.questionnaireTitle)">
				<text class="uni-body">{{item.questionnaireTitle}}</text>
				<view class="">
					<text>截止时间:{{item.endTime}}</text>
				</view>
			</uni-card>
		</view>
	</view>
</template>

<script>
	import {
		getQuestionnaire
	} from '@/api/questionnaire.js'


	export default {
		data() {
			return {
				DataTheme: "",
				DataTitle: "",
				tableData: [],
				RecordList: [],
				pageIndex: 1,
				total: 0,
				pageNum: 1,
				pageSize: 10,
			}
		},
		methods: {
			ToTeacherSystem(id,title) {
				// 传入问卷详情页的数据，只能传入数组
				uni.navigateTo({
					url: `/pages/work/questionnaireDetails?id=${id}&title=${title}`,
				});
			}
		},
		onLoad() {
			var _this = this;
			let data = {
				pageIndex: this.pageIndex,
				pageSize: this.pageSize,
			};
			getQuestionnaire(data).then(res => {
				console.log(res);
				var tableData = res.data.data

				for (var i = 0; i < tableData.length; i++) {
					tableData[i].endTime = tableData[i].endTime.split("+")[0].replace("T", " ").slice(0, 19);
				}
				
				_this.tableData = tableData;
			});
		}
	}
</script>

<style>
	.cp_tupian {
		margin-left: 25%;
		width: 50%;
		height: 50%;
	}

	.cp-biaoti {
		text-align: center;
		font-size: 14px;
		color: gray;
		overflow: hidden;
		white-space: nowrap;
		text-overflow: ellipsis;
	}

	.cp-xiangmu {
		width: 50%;
		display: flex;
		padding: 3px;
		box-sizing: border-box;
		flex-direction: column;
		margin-bottom: 10%;
	}

	.content {
		display: flex;
		flex-wrap: wrap;
	}
</style>
