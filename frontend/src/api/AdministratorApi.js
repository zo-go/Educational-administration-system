import request from "@/utils/request";

// 获取用户
export async function getAdmin(pageIndex,pageSize,keyword) {
    return await request.get(`/user/admin?PageIndex=${pageIndex}&PageSize=${pageSize}&keyword=${keyword}`);
}

// 添加用户
export async function addUser(data) {
    return await request.post("/user", data);
}

// 删除用户
export async function deleteUser(id) {
    return await request.delete(`/user/${id}`);
}

export async function getRole() {
    return await request.get("/role");
}
