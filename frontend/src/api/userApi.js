import request from "@/utils/request";
import { getToken, getRefreshToken } from '@/utils/auth'

// 登录并获得token
export function login(data) {
    return request.post("/login",data);
}

// 不登录的情况下刷新token
export function refreshToken() {
    const accessToken = getToken();
    const refreshToken = getRefreshToken();
    const data = {
        accessToken,
        refreshToken
    }
    if (accessToken && refreshToken) {
        return request.post("/user/refreshtoken", data);
    } else {
        return false;
    }
}

// 获取用户
export function getUsersInfo(pageIndex,pageSize,keyword) {
    return request.get(`/user?PageIndex=${pageIndex}&PageSize=${pageSize}&keyword=${keyword}`)
}

// 注册用户
export function regUser(data) {
    return request.post("/user", data)
}

// 根据用户名获取指定用户
export function getUserInfo(uesrname) {
    return request.get(`/user/${uesrname}`)
}

// 修改用户信息
export function updateUserInfo(id, data) {
    return request.put(`/user/${id}`, data)
}

export function deleteUser(id) {
    return request.delete(`/user/${id}`)
}
