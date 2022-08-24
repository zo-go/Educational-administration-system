import request from "@/utils/request";

// 获取班级
export async function getClass() {
    return await request.get("/class");
}
