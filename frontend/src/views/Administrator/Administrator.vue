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
              <el-button type="primary" @click="add">添加</el-button>
              <el-dialog :title="title" :visible.sync="dialogVisible" width="30%" :before-close="handleClose">
                <!-- 内容主体区 -->
                <!-- :rules="addFormRules" -->
                <el-form :model="addForm" ref="addFormRef" label-width="70px">
                  <el-form-item label="用户名" prop="UserName">
                    <el-input v-model="addForm.UserName"></el-input>
                  </el-form-item>
                  <el-form-item label="密码" prop="PassWord">
                    <el-input v-model="addForm.PassWord"></el-input>
                  </el-form-item>
                </el-form>
                <!-- 底部区域 -->
                <span slot="footer" class="dialog-footer">
                  <el-button @click="cancel">取 消</el-button>
                  <el-button type="primary" @click="save">确 定</el-button>
                </span>
              </el-dialog>
            </el-col>
          </el-row>
          <el-table ref="multipleTable" :data="tableData" tooltip-effect="dark" style="width: 100%" border>
            <el-table-column type="selection"> </el-table-column>
            <el-table-column prop="userName" label="用户名"></el-table-column>
            <el-table-column prop="passWord" label="密码"></el-table-column>
            <!-- <el-table-column prop="sex" label="性别"> </el-table-column>
            <el-table-column prop="phone" label="手机号"> </el-table-column>
            <el-table-column prop="address" label="住址"> </el-table-column> -->
            <el-table-column label="操作">
              <template v-slot="{ row }">
                <el-button type="primary" size="mini" icon="el-icon-edit" @click="updateTeacher(row)">
                </el-button>
                <el-button type="danger" size="mini" icon="el-icon-delete" @click="deleteTeacher(row.id)">
                </el-button>
              </template>
            </el-table-column>
          </el-table>
          <el-row>
            <el-col :span="24">
              <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange"
                :current-page.sync="currentPage1" :page-sizes="[10, 15, 20]" :page-size="100"
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
import { getAdmin, addUser, getRole } from "@/api/AdministratorApi";
import { deleteUser, updateUserInfo } from "@/api/userApi";
import { exportExcel } from "@/utils/excle";

export default {
  data() {
    return {
      exportLoading: false,
      dialogVisible: false,
      addForm: {
        UserName: "",
        PassWord: "",
        RoleId: ""
      },
      id: "",
      title: "添加",
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
    // 改变当前页大小
    handleSizeChange(val) {
      this.fetchData(this.q.pageIndex, val, this.q.keyword);
    },
    // 改变当前页数
    handleCurrentChange(val) {
      this.fetchData(val, this.q.pageSize, this.q.keyword);
    },
    // 添加按钮
    add() {
      this.dialogVisible = true;
      this.title = "添加"

    },
    // 更新按钮
    updateTeacher(row) {
      // console.log(row);
      this.dialogVisible = true;
      this.title = "修改";
      this.id = row.id;

      this.addForm.UserName = row.userName
      this.addForm.PassWord = row.passWord

      // console.log(this.addForm);
    },
    // 弹窗内取消按钮
    cancel() {
      this.addForm.UserName = "";
      this.addForm.PassWord = "";
      this.id = "";

      this.dialogVisible = false;
    },
    // 删除
    deleteTeacher(id) {
      deleteUser(id).then(res => {
        console.log(res);
      })
    },
    // 抓取后台数据
    fetchData(pageIndex, pageSize, keyword) {
      getAdmin(pageIndex, pageSize, keyword).then((res) => {
        // console.log(res);
        this.tableData = res.data.data;
        // this.CateList.forEach((item) => {
        //     item.createdAt = dayjs(item.createdAt).format(" YYYY-MM-DDHH:mm:ss");
        // });
        // this.total = res.data.pageData.total;
        // this.q = res.data.pageData

        // console.log(pageIndex);
        // console.log(pageSize);
        // console.log(keyword);
        // this.tableData = res.data.data;
        // console.log(this.tableData);

        // this.q.pageIndex = res.data.page.pageIndex;
        // this.q.pageSize = res.data.page.pageSize;
        // this.tatal = res.data.page.count;
      });
    },
    // 搜索
    query() {
      // console.log(this.q.keyword);
      this.fetchData(this.q.pageIndex, this.q.pageSize, this.q.keyword);
    },
    // 重置搜索
    resetForm() {
      this.q.keyword = "";
    },
    handleClose() {

    },
    // 弹窗保存按钮
    save() {
      console.log(this.addForm);
      if (this.title == "添加") {
        getRole().then(res => {
          // console.log(res);
          res.data.data.forEach(item => {
            if (item.roleName == "管理员") {
              this.addForm.RoleId = item.id
            }
          });
          console.log(this.addForm);
          addUser(this.addForm).then(res => {
            // console.log(res);
            this.cancel();
            this.fetchData(1, 10, "");
          })
        })
      } else if (this.title == "修改") {
        getRole().then(res => {
          // console.log(res);
          res.data.data.forEach(item => {
            if (item.roleName == "管理员") {
              this.addForm.RoleId = item.id
            }
          });
          console.log(this.addForm);
          updateUserInfo(this.id, this.addForm).then(res => {
            console.log(res);
            this.cancel();
            this.fetchData(1, 10, "");
          })
        })
      }

    },
    addFormRules() {

    },
    handleExport() {
      const header = ["姓名", "性别", "手机号", "住址"];
      exportExcel(header, this.tableData, "数据表");
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