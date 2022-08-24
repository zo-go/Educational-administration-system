import request from "@/utils/request";

// 获取专业
export async function getSpecialized(pageIndex, pageSize, keyword) {
    return await request.get(`/specialized?PageIndex=${pageIndex}&PageSize=${pageSize}&keyword=${keyword}`);
}
// 添加专业
export async function addSpecialized(data) {
    return await request.post(`/specialized`, data);
}
// 修改专业
export async function updataSpecialized(id, data) {
    return await request.put(`/specialized/${id}`, data);
}
// 删除专业
export async function deleteSpecialized(id) {
    return await request.delete(`/specialized/${id}`);
}