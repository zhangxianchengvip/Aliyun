using System;
using System.Collections.Generic;
using System.Text;

namespace Aliyun.OSS.Sdk.AspNetCore
{
    public class ClientConnectionConfiguration
    {
        /// 填写Bucket所在地域对应的Endpoint。或自定义域名
        public string Endpoint { get; set; }

        //阿里云账号AccessKey拥有所有API的访问权限，风险很高。强烈建议您创建并使用RAM用户进行API访问或日常运维，请登录RAM控制台创建RAM用户。
        public string AccessKeyId { get; set; }
        public string AccessKeySecret { get; set; }

        //打开CNAME开关。CNAME是指将自定义域名绑定到存储空间的过程。
        public bool IsCname { get; set; }
    }
}
