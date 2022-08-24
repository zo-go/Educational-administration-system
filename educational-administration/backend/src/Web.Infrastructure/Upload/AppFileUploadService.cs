using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Web.Application.Common.Interface;
using Web.Application.Configuration;
using Web.Domain.Entity;

namespace Web.Infrastructure.Upload
{
    public class AppFileUploadService : IAppFileUploadService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<AppUploadFileInfo> _appuploadfileinfo;
        private readonly ISessionUserService _sessionUserService;
        private readonly IRepository<AppUser> _userRepository;

        public AppFileUploadService(IConfiguration configuration, IRepository<AppUser> userRepository, ISessionUserService sessionUserService, IRepository<AppUploadFileInfo> appuploadfileinfo)
        {
            _configuration = configuration;
            _appuploadfileinfo = appuploadfileinfo;
            _sessionUserService = sessionUserService;
            _userRepository = userRepository;
        }

        public async Task<FileContentResult> GetFileAsync(Guid id)
        {
            var currentPath = Directory.GetCurrentDirectory();
            var fileInfo = await _appuploadfileinfo.Table.FirstOrDefaultAsync(x => x.Id == id);
            if (fileInfo == null)
            {
                return new FileContentResult(new byte[0], "image/jpeg");
            }
            var fullPath = Path.Combine(currentPath, fileInfo.RelativePath);
            if (!System.IO.File.Exists(fullPath))
            {
                return new FileContentResult(new byte[0], "image/jpeg");
            };
            using (var sw = new FileStream(fullPath, FileMode.Open))
            {
                var contenttype = fileInfo.FileType;
                var bytes = new byte[sw.Length];
                sw.Read(bytes, 0, bytes.Length);
                sw.Close();
                return new FileContentResult(bytes, contenttype);
            }
        }

        public async Task<IEnumerable<Guid>> UploadFilesAsync(IFormCollection files)
        {

            var userid = new Guid(_sessionUserService.UserId.ToString()!);

            System.Console.WriteLine("                   ++++++++++++++++++++++++++++++++++++" + userid);
            //当前路径(完整路径)
            var currentPath = Directory.GetCurrentDirectory();
            //读取配置文件 模型接受对应的值
            var uploadFileSetting = _configuration.GetSection("UploadFileSetting").Get<UploadFileSetting>();

            // 配置路径（相对路径）
            var configPath = uploadFileSetting.FilePath;
            //获取配置文件 中允许的扩展名
            var permittedExtensions = uploadFileSetting.AllowFileExtensions.Split(',');

            var maxFileSize = uploadFileSetting.AllowMaxFileSize;

            // 用于装载文件的相对路径
            var list = new List<Guid>();
            var uploadedFilesInfo = new List<AppUploadFileInfo>();


            // 过滤1查找这（上传的文件中的扩展名）有没有任何不在允许列表中的情况，如果有，则全部拒绝上传    
            var uploadedFileExtens = files.Files.Where(x =>
          {
              var ext = Path.GetExtension(x.FileName).ToLowerInvariant();
              return string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext);
          });
            var count = uploadedFileExtens.Count();
            if (count > 0)
            {
                return list;
            }

            var uploadeFileMaxSize = files.Files.Where(x =>
            {
                return x.Length > maxFileSize;
            });
            if (uploadeFileMaxSize.Count() > 0)
            {
                return list;
            }

            foreach (var formFile in files.Files)
            {
                if (formFile.Length > 0)
                {
                    var now = DateTime.Now;
                    // 配置路径 + 年 + 月 +日的路径
                    var preFilePath = Path.Combine(configPath, now.Year.ToString(), now.Month.ToString(), now.Day.ToString());
                    // 路径不存在则创建
                    if (!Directory.Exists(Path.Combine(currentPath, preFilePath)))
                    {
                        Directory.CreateDirectory(Path.Combine(currentPath, preFilePath));
                    }

                    // 随机文件名称
                    var rndName = Guid.NewGuid().ToString("N");
                    // 上传文件的扩展名（带点的，形如 .txt .sql .jpeg）
                    var extName = formFile.FileName.Substring(formFile.FileName.IndexOf("."));
                    // 新的文件名（随机文件名+扩展名）
                    var newFileName = rndName + extName;
                    // 完整的文件名称（包含当前路径、配置路径、年、月、日、新文件名）
                    var fullName = Path.Combine(currentPath, preFilePath, newFileName);
                    // var filePath = Path.GetTempFileName();

                    // 利用文件流创建文件，写入到磁盘
                    using (var stream = System.IO.File.Create(fullName))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    // 最后替换非主流路径符号为正斜杠

                    var relativePath = Path.Combine(preFilePath, newFileName).Replace("\\", "/");

                    var fileInfo = new AppUploadFileInfo
                    {
                        UserId = userid,
                        OriginFileName = formFile.FileName,
                        CurrentFileName = newFileName,
                        RelativePath = relativePath,
                        FileType = formFile.ContentType
                    };

                    uploadedFilesInfo.Add(fileInfo);

                }
            }

            var hasAvatar = _appuploadfileinfo.Table.Where(x => x.UserId == userid).FirstOrDefault();
            if (hasAvatar != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), hasAvatar.RelativePath);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                await _appuploadfileinfo.DeleteAsync(hasAvatar.Id, true);

            }
            await _appuploadfileinfo.AddBulkAsync(uploadedFilesInfo);

            var userinfo = _userRepository.Table.Where(x => x.Id == userid).FirstOrDefault();
            userinfo!.AvatarId = uploadedFilesInfo[0].Id;

            await _userRepository.UpdateAsync(userinfo);

            list = uploadedFilesInfo.Select(x => x.Id).ToList();
            return list;
        }
    }
}