<template>
  <el-container>
    <el-main>
      <el-row>
        <el-col :span="24" class="grade-desc">
          <strong>学生列表</strong>
          <el-row class="margin">
            <el-col :span="24">
              关键字：<el-input v-model="q.keyword" placeholder="请输入姓名或手机号" class="input"></el-input>
              所在年级：<el-select v-model="q.grade" placeholder="请选择">
                <el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value">
                </el-option>
              </el-select>
              <el-button type="primary" size="small" class="button" @click="query">搜索</el-button>
              <el-button type="info" size="small" @click="resetForm">重置</el-button>
              <el-button type="primary" :loading="exportLoading" @click="handleExport">导出excel</el-button>
              <el-button type="primary" @click="dialogVisible = true">添加</el-button>

              <!-- 添加区域 -->
              <el-dialog title="学生管理" :visible.sync="dialogVisible" width="30%" :before-close="handleClose">
                <!-- 内容主体区 -->
                <el-form :model="addForm" :rules="addFormRules" ref="addForm" label-width="70px">
                  <el-form-item label="姓名" prop="studentName">
                    <el-input v-model="addForm.studentName"></el-input>
                  </el-form-item>
                  <el-form-item label="班级" prop="classId">
                    <!-- <el-input v-model="addForm.classId"></el-input> -->
                    <el-select v-model="addForm.classId" placeholder="请选择">
                      <el-option v-for="item in classInfo" :key="item.classinfo.id" :label="item.classinfo.className"
                        :value="item.classinfo.id">
                      </el-option>
                    </el-select>
                  </el-form-item>
                  <el-form-item label="身份证" prop="IdNumber">
                    <el-input v-model="addForm.IdNumber"></el-input>
                  </el-form-item>
                  <el-form-item label="入学年份" prop="enrollmentTime">
                    <el-input v-model="addForm.enrollmentTime"></el-input>
                  </el-form-item>
                  <el-form-item label="qq号" prop="qqNumber">
                    <el-input v-model="addForm.qqNumber"></el-input>
                  </el-form-item>
                  <el-form-item label="微信号" prop="weChat">
                    <el-input v-model="addForm.weChat"></el-input>
                  </el-form-item>
                  <el-form-item label="性别" prop="sex">
                    <el-radio v-model="addForm.sex" label="男">男</el-radio>
                    <el-radio v-model="addForm.sex" label="女">女</el-radio>
                  </el-form-item>
                  <el-form-item label="手机号" prop="phoneNumber">
                    <el-input v-model="addForm.phoneNumber"></el-input>
                  </el-form-item>
                  <el-form-item label="住址" prop="address">
                    <el-input v-model="addForm.address"></el-input>
                  </el-form-item>
                </el-form>
                <!-- 底部区域 -->
                <span slot="footer" class="dialog-footer">
                  <el-button @click="dialogVisible = false">取 消</el-button>
                  <el-button type="primary" @click="addUser('addForm')">确 定</el-button>
                </span>
              </el-dialog>

              <!-- 编辑区域 -->
              <el-dialog title="学生管理" :visible.sync="updateVisible" width="30%" :before-close="handleClose">
                <!-- 内容主体区 -->
                <el-form :model="addForm" :rules="addFormRules" ref="addForm" label-width="70px">
                  <el-form-item label="姓名" prop="studentName">
                    <el-input v-model="addForm.studentName"></el-input>
                  </el-form-item>
                  <el-form-item label="班级" prop="classId">
                    <!-- <el-input v-model="addForm.classId"></el-input> -->
                    <el-select v-model="addForm.classId" placeholder="请选择">
                      <el-option v-for="item in classInfo" :key="item.classinfo.id" :label="item.classinfo.className"
                        :value="item.classinfo.id">
                      </el-option>
                    </el-select>
                  </el-form-item>
                  <el-form-item label="身份证" prop="idNumber">
                    <el-input v-model="addForm.idNumber"></el-input>
                  </el-form-item>
                  <el-form-item label="入学年份" prop="enrollmentTime">
                    <el-input v-model="addForm.enrollmentTime"></el-input>
                  </el-form-item>
                  <el-form-item label="qq号" prop="qqNumber">
                    <el-input v-model="addForm.qqNumber"></el-input>
                  </el-form-item>
                  <el-form-item label="微信号" prop="weChat">
                    <el-input v-model="addForm.weChat"></el-input>
                  </el-form-item>
                  <el-form-item label="性别" prop="sex">
                    <el-radio v-model="addForm.sex" label="男">男</el-radio>
                    <el-radio v-model="addForm.sex" label="女">女</el-radio>
                  </el-form-item>
                  <el-form-item label="手机号" prop="phoneNumber">
                    <el-input v-model="addForm.phoneNumber"></el-input>
                  </el-form-item>
                  <el-form-item label="住址" prop="address">
                    <el-input v-model="addForm.address"></el-input>
                  </el-form-item>
                </el-form>
                <!-- 底部区域 -->
                <span slot="footer" class="dialog-footer">
                  <el-button @click="updateVisible = false">取 消</el-button>
                  <el-button type="primary" @click="update('addForm')">确 定</el-button>
                </span>
              </el-dialog>

            </el-col>
          </el-row>
          <el-table ref="multipleTable" :data="tableData" tooltip-effect="dark" style="width: 100%"
            @selection-change="handleSelectionChange" border>
            <el-table-column type="selection"> </el-table-column>
            <el-table-column prop="studentInfo.enrollmentTime" label="入学时间"></el-table-column>
            <el-table-column prop="studentInfo.studentName" label="姓名"> </el-table-column>
            <el-table-column prop="specializedInfo.specializedName" label="专业"></el-table-column>
            <el-table-column prop="classInfo.className" label="所在班级"></el-table-column>
            <el-table-column prop="studentInfo.sex" label="性别"></el-table-column>
            <el-table-column prop="studentInfo.phoneNumber" label="手机号"></el-table-column>
            <el-table-column prop="studentInfo.address" label="住址"></el-table-column>
            <el-table-column label="操作">
              <template v-slot="{ row }">
                <!-- <el-button type="primary" size="mini" icon="el-icon-edit" @click="updateStudent(row.studentInfo.id)"> -->
                <el-button type="primary" size="mini" icon="el-icon-edit" @click="btn_updateStudent(row)">
                </el-button>
                <el-button type="danger" size="mini" icon="el-icon-delete" @click="deleteStudent(row.studentInfo.id)">
                </el-button>
              </template>
            </el-table-column>
          </el-table>
          <el-row>
            <el-col :span="24">
              <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange"
                :current-page.sync="currentPage1" :page-sizes="[5, 10, 15, 20]" :page-size="q.pageSize"
                layout="total, prev, pager, next,sizes" :total="tatal">
              </el-pagination>
            </el-col>
          </el-row>
        </el-col>
      </el-row>
    </el-main>
  </el-container>
