<template>
	<div>
		<v-ClassTable :classTableData="classTableData" />
	</div>
</template>
<script>
	var that;
	import ClassTable from "@/components/ClassTable2";
	import {
		reqFindTeacherLesson,
		getCurriculum
	} from "@/api/courseApi.js";
	import store from '@/store/index.js'
	export default {
		data() {
			return {
				msg: "",
				classTableData: {
					weeks: [" ", "周一", "周二", "周三", "周四", "周五", "周六", "周日"],
					courses: [],
				}
			};
		},
		components: {
			"v-ClassTable": ClassTable
		},
		created() {
			console.log(store.state.userInfo.classInfo.className);
			getCurriculum(store.state.userInfo.classInfo.className).then(res => {
				this.classTableData.courses = res.data;
				// console.log(this.classTableData.courses);
				var arr = [];
				this.classTableData.courses.forEach(item => {
					// console.log(item);
					arr.push(item.curriCulumData.split(','))
				})
				console.log(arr);
				this.classTableData.courses = arr
				console.log(this.classTableData);
				// console.log(this.classTableData.courses);
			})
			// const { userId, userName, roleId } = JSON.parse(
			//   this.$cookie.get("userInfo")
			// );
			// this.$cookie.get("userInfo")
			// this.findTeacherLesson(userName);
			// this.findTeacherLesson()
		},
		methods: {
			// 查询学生课表
			async findTeacherLesson(userName) {
				that = this;
				var params = {
					userName: userName
				};
				const {
					msg,
					status,
					info
				} = await reqFindTeacherLesson(params);
				that.classTableData.courses = info;
			}
		}
	};
</script>

<style lang="scss" scoped>
	page {
		// background-color: #f0f0f0;
		background-image: linear-gradient(to bottom, #ffffff 0%, #c6dffd 100%);
	}
</style>