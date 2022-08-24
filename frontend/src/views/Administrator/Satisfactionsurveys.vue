<template>
  <div class="container">
    <el-form ref="form" :model="form" label-width="80px">
      <el-form-item label="主题">
        <el-input v-model="form.theme"></el-input>
      </el-form-item>
      <el-form-item label="标题">
        <el-input v-model="form.name"></el-input>
      </el-form-item>
      <el-form-item label="时间">
        <el-col :span="11">
          <el-date-picker
            v-model="form.date"
            style="width: 100%"
            type="daterange"
            format="yyyy 年 MM 月 dd 日"
            value-format="yyyy-MM-dd HH:mm:ss"
            range-separator="至"
            start-placeholder="开始日期"
            end-placeholder="结束日期"
            :default-time="['00:00:00', '23:59:59']"
            placement="bottom-start"
            :picker-options="pickerOptions"
          ></el-date-picker>
        </el-col>
      </el-form-item>
      <!-- 遍历已创建的选项渲染 -->
      <div class="item" v-for="(item, index) in form.itemList" :key="index">
        <el-form-item :label="index + 1 + ' '">
          <div class="item_title">
            <span>{{ typeMap[item.type] }}: </span>
            <span v-text="item.title"></span>
          </div>
          <!-- 单项填空 -->
          <div v-if="item.type === 'input'">
            <el-input
              class="disabled"
              placeholder="请输入内容"
              v-model="input"
            ></el-input>
          </div>
          <!-- 单选 -->
          <div v-else-if="item.type === 'radio'">
            <div class="warp" v-for="(elm, i) in item.textList" :key="i">
              <el-radio :label="i + 1 + '、'" v-model="radio"></el-radio>
              <el-input v-model="item.textList[i]"></el-input>
            </div>
          </div>

          <!-- 多选 -->
          <div v-else-if="item.type === 'checkbox'">
            <div class="warp" v-for="(elm, i) in item.textList" :key="i">
              <el-checkbox :label="i + 1 + '、'"></el-checkbox>
              <el-input v-model="item.textList[i]"></el-input>
            </div>
          </div>

          <!-- 选择填空 -->
          <div v-else-if="item.type === 'select'">
            <el-select v-model="value" placeholder="请选择">
              <el-option
                v-for="(elm, i) in item.textList"
                :key="i"
                :label="item.textList[i]"
                :value="i + ''"
              >
              </el-option>
            </el-select>
          </div>

          <!-- 矩阵填空 -->
          <div v-else-if="item.type === 'matrix'">
            <div class="warp" v-for="(elm, i) in item.textList" :key="i">
              <span> {{ item.textList[i] }}：</span>
              <el-input class="disabled" placeholder="请输入内容"></el-input>
            </div>
          </div>
          <!-- 上移、下移、删除 -->
          <div style="margin-top: 10px">
            <el-button @click="handleItem('up', item)" v-if="index != 0"
              >上移</el-button
            >
            <el-button
              @click="handleItem('down', item)"
              v-if="index != form.itemList.length - 1"
              >下移</el-button
            >
            <el-button @click="handleItem('del', item)">删除</el-button>
            <el-button @click="edit(item, index)">编辑</el-button>
          </div>
        </el-form-item>
      </div>

      <!-- 添加选项 -->
      <el-form-item>
        <el-button @click="add('radio')">单选</el-button>
        <el-button @click="add('checkbox')">多选</el-button>
        <el-button @click="add('input')">单项填空</el-button>
      </el-form-item>

      <el-form-item>
        <el-button type="primary" @click="onSubmit">立即创建</el-button>
      </el-form-item>

      <!-- 添加选项弹出框 -->
      <div class="additem">
        <el-dialog
          :title="typeMap[questItem.type]"
          :visible.sync="showItem"
          width="50%"
        >
          <el-form-item label="标题">
            <el-input v-model="itemTitle"></el-input>
          </el-form-item>
          <el-form-item label="添加选项" v-show="questItem.type != 'input'">
            <el-input-number
              v-model="num"
              @change="handleChange"
              :min="1"
              :max="10"
            ></el-input-number>
          </el-form-item>
          <el-form-item
            label="选项"
            v-for="(text, i) in itemText"
            :key="i"
            v-show="questItem.type != 'input'"
          >
            <el-input v-model="itemText[i]"></el-input>
          </el-form-item>
          <span slot="footer" class="dialog-footer">
            <el-button @click="clearItem">取 消</el-button>
            <el-button type="primary" @click="determine">确 定</el-button>
          </span>
        </el-dialog>
      </div>
    </el-form>
  </div>
</template>

