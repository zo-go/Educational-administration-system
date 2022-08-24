<template>
  <div>
    <input
      style="display: none"
      type="file"
      ref="fileInput"
      accept=".xlsx,.xls"
      @change="selectFile"
    />
    <el-button type="info" round icon="el-icon-upload" @click="handleUpload"
      >点击选择文件</el-button
    >
    <span class="file-name" v-show="fileName">
      <i class="el-icon-document"></i>
      {{ fileName }}
    </span>

    <el-table :data="tableData" border highlight-current-row>
      <el-table-column
        v-for="item of tableHeader"
        :key="item"
        :prop="item"
        :label="item"
      />
    </el-table>
  </div>
</template>

<script>
import { readExcelData } from "@/utils/excle";

export default {
  data() {
    return {
      tableData: [],
      tableHeader: [],
      fileName: "",
    };
  },
  methods: {
    handleUpload() {
      // this.$refs.fileInput.click();
      this.$nextTick(() => {
        this.$refs["form"].clearValidate(["firstParty"]);
      });
    },
    async selectFile(event) {
      const files = event.target.files;
      const firstFile = files[0];
      if (!this.isExcel(firstFile)) {
        this.$message.error("上传的文件只能是xls或xlsx格式!");
        return false;
      }
      this.fileName = firstFile.name;
      const valid = this.beforeUpload(firstFile);
      if (valid) {
        readExcelData(firstFile, (result) => {
          const { header, data } = result;
          console.log(result);
          this.tableHeader = header;
          this.tableData = data;
          this.$message.success("导入成功！");
        });
      }
      this.$refs.fileInput.value = null; //将文件清掉，不然再次选择这个文件，change事件不会被触发。
    },
    isExcel(file) {
      return /\.(xls|xlsx)$/.test(file.name);
    },
    beforeUpload(file) {
      const isLt20M = file.size / 1024 / 1024 < 20;
      if (!isLt20M) {
        this.$message.error("上传的文件大小不能超过 20MB!");
      }
      return isLt20M;
    },
  },
};
</script>

<style scoped>
.el-table {
  margin-top: 20px;
}
</style>
