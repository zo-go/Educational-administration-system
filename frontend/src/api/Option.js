import request from '@/utils/request' //axios的实例 请求和响应拦截器



//修改问题选项
export function UpdateOption(id, data) {
    return request.put(`/questionnaire/option/${id}`, data)
}

//新增选项
export function AddOption(data) {
    return request.post(`/questionnaire/option`, data)
}

//删除问题选项
export function DeleteOption(id) {
    return request.delete(`/questionnaire/option/${id}`)
}
