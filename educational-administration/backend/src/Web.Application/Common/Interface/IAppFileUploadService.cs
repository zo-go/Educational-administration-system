using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Application.Common.Interface
{
    public interface IAppFileUploadService
    {
        //上传文件
        Task<IEnumerable<Guid>> UploadFilesAsync(IFormCollection files);
        //根据Id图片
        Task<FileContentResult> GetFileAsync(Guid id);
    }
}