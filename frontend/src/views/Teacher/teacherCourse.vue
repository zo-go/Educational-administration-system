<template>
  <el-card class="card">
    <div class="header">
      <div class="header_left">
        <el-select v-model="optionValue.ClassName" placeholder="请选择班级">
          <el-option v-for="item in semesterOptions" :key="item.id" :label="item.className"
            :value="item.classinfo.className" @click.native="queryByClass(item.className)">
          </el-option>
        </el-select>
      </div>
      <div class="header_rigth">
        <div style="float: left; margin-right: 9px">
          <el-button type="primary" @click="dialogFormVisible = true">选课</el-button>
        </div>
      </div>
    </div>

    <el-dialog title="添加课程" :visible.sync="dialogFormVisible" width="30%" center>
      <el-form ref="addForm" :model="addForm" label-width="160px" :rules="formRules">
        <el-form-item label="请选择上课时间">
          <el-col :span="17">
            <el-select v-model="addForm.week" placeholder="请选择哪一天">
              <el-option v-for="(item, index) in week" :label="item" :key="index" :value="index">
              </el-option>
            </el-select>
          </el-col>
        </el-form-item>
        <el-form-item prop="course" label="请选择课程名称">
          <el-col :span="17">
            <el-select v-model="addForm.course" placeholder="请选择课程">
              <el-option v-for="(item, index) in courseOptions" :key="index" :label="item.courseName"
                :value="item.courseName">
              </el-option>
            </el-select>
          </el-col>
        </el-form-item>
        <el-form-item prop="startLesson" label="第几节课开始">
          <el-col :span="17">
            <el-select v-model="addForm.startLesson" placeholder="请选择第几节课">
              <el-option v-for="(item, index) in courselist[addForm.week]" :label="index + 1" :key="index"
                :value="index"></el-option>
            </el-select>
          </el-col>
        </el-form-item>
        <el-form-item prop="overLesson" label="连续上几节课">
          <el-col :span="17">
            <el-select v-model="addForm.overLesson" placeholder="请选择">
              <el-option v-for="(item, index) in [1, 2, 4]" :label="item" :key="index" :value="item">
              </el-option>
            </el-select>
          </el-col>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="save('addForm')">确 定</el-button>

          <el-button @click="cancel()">取 消</el-button>
        </el-form-item>
      </el-form>
    </el-dialog>
    <div id="coursesTable"></div>
  </el-card>
</template>

<script>
import timetable from "timetables";
import { getCurriCulumByName, updataCurriCulum } from "@/api/curriCulumApi";
import { getClass } from "@/api/classApi";
import { getCourse, addCourse, updataCourse, deleteCourse, getCourseById } from "@/api/courseApi";
// import CryptoJs from "@/Utils/crypto";

