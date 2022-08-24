<template>
  <div>
    <el-card>


      <div class="header-breadcrumb"></div>
      <div class="top-search-form text-center">
        <el-form :inline="true" class="demo-form-inline">
          <el-form-item>
            <el-input v-model="key" placeholder="请输入问卷标题"></el-input>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" size="small" class="button" @click="getKeyword">搜索</el-button>
          </el-form-item>
          <!-- <el-form-item>
          <el-button type="warning" icon="el-icon-search" circle @click="getKeyword"></el-button>
        </el-form-item> -->
          <el-form-item>
            <div class="table-data-action">
              <div style="text-align: right; margin-bottom: 3%">
                <el-button size="small" type="primary" @click="AddRecord" icon="el-icon-delete">创建问卷</el-button>
              </div>
            </div>
          </el-form-item>
        </el-form>
      </div>
      <div class="lcz-statistics-main">
        <div class="main-table-list">
          <el-table :data="tableData" v-loading="loading" style="width: 100%">
            <el-table-column type="selection" width="55"></el-table-column>
            <el-table-column prop="questionnaireTheme" label="问卷主题"></el-table-column>
            <el-table-column prop="questionnaireTitle" label="问卷标题"></el-table-column>
            <el-table-column prop="createdAt" label="发布时间"></el-table-column>
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
import {
  GetQuestionnaireList,
  GetQuestionnaireByKeyword,
  DeleteQuestionnaire,
} from "@/api/Questionnaire";

import { changeTime } from "@/utils/timeFormat";
export default {
  data() {
    return {
      key: "",
      loading: true,
      tableData: [],
      exportLoading: false,
      pageIndex: 1, // 当前页码
      total: 0, // 总条数
      pageNum: 1, // 每页的数据条数
      pageSize: 10,
    };
  },
  created() {
    this.getData();
  },
  methods: {
    // 获取初始值
    getData() {
      this.loading = true;
      this.tableData = [];
      let data = {
        pageIndex: this.pageIndex,
        pageSize: this.pageSize,
      };
      GetQuestionnaireList(data).then((res) => {
        this.tableData = res.data.data;
        this.tableData.forEach((item) => {
          item.createdAt = changeTime(item.createdAt);
        });
        this.loading = false;
      });
    },
    AddRecord() {
      this.$router.push("/Administrator/satisfactionsurveys");
    },
    // 编辑
    handleEdit(index, row) {
      this.$router.push(`/Administrator/publish/${row.id}`);
    },
    // 导出excel
    handleExport() {
      //   const header = ["ID", "院系编号", "院系名称", "专业编号", "专业名称"];
      //   exportExcel(header, this.tableData, "数据表");
    },
    // 删除
    handleDelete(index, row) {
      DeleteQuestionnaire(row.id).then(({ data }) => {
        if (data.code == 200) {
          this.getData();
        } else {
          alert("删除失败");
        }
      });
    },
    // 关键字查询
    getKeyword() {
      if (this.key.length > 0) {
        let data = {
          pageIndex: this.pageIndex,
          pageSize: this.pageSize,
          keyword: this.key,
        };

        GetQuestionnaireByKeyword(data).then((res) => {
          this.tableData = res.data.data;
          this.total = res.data.page.count;
        });
      } else {
        this.getData();
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