<template>
  <div>
    <el-card>
      <div class="header-breadcrumb"></div>
      <div class="top-search-form text-center">
        <el-form :inline="true" class="demo-form-inline">
          <el-form-item>
            <el-input v-model="keyword" placeholder="请输入问卷标题"></el-input>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" size="small" class="button" @click="getKeyword">搜索</el-button>
          </el-form-item>
          <!-- <el-form-item>
          <el-button type="warning" icon="el-icon-search" circle @click="getKeyword"></el-button>
        </el-form-item> -->
          <!-- <el-form-item>
          <div class="table-data-action">
            <div style="text-align: right; margin-bottom: 3%">
              <el-button size="small" type="primary" @click="AddRecord" icon="el-icon-delete">创建问卷</el-button>
            </div>
          </div>
        </el-form-item> -->
          <el-form-item>
            <el-button size="small" type="danger" icon="el-icon-delete">批量删除</el-button>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" size="small" :loading="exportLoading" @click="handleExport">导出excel</el-button>
          </el-form-item>
        </el-form>
      </div>
      <div class="lcz-statistics-main">
        <div class="main-table-list">
          <el-table :data="tableData" v-loading="loading" style="width: 100%">
            <el-table-column type="selection" width="55"></el-table-column>
            <el-table-column prop="list.questionnaireTheme" label="问卷主题"></el-table-column>
            <el-table-column prop="list.questionnaireTitle" label="问卷标题"></el-table-column>
            <el-table-column prop="list.studentName" label="提交人"></el-table-column>
            <el-table-column fixed="right" label="操作" width="200">
              <template slot-scope="scope">
                <el-button type="text" size="mini" icon="el-icon-s-unfold" @click="handleEdit(scope.$index, scope.row)">
                  详情
                </el-button>
                <el-popconfirm title="确定删除吗？" @confirm="handleDelete(scope.$index, scope.row)">
                  <el-button slot="reference" size="mini" type="text" icon="el-icon-delete">删除</el-button>
                </el-popconfirm>
              </template>
            </el-table-column>
          </el-table>
        </div>
        <div class="footer-chart-data">
          <div class="block">
            <el-pagination class="pager" background layout="prev, pager, next" :total="total" :page-size="pageSize"
              :current-page="pageNum" @current-change="handleSizeChange"></el-pagination>
          </div>
        </div>
      </div>
    </el-card>
  </div>
</template>

<script>
import { exportExcel } from "@/utils/excle";
import { GetRecordResList } from "@/api/RecordRes";

export default {
  data() {
    return {
      loading: true,
      tableData: [],
      exportLoading: false,
      pageIndex: 1, // 当前页码
      total: 0, // 总条数
      pageNum: 1, // 每页的数据条数
      pageSize: 10,
      keyword: "",
      keyWord: "",
    };
  },
  created() {
    this.getDate();
  },
  methods: {
    // 获取初始值
    getDate() {
      this.loading = true;
      this.tableData = [];
      let data = {
        pageIndex: this.pageIndex,
        pageSize: this.pageSize,
      };
      GetRecordResList(data).then((res) => {
        this.tableData = res.data.data;
      });
      this.loading = false;
    },
    // 详情
    handleEdit(index, row) {
      this.$router.push({
        name: "QuestionnaireFullInfo",
        params: {
          id: row.list.questionnaireId,
          created: row.list.createdBy,
        },
      });
    },
    // 导出excel
    handleExport() {
      const header = ["ID", "院系编号", "院系名称", "专业编号", "专业名称"];
      exportExcel(header, this.tableData, "数据表");
    },
    // 删除
    handleDelete(index, row) {
      console.log("删除");
    },
    // 关键字查询
    getKeyword() {
      if (this.keyword) {
        this.keyWord = this.keyword;
        let data = {
          pageIndex: this.pageIndex,
          pageSize: this.pageSize,
          keyword: this.keyWord,
        };
        console.log(this.keyword + "查询");
      } else {
        this.getDate();
      }
    },
    //每页条数改变时触发 选择一页显示多少行
    handleSizeChange(val) {
      this.pageIndex = val;
      if (this.keyWord) {
        this.keyword = this.keyWord;
        this.getKeyword();
      } else {
        this.getData();
      }
    },
  },
  mounted() { },
};
</script>



<style lang="less" scoped>
.lcz-statistics-main {
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;

  .text-center {
    align-items: center;
    text-align: center;
  }

  .main-table-list {
    width: 100%;
  }

  .footer-chart-data {
    margin-top: 2rem;
    width: 100%;
    align-items: flex-end;
    text-align: right;
  }

  .table-data-action {
    width: 100%;
    text-align: left;
    align-items: flex-start;
  }

  .header-breadcrumb {
    float: left;
  }
}
</style>