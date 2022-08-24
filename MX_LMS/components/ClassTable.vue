<template>
	<!-- 课表组件 -->
	<div class="class-table">
		<div class="table-wrapper">
			<div class="tabel-container">
				<table>
					<thead>
						<tr>
							<th v-for="(item,index) in classTableData.weeks" :key="index">{{item?'周'+item:''}}</th>
						</tr>
					</thead>
					<tbody>
						<tr v-for="(item,index) in classTableData.courses" :key="index">
							<td style="font-size:12px;background:#d4f7fd;word-wrap: break-word; 
  word-break: break-all;">{{changeCharacter(index)}}</td>
							<td v-for="(innerItem,idx) in item" :key="idx" @click="toScanDetail(innerItem,idx)">
								<div style v-if="innerItem.lessonsName">
									{{innerItem.lessonsName}}
									<br />
									{{innerItem.lessonsTime}}
								</div>
							</td>
						</tr>
					</tbody>
				</table>
			</div>
		</div>
	</div>
</template>

<script>
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
						"一",
						"二",
						"三",
						"四",
						"五",
						"六",
						"七",
						"八",
						"九"
					];
					return characterArr[num];
				};
			}
		},
		methods: {
			// 查看该课程的相关详情
			toScanDetail(item, idx) {
				var con =`课程名称：${item.lessonsName}\n上课时间：${item.lessonsTime}\n上课地点：${item.lessonsAddress}\n授课老师：${item.lessonsTeacher}\n课程课时：${item.lessonsRemark}`;
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

<style scoped>
	* {
		margin: 0;
		padding: 0;
	}

	.table-wrapper {
		width: 100%;
		overflow: auto;
		margin-bottom: 60px;
	}

	table {
		table-layout: fixed;
		width: 100%;
		border-collapse: collapse;
		color: #677998;
	}

	thead {
		background: #d4f7fd;
	}

	th {
		font-weight: normal;
		height: 46px !important;
	}

	tbody {
		font-size: 12px;
	}

	th,
	td {
		text-align: center;
		height: 70px;
		border-right: 1px solid #fefefe;
		border-bottom: 1px solid #fefefe;
	}

	tr td div {
		background: #a5d16d;
		width: 100%;
		height: 100%;
		color: #efefef;
		border-radius: 10px;
		padding: 5px;
		box-sizing: border-box;
	}

	tr td:first-child {
		color: #333;
	}

	.course {
		background-color: #ebbbbb;
		color: #fff;
		display: inline-block;
		border-radius: 3px;
		width: 47%;
		margin: 1px 1%;
	}

	.bgColor {
		background-color: #89b2e5;
	}
</style>
