using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Application.Configuration
{
    public class UploadFileSetting
    {
        public string FilePath { get; set; } = null!;
        public string AllowFileExtensions { get; set; } = null!;
        public long AllowMaxFileSize { get; set; }
    }
}