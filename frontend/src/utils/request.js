import axios from 'axios'
import { getToken } from '@/utils/auth'
import { refreshToken } from '@/api/userApi'
import appConfig from '@/config/appConfig'
import store from '@/store/index'

const instance = axios.create({
    baseURL: appConfig.baseUrl,
    timeout: 50000
});

// 添加请求拦截器
instance.interceptors.request.use(
    function (config) {
        // 在发送请求之前做些什么
        // store.state.token = getToken()
        const token = getToken();
        if (token) {
            config.headers.Authorization = 'Bearer ' + token
        }

        return config;
    }, function (error) {
        // 对请求错误做些什么
        return Promise.reject(error);
    });

// // 添加响应拦截器
// instance.interceptors.response.use(function (response) {
//     // 对响应数据做点什么
//     return response.data;
// }, function (error) {
//     // 对响应错误做点什么
//     return Promise.reject(error);
// });

// 添加响应拦截器,response 是返回的数据
instance.interceptors.response.use(function (response) {
    // 对响应数据做点什么
    // code: 200-成功，400-失败，401-重新登录，403-未认证，404-接口不存在，500-服务器内部错误
    return response;
}, function (error) {
    console.log("发生错误，已被响应拦截器拦截");
    // 对响应错误做点什么
    if (error.response.status == 401) {
        console.log("错误代码：" + error.response.status);
        // refreshToken().then(res => {
        //     console.log(res);
        //     // setToken(res.data.data.accessToken, res.data.data.refreshToken);
        // })
        if (sessionStorage.getItem("refreshToken")) {
            return instance.request(error.config);
        } else {
            alert("登录过期，请重新登录")
            window.location.href = front + "/login"
        }
    }
    return Promise.reject(error);
});


export default instance
