<template>
    <div>
        <el-tabs v-model="active_route" @tab-click="handleClick">
            <el-tab-pane label="教师管理" name="teacher"></el-tab-pane>
            <el-tab-pane label="成绩录入" name="recordScore"></el-tab-pane>
            <el-tab-pane label="教师课程" name="teacherCourse"></el-tab-pane>
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
