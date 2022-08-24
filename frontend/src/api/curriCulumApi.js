import request from "@/utils/request";

// 获取课程表
export async function getCurriCulum() {
    return await request.get(`/curriCulum`);
}
// 获取课程表
export async function getCurriCulumByName(name) {
    return await request.get(`/curriCulum/class/${name}`);
}
// 添加课程表
export async function addCurriCulum(data) {
    return await request.post(`/curriCulum`, data);
}
// 修改课程表
export async function updataCurriCulum(id,data) {
    return await request.put(`/curriCulum/${id}`, data);
}
// 删除课程表
export async function deleteCurriCulum(id) {
    return await request.delete(`/curriCulum/${id}`);
}