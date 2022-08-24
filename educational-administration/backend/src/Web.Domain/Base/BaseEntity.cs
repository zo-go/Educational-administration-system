using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Base
{
    public class BaseEntity
    {
        /// 主键
        public Guid Id { get; set; }

        /// 创建时间
        public DateTime CreatedAt { get; set; }

        /// （最后）更新时间
        public DateTime UpdatedAt { get; set; }

        /// 创建人（的Id）
        public Guid? CreatedBy { get; set; }

        /// （最后）更新人（的Id）
        public Guid? UpdatedBy { get; set; }

        /// 是否删除，默认否
        public bool IsDeleted { get; set; }

        /// 是否启用，默认是
        public bool IsActived { get; set; }

        /// 备注
        public string? Remarks { get; set; }
    }
}