export default {
  data() {
    return {
      styles: {
        Gheight: 60,
        leftHandWidth: 130,
        palette: ["#ff6633", "#eeeeee"],
      },
      classScheduleId: "",
      isEdit: false,
      tchId: null,
      semesterOptions: [],
      courseOptions: [],
      classOptions: [],
      optionValue: {
        SemesterId: null,
        ClassName: "软件一班",
      }, // 选项的值
      courselist: [
        ["", "", "", "", "", "", "", "", ""],
        ["", "", "", "", "", "", "", "", ""],
        ["", "", "", "", "", "", "", "", ""],
        ["", "", "", "", "", "", "", "", ""],
        ["", "", "", "", "", "", "", "", ""],
      ],
      timetableType: [
        [{ name: "第一节课\n(8:00-8:45)" }, 1],
        [{ name: "第二节课\n(8:55-9:40)" }, 1],
        [{ name: "第三节课\n(10:00-10:45)" }, 1],
        [{ name: "第四节课\n(10:55-11:4)" }, 1],
        [{ name: "第五节课\n(14:00-14:45)" }, 1],
        [{ name: "第六节课\n(14:55-15:40)" }, 1],
        [{ name: "第七节课\n(16:00-16:45)" }, 1],
        [{ name: "第八节课\n(16:55-17:40)" }, 1],
        [{ name: "第九节课\n(19:00-19:45)" }, 1],
      ],
      addForm: {
        week: 0,
        course: null,
        startLesson: 0,
        overLesson: 1,
      },
      formRules: {
        course: [
          {
            required: true,
            message: "请选择课程",
            trigger: "change",
          },
        ],
      },
      week: ["周一", "周二", "周三", "周四", "周五"],
      timetable: null,
      dialogFormVisible: false,
    };
  },
  mounted() {
    this.timetable = new timetable({
      el: "#coursesTable",
      timetables: this.courselist,
      week: this.week,
      highlightWeek: new Date().getDay(),
      timetableType: this.timetableType,
      styles: this.styles,
      gridOnClick: this.TimeTableOnClick,
    });
    this.init();
    this.getCourseInfo();
  },
  methods: {
    // 初始化
    init() {
      getCurriCulumByName(this.optionValue.ClassName).then((res) => {
        console.log(res);
        let arr = [];
        res.data.data.forEach((item, index) => {
          sessionStorage.setItem(index, item.id);
          arr.push(item.curriCulumData.split(","));
          this.courselist = arr;
        });
        this.refresh();
      });
      this.getClass();
    },
    // 刷新课表
    refresh() {
      this.timetable.setOption({
        timetables: this.courselist,
        week: this.week,
        styles: this.styles,
        highlightWeek: new Date().getDay(),
        timetableType: this.timetableType,
      });
    },
    // 保存
    save(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          var a = -1;
          for (let i = 0; i < this.addForm.overLesson; i++) {
            if (
              this.courselist[this.addForm.week][
              this.addForm.startLesson + i
              ] === ""
            ) {
              this.courselist[this.addForm.week][this.addForm.startLesson + i] =
                this.addForm.course ;
            } else {
              a++;
              console.log(a);
            }
          }
          if (a == -1) {
            var obj = {
              CurriCulumData: this.courselist[this.addForm.week].toString(),
              SpecializedName: this.optionValue.ClassName,
            };
            updataCurriCulum(sessionStorage.getItem(this.addForm.week), obj).then(
              (res) => {
                console.log(res);
                this.$message({
                  type: "success",
                  message: res.data.msg,
                });
              }
            );
          } else {
            this.$message({
              type: "error",
              message: "这节课已被其他老师选择",
            });
          }

          this.cancel();
          this.refresh();
        }
      });
    },
    // 取消
    cancel() {
      this.addForm = {
        week: 0,
        course: null,
        startLesson: 0,
        overLesson: 1,
      };
      this.dialogFormVisible = false;
    },
    // 获取班级信息
    getClass() {
      getClass().then((res) => {
        this.semesterOptions = res.data.data;
      });
    },
    // 获取课程信息
    getCourseInfo() {
      var id = this.$store.state.userInfo.userInfo.userName
      getCourseById(id).then(
        (res) => {
          console.log(res);
          this.courseOptions = res.data.data;
        }
      );
    },
    // 切换班级
    queryByClass(ClassName) {
      this.optionValue.ClassName = ClassName;
      this.init();
      this.refresh();
    },
    // 单元格点击触发事件
    TimeTableOnClick(e) {
      console.log(e);
    },
  },
  watch: {
    "optionValue.ClassId": {
      handler() {
        if (this.optionValue.ClassName !== null) {
          this.isEdit = true;
        } else {
          this.isEdit = false;
        }
      },
    },
    dialogFormVisible: {
      handler() {
        if (this.dialogFormVisible === false) {
          this.$refs.addForm.resetFields();
        }
      },
    },
  },
};
</script>

<style scoped>
.bottom {
  width: 100%;
  display: flex;
  justify-content: flex-end;
}

.header {
  width: 100%;
  display: flex;
  justify-content: space-between;
  margin-bottom: 10px;
}

.header_left {
  width: 500px;
  display: flex;
  justify-content: space-between;
  margin-left: 10px;
  margin-bottom: 10px;
}

#coursesTable {
  padding: 15px 10px;
  text-align: center;
}

.Courses-head {
  background-color: #edffff;
}

.Courses-head>div {
  text-align: center;
  font-size: 14px;
  line-height: 28px;
}

.left-hand-TextDom,
.Courses-head {
  background-color: #f2f6f7;
}

.Courses-leftHand {
  background-color: #f2f6f7;
  font-size: 12px;
}

.Courses-leftHand .left-hand-index {
  color: #9c9c9c;
  margin-bottom: 4px !important;
}

.Courses-leftHand .left-hand-name {
  color: #666;
}

.Courses-leftHand p {
  text-align: center;
  font-weight: 900;
}

.Courses-head>div {
  border-left: none !important;
}

.Courses-leftHand>div {
  padding-top: 5px;
  border-bottom: 1px dashed rgb(219, 219, 219);
}

.Courses-leftHand>div:last-child {
  border-bottom: none !important;
}

.left-hand-TextDom,
.Courses-head {
  border-bottom: 1px solid rgba(0, 0, 0, 0.1) !important;
}

.Courses-content>ul {
  border-bottom: 1px solid rgb(21, 20, 20);
  box-sizing: border-box;
}

.Courses-content>ul:last-child {
  border-bottom: none !important;
}

.highlight-week {
  color: #02a9f5 !important;
}

.Courses-content li {
  text-align: center;
  color: #666666;
  font-size: 14px;
  line-height: 50px;
}

.course-hasContent {
  border-radius: 10px !important;
}

.Courses-content li span {
  padding: 6px 2px;
  box-sizing: border-box;
  line-height: 18px;
  border-radius: 4px;
  white-space: normal;
  word-break: break-all;
  cursor: pointer;
}

.grid-active {
  z-index: 9999;
}

.grid-active span {
  box-shadow: 0px 2px 4px rgba(0, 0, 0, 0.2);
}
</style>