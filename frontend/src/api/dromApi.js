import request from "@/utils/request";

// 获取教材
export async function getDrom(pageIndex, pageSize, keyword) {
    return await request.get(`/drom?PageIndex=${pageIndex}&PageSize=${pageSize}&keyword=${keyword}`);
}
// 添加教材
export async function addDrom(data) {
    return await request.post(`/drom`, data);
}
// 修改教材
export async function updataDrom(id, data) {
    return await request.put(`/drom/${id}`, data);
}
// 删除教材
export async function deleteDrom(id) {
    return await request.delete(`/drom/${id}`);
}