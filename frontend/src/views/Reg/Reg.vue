<template>
  <!-- 注册页面的整体盒子 -->
  <div class="reg-container">
    <div class="a">
      <div class="b"></div>
      <div class="c"></div>
      <div class="d"></div>
      <div class="e"></div>
      <div class="f">
        <!-- 注册的盒子 -->
        <div
          class="reg-box"
          style="box-shadow: 5px 3px 5px 4px rgba(27, 26, 26, 0.3)"
        >
          <!-- 标题的盒子 -->
          <div class="title-box">Register</div>
          <!-- 注册的表单区域 -->
          <el-form
            style="padding: 0 25px"
            :model="regForm"
            :rules="regFormRules"
            ref="regFormRef"
            class="register"
          >
            <!-- 用户名 -->
            <el-form-item prop="username">
              <el-input
                v-model="regForm.username"
                placeholder="请输入用户名"
                prefix-icon="el-icon-user"
              ></el-input>
            </el-form-item>
            <!-- 密码 -->
            <el-form-item prop="password" style="padding-top: 18px">
              <el-input
                v-model="regForm.password"
                type="password"
                placeholder="请输入密码"
                prefix-icon="el-icon-lock"
              ></el-input>
            </el-form-item>
            <!-- 再次确认密码 -->
            <el-form-item
              prop="repassword"
              style="padding-top: 18px; padding-bottom: 18px"
            >
              <el-input
                v-model="regForm.repassword"
                type="password"
                placeholder="请确认密码"
                prefix-icon="el-icon-lock"
              ></el-input>
            </el-form-item>
            <div>
              <el-form-item>
                <el-button
                  type="primary"
                  style="
                    width: 100%;
                    box-shadow: 3px 3px 2px 2px rgba(27, 26, 26, 0.3);
                  "
                  @click="regNewUser"
                  class="reg-button"
                  >注册</el-button
                >
              </el-form-item>
            </div>
            <el-link
              type="info"
              @click="$router.push('/login')"
              class="btn-login"
              >返回登录</el-link
            >
          </el-form>
        </div>
      </div>
    </div>
    <div class="square-circle">
      <div class="square">
        <ul>
          <li></li>
          <li></li>
          <li></li>
          <li></li>
          <li></li>
        </ul>
      </div>
      <div class="circle">
        <ul>
          <li></li>
          <li></li>
          <li></li>
          <li></li>
          <li></li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script>
import { regUser, getUsersInfo } from "@/api/userApi";
export default {
  name: "Reg",
  data() {
    const validatePass2 = (rule, value, callback) => {
      if (value === "") {
        callback(new Error("请再次输入密码"));
      } else if (value !== this.regForm.password) {
        callback(new Error("两次输入密码不一致!"));
      } else {
        callback();
      }
    };
    return {
      regForm: {
        username: "",
        password: "",
        repassword: "",
      },
      regFormRules: {
        username: [
          { required: true, message: "请输入用户名", trigger: "blur" },
          {
            pattern: /^[a-zA-Z][a-zA-Z0-9]{0,9}$/,
            message: "用户名必须是1-10的字母数组，且数字不能开头",
            trigger: "blur",
          },
        ],
        password: [
          { required: true, message: "请输入密码", trigger: "blur" },
          {
            pattern: /^\S{6,15}$/,
            message: "密码必须是6-15的非空字符串",
            trigger: "blur",
          },
        ],
        repassword: [
          {
            pattern: /^\S{6,15}$/,
            message: "密码必须是6-15的非空字符串",
            trigger: "blur",
          },
          { validator: validatePass2, trigger: "blur" },
        ],
      },
    };
  },
  methods: {
    regNewUser() {
      // 绑定事件函数
      // 进行表单校验
      let isExitUser = -1;
      getUsersInfo().then((res) => {
        res.data.data.forEach((item) => {
          if (item.username == this.regForm.username) {
            isExitUser++;
          }
        });
        if (isExitUser === -1) {
          this.$refs.regFormRef.validate(async (valid) => {
            // valid 只能所有的表单验证通过返回true，反之，返回false
            if (!valid) return this.$message.warning("请正确输入账号，密码!");
            const userData = {
              username: this.regForm.username,
              password: this.regForm.password,
            };
            // console.log(userData);

            regUser(userData).then((res) => {
              if (res.code == 10001) {
                this.$message({
                  showClose: true,
                  message: "注册成功!",
                  type: "success",
                });
                this.$router.push("/login");
              } else {
                this.$message.warning("注册失败!");
              }
            });
            // if (res.code !== 10001) return this.$message.error(res.message);
            // this.$message.success(res.message);
          });
        } else {
          this.$message({
            showClose: true,
            message: "用户存在,请重新输入用户名!",
            type: "error",
          });
        }
      });

      // this.$refs.regFormRef.validate(async (valid) => {
      //   // valid 只能所有的表单验证通过返回true，反之，返回false
      //   if (!valid) return;
      //   let userData = {
      //     username: this.regForm.username,
      //     password: this.regForm.password,
      //   };
      //   // console.log(userData);

      //   regUser(userData).then((res) => {
      //     if (res.code == 10001) {
      //       this.$router.push("/login");
      //     }
      //   });
      //   // if (res.code !== 10001) return this.$message.error(res.message);
      //   // this.$message.success(res.message);
      // });
    },
  },
};
</script>

