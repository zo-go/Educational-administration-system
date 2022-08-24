const ACCESSTOKEN = 'accessToken';
const REFRESHTOKEN = 'refreshToken';

// 获得token
export function getToken() {
    const token = localStorage.getItem(ACCESSTOKEN);
    return token;
}

// 获得refreshToken
export function getRefreshToken() {
    const refreshToken = localStorage.getItem(REFRESHTOKEN);
    return refreshToken;
}

// 保存token和refreshToken
export function setToken(accessToken, refreshToken) {
    if (accessToken && refreshToken) {
        localStorage.setItem(ACCESSTOKEN, accessToken)
        localStorage.setItem(REFRESHTOKEN, refreshToken)
    }
}

// 根据token是否存在，判断当前是否登录
export function isLogin() {
    const token = getToken();
    if (token) {
        return true;
    } else {
        return false;
    }
}
