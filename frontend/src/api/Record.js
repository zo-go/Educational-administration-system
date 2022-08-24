import request from '@/utils/request' //axios的实例 请求和响应拦截器

// 问题

//问题列表
export function GetRecordList(data) {
    return request.get(`/questionnaire/record?pageIndex=${data.pageIndex}&&pageSize=${data.pageSize}`)
}

//模糊查询
export function GetvByKeyword(data) {
    return request.get(`/questionnaire/record?pageIndex=${data.pageIndex}&&pageSize=${data.pageSize}&&keyword=${data.keyword}`)
}

//修改问卷问题
export function UpdateRecord(id, data) {
    return request.put(`/questionnaire/record/${id}`, data)
}

//新增问卷调查问题
export function AddRecord(data) {
    return request.post(`/questionnaire/record`, data)
}

//删除问卷问题
export function DeleteRecord(id) {
    return request.delete(`/questionnaire/record/${id}`)
}