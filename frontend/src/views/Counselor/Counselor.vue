<template>
  <el-container>
    <el-main>
      <el-row>
        <el-col :span="24" class="grade-desc">
          <strong>辅导员列表</strong>
          <el-row class="margin">
            <el-col :span="24">
              关键字：<el-input v-model="q.keyword" placeholder="请输入姓名" class="input"></el-input>
              执教年级：<el-select v-model="q.grade" placeholder="请选择">
                <el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value">
                </el-option>
              </el-select>
              <el-button type="primary" size="small" class="button" @click="query">搜索</el-button>
              <el-button type="info" size="small" @click="resetForm">重置</el-button>
              <el-button type="primary" :loading="exportLoading" @click="handleExport">
                导出excel
              </el-button>
              <el-button type="primary" @click="addDialog">添加</el-button>

              <!-- 添加 修改-->
              <el-dialog :title="title" :visible.sync="dialogVisible" width="30%" :before-close="handleClose">
                <!-- 内容主体区 -->
                <el-form :model="addForm" :rules="addFormRules" ref="addForm" label-width="70px">
                  <el-form-item label="姓名" prop="teacherName">
                    <el-input v-model="addForm.teacherName"></el-input>
                  </el-form-item>
                  <el-form-item label="学院" prop="academyNum">
                    <el-select v-model="addForm.academyNum" placeholder="请选择">
                      <el-option v-for="item in academy" :key="item.id" :label="item.academyName"
                        :value="item.academyNum">
                      </el-option>
                    </el-select>
                  </el-form-item>
                  <el-form-item label="角色" prop="roleName">
                    <el-select v-model="addForm.roleName" placeholder="请选择">
                      <el-option v-for="item in role" :key="item.id" :label="item.roleName" :value="item.roleName">
                      </el-option>
                    </el-select>
                  </el-form-item>
                  <el-form-item label="性别" prop="sex">
                    <el-radio v-model="addForm.sex" label="男">男</el-radio>
                    <el-radio v-model="addForm.sex" label="女">女</el-radio>
                  </el-form-item>
                  <el-form-item label="身份证" prop="idNumber">
                    <el-input v-model="addForm.idNumber"></el-input>
                  </el-form-item>
                </el-form>
                <!-- 底部区域 -->
                <span slot="footer" class="dialog-footer">
                  <el-button @click="dialogVisible = false">取 消</el-button>
                  <el-button type="primary" @click="save('addForm')">确 定</el-button>
                </span>
              </el-dialog>
            </el-col>
          </el-row>
          <el-table ref="multipleTable" :data="tableData" tooltip-effect="dark" style="width: 100%"
            @selection-change="handleSelectionChange" border>
            <el-table-column type="selection"> </el-table-column>
            <el-table-column prop="teacherInfo.createdAt" label="入职时间"> </el-table-column>
            <el-table-column prop="teacherInfo.teacherName" label="姓名"> </el-table-column>
            <el-table-column prop="academyName" label="所在学院"> </el-table-column>
            <el-table-column prop="teacherInfo.sex" label="性别"> </el-table-column>
            <el-table-column prop="teacherInfo.phoneNumber" label="手机号"> </el-table-column>
            <el-table-column prop="teacherInfo.address" label="住址"> </el-table-column>
            <el-table-column label="操作">
              <template v-slot="{ row }">
                <el-button type="primary" size="mini" icon="el-icon-edit" @click="updateDialog(row)">
                </el-button>
                <el-button type="danger" size="mini" icon="el-icon-delete" @click="deleteAA(row.teacherInfo.id)">
                </el-button>
              </template>
            </el-table-column>
          </el-table>
          <el-row>
            <el-col :span="24">
              <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange"
                :current-page.sync="currentPage1" :page-sizes="[5, 10, 15, 20]" :page-size="100"
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
import dayjs from 'dayjs';
import axios from 'axios'
import { exportExcel } from "@/utils/excle";
import store from '@/store';
import { getTeacher, addTeacher, updateTeacher, deleteTeacher } from '@/api/teacherApi'
import { getAcademy } from '@/api/academyApi'
import { getRole } from '@/api/roleApi'

