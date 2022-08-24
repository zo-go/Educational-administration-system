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
                  style="margin-left: 47px"
                  class="disabled"
                  v-model="item.CheckValue"
                ></el-input>
              </div>
              <!-- 单选 -->
              <div class="radio_box" v-else-if="item.Type === 'radio'">
                <div class="warp" v-for="(elm, i) in item.OptionList" :key="i">
                  <el-radio
                    v-model="item.CheckValue"
                    :label="item.OptionList[i]"
                    type="radio"
                    disabled
                    border
                  ></el-radio>
                </div>
              </div>

              <!-- 多选 -->
              <div class="check_box" v-else-if="item.Type === 'checkbox'">
                <div class="warp" v-for="(elm, i) in item.OptionList" :key="i">
                  <div
                    v-if="item.CheckValue.split(',')[i] == item.OptionList[i]"
                  >
                    <el-checkbox
                      v-model="checked"
                      border
                      :label="item.OptionList[i]"
                      disabled
                    ></el-checkbox>
                  </div>
                  <div v-else>
                    <el-checkbox
                      v-model="checked2"
                      :label="item.OptionList[i]"
                      disabled
                    ></el-checkbox>
                  </div>
                </div>
              </div>
            </div>
          </el-form-item>
        </div>
      </el-form>
      <div class="button_box">
        <el-button style="margin-left: 180px" @click="toHistory"
          >历史发布</el-button
        >
      </div>
    </el-card>
  </div>
</template>


<script>
import { GetRecordFullInfo } from "@/api/RecordRes";

export default {
  data() {
    return {
      checked: true,
      checked2: false,
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
    toHistory() {
      window.location.href = "/index/Administrator/record";
    },
    getData() {
      this.loading = true;
      let data = {
        QuestionnaireId: this.$route.params.id,
        CreatedBy: this.$route.params.created,
      };

      GetRecordFullInfo(data).then((res) => {
        let list = res.data.data[0].list;
        this.DataTheme = list[0].questionnaireTheme;
        this.DataTitle = list[0].questionnaireTitle;
        list.forEach((item) => {
          let data = {};
          data.CheckValue = item.optionValue;
          data.OptionList = item.optionName.split(",");
          data.Type = item.questionnaireOptionType;
          data.RecordName = item.question;
          this.RecordList.push(data);
        });
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
  /* height: 77.8vh; */
  overflow-y: auto !important;
}

.demo-form-inline {
  /* padding-top: 125px !important; */
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
  padding-top: 7px;
}

.item {
  margin-left: -50px !important;
}

.button_box {
  display: flex;
  align-items: center;
}
</style>



