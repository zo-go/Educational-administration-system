<template>
    <div>
        <el-tabs v-model="active_route" @tab-click="handleClick">
            <el-tab-pane label="辅导员管理" name="counselor"></el-tab-pane>
            <el-tab-pane label="宿舍管理" name="dormitory"></el-tab-pane>
            <el-tab-pane label="我的班级" name="class"></el-tab-pane>
            <el-tab-pane label="班级学生" name="classStudents"></el-tab-pane>
            <el-tab-pane label="转班申请" name="changeClass"></el-tab-pane>
            <el-tab-pane label="班级课程" name="arrangingCourses"></el-tab-pane>
        </el-tabs>
        <router-view></router-view>
    </div>
</template>

<script>
export default {
    computed: {
        active_route: {
            get() {
                return this.$route.name;
            },
            set() { },
        },
    },
    methods: {
        handleClick(tab) {
            this.$router.push({ name: `${tab.name}` });
        },
    },
};
</script>
