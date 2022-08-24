<template>
    <div>
        <el-tabs v-model="active_route" @tab-click="handleClick">
            <el-tab-pane label="学生管理" name="student"></el-tab-pane>
            <el-tab-pane label="学生课程" name="courseschedule"></el-tab-pane>
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
