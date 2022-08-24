import request from "@/utils/request";

// 获取课程
export async function getCourse() {
    return await request.get(`/course`);
}
// 添加课程
export async function addCourse(data) {
    return await request.post(`/course`, data);
}
// 修改课程
export async function updataCourse(id,data) {
    return await request.put(`/course/${id}`, data);
}
// 删除课程
export async function deleteCourse(id) {
    return await request.delete(`/course/${id}`);
}

// 获取课程
export async function getCourseById(id) {
    return await request.get(`/course/teacher/${id}`);
}