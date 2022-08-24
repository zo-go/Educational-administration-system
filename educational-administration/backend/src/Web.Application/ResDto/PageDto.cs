using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Application.ResDto
{
    public class PageDto
    {
        /// <summary>
        /// 当前页数
        /// </summary>
        /// <value></value>
        public int pageIndex { get; set; }

        /// <summary>
        /// 页面大小
        /// </summary>
        /// <value></value>
        public int pageSize { get; set; }

        /// <summary>
        /// 当前页数信息总数
        /// </summary>
        /// <value></value>
        public int OnThisPage { get; set; }

        /// <summary>
        /// 所有信息总数
        /// </summary>
        /// <value></value>
        public int Count { get; set; }
    }
}