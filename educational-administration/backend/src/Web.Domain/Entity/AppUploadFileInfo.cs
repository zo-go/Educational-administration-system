using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Entity
{
    public class AppUploadFileInfo : Base.BaseEntity
    {
        /// <summary>
        /// 原始文件名
        /// </summary>
        /// <value></value>
        public string OriginFileName { get; set; } = null!;
        /// <summary>
        /// 更改后的文件名
        /// </summary>
        /// <value></value>
        public string CurrentFileName { get; set; } = null!;
        /// <summary>
        /// 相对路径
        /// </summary>
        /// <value></value>
        public string RelativePath { get; set; } = null!;

        /// <summary>
        /// 文件类型
        /// </summary>
        /// <value></value>
        public string FileType { get; set; } = null!;
        /// <summary>
        /// 文件上传用户的Id
        /// </summary>
        /// <value></value>
        public Guid UserId { get; set; }
    }
}