import request from "@/utils/request";

// 获取角色
export async function getRole() {
    return await request.get(`/role`);
}