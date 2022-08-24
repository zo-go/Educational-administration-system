<template>
  <el-container>
    <el-main>
      <el-row>
        <el-col :span="24" class="grade-desc">
          <strong>管理员列表</strong>
          <el-row class="margin">
            <el-col :span="24">
              管理员：<el-input v-model="q.keyword" placeholder="请输入管理员姓名" class="input"></el-input>
              <el-button type="primary" size="small" class="button" @click="query">搜索</el-button>
              <el-button type="info" size="small" @click="resetForm">重置</el-button>
              <el-button type="primary" :loading="exportLoading" @click="handleExport">导出excel</el-button>
              <el-button type="primary" @click="dialogVisible = true">添加</el-button>
              <el-dialog title="成绩录入" :visible.sync="dialogVisible" width="30%" :before-close="handleClose">
                <!-- 内容主体区 -->
                <!-- :rules="addFormRules" -->
                <el-form :model="addForm" ref="addFormRef" label-width="70px">
                  <el-form-item label="姓名 必填" prop="TeacherName">
                    <el-input v-model="addForm.TeacherName"></el-input>
                  </el-form-item>
                  <el-form-item label="性别 必填" prop="Sex">
                    <el-input v-model="addForm.Sex"></el-input>
                  </el-form-item>
                  <el-form-item label="手机号" prop="PhoneNumber">
                    <el-input v-model="addForm.PhoneNumber"></el-input>
                  </el-form-item>
                  <el-form-item label="身份证号 必填" prop="IdNumber">
                    <el-input v-model="addForm.IdNumber"></el-input>
                  </el-form-item>
                  <el-form-item label="住址" prop="Address">
                    <el-input v-model="addForm.Address"></el-input>
                  </el-form-item>
                  <el-form-item label="角色" prop="RoleName">
                    <el-input v-model="addForm.RoleName"></el-input>
                  </el-form-item>
                </el-form>
                <!-- 底部区域 -->
                <span slot="footer" class="dialog-footer">
                  <el-button @click="dialogVisible = false">取 消</el-button>
                  <el-button type="primary" @click="addUser">确 定</el-button>
                </span>
              </el-dialog>
            </el-col>
          </el-row>
          <el-table ref="multipleTable" :data="tableData" tooltip-effect="dark" style="width: 100%" border>
            <el-table-column type="selection"></el-table-column>
            <el-table-column prop="teacherName" label="姓名">
              <template slot-scope="scope">
                {{ scope.row.teacherInfo.teacherName }}
              </template>
            </el-table-column>
            <el-table-column prop="sex" label="性别">
              <template slot-scope="scope">
                {{ scope.row.teacherInfo.sex }}
              </template>
            </el-table-column>
            <el-table-column prop="phoneNumber" label="手机号">
              <template slot-scope="scope">
                {{ scope.row.teacherInfo.phoneNumber }}
              </template>
            </el-table-column>
            <el-table-column prop="address" label="住址">
              <template slot-scope="scope">
                {{ scope.row.teacherInfo.address }}
              </template>
            </el-table-column>
            <el-table-column prop="roleName" label="角色">
              <template slot-scope="scope">
                {{ scope.row.roleName }}
              </template>
            </el-table-column>
            <el-table-column label="操作">
              <template v-slot="{ row }">
                <el-button type="primary" size="mini" icon="el-icon-edit" @click="updateTeacher(row.teacherInfo.id)">
                </el-button>
                <el-button type="danger" size="mini" icon="el-icon-delete" @click="deleteTeacher(row.teacherInfo.id)">
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
import { getTeacher, addTeacher } from "@/api/teacherApi";
import { exportExcel } from "@/utils/excle";

export default {
  data() {
    return {
      exportLoading: false,
      dialogVisible: false,
      addForm: {
        WorkNumber: "",
        RoleName: "",
        TeacherName: "",
        IdNumber: "",
        AcademyNum: "",
        Sex: "",
        PhoneNumber: "",
        qqNumber: "",
        WeChat: "",
        Address: "",
      },
      tableData: [],
      q: {
        // 初始页
        pageIndex: 1,
        // 返回数据量
        pageSize: 10,
        // 搜索关键字
        keyword: "",
      },
      // 总数据条数
      tatal: 0,
      // 初始页为1
      currentPage1: 1,
    };
  },
  beforeMount() {
    this.fetchData(1, 10, "");
  },
  methods: {
    handleSizeChange(val) {
      this.fetchData(this.q.pageIndex, val, this.q.keyword);
    },
    handleCurrentChange(val) {
      this.fetchData(val, this.q.pageSize, this.q.keyword);
    },
    updateTeacher(id) {
      console.log(id);
    },
    deleteTeacher(id) {
      console.log(id);
    },
    // 抓取后台数据
    fetchData(pageIndex, pageSize, keyword) {
      getTeacher(pageIndex, pageSize, keyword).then((res) => {
        // console.log(res);

        // console.log(pageIndex);
        // console.log(pageSize);
        // console.log(keyword);
        this.tableData = res.data.data;
        // console.log(this.tableData);

        this.q.pageIndex = res.data.page.pageIndex;
        this.q.pageSize = res.data.page.pageSize;
        this.tatal = res.data.page.count;
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
    addUser() {
      let data = {
        WorkNumber: "",
        RoleName: this.addForm.RoleName,
        TeacherName: this.addForm.TeacherName,
        IdNumber: this.addForm.IdNumber,
        AcademyNum: this.addForm.AcademyNum,
        Sex: this.addForm.Sex,
        PhoneNumber: this.addForm.PhoneNumber,
        qqNumber: this.addForm.qqNumber,
        WeChat: this.addForm.WeChat,
        Address: this.addForm.Address,
      }
      console.log(data);
      // addTeacher(data).then(res=>{
      //   console.log(res);
      // })
    },
    addFormRules() {

    },
    handleExport() {
      const header = [
        "姓名",
        "性别",
        "手机号",
        "住址",
        "角色",
      ];
      var list = []
      // console.log(this.tableData);
      for (let i = 0; i < this.tableData.length; i++) {
        var data = {}
        data.teacherName = this.tableData[i].teacherInfo.teacherName
        data.sex = this.tableData[i].teacherInfo.sex
        data.phoneNumber = this.tableData[i].teacherInfo.phoneNumber
        data.address = this.tableData[i].teacherInfo.address
        data.roleName = this.tableData[i].roleName
        list.push(data)
      }
      // console.log(list);
      exportExcel(header, list, "权限数据表");
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