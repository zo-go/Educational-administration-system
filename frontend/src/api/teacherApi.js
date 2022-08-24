import request from "@/utils/request";

// 获取教师信息
export async function getTeacher(pageIndex, pageSize, keyword) {
    return await request.get(`/teacher?PageIndex=${pageIndex}&PageSize=${pageSize}&keyword=${keyword}`);
}

// 添加教师信息
export async function addTeacher(data) {
    return await request.post(`/teacher`, data);
}

// 修改教师信息
export async function updateTeacher(id, data) {
    return await request.put(`/teacher/${id}`, data);
}

// 删除教师信息
export function deleteTeacher(id) {
    return request.delete(`/teacher/${id}`)
}