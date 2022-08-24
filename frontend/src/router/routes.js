const routes = [
    {
        path: '/login',
        name: 'login',
        component: () => import('@/views/Login/Login.vue')
    },
    {
        path: '/reg',
        name: 'reg',
        component: () => import('@/views/Reg/Reg.vue')
    },
    {
        path: '/',
        name: 'main',
        redirect: '/home',
        component: () => import('@/views/Main/Main.vue'),
        children: [
            {
                path: 'home',
                name: 'home',
                component: () => import('@/views/Menus/Home.vue')
            },
            {
                path: 'excel',
                name: 'excel',
                component: () => import('@/views/Excel/Excel.vue')
            },
            // 学院
            {
                path: 'college',
                name: 'college',
                redirect: 'college',
                component: () => import('@/views/College/Index.vue'),
                meta: { roles: ['T'] },
                children: [
                    {
                        path: 'college',
                        name: 'college',
                        component: () => import('@/views/College/College.vue')
                    },
                    {
                        path: 'teachingMaterial',
                        name: 'teachingMaterial',
                        component: () => import('@/views/College/TeachingMaterial.vue')
                    },
                    {
                        path: "auditDocument",
                        name: "auditDocument",
                        component: () => import('@/views/College/AuditDocument.vue')
                    },
                    {
                        path: "professional",
                        name: "professional",
                        component: () => import('@/views/College/Professional.vue')
                    },
                    {
                        path: "architecture",
                        name: "architecture",
                        component: () => import('@/views/College/Architecture.vue')
                    },
                ]
            },
            // 学生管理
            {
                path: 'student',
                name: 'student',
                redirect: 'student',
                component: () => import('@/views/Student/Index.vue'),
                meta: { roles: ['T'] },
                children: [
                    {
                        path: 'student',
                        name: 'student',
                        component: () => import('@/views/Student/Student.vue')
                    },
                    {
                        path: 'courseschedule',
                        name: 'courseschedule',
                        component: () => import('@/views/Student/Courseschedule.vue')
                    },
                ]
            },
            // 教师管理
            {
                path: 'teacher',
                name: 'teacher',
                redirect: 'teacher',
                component: () => import('@/views/Teacher/Index.vue'),
                meta: { roles: ['T'] },
                children: [
                    {
                        path: 'teacher',
                        name: 'teacher',
                        component: () => import('@/views/Teacher/Teacher.vue')
                    },
                    {
                        path: 'recordscore',
                        name: 'recordScore',
                        component: () => import('@/views/Teacher/RecordScore.vue')
                    },
                    {
                        path: 'teacherCourse',
                        name: 'teacherCourse',
                        component: () => import('@/views/Teacher/teacherCourse.vue')
                    },
                ]
            },
            // 管理员
            {
                path: 'administrator',
                name: 'administrator',
                redirect: 'administrator',
                component: () => import('@/views/Administrator/Index.vue'),
                meta: { roles: ['T'] },
                children: [
                    {
                        path: 'administrator',
                        name: 'administrator',
                        component: () => import('@/views/Administrator/Administrator.vue')
                    },
                    {
                        path: 'permissions',
                        name: 'permissions',
                        component: () => import('@/views/Administrator/Permissions.vue')
                    },
                    {
                        path: 'record',
                        name: 'record',
                        component: () => import('@/views/Administrator/QuestionnaireRecord.vue')
                    },
                    {
                        path: 'history',
                        name: 'history',
                        component: () => import('@/views/Administrator/QuestionnaireHistory.vue')
                    },
                    {
                        path: 'recordinfo/:id/:created',
                        name: 'QuestionnaireFullInfo',
                        component: () => import('@/views/Administrator/QuestResFullInfo.vue')
                    },
                    {
                        path: 'publish/:id',
                        name: 'publish',
                        component: () => import('@/views/Administrator/PublishQuestionnaire.vue')
                    },
                    {
                        path: 'satisfactionsurveys',
                        name: 'satisfactionsurveys',
                        component: () => import('@/views/Administrator/Satisfactionsurveys.vue')
                    },
                ]
            },
            // 辅导员
            {
                path: 'counselor',
                name: 'counselor',
                redirect: 'counselor',
                component: () => import('@/views/Counselor/Index.vue'),
                meta: { roles: ['T'] },
                children: [
                    // 辅导员管理
                    {
                        path: 'counselor',
                        name: 'counselor',
                        component: () => import('@/views/Counselor/Counselor.vue')
                    },
                    // 宿舍管理
                    {
                        path: 'dormitory',
                        name: 'dormitory',
                        component: () => import('@/views/Counselor/Dormitory.vue')
                    },
                    // 我的班级
                    {
                        path: 'class',
                        name: 'class',
                        component: () => import('@/views/Counselor/Class.vue')
                    },
                    // 班级学生
                    {
                        path: 'classStudents',
                        name: 'classStudents',
                        component: () => import('@/views/Counselor/ClassStudents.vue')
                    },
                    // 转班申请
                    {
                        path: 'changeClass',
                        name: 'changeClass',
                        component: () => import('@/views/Counselor/ChangeClass.vue')
                    },
                    // 班级课程
                    {
                        path: 'arrangingCourses',
                        name: 'arrangingCourses',
                        component: () => import('@/views/Counselor/ArrangingCourses.vue')
                    },
                ]
            },
        ],
    },
    {
        path: '*',
        name: 'notFound',
        component: () => import('@/views/NotFound/NotFound.vue')
    },
]

export default routes