</template>

<script>
import { exportExcel } from "@/utils/excle";
import { getStudents, addStudents, deleteStudents, updateStudents } from '@/api/studentApi';
import { getClass } from '@/api/classApi'
import store from "@/store";
import dayjs from "dayjs";

export default {
  inject: ["reload"],
  data() {
    return {
      exportLoading: false,
      // 添加区域是否打开
      dialogVisible: false,
      // 编辑区域是否打开
      updateVisible: false,
      // 添加、修改学生数据
      addForm: {
        studentName: "",
        qqNumber: "",
        idNumber: "",
        enrollmentTime: "",
        weChat: "",
        phoneNumber: "",
        address: "",
        id: "",
        studentId: "",
      },

      //班级信息
      classInfo: [],
      // 数据规则
      addFormRules: {
        studentName: [
          { required: true, message: "请输入用户名", trigger: "blur" },
        ],
        sex: [
          { required: true, message: "请选择性别", trigger: "blur" },
        ],
        IdNumber: [
          { required: true, message: "请输入身份证", trigger: "blur" },
        ],
        enrollmentTime: [
          { required: true, message: "请选择入学时间", trigger: "blur" },
        ],
        classId: [
          { required: true, message: "请选择班级", trigger: "blur" },
        ],
      },
      q: {
        // 初始页
        pageIndex: 1,
        // 返回数据量
        pageSize: 5,
        // 搜索关键字
        keyword: ""
      },
      // 总数据条数
      tatal: 50,
      // 年级
      options: [
        {
          label: "一年级",
          value: "1",
        },
        {
          label: "二年级",
          value: "2",
        },
        {
          label: "三年级",
          value: "3",
        },
        {
          label: "四年级",
          value: "4",
        },
      ],
      // 初始页为1
      currentPage1: 1,
      //   学生数据表
      tableData: [],
    };
  },
  beforeMount() {
    this.fetchData(1, 5, "")
    // 获取学生信息
    // getStudents().then(res => {
    //   console.log(res);
    //   if (res.code = 200) {
    //     this.$message.success("获取学生数据成功")
    //     this.tableData = res.data
    //     this.tatal = res.page.count
    //     this.q.pageIndex = pageIndex
    //     this.q.pageSize = pageSize
    //   }
    // });
    // 获取班级信息
    getClass().then(res => {
      this.classInfo = res.data.data
    })
  },
  methods: {
    // 删除学生
    deleteStudent(id) {
      deleteStudents(id).then(res => {
        if (res.code == 200) {
          this.$message.success('删除成功')
          this.reload();
        }
      })
    },
    // 修改至数据库
    update(updateForm) {
      this.$refs[updateForm].validate((valid) => {
        if (valid) {
          console.log(this.addForm);
          updateStudents(this.addForm.id, this.addForm).then(res => {
            console.log(res);
            this.reload()
          })
        } else {
          this.$message({
            showClose: true,
            message: "输入错误，请确认重试",
            type: "error",
          });
          return false;
        }

      })
    },
    // 修改学生数据
    btn_updateStudent(data) {
      this.updateVisible = true
      console.log(data);
      this.addForm.studentName = data.studentInfo.studentName
      this.addForm.qqNumber = data.studentInfo.qqNumber
      this.addForm.idNumber = data.studentInfo.idNumber
      this.addForm.enrollmentTime = data.studentInfo.enrollmentTime
      this.addForm.weChat = data.studentInfo.weChat
      this.addForm.phoneNumber = data.studentInfo.phoneNumber
      this.addForm.address = data.studentInfo.address
      this.addForm.id = data.studentInfo.id
      this.addForm.studentId = data.studentInfo.studentId
      console.log(this.addForm);
      // updateStudents(data.studentInfo.id, this.addForm).then(res => {
      //   console.log(res);
      // })
    },
    // 修改每页条数
    handleSizeChange(val) {
      console.log(`每页 ${val} 条`);
      this.q.pageSize = val;
      this.fetchData(this.q.pageIndex, val, this.q.keyword)
      // this.reload()
    },
    // 修改当前页
    handleCurrentChange(val) {
      this.q.pageIndex = val;
      this.fetchData(val, this.q.pageSize, this.q.keyword)
    },
    handleSelectionChange(val) {
      this.multipleSelection = val;
    },
    updateCollege(id) {
      console.log(id);
    },
    deleteCollege(id) {
      console.log(id);
    },
    // 抓取后台数据
    fetchData(PageIndex, PageSize, keyword) {
      getStudents(PageIndex, PageSize, keyword).then((res) => {
        console.log(res);
        if (res.code = 200) {
          this.$message.success("获取学生数据成功")
          this.tableData = res.data.data
          this.tatal = res.data.page.count
          this.q.pageIndex = res.data.page.pageIndex
          this.q.pageSize = res.data.page.pageSize
        } else {
          this.$message.warring('获取学生数据失败')
        }
      });
    },
    // 搜索
    query() {
      this.fetchData(this.q.pageIndex, this.q.pageSize, this.q.keyword);
    },
    // 重置搜索
    resetForm() {
      this.q.keyword = "";
      this.q.grade = "";
    },

    handleClose() { },
    // 添加学生
    addUser(addForm) {
      this.$refs[addForm].validate((valid) => {
        if (valid) {
          console.log(this.addForm);
          this.addForm.StudentId = ""
          addStudents(this.addForm).then(res => {
            console.log(res);
            if (res.data.code == 200) {
              this.$message.success('添加成功')
              this.dialogVisible = false
              this.reload();
            }
          })
        } else {
          this.$message({
            showClose: true,
            message: "输入错误，请确认重试",
            type: "error",
          });
          return false;
        }
      });

    },

    handleExport() {
      const header = [
        "入学时间",
        "姓名",
        "所在班级",
        "性别",
        "手机号",
        "住址",
      ];
      var list = []
      // console.log(this.tableData);
      for (let i = 0; i < this.tableData.length; i++) {
        var data = {}
        data.enrollmentTime = this.tableData[i].studentInfo.enrollmentTime
        data.studentName = this.tableData[i].studentInfo.studentName
        data.className = this.tableData[i].classInfo.className
        data.sex = this.tableData[i].studentInfo.sex
        data.phoneNumber = this.tableData[i].studentInfo.phoneNumber
        data.address = this.tableData[i].studentInfo.address
        list.push(data)
      }
      // console.log(list);
      exportExcel(header, list, "学生数据表");
    },
  },
};
</script>

<style lang="less" scoped>
.class {
  background: #fff;
  height: 60px;
  line-height: 60px;
  padding-left: 20px;
  margin: 20px 0;
}

.grade-desc {
  background: #fff;
  padding: 20px;
}

.margin {
  margin: 20px 0;
}

.input {
  width: 200px;
}

.button {
  margin-left: 20px;
}

.el-pagination {
  text-align: right;
  margin: 20px 0;
}
</style>