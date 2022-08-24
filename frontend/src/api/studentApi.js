import request from "@/utils/request";

// 获取学生
export async function getStudents(PageIndex, PageSize, keyword) {
    return await request.get(`/student?PageIndex=${PageIndex}&PageSize=${PageSize}&keyword=${keyword}`);
}

// 添加学生
export async function addStudents(data) {
    return await request.post("/student", data);
}
// 修改学生
export async function updateStudents(id, data) {
    return await request.put(`/student/${id}`, data);
}
// 删除学生
export async function deleteStudents(id) {
    return await request.delete(`/student/${id}`);
}
