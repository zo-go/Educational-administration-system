import request from "@/utils/request";

// 获取教材
export async function getTextbook(pageIndex, pageSize, keyword) {
    return await request.get(`/textbook?PageIndex=${pageIndex}&PageSize=${pageSize}&keyword=${keyword}`);
}
// 添加教材
export async function addTextbook(data) {
    return await request.post(`/textbook`, data);
}
// 修改教材
export async function updataTextbook(id, data) {
    return await request.put(`/textbook/${id}`, data);
}
// 删除教材
export async function deleteTextbook(id) {
    return await request.delete(`/textbook/${id}`);
}