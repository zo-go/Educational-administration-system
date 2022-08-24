<template>
  <el-container class="main-container">
    <el-aside width="auto">
      <!-- <div class="user-box">
        <img :src="this.avatar" alt="" v-if="this.avatar" />
        <img src="../../assets/logo.png" alt="" v-else />
        <span>欢迎 {{ userInfo.Nickname || userInfo.Username }}</span>
      </div> -->
      <el-menu :default-active="$route.path" class="el-menu-vertical-demo" background-color="#FFFFFF" text-color="#23262E"
        active-text-color="#409EFF" unique-opened router :collapse="isCollapse">
        <template v-for="item in menuList">
          <!-- 不包含子菜单的“一级菜单” -->
          <el-menu-item :index="item.path" :key="item.path" v-if="!item.children" @click="clickMenu(item)">
            <i :class="item.icon"></i>
            <span slot="title">{{ item.title }}</span>
          </el-menu-item>
          <!-- 包含子菜单的“一级菜单” -->
          <el-submenu :index="item.path" :key="item.children.path" v-else>
            <template slot="title">
              <i :class="item.icon"></i>
              <span slot="title">{{ item.title }}</span>
            </template>
            <el-menu-item :index="childItem.path" v-for="childItem in item.children" :key="childItem.path"
              @click="clickMenu(childItem)">
              <i :class="childItem.icon"></i>{{ childItem.title }}
            </el-menu-item>
          </el-submenu>
        </template>
      </el-menu>
    </el-aside>
    <!-- 头部区域 -->
    <el-container>
      <el-header>
        <div class="l-content">
          <el-button icon="el-icon-menu" size="mini" @click="handleMenu"></el-button>
          <!-- <el-breadcrumb separator="/">
            <el-breadcrumb-item v-for="item in tags" :key="item.path" :to="{ path: item.path }">{{ item.label }}
            </el-breadcrumb-item>
          </el-breadcrumb> -->
        </div>
        <!-- 右侧的菜单 -->
        <el-menu class="el-menu-top" mode="horizontal" background-color="#FFFFFF" text-color="#23262E"
          active-text-color="#409EFF">
          <el-submenu index="1">
            <template slot="title">
              <!-- 头像 -->
              <img :src="this.avatar" alt="" class="avatar" v-if="this.avatar" />
              <img src="../../assets/logo.png" alt="" class="avatar" v-else />
              <span>个人中心</span>
            </template>
            <el-menu-item index="1" @click="BasicInfon"><i class="el-icon-s-operation"></i>首页</el-menu-item>
          </el-submenu>
          <el-menu-item index="2" @click="logout"><i class="el-icon-switch-button"></i>退出</el-menu-item>
        </el-menu>
      </el-header>
      <common-tag></common-tag>
      <el-main>
        <router-view></router-view>
      </el-main>
      <!-- <el-footer></el-footer> -->
    </el-container>
  </el-container>
</template>
<script>
import { mapState } from "vuex";
import { getUserInfo } from "@/api/userApi";
import baseUrl from "@/config/appConfig";
import store from "@/store";
import CommonTag from "@/components/CommonTag.vue"
export default {
  name: "Main",
  computed: {
    ...mapState(["userInfo", "token"]),
    // ...mapState({
    //   tags: state => state.tag.tagList
    // }),
    isCollapse() {
      return this.$store.state.tag.isCollapse;
    },
  },
  components: {
    CommonTag
  },
  data() {
    return {
      menuList: [
      ],
      // filesUrl: baseUrl.baseUrl + "/files/",
      avatar: "",
    };
  },
  methods: {
    BasicInfon() {
      this.$router.push("/home");
    },
    handleMenu() {
      return this.$store.commit("collapseMenu");
    },
    // 退出登录
    logout() {
      // 询问用户是否退出登录
      // confirm可更改文字 提示样式 confrim('文字' , type:""(提示类型),title:""(提示框标题文字),cancelButtonText:''(取消按钮文字内容)，confirmButtonText:''(按钮文字内容))
      this.$confirm("你确定退出吗?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      }).then(() => {
        // console.log(localStorage.getItem('accessToken'));
        // console.log(this.$store.state.token);
        // localStorage.getItem('accessToken') = "";
        localStorage.removeItem("accessToken");
        localStorage.removeItem("refreshToken");
        localStorage.removeItem("vuex");
        localStorage.removeItem("menu");
        localStorage.removeItem("roleName");
        this.$store.state.token = "";
        this.$router.push("/login");
      });
    },
    async getMunuList() {
      // const { data: res } = await this.$http.get("/my/menus");
      // // console.log(res);
      // if (res.code === 0) {
      //   this.menuList = res.data;
      // }
    },
    setToken() {
      const token = localStorage.getItem("accessToken");
      // console.log(localStorage.getItem("accessToken"));
      if (!token) {
        this.$router.push("/login");
      }
    },
    clickMenu(item) {
      // console.log(item);
      this.$router.push({
        name: item.name,
      });
      this.$store.commit("selectMenu", item);
    },

  },
  // setup() {
  //   // 定义响应式数据count
  //   const isCollapse = computed(() => { this.$store.state.isCollapse; })
  //   return {
  //     isCollapse
  //   }
  // },

  created() {
    // console.log(this.$store.state);
    this.menuList = this.$store.state.userInfo.menu
  },
};
</script>

<style lang="less" scoped>
.el-menu-vertical-demo:not(.el-menu--collapse) {
  width: 200px;
  min-height: 400px;
}

.l-content {
  display: flex;
  align-items: center;

  .el-button {
    margin-right: 20px;
  }
}

.main-container {
  height: 100%;

  .el-header,
  .el-aside {
    background-color: #FFFFFF;
  }

  .el-header {
    padding: 0;
    display: flex;
    justify-content: space-between;
    // justify-content: flex-end;
  }

  .el-main {
    overflow-y: scroll;
    height: 0;
    background-color: #f5f6fB;
  }

  .el-footer {
    background-color: #eee;
    font-size: 12px;
    display: flex;
    justify-content: center;
    align-items: center;
  }
}

.avatar {
  border-radius: 50%;
  width: 35px;
  height: 35px;
  background-color: #fff;
  margin-right: 10px;
  object-fit: cover;
}

.user-box {
  height: 70px;
  display: flex;
  justify-content: center;
  align-items: center;
  border-top: 1px solid #000;
  border-bottom: 1px solid #000;
  user-select: none;

  img {
    width: 35px;
    height: 35px;
    border-radius: 50%;
    background-color: #fff;
    margin-right: 15px;
    object-fit: cover;
  }

  span {
    color: white;
    font-size: 12px;
  }
}

.el-aside {

  .el-submenu,
  .el-menu-item {
    // width: 200px;
    user-select: none;
  }
}

.el-menu {
  height: 100%;
  border: none;
  // background-color: #545c64;
  // text-color: #fff;
  // active-text-color: #ffd04b;

  h3 {
    color: #fff;
    text-align: center;
    line-height: 48px;
  }
}
</style>

