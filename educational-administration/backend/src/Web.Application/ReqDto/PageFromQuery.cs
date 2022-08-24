using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Application.ReqDto
{
    //分页接受数据类
    public class PageFromQuery
    {
        private int _pageIndex { get; set; }
        private int _pageSize { get; set; }

        /// <summary>
        /// 每页展示条数
        /// </summary>
        /// <value></value>
        public int PageSize
        {
            get
            {
                if (_pageSize < 10)
                {
                    _pageSize = 10;
                }
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }
        /// <summary>
        /// 当前页码
        /// </summary>
        /// <value></value>
        public int PageIndex
        {
            get
            {
                if (_pageIndex < 1)
                {
                    _pageIndex = 1;
                }
                return _pageIndex;
            }
            set
            {
                _pageIndex = value;
            }
        }

        //搜索的关键字
        public string? keyword { get; set; }
    }
}