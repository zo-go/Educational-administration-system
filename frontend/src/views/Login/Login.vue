<template>
  <div class="login" clearfix>
    <div class="login-wrap">
      <el-row type="flex" justify="center">
        <el-form ref="user" :model="user" :rules="loginRules" status-icon label-width="80px">
          <h3>登录</h3>
          <hr />
          <el-form-item prop="username" label="用户名">
            <el-input v-model="user.username" placeholder="请输入用户名" prefix-icon></el-input>
          </el-form-item>
          <el-form-item prop="password" label="密码">
            <el-input v-model="user.password" show-password placeholder="请输入密码"></el-input>
          </el-form-item>
          <el-form-item prop="verification" label="验证码">
            <el-input v-model="user.verification" placeholder="请输入验证码"></el-input>
            <div class="login-code" width="100%" @click="refreshCode">
              <!--验证码组件-->
              <SlideVerify :identifyCode="identifyCode"></SlideVerify>
            </div>
          </el-form-item>

          <el-form-item>
            <router-link class="pwdInput" to="/">找回密码</router-link>
            <router-link to="/register">注册账号</router-link>
          </el-form-item>

          <el-form-item>
            <el-button type="primary" icon="el-icon-upload" @click="submitForm('user')">登 录</el-button>
            <el-button type="primary" @click="resetForm('user')">重 置</el-button>
          </el-form-item>
        </el-form>
      </el-row>
    </div>
  </div>
</template>
 
<script>
import axios from "axios";
import SlideVerify from "@/components/login/SlideVerify.vue";
import { getMenu } from "@/api/menu";
import { login } from "@/api/userApi"
import store from '@/store/index'

export default {
  name: "login",
  components: {
    SlideVerify,
  },

  data() {
    return {
      identifyCodes: "1234567890abcdefjhijklinopqrsduvwxyz", //随机串内容
      identifyCode: "1234", //随机串内容
      // isShowSlide: true,
      user: {
        username: "",
        password: "",
        verification: ""
      },
      loginRules: {
        username: [
          { required: true, message: "请输入用户名", trigger: "blur" },
          {
            pattern: /^\S{1,15}$/,
            message: "用户名必须是1-10的字母数字",
            trigger: "blur",
          },
        ],
        password: [
          { required: true, message: "请输入密码", trigger: "blur" },
          {
            pattern: /^\S{1,15}$/,
            message: "密码必须是1-15的非空字符",
            trigger: "blur",
          },
        ],
        verification: [
          { required: true, message: "请输入验证码", trigger: "blur" },
          {
            trigger: "blur",
          },
        ],
      },
    };
  },
  //   hideSlide() {
  //   setTimeout(() => {
  //     this.isShowSlide = false;
  //   }, 500);
  // },
  created() { },
  methods: {
    // 重置验证码
    refreshCode() {
      this.identifyCode = "";
      this.makeCode(this.identifyCodes, 4);
    },
    makeCode(o, l) {
      for (let i = 0; i < l; i++) {
        this.identifyCode +=
          this.identifyCodes[this.randomNum(0, this.identifyCodes.length)];
      }
    },
    randomNum(min, max) {
      return Math.floor(Math.random() * (max - min) + min);
    },
    // doLogin() {
    //   if (!this.user.username && this.user.username != this.username) {
    //     this.$message.error("用户名错误！");
    //     return;
    //   } else if (!this.user.password && this.user.password != this.password) {
    //     this.$message.error("密码错误！");
    //     return;
    //   } else if (!this.user.verification && this.user.verification != this.identifyCode) {
    //     this.$message.error("验证码错误！");
    //     return;
    //   } else {
    //     // 校验用户名和密码是否正确;

    //     this.$store.commit("clearMenu");
    //     this.$store.commit("setMenu", menu);
    //     this.$router.push("/home");
    //   }
    // },
    // 提交用户登录信息,注册路由
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          if (this.user.verification == this.identifyCode) {
            console.log(this.user.verification);
            console.log(this.verification);
            login(this.user).then((res) => {
              console.log(res);
              if (res.data.code == 200) {
                this.$message.success('登录成功')
                store.commit("setRoleName", res.data.data[0].roleInfo.roleName)
                store.commit("setToken", res.data.token)

                var menu = getMenu(res.data.data[0].roleInfo.roleName).data.menu
                this.$store.commit("clearMenu");
                this.$store.commit("setMenu", menu);
                this.$store.commit("updataUserInfo", res.data.data[0].userInfo);
                this.$router.push("/home");
              } else {
                this.$message.error('账号或密码不正确，登录失败')
              }
            });
          } else {
            this.$message({
              showClose: true,
              message: "用户名、密码、验证码错误，请确认重试",
              type: "error",
            });
            return false;
          }
        } else {
          this.$message.error('验证码错误')
        }

      });
    },
    resetForm(formName) {
      this.$refs[formName].resetFields();
    },
  },
  mounted() {
    // 初始化验证码
    this.identifyCode = "";
    this.makeCode(this.identifyCodes, 4);
  },
};
</script>
 
<style scoped>
.login {
  width: 100%;
  height: 100%;
  background: url("../../assets/images/logining.jpg") center;
  background-size: cover;
  overflow: hidden;
}

.pwdInput {
  margin-right: 10px;
}

.login-wrap {
  background: url("../../assets/images/logining.jpg") center;
  background-size: cover;
  width: 40%;
  height: 60%;
  margin: 120px auto;
  overflow: hidden;
  padding-top: 10px;
  line-height: 20px;
}

#password {
  margin-bottom: 5px;
}

h3 {
  color: #0babeab8;
  font-size: 24px;
}

hr {
  background-color: #444;
  margin: 20px auto;
}

a {
  text-decoration: none;
  color: #aaa;
  font-size: 15px;
}

a:hover {
  color: coral;
}

/* .el-button {
  width: 50%;
  margin-left: -50px;
} */

.login-code {
  margin-top: 5px;
}
</style>