<script>
import { AddQuestionnaire } from "@/api/Questionnaire";
import { AddRecord } from "@/api/Record";
import { AddOption } from "@/api/Option";
export default {
  // name: "Satisfactionsurveys",
  data() {
    return {
      pickerOptions: {
        disabledDate(time) {
          return time.getTime() < Date.now() - 86400000; //86400000毫秒等于一天 禁用今天之前的日期 去掉则今天也禁用
        },
      },
      value: "",
      matrixsNum: 1,
      num: 1,
      radio: "",
      input: "",
      form: {
        itemList: [],
        name: "",
        date: "",
        theme: "",
      },
      itemTitle: "",
      itemText: new Array(1),
      questItem: {},
      showItem: false,
      typeMap: {
        radio: "单选",
        checkbox: "多选",
        input: "问答",
        // matrix: "矩阵问答",
        // select: "选择",
      },
      editIndex: "",
    };
  },
  watch: {
    showItem() {
      if (!this.showItem) {
        this.clearItem();
      }
    },
  },
  methods: {
    // 创建选项
    add(type) {
      this.questItem.type = type;
      this.showItem = true;
    },
    // 增加/减少 子选项
    handleChange(val) {
      this.itemText.length = val;
    },
    // 确定将选项添加进列表中进行渲染
    determine() {
      if (this.questItem.type == "input") {
        // 填空
        if (this.itemTitle == "") {
          this.$message("请输入选项的标题内容");
          return;
        }
        if (this.editIndex !== "") {
          this.questItem.title = this.itemTitle;
          this.form.itemList.splice(this.editIndex, 1, this.questItem);
          this.editIndex = "";
        } else {
          this.questItem.title = this.itemTitle;
          this.form.itemList.push(this.questItem);
        }
        this.clearItem();
      } else if (
        this.questItem.type == "radio" ||
        this.questItem.type == "checkbox" ||
        this.questItem.type == "matrix" ||
        this.questItem.type == "select"
      ) {
        // 单选、多选、矩阵
        if (this.itemTitle == "") {
          this.$message("请输入选项的标题内容");
          return;
        }
        for (var i = 0; i < this.itemText.length; i++) {
          if (this.itemText[i] == "" || this.itemText[i] == undefined) {
            this.$message("请完整输入每个选项内容");
            return;
          }
        }
        if (this.editIndex !== "") {
          this.questItem.title = this.itemTitle;
          this.questItem.textList = this.itemText;
          this.form.itemList.splice(this.editIndex, 1, this.questItem);
          this.editIndex = "";
        } else {
          this.questItem.title = this.itemTitle;
          this.questItem.textList = this.itemText;
          this.form.itemList.push(this.questItem);
        }
        this.clearItem();
      }
    },
    // 编辑
    edit(item, editIndex) {
      this.editIndex = editIndex;
      if (item.type !== "input") {
        this.num = item.textList.length;
        this.showItem = true;
        this.questItem = item;
        this.itemTitle = item.title;
        this.itemText = [];
        this.itemText.push(...item.textList);
      } else {
        this.showItem = true;
        this.questItem = item;
        this.itemTitle = item.title;
      }
    },
    // 关闭弹窗，清空数据
    clearItem() {
      this.num = 1;
      this.itemTitle = "";
      this.itemText = [""];
      this.questItem = {};
      this.showItem = false;
    },
    // 判断上移、下移、删除
    handleItem(val, item) {
      switch (val) {
        case "up":
          this.moveUp(item);
          break;
        case "down":
          this.moveDown(item);
          break;
        case "del":
          this.delItem(item);
          break;
        default:
          throw new Error("该操作不存在！");
      }
    },
    // 上移
    moveUp(item) {
      let index = this.form.itemList.indexOf(item);
      this.form.itemList.splice(index, 1);
      this.form.itemList.splice(index - 1, 0, item);
    },
    // 下移
    moveDown(item) {
      let index = this.form.itemList.indexOf(item);
      this.moveUp(this.form.itemList[index + 1]);
    },
    // 删除
    delItem(item) {
      let index = this.form.itemList.indexOf(item);
      this.form.itemList.splice(index, 1);
    },
    // 提交
    onSubmit() {
      if (this.form.name == "") {
        this.$message("请输入标题内容");
        return;
      }
      if (this.form.date == "") {
        this.$message("请选择时间");
        return;
      }
      if (this.form.itemList.length == 0) {
        this.$message("至少添加一个选项");
        return;
      }

      //添加 主题 拿到主题id后查问问题和选项
      let theme = {
        QuestionnaireTitle: this.form.name,
        QuestionnaireTheme: this.form.theme,
        EndTime: this.form.date[1],
      };
      AddQuestionnaire(theme).then((res) => {
        let themeId = res.data.data.id;
        var listId = [];
        //存储问题的 数组
        let RecordDTOs = [];
        let optionList = [];
        let optionDTOs = [];

        this.form.itemList.forEach((item) => {
          let record = {
            QuestionnaireQuestion: item.title,
            QuestionnaireOptionType: item.type,
            QuestionnaireFlag: 1,
          };

          let option = {
            QuestionnaireID: themeId,
            OptionName: !item.textList ? "" : item.textList.toString(),
          };
          optionList.push(option);

          //将问题按照后端接受格式 添加进数组
          RecordDTOs.push(record);
        });

        //将问卷主题id和正确格式的问题数组传给后端
        let Recorddata = {
          QuestionnaireID: themeId,
          RecordDTOs,
        };

        //添加 问卷问题
        AddRecord(Recorddata).then((res) => {
          let list = res.data.data;

          console.log(res);
          //遍历问题Id赋值给对应选项
          for (let i = 0; i < list.length; i++) {
            listId.push(list[i].id);
          }

          for (let i = 0; i < list.length; i++) {
            optionList[i].QuestionnaireQuestionId = listId[i];
          }

          optionDTOs = {
            optionDTOs: optionList,
          };

          AddOption(optionDTOs).then((res) => {
            this.$router.push(`/Administrator/publish/${themeId}`);
          });
        });
      });
    },
  },
};
</script>
<style  scoped>
.warp {
  display: flex;
  align-items: center;
}
.disabled {
  background: #f5f7fa;
}
input {
  -webkit-appearance: none;
  background-color: #fff;
  background-image: none;
  border-radius: 4px;
  border: 1px solid #dcdfe6;
  box-sizing: border-box;
  color: #606266;
  font-size: inherit;
  height: 40px;
  line-height: 40px;
  outline: 0;
  padding: 0 15px;
  transition: border-color 0.2s cubic-bezier(0.645, 0.045, 0.355, 1);
  margin: 10px 0 0;
}
.el-radio {
  color: #606266;
  cursor: pointer;
  margin-right: 0;
}
</style>