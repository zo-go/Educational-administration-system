<template>
    <div>
        <el-tabs v-model="active_route" @tab-click="handleClick">
            <el-tab-pane label="管理员列表" name="administrator"></el-tab-pane>
            <el-tab-pane label="权限管理" name="permissions"></el-tab-pane>
           
            <el-tab-pane label="问卷调查" name="history"></el-tab-pane>
            <el-tab-pane label="提交的问卷" name="record"></el-tab-pane>
           
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
