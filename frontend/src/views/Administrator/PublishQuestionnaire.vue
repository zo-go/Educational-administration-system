<template>
  <div>
    <el-card class="box-card">
      <el-form
        style="width: 700px"
        :model="List"
        class="demo-form-inline"
        label-width="80px"
        v-loading="loading"
      >
        <el-form-item label="问卷主题">
          <el-input readonly v-model="DataTheme"></el-input>
        </el-form-item>
        <el-form-item label="问卷标题">
          <el-input readonly v-model="DataTitle"></el-input>
        </el-form-item>
        <el-form-item label="开始 截至">
          <el-tag style="margin: 5px" type="success">{{
            this.StartTime
          }}</el-tag>
          <el-tag type="danger">{{ this.EndTime }}</el-tag>
        </el-form-item>

        <!-- 遍历对应的问题及选项 -->
        <div class="item" v-for="(item, index) in RecordList" :key="index">
          <el-form-item :label="index + 1 + ' '">
            <div class="item_title">
              <span>{{ typeMap[item.Type] }}: </span>
              <span v-text="item.RecordName"></span>

              <!-- 单项填空 -->
              <div v-if="item.Type === 'input'">
                <el-input
                  :readonly="true"
                  style="margin-left: 45px"
                  class="disabled"
                  placeholder=""
                ></el-input>
              </div>
              <!-- 单选 -->
              <div class="radio_box" v-else-if="item.Type === 'radio'">
                <div class="warp" v-for="(elm, i) in item.OptionList" :key="i">
                  <el-radio :label="i + 1 + '、'" type="radio"></el-radio>
                  <el-input
                    :readonly="true"
                    v-model="item.OptionList[i]"
                  ></el-input>
                </div>
              </div>

              <!-- 多选 -->
              <div class="check_box" v-else-if="item.Type === 'checkbox'">
                <div class="warp" v-for="(elm, i) in item.OptionList" :key="i">
                  <el-checkbox :label="i + 1 + '、'"></el-checkbox>
                  <el-input
                    :readonly="true"
                    v-model="item.OptionList[i]"
                  ></el-input>
                </div>
              </div>
            </div>
          </el-form-item>
        </div>
      </el-form>
      <div class="button_box">
        <el-button style="margin-left: 180px" @click="toRecord"
          >历史发布</el-button
        >
        <el-button @click="toPublish">再发布一个</el-button>
      </div>
    </el-card>
  </div>
</template>


<script>
import { GetFullInfo } from "@/api/Questionnaire";

export default {
  data() {
    return {
      loading: true,
      List: {},
      FromData: [],
      DataTheme: "",
      DataTitle: "",
      StartTime: "",
      createdBy: "",
      EndTime: "",
      RecordList: [],
      ResData: [],
      From: {},
      readonly: true,
      typeMap: {
        radio: "单选",
        checkbox: "多选",
        input: "问答",
        // matrix: "矩阵问答",
        // select: "选择",
      },
    };
  },
  created() {
    this.getData();
  },
  methods: {
    toRecord() {
      window.location.href = "/Administrator/histroy";
    },
    toPublish() {
      this.$router.push("/Administrator/satisfactionsurveys");
    },
    getData() {
      this.loading = true;
      let id = this.$route.params.id;

      GetFullInfo(id).then((res) => {
        this.FromData = res.data.data;
        // console.log(this.FromData);

        //提取该问卷的公共属性
        this.DataTheme = this.FromData[0].questionnaireTheme;
        this.DataTitle = this.FromData[0].questionnaireTitle;
        this.createdBy = this.FromData[0].createdBy;

        //分割替换 让字符串为需要的时间格式
        var tmp1 = this.FromData[0].createdAt.split("+");
        this.StartTime = tmp1[0].replace("T", " ").slice(0, 19);
        var tmp = this.FromData[0].endTime.split("+");
        this.EndTime = tmp[0].replace("T", " ");

        for (var i = 0; i < this.FromData.length; i++) {
          let aaa = {};
          aaa.OptionList = this.FromData[i].optionName.split(",");
          aaa.Type = this.FromData[i].questionnaireOptionType;
          aaa.RecordName = this.FromData[i].questionnaireQuestion;

          this.RecordList.push(aaa);
        }
        console.log(this.RecordList);
      });
      this.loading = false;
    },
  },
};
</script>

<style scoped>
.box-card {
  margin-top: 2px;
  display: flex;
  justify-content: center;
  align-items: center;
  overflow-x: hidden !important;
  overflow-y: scroll !important;
  height: auto;

  /* padding-top: 600px; */
}
.box-card::-webkit-scrollbar {
  display: none !important;
}
.demo-form-inline {
  /* overflow-y: auto !important; */
  /* overflow-x: hidden !important; */
  /* overflow-y: scroll !important; */
}
/deep/.el-input__inner {
  width: 300px !important;
}

.el-radio {
  color: #606266;
  cursor: pointer;
  margin-right: 0;
}

.warp {
  display: flex;
  align-items: center;
}

.item {
  margin-left: -50px !important;
}

.button_box {
  display: flex;
  align-items: center;
}
</style>

