<template>
  <el-container>
    <el-main>
      <el-row>
        <el-col :span="24" class="grade-desc">
          <strong>转班申请</strong>
          <el-row class="margin">
            <el-col :span="24">
              关键字：<el-input
                v-model="input"
                placeholder="请输入教材名称"
                class="input"
              ></el-input>
              <el-button
                type="primary"
                size="small"
                class="button"
                @click="query"
                >搜索</el-button
              >
              <el-button type="info" size="small" @click="resetForm"
                >重置</el-button
              >
              <el-button
                type="primary"
                :loading="exportLoading"
                @click="handleExport"
                >导出excel</el-button
              >
              <el-button type="primary" @click="dialogVisible = true"
                >添加</el-button
              >
              <el-dialog
                title="成绩录入"
                :visible.sync="dialogVisible"
                width="30%"
                :before-close="handleClose"
              >
                <!-- 内容主体区 -->
                <el-form
                  :model="addForm"
                  :rules="addFormRules"
                  ref="addFormRef"
                  label-width="70px"
                >
                  <el-form-item label="现班级" prop="date">
                    <el-input v-model="addForm.name"></el-input>
                  </el-form-item>
                  <el-form-item label="申请人" prop="Applicant">
                    <el-input v-model="addForm.College"></el-input>
                  </el-form-item>
                  <el-form-item label="申请时间" prop="ApplyTime">
                    <el-input v-model="addForm.class"></el-input>
                  </el-form-item>
                  <el-form-item label="申请原因" prop="ApplyReason">
                    <el-input v-model="addForm.Course"></el-input>
                  </el-form-item>
                  <el-form-item label="申请班级" prop="ApplyClass">
                    <el-input v-model="addForm.ClassTime"></el-input>
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
          <el-table
            ref="multipleTable"
            :data="tableData"
            tooltip-effect="dark"
            style="width: 100%"
            @selection-change="handleSelectionChange"
            border
            class="student-table"
          >
            <el-table-column type="selection"> </el-table-column>
            <el-table-column label="现班级">
              <template slot-scope="scope">{{ scope.row.date }}</template>
            </el-table-column>
            <el-table-column prop="Applicant" label="申请人"> </el-table-column>
            <el-table-column prop="ApplyTime" label="申请时间">
            </el-table-column>
            <el-table-column
              prop="ApplyReason"
              label="申请原因"
            ></el-table-column>
            <el-table-column prop="ApplyClass" label="申请班级">
            </el-table-column>
            <el-table-column label="操作">
              <el-button type="primary" class="editor">编辑</el-button>
              <el-button type="primary" class="delete">删除</el-button>
            </el-table-column>
          </el-table>
          <el-row>
            <el-col :span="24">
              <el-pagination
                @size-change="handleSizeChange"
                @current-change="handleCurrentChange"
                :current-page.sync="currentPage1"
                :page-sizes="[100, 200, 300, 400]"
                :page-size="100"
                layout="total, prev, pager, next,sizes"
                :total="1000"
              >
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
export default {
  data() {
    return {
      exportLoading: false,
       dialogVisible: false,
      addForm:{},
      tableData: [],
      input: "",
      value1: "",
      currentPage1: 5,
      tableData: [
        {
          date: "软件1班",
          Applicant: "山猪",
          ApplyTime: "2022-8-11",
          ApplyReason: "没原因",
          ApplyClass: "软件7班",
        },
        {
          date: "软件1班",
          Applicant: "山猪",
          ApplyTime: "2022-8-11",
          ApplyReason: "没原因",
          ApplyClass: "软件7班",
        },
        {
          date: "软件1班",
          Applicant: "山猪",
          ApplyTime: "2022-8-11",
          ApplyReason: "没原因",
          ApplyClass: "软件7班",
        },
        {
          date: "软件1班",
          Applicant: "山猪",
          ApplyTime: "2022-8-11",
          ApplyReason: "没原因",
          ApplyClass: "软件7班",
        },
      ],
      multipleSelection: [],
    };
  },
  methods: {
    handleSizeChange(val) {
      console.log(`每页 ${val} 条`);
    },
    handleCurrentChange(val) {
      console.log(`当前页: ${val}`);
    },
    handleSelectionChange(val) {
      this.multipleSelection = val;
    },
    fetchData() {
      let params = {
        pageIndex: this.q.pageIndex,
        pageSize: this.q.pageSize,
        keyword: this.q.keyword,
        grade: this.q.grade,
      };
      console.log(params);
      // getUsersInfo(params).then((res) => {
      //     console.log(res);
      //     this.CateList = res.data.data;
      //     this.CateList.forEach((item) => {
      //         item.createdAt = dayjs(item.createdAt).format(" YYYY-MM-DDHH:mm:ss");
      //     });
      //     this.total = res.data.pageData.total;
      //     // this.q = res.data.pageData
      // });
    },
    // 搜索
    query() {
      this.fetchData();
    },
    // 重置搜索
    resetForm() {
      this.q.keyword = "";
      this.q.grade = "";
    },
     handleClose() {},
    addUser(){

    },
    addFormRules(){

    },
    handleExport() {
      const header = ["现班级", "申请人", "申请时间", "申请原因", "申请班级"];
      exportExcel(header, this.tableData, "数据表");
    },
  },
};
</script>

<style scoped>
.el-container {
  background: #f3f3f3;
}
.class {
  background: #fff;
  height: 60px;
  line-height: 60px;
  padding-left: 20px;
  border: solid 1px #aaa;
  margin: 20px 0;
}

.grade-desc {
  background: #fff;
  /* height: 60px; */
  border: solid 1px #aaa;
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
