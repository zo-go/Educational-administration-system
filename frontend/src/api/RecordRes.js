import request from '@/utils/request' //axios的实例 请求和响应拦截器


//获取问卷调查返回值列表
export function GetRecordResList(data) {
    return request.get(`/questionnaireres/?pageIndex=${data.pageIndex}&&pageSize=${data.pageSize}`)
}

export function GetRecordFullInfo(data) {
    return request.get(`/questionnaireres/key/?QuestionnaireId=${data.QuestionnaireId}&&CreatedBy=${data.CreatedBy}`)
}