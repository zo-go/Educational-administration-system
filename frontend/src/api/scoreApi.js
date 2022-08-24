import request from "@/utils/request";

// 获取成绩
export async function getScore() {
    return await request.get(`/score`);
}
// 添加成绩
export async function addScore(data) {
    return await request.post(`/score`, data);
}
// 修改成绩
export async function updataScore(id, data) {
    return await request.put(`/score/${id}`, data);
}
// 删除成绩
export async function deleteScore(id) {
    return await request.delete(`/score/${id}`);
}