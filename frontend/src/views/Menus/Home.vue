<template>
  <div class="center">
    <el-card class="box-card">
      <template #header>
        <div class="card-header">
          <span>教务管理</span>
        </div>
      </template>
      <el-row :gutter="12">
        <template>
          <div class="header">
            <span>学院</span>
          </div>
        </template>
        <div style="height: 30%">
          <el-col :span="5">
            <el-card shadow="hover"> 学院管理 </el-card>
          </el-col>
        </div>
        <div style="height: 30%">
          <el-col :span="5">
            <el-card shadow="hover"> 学院专业 </el-card>
          </el-col>
        </div>
        <div>
          <div style="height: 30%">
            <el-col :span="5">
              <el-card shadow="hover"> 教材管理 </el-card>
            </el-col>
          </div>
        </div>
        <div>
          <div style="height: 30%">
            <el-col :span="5">
              <el-card shadow="hover"> 审查文件 </el-card>
            </el-col>
          </div>
        </div>
      </el-row>
      <el-row :gutter="12">
        <template>
          <div class="header">
            <span>学生管理</span>
          </div>
        </template>
        <div style="height: 30%">
          <el-col :span="5">
            <el-card shadow="hover"> 学生管理 </el-card>
          </el-col>
        </div>
        <div style="height: 30%">
          <el-col :span="5">
            <el-card shadow="hover"> 学生课程 </el-card>
          </el-col>
        </div>
      </el-row>
      <el-row :gutter="12">
        <template>
          <div class="header">
            <span>教师管理</span>
          </div>
        </template>
        <div style="height: 30%">
          <el-col :span="5">
            <el-card shadow="hover"> 教师管理 </el-card>
          </el-col>
        </div>
        <div style="height: 30%">
          <el-col :span="5">
            <el-card shadow="hover"> 成绩录入 </el-card>
          </el-col>
        </div>
        <div>
          <div style="height: 30%">
            <el-col :span="5">
              <el-card shadow="hover"> 教师课程 </el-card>
            </el-col>
          </div>
        </div>
      </el-row>
      <el-row :gutter="12">
        <template>
          <div class="header">
            <span>辅导员</span>
          </div>
        </template>
        <div style="height: 30%">
          <el-col :span="5">
            <el-card shadow="hover"> 辅导员管理 </el-card>
          </el-col>
        </div>
        <div style="height: 30%">
          <el-col :span="5">
            <el-card shadow="hover"> 宿舍管理 </el-card>
          </el-col>
        </div>
        <div>
          <div style="height: 30%">
            <el-col :span="5">
              <el-card shadow="hover"> 我的班级 </el-card>
            </el-col>
          </div>
        </div>
        <div>
          <div style="height: 30%">
            <el-col :span="5">
              <el-card shadow="hover"> 班级学生 </el-card>
            </el-col>
          </div>
        </div>
        <div>
          <div style="height: 30%">
            <el-col :span="5">
              <el-card shadow="hover"> 转班申请 </el-card>
            </el-col>
          </div>
        </div>
        <div>
          <div style="height: 30%">
            <el-col :span="5">
              <el-card shadow="hover"> 班级课程 </el-card>
            </el-col>
          </div>
        </div>
      </el-row>
      <el-row :gutter="12">
        <template>
          <div class="header">
            <span>管理员</span>
          </div>
        </template>
        <div style="height: 30%">
          <el-col :span="5">
            <el-card shadow="hover"> 管理员列表 </el-card>
          </el-col>
        </div>
        <div style="height: 30%">
          <el-col :span="5">
            <el-card shadow="hover"> 权限管理 </el-card>
          </el-col>
        </div>
      </el-row>
    </el-card>
  </div>
</template>
 
<script>
export default {};
</script>
 

<style lang="less" scoped>
.center {
  width: 100%;
}
.box-CardCollege {
  width: 40%;
}
.box-CardTeacher {
  width: 40%;
}
.box-CardStudent {
  width: 40%;
}
.header {
  margin-top: 5px;
  margin-bottom: 5px;
  margin-left: 5px;
}
.el-card__body, .el-main{
  padding-top: 5px;
  padding-bottom: 5px;
}
</style>