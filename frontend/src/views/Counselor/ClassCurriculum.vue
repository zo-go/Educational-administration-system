<template>
  <el-container>
    <el-main>
      <el-row>
        <el-col :span="24" class="grade-desc">
          <strong>班级课程</strong>
          <el-row class="margin">
            <el-col :span="24">
              课程：<el-input
                v-model="q.keyword"
                placeholder="请输入教材名称"
                class="input"
              ></el-input>
              <el-button type="primary" size="small" class="query"
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
                  <el-form-item label="班级" prop="date">
                    <el-input v-model="addForm.name"></el-input>
                  </el-form-item>
                  <el-form-item label="上课时间" prop="ClassTime">
                    <el-input v-model="addForm.College"></el-input>
                  </el-form-item>
                  <el-form-item label="课程" prop="Course">
                    <el-input v-model="addForm.class"></el-input>
                  </el-form-item>
                  <el-form-item label="老师" prop="Teacher">
                    <el-input v-model="addForm.Course"></el-input>
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
            <el-table-column label="班级">
              <template slot-scope="scope">{{ scope.row.date }}</template>
            </el-table-column>
            <el-table-column prop="ClassTime" label="上课时间">
            </el-table-column>
            <el-table-column prop="Course" label="课程"> </el-table-column>
            <el-table-column prop="Teacher" label="老师"> </el-table-column>
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
      addForm: {},
      tableData: [],
      q: [
        {
          // 初始页
          pageIndex: 1,
          // 返回数据量
          pageSize: 10,
          // 搜索关键字
          keyword: "",
        },
      ],
      currentPage1: 5,
      tableData: [
        {
          date: "软件2班",
          ClassTime: "8:00-8:45",
          Course: "vue",
          Teacher: "老虎",
        },
        {
          date: "人教版",
          ClassTime: "8:00-8:45",
          Course: "vue",
          Teacher: "老虎",
        },
        {
          date: "软件2班",
          ClassTime: "8:00-8:45",
          Course: "vue",
          Teacher: "老虎",
        },
        {
          date: "软件2班",
          ClassTime: "8:00-8:45",
          Course: "vue",
          Teacher: "老虎",
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
    // 抓取后台数据
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
      const header = ["班级", "上课时间", "课程", "老师"];
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