<style lang="less" scoped>
.reg-container {
  background: url("../../assets/images/login_bg2.0.jpg") center;
  background-size: cover;
  height: 100%;
  /* 100%窗口高度 */
  height: 100vh;
  /* 弹性布局 居中 */
  display: flex;
  justify-content: center;
  align-items: center;
  /* 溢出隐藏 */
  overflow: hidden;

  .reg-box {
    border-left: 1px solid rgba(255, 255, 255, 0.5);
    border-bottom: 1px solid rgba(255, 255, 255, 0.3);
    border-right: 1px solid rgba(255, 255, 255, 0.3);
    backdrop-filter: blur(10px);
    background: rgba(50, 50, 50, 0.2);
    border-radius: 10px;
    width: 400px;
    height: 500px;

    .register {
      margin-left: 8%;
      margin-top: 8%;
      width: 288px;
    }

    .title-box {
      padding-top: 3%;
      text-transform: uppercase;
      color: #f1ebe5;
      text-shadow: 0 8px 9px #c4b59d, 0px -2px 1px #fff;
      font-weight: bold;
      letter-spacing: -4px;
      width: 100%;
      border: none;
      border-bottom: 1px solid rgb(223, 203, 203);
      outline: none;
      background: transparent;
      font-size: 57px;
      text-align: center;
      font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Oxygen,
        Ubuntu, Cantarell, "Open Sans", "Helvetica Neue", sans-serif;
    }

    .reg-button {
      position: absolute;
      padding-left: 1000px;
      letter-spacing: 3px;
      font-size: 15px;
      box-sizing: border-box;
      width: 88px;
      height: 35px;
      border-radius: 5px;
      border: 1px solid rgba(255, 255, 255, 0.5);
      background: rgba(255, 255, 255, 0.2);
      outline: none;
      padding: 0 12px;
      color: rgba(255, 255, 255, 0.9);
      transition: 0.2s;
    }

    .btn-login {
      // 下划线
      margin-top: 8%;
      text-decoration: underline;
      color: rgb(173, 219, 234);
      font-size: 16px;
      margin-left: 183px;
      position: absolute;
    }
  }
}

