import request from '@/utils/request' //axios的实例 请求和响应拦截器


//查询 问卷调查主题
export function GetQuestionnaireList(data) {
    return request.get(`/questionnaire?pageIndex=${data.pageIndex}&&pageSize=${data.pageSize}`)
}


//模糊查询 问卷调查主题
export function GetQuestionnaireByKeyword(data) {
    return request.get(`/questionnaire?pageIndex=${data.pageIndex}&&pageSize=${data.pageSize}&&keyword=${data.keyword}`)
}


//新增 问卷调查表主题
export function AddQuestionnaire(data) {
    return request.post(`/questionnaire`, data)
}

//删除问卷调查表
export function DeleteQuestionnaire(id) {
    return request.delete(`/questionnaire/${id}`)
}

//问卷调查详情
export function GetFullInfo(id) {
    return request.get(`/questionnaire/${id}`)
}

