<template>
	<view class="box">
		<view class="title">
			{{title}}
		</view>
		<view v-for="(item,i) in fromData" :key="i">
			<view class="">
				<!-- <text>{{typeMap[item.Type]}}</text> -->
				<text>{{i+1}}: {{item.RecordName}}</text>
				<view class="warp" v-if="item.Type==='input'">
					<input id="input" v-model="item.optionvalue" class="input_box" type="text" placeholder="请输入">
				</view>


				<view class="" v-if="item.Type==='checkbox'">
					<checkbox-group class="checkbox " @change="checkboxValue($event,i)">
						<div class="warp" v-for="(elm, i) in item.OptionList" :key="i">
							<label>
								<checkbox :value="item.OptionList[i]" color="#2B7BED" :checked="i === current" />
								<text></text>
								<text>{{item.OptionList[i]}}</text>
							</label>
						</div>
					</checkbox-group>
				</view>


				<view class="" v-if="item.Type=='radio'">
					<radio-group @change="radioValue($event,i)">
						<div class="warp" v-for="(elm, i) in item.OptionList" :key="i">
							<label class="radio">
								<radio :value="item.OptionList[i]" color="#2B7BED" :checked="i === current" />
								<text>{{item.OptionList[i]}}</text>
							</label>
						</div>
					</radio-group>
				</view>

			</view>
		</view>
		<view class="button_box">
			<button @click="addRecord">提交</button>
		</view>
	</view>
</template>



<script>
	import {
		getQuestionnaireRecord,
		addQuestionnaireRecord
	} from '@/api/questionnaire.js'


	export default {
		data() {
			return {
				fromData: [],
				list: {},
				typeMap: {
					radio: "单选",
					checkbox: "多选",
					input: "填空",
				},
				radioList: [],
				recordList: [],
				checkList: [],
				resDtoList: [],
				title: ""
			}
		},
		onLoad(options) {
			// console.log(options);
			
			var _this = this;
			this.title = options.title
			console.log(this.title);
			getQuestionnaireRecord(options.id).then(res => {
				var data = res.data.data;
					console.log(res);
				for (let i = 0; i < data.length; i++) {

					_this.list = {};
					_this.list.OptionList = data[i].optionName.split(",");
					_this.list.Type = data[i].questionnaireOptionType;
					_this.list.RecordName = data[i].questionnaireQuestion;
					_this.list.id = data[i].id;
					_this.list.QuestionId = data[i].questionnaireQuestionId;
					_this.list.optionId = data[i].optionId;
					_this.list.optionvalue = "";

					_this.fromData.push(_this.list);
				}

			});
			console.log(this.fromData);
		},
		methods: {
			addRecord() {
				var _this = this;
				for (let i = 0; i < this.fromData.length; i++) {

					if (this.fromData[i].optionvalue == "" || this.fromData[i].optionvalue == undefined || this.fromData[i]
						.optionvalue
						.length == 0) {
						uni.showToast({
							icon: "error",
							title: "答题未完成"
						})
						return;
					}
				}

				let resDtoList = [];
				for (let i = 0; i < this.fromData.length; i++) {

					let QuestionnaireResDTO = {
						QuestionnaireId: this.fromData[i].id,
						QuestionnaireQuestionId: this.fromData[i].QuestionId,
						OptionId: this.fromData[i].optionId,
						OptionValue: this.fromData[i].optionvalue,
					}

					resDtoList.push(QuestionnaireResDTO);
				}


				let ResListDto = {
					StudentNum: uni.getStorageSync("StudentAccount"),
					resDtoList: resDtoList
				}

				console.log(ResListDto);
				addQuestionnaireRecord(ResListDto).then(res => {
					console.log(res);
					uni.navigateBack({
						delta: 1, //返回层数，2则上上页
					})
				})
			},
			checkboxValue(e, index) {
				this.fromData[index].optionvalue = e.target.value.toString();
			},
			radioValue(e, index) {
				this.fromData[index].optionvalue = (e.target.value).toString();
			},

		}
	}
</script>

<style>
	.title {
		text-align: center;
		font: bold;
		margin-bottom: 2%;
	}

	.box {
		margin-top: 2%;
		margin-left: 2%;
	}

	.warp {
		display: flex;
		align-items: center;
		margin-top: 1%;
	}

	.input_box {
		/* margin-left: %; */
		width: 80%;
		height: 60px;
		border-radius: 5px;
		border: 2rpx solid;
	}
</style>