ul li {
  position: absolute;
  border: 1px solid #fff;
  background-color: #fff;
  width: 30px;
  height: 30px;
  list-style: none;
  opacity: 0;
}
.square li {
  top: 40vh;
  left: 60vw;
  /* 执行动画：动画名 时长 线性的 无限次播放 */
  animation: square 10s linear infinite;
}
.square li:nth-child(2) {
  top: 80vh;
  left: 10vw;
  /* 设置动画延迟时间 */
  animation-delay: 2s;
}
.square li:nth-child(3) {
  top: 80vh;
  left: 85vw;
  /* 设置动画延迟时间 */
  animation-delay: 4s;
}
.square li:nth-child(4) {
  top: 10vh;
  left: 70vw;
  /* 设置动画延迟时间 */
  animation-delay: 6s;
}
.square li:nth-child(5) {
  top: 10vh;
  left: 10vw;
  /* 设置动画延迟时间 */
  animation-delay: 8s;
}
.circle li {
  bottom: 0;
  left: 15vw;
  /* 执行动画 */
  animation: circle 10s linear infinite;
}
.circle li:nth-child(2) {
  left: 35vw;
  /* 设置动画延迟时间 */
  animation-delay: 2s;
}
.circle li:nth-child(3) {
  left: 55vw;
  /* 设置动画延迟时间 */
  animation-delay: 6s;
}
.circle li:nth-child(4) {
  left: 75vw;
  /* 设置动画延迟时间 */
  animation-delay: 4s;
}
.circle li:nth-child(5) {
  left: 90vw;
  /* 设置动画延迟时间 */
  animation-delay: 8s;
}

/* 定义动画 */
@keyframes square {
  0% {
    transform: scale(0) rotateY(0deg);
    opacity: 1;
  }
  100% {
    transform: scale(5) rotateY(1000deg);
    opacity: 0;
  }
}
@keyframes circle {
  0% {
    transform: scale(0) rotateY(0deg);
    opacity: 1;
    bottom: 0;
    border-radius: 0;
  }
  100% {
    transform: scale(5) rotateY(1000deg);
    opacity: 0;
    bottom: 90vh;
    border-radius: 50%;
  }
}
.a {
  box-shadow: 2px 6px 8px 8px rgba(27, 26, 26, 0.3);
  border-radius: 15px;
  position: relative;
  top: 0px;
  width: 1000px;
  height: 600px;
  background-image: url("login_bg2.0.jpg");
  background-size: cover;
  display: flex;
  justify-content: center;
  align-items: center;
  overflow: hidden;
}
.b,
.c,
.d,
.e {
  position: absolute;
  width: 1000px;
  height: 600px;
  /* 设置阴影 */
  filter: drop-shadow(4px 4px 12px rgba(0, 0, 0, 0.5));
  background-size: cover;
  opacity: 0.7;
  transition: 0.5s;
}
.b::after,
.c::after,
.d::after,
.e::after {
  content: "";
  position: absolute;
  width: 1000px;
  height: 600px;
  background-image: url("login_bg2.0.jpg");
}
.b {
  left: -400px;
  transform: rotateZ(100deg);
  overflow: hidden;
}
.b::after {
  transform: rotateZ(-100deg);
}

.c {
  left: -400px;
  transform: rotateZ(-100deg);
  overflow: hidden;
}
.c::after {
  transform: rotateZ(100deg);
}

.d {
  right: -400px;
  transform: rotateZ(105deg);
  overflow: hidden;
}
.d::after {
  transform: rotateZ(-105deg);
}

.e {
  right: -400px;
  transform: rotateZ(-100deg);
  overflow: hidden;
}
.e::after {
  transform: rotateZ(100deg);
}
.f {
  z-index: 99;
  opacity: 0;
  font: 900 50px "";
  /* 设置字体间距 */
  letter-spacing: 10px;
  color: rgb(60, 60, 70);
  transition: 0.5s;
}
/* 设置动画 */
.a:hover .b {
  left: -550px;
}
.a:hover .c {
  left: -600px;
}
.a:hover .d {
  right: -550px;
}
.a:hover .e {
  right: -610px;
}
.a:hover .f {
  opacity: 1;
}
</style>
