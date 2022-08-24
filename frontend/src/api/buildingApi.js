import request from "@/utils/request";

// 获取建筑
export async function getBuilding(pageIndex,pageSize,keyword) {
    return await request.get(`/building?PageIndex=${pageIndex}&PageSize=${pageSize}&keyword=${keyword}`);
}

// 添加建筑
export async function addBuilding(data) {
    return await request.post("/building", data);
}

// 删除建筑
export async function deleteBuilding(id) {
    return await request.delete(`/building/${id}`);
}

// 修改建筑
export async function updataBuilding(id,data) {
    return await request.put(`/building/${id}`, data);
}