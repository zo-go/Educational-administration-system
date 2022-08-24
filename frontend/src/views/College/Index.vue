<template>
    <div>
        <el-tabs v-model="active_route" @tab-click="handleClick">
            <el-tab-pane label="学院管理" name="college"></el-tab-pane>
            <el-tab-pane label="学院专业" name="professional"></el-tab-pane>
            <el-tab-pane label="教材管理" name="teachingMaterial"></el-tab-pane>
            <el-tab-pane label="学校建筑" name="architecture"></el-tab-pane>
            <el-tab-pane label="审查文件" name="auditDocument"></el-tab-pane>
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