export default {
  inject: ["reload"],
  data() {
    return {
      exportLoading: false,
      dialogVisible: false,
      // 用户信息个人
      personalInfo: {},
      // 添加、修改用户
      addForm: {},
      // 添加、修改标题
      title: "",
      // 学院
      academy: {},
      // 角色
      role: {},
      q: {
        // 初始页
        pageIndex: 1,
        // 返回数据量
        pageSize: 10,
        // 搜索关键字
        keyword: "",
        // 年级
        grade: "",
      },

      // 表单规则
      addFormRules: {
        teacherName: [
          { required: true, message: "请输入用户名", trigger: "blur" },
        ],
        sex: [
          { required: true, message: "请选择性别", trigger: "blur" },
        ],
        idNumber: [
          { required: true, message: "请输入身份证", trigger: "blur" },
        ],
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
      // 老师数据表
      tableData: [],
    };
  },
  created() {
    this.fetchData(1, 5, "")
    // 获取学院
    // getAcademy().then(res => {
    //   console.log(res);
    //   this.academy = res.data
    // })

    // // 获取角色
    // getRole().then(res => {
    //   console.log(res);
    //   this.role = res.data
    // })
  },
  methods: {
    // 添加按钮
    addDialog() {
      this.title = '添加',
        this.dialogVisible = true
    },
    handleClose() { },
    // 添加、修改教师
    save(addForm) {
      this.$confirm('此操作将永久删除该文件, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        this.$refs[addForm].validate((valid) => {
          if (valid) {
            if (this.title == '添加') {
              console.log(this.addForm);
              addTeacher(this.addForm).then(res => {
                // console.log(res);
                if (res.code == 200) {
                  this.$message.success(res.msg)
                  this.dialogVisible = false
                  this.reload()
                }
              })
            } else {
              console.log(this.addForm);
              this.addForm.id = this.personalInfo.teacherInfo.id
              console.log(this.personalInfo);
              updateTeacher(this.addForm.id, this.addForm).then(res => {
                if (res.code == 200) {
                  this.$message.success(res.msg)
                  this.dialogVisible = false
                  this.reload();
                }
              })
            }

          } else {
            this.$message({
              showClose: true,
              message: "输入错误，请确认重试",
              type: "error",
            });
            return false;
          }
        });
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消'
        });
      });
    },

    handleExport() {
      const header = [
        "入职时间",
        "姓名",
        "所在学院",
        "性别",
        "手机号",
        "住址",
      ];
      var list = []
      // console.log(this.tableData);
      for (let i = 0; i < this.tableData.length; i++) {
        var data = {}
        data.createdAt = this.tableData[i].teacherInfo.createdAt
        data.academyName = this.tableData[i].academyName
        data.teacherName = this.tableData[i].teacherInfo.teacherName
        data.sex = this.tableData[i].teacherInfo.sex
        data.phoneNumber = this.tableData[i].teacherInfo.phoneNumber
        data.address = this.tableData[i].teacherInfo.address
        list.push(data)
      }
      // console.log(list);
      exportExcel(header, list, "辅导员数据表");
    },
    // 修改页面大小
    handleSizeChange(val) {
      this.q.pageSize = val;
      this.fetchData(this.q.pageIndex, val, this.q.keyword)
    },
    // 修改页
    handleCurrentChange(val) {
      this.q.pageIndex = val;
      console.log(this.q);
      this.fetchData(val, this.q.pageSize, this.q.keyword)
    },
    handleSelectionChange(val) {
      this.multipleSelection = val;
    },
    // 修改按钮
    updateDialog(data) {
      this.title = '修改'
      this.dialogVisible = true
      this.personalInfo = data
    },
    // 删除
    deleteAA(id) {
      this.$confirm('此操作将永久删除该文件, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        deleteTeacher(id).then(res => {
          if (res.code == 200) {
            this.$message.success(res.msg)
            this.reload()
          }
        })
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消删除'
        });
      });

    },
    // 抓取后台数据
    fetchData(PageIndex, PageSize, keyword) {
      getTeacher(PageIndex, PageSize, keyword).then((res) => {
        console.log(res);
        if (res.code = 200) {
          this.$message.success("获取教师数据成功")
          this.tableData = res.data.data
          this.tableData.forEach(item => {
            item.teacherInfo.createdAt = dayjs(item.teacherInfo.createdAt).format("YYYY-MM-DD");
          });
          // console.log(this.tableData);
          this.tatal = res.data.page.count
          this.q.pageIndex = res.data.page.pageIndex
          this.q.pageSize = res.data.page.pageSize
        } else {
          this.$message.warring('获取教师数据失败')
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

    add() {
      let newObj = {
        date: this.q.date,
        name: this.q.name,
        phoneNo: this.q.phoneNo,
        class: this.q.class,
        address: this.q.address,
        profession: this.q.profession,
      };
      this.list.push(newObj);
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