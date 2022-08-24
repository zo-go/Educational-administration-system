import request from "@/utils/request";

// 获取学院
export async function getAcademy() {
    return await request.get(`/academy`);
}
// 添加学院
export async function addAcademy(data) {
    return await request.post(`/academy`, data);
}
// 修改学院
export async function updataAcademy(id,data) {
    return await request.put(`/academy/${id}`, data);
}
// 删除学院
export async function deleteAcademy(id) {
    return await request.delete(`/academy/${id}`);
}