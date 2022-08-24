<template>
    <el-container>
        <el-main>
            <el-row>
                <el-col :span="24" class="grade-desc">
                    <strong>学校建筑</strong>
                    <el-row class="margin">
                        <el-col :span="24">
                            关键字：<el-input v-model="q.keyword" placeholder="请输入楼栋号" class="input"></el-input>
                            <el-button type="primary" size="small" class="button" @click="query">搜索</el-button>
                            <el-button type="info" size="small" @click="resetForm">重置</el-button>
                            <el-button type="primary" :loading="exportLoading" @click="handleExport">
                                导出excel
                            </el-button>
                            <el-button type="primary" @click="addDialog">添加</el-button>

                            <!-- 添加 修改-->
                            <el-dialog :title="title" :visible.sync="dialogVisible" width="30%"
                                :before-close="handleClose">
                                <!-- 内容主体区 -->
                                <el-form :model="addForm" :rules="addFormRules" ref="addForm" label-width="70px">
                                    <el-form-item label="建筑名称" prop="buildingName">
                                        <el-input v-model="addForm.buildingName"></el-input>
                                    </el-form-item>
                                    <el-form-item label="建筑编号" prop="buildingNum">
                                        <el-input v-model="addForm.buildingNum"></el-input>
                                    </el-form-item>
                                    <el-form-item label="楼层" prop="floor">
                                        <el-input v-model="addForm.floor"></el-input>
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
                        <el-table-column prop="buildingName" label="建筑名称"> </el-table-column>
                        <el-table-column prop="buildingNum" label="建筑编号"> </el-table-column>
                        <el-table-column prop="floor" label="楼层"> </el-table-column>
                        <el-table-column label="操作">
                            <template v-slot="{ row }">
                                <el-button type="primary" size="mini" icon="el-icon-edit" @click="updateDialog(row)">
                                </el-button>
                                <el-button type="danger" size="mini" icon="el-icon-delete" @click="deleteAA(row.id)">
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
import { getBuilding, addBuilding, deleteBuilding, updataBuilding, } from '@/api/buildingApi'
import { getAcademy } from '@/api/academyApi'

export default {
    inject: ["reload"],
    data() {
        return {
            exportLoading: false,
            dialogVisible: false,
            // 用户信息个人
            personalInfo: {},
            // 添加、修改用户
            addForm: {
                buildingName: "",
                buildingNum: "",
                floor: "",
            },
            // 添加、修改标题
            title: "",
            // 学院
            academy: {},
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
                buildingName: [
                    { required: true, message: "请输入建筑名称", trigger: "blur" },
                ],
                buildingNum: [
                    { required: true, message: "请输入建筑编号", trigger: "blur" },
                ],
                floor: [
                    { required: true, message: "请输出楼层", trigger: "blur" },
                ]
            },
            // 总数据条数
            tatal: 50,
            // 初始页为1
            currentPage1: 1,
            // 学院数据表
            tableData: [],
        };
    },
    created() {
        this.fetchData(1, 5, "")
    },
    methods: {
        // 添加按钮
        addDialog() {
            this.addForm = {}
            this.title = '添加'
            this.dialogVisible = true
        },
        handleClose() { },
        // 添加、修改教师
        save(addForm) {
            this.$confirm('确定保存', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'info'
            }).then(() => {
                this.$refs[addForm].validate((valid) => {
                    if (valid) {
                        if (this.title == '添加') {
                            // console.log(this.addForm);
                            addBuilding(this.addForm).then(res => {
                                if (res.data.code == 200) {
                                    this.$message.success(res.data.msg)
                                    this.dialogVisible = false
                                    this.reload()
                                }
                            })
                        } else {
                            console.log(this.personalInfo);
                            var id = this.personalInfo.id
                            console.log(this.addForm);

                            updataBuilding(id, this.addForm).then(res => {
                                if (res.data.code == 200) {
                                    this.$message.success(res.data.msg)
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
                "建筑名称",
                "建筑编号",
                "楼层",
            ];
            var list = []
            // console.log(this.tableData);
            for (let i = 0; i < this.tableData.length; i++) {
                var data = {}
                data.buildingName = this.tableData[i].buildingName
                data.buildingNum = this.tableData[i].buildingNum
                data.floor = this.tableData[i].floor
                list.push(data)
            }
            // console.log(list);
            exportExcel(header, list, "建筑数据表");
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
            this.addForm.buildingName = data.buildingName
            this.addForm.buildingNum = data.buildingNum
            this.addForm.floor = data.floor
        },
        // 删除
        deleteAA(id) {
            this.$confirm('此操作将永久删除该文件, 是否继续?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(() => {
                deleteBuilding(id).then(res => {
                    if (res.data.code == 200) {
                        this.$message.success(res.data.msg)
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
        // 抓取专业后台数据
        fetchData(PageIndex, PageSize, keyword) {
            getBuilding(PageIndex, PageSize, keyword).then((res) => {
                // console.log(res);
                if (res.data.code = 200) {
                    this.$message.success("获取建筑数据成功")
                    this.tableData = res.data.data
                    console.log(this.tableData);
                    this.tatal = res.data.page.count
                    this.q.pageIndex = res.data.page.pageIndex
                    this.q.pageSize = res.data.page.pageSize
                } else {
                    this.$message.warring('获取建筑数据失败')
                }
            });
        },
        // 搜索
        query() {
            this.fetchData(this.q.pageIndex, this.q.pageSize, this.q.keyword);
        },
        // 重置表单
        resetForm(formName) {
            this.$refs[formName].resetFields();
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