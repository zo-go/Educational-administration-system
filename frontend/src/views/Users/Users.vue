<template>
  <div>
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <span>用户列表</span>
      </div>
      <!-- 搜索区域 -->
      <div class="search-box">
        <el-input
          v-model="keyword"
          placeholder="请输入关键字"
          style="width: 300px"
        ></el-input>
        <!-- <el-form-item label="发布状态" style="margin-left: 15px">
            <el-select v-model="q.state" placeholder="请选择状态" size="small">
              <el-option label="已发布" value="已发布"></el-option>
              <el-option label="草稿" value="草稿"></el-option>
            </el-select>
          </el-form-item> -->
        <el-button
          type="primary"
          size="small"
          @click="getCateList"
          style="margin-left: 0.8%"
          >搜索</el-button
        >
        <el-button type="info" size="small" @click="resetForm">重置</el-button>
      </div>

      <!-- 文章表格区域 -->
      <el-table :data="CateList" style="width: 100%" border>
        <el-table-column prop="title" label="用户名">
          <template v-slot="{ row }">
            <el-link type="primary" @click="ArticleDetails(row.id)">{{
              row.username
            }}</el-link>
          </template>
        </el-table-column>
        <el-table-column prop="cate_name" label="用户密码">
          <template v-slot="{ row }">{{ row.password }}</template>
        </el-table-column>
        <el-table-column prop="cate_name" label="用户昵称">
          <template v-slot="{ row }">{{ row.nickname }}</template>
        </el-table-column>
        <el-table-column prop="cate_name" label="用户邮箱">
          <template v-slot="{ row }">{{ row.email }}</template>
        </el-table-column>
        <el-table-column prop="pub_date" label="创建时间">
          <template v-slot="{ row }">{{ row.createdAt }}</template>
        </el-table-column>
        <el-table-column prop="state" label="状态"> </el-table-column>
        <el-table-column label="操作">
          <template v-slot="{ row }">
            <el-button type="danger" size="mini" @click="deleteUser(row.id)"
              >删除</el-button
            >
          </template>
        </el-table-column>
      </el-table>
      <!-- 分页区域 -->
      <el-pagination
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
        :current-page="q.pageIndex"
        :page-sizes="[5, 10, 15, 20]"
        :page-size="q.pageSize"
        layout="total, sizes, prev, pager, next, jumper"
        :total="total"
      >
      </el-pagination>
    </el-card>
  </div>
</template>

<script>
import dayjs from "dayjs";
import { time } from "echarts";
import { getUsersInfo, deleteUser } from "@/api/userApi";
export default {
  name: "ArtList",
  inject: ["reload"],
  data() {
    return {
      keyword: "",
      // 查询参数对象
      q: {
        pageIndex: 1,
        pageSize: 10,
        keyword: "",
      },

      // 点击文章详情开关
      artDetailsVisible: false,
      total: 0,
      // 新增文章存储数据的对象
      pubForm: {
        title: "",
        content: "",
        cover_img: null,
      },
      // 存放文章详情的对象
      ArtDetail: {},
      // 页面文章列表
      CateList: [],
      // 新增文章表单校验规则
      pubFormRules: {
        title: [
          { required: true, message: "请输入文章标题", trigger: "blur" },
          {
            min: 1,
            max: 30,
            message: "长度在 1 到 30 个字符",
            trigger: "blur",
          },
        ],
        content: [
          { required: true, message: "请输入文章内容", trigger: "blur" },
        ],
      },
      // 存储文章分类列表
      ArtList: [],
    };
  },
  methods: {
    handleSizeChange(val) {
      // console.log(`每页 ${val} 条`);
      this.q.pageSize = val;
      this.fetchData();
      // this.reload();
    },
    handleCurrentChange(val) {
      console.log(`当前页: ${val}`);
      this.q.pageIndex = val > 0 ? val : 1;
      console.log(this.q);
      this.fetchData();
    },
    // 拉取后台数据（带分页）
    fetchData() {
      const params = {
        pageIndex: this.q.pageIndex,
        pageSize: this.q.pageSize,
        keyword: this.q.keyword,
      };
      getUsersInfo(params).then((res) => {
        console.log(res);
        this.CateList = res.data.data;
        this.CateList.forEach((item) => {
          item.createdAt = dayjs(item.createdAt).format(" YYYY-MM-DDHH:mm:ss");
        });
        this.total = res.data.pageData.total;
        // this.q = res.data.pageData
      });
    },

    // 初始化的时候获取页面文章的函数
    async getCateList() {
      this.$message.success("查询成功");
      console.log(this.keyword);
      this.q.keyword = this.keyword;
      console.log(this.q);
      this.fetchData();
      // this.CateList = res.data;
      // this.total = res.total;
    },
    // 格式化时间
    FormDate(time) {
      return dayjs(time).format(" YYYY-MM-DDHH:mm:ss");
    },
    // 点击重置按钮重新加载
    resetForm() {
      this.keyword = "";
    },
    // 点击删除对应的文章
    deleteUser(id) {
      this.$confirm("此操作将删除该用户, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          deleteUser(id).then((res) => {
            if (res.code == 10002) {
              this.$message.success("删除成功");
              this.reload();
            } else {
              this.$message.warning("删除失败");
            }
          });
        })
        .catch(() => {
          this.$message.info("已取消删除");
        });
    },
  },
  created() {
    this.fetchData();
  },
};
</script>

<style lang="less" scoped>
.search-box {
  margin-bottom: 5px;
  display: flex;
  // justify-content: space-between;
  align-items: stretch;
  // .btn-pub {
  //   margin-top: 5px;
  // }
}
/deep/ .ql-editor {
  height: 300px;
}
</style>
