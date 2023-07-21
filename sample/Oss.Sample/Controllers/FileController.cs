using Aliyun.OSS;
using Aliyun.OSS.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace Oss.Sample.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly OssClient _ossClient;
    public FileController(OssClient ossClient)
    {
        _ossClient = ossClient;
    }

    [HttpPost]
    public void CreateBucket(string bucketName)
    {
        try
        {
            if (!_ossClient.DoesBucketExist(bucketName))
            {
                var result = _ossClient.CreateBucket(bucketName);
                Console.WriteLine("Create bucket, ETag: {0} ", result.Name);
            }
            else
            {
                Console.WriteLine("bucket Exist, ETag: {0} ", bucketName);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Put object failed, {0}", ex.Message);
        }

    }


    [HttpPost]
    public void PutObject(IFormFile file)
    {
        try
        {
            var stream = file.OpenReadStream();
            // 上传文件。
            var result = _ossClient.PutObject("onevip", file.FileName, stream);
            Console.WriteLine("Put object succeeded, ETag: {0} ", result.ETag);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Put object failed, {0}", ex.Message);
        }
    }

    [HttpPost]
    public void Download()
    {
        try
        {
            // 下载文件。
            var result = _ossClient.GetObject("onevip", "FreeRedis.AspNetCore.1.0.0.nupkg");
            using (var requestStream = result.Content)
            {
                using (var fs = System.IO.File.Open($"D:\\{result.Key}", FileMode.OpenOrCreate))
                {
                    int length = 4 * 1024;
                    var buf = new byte[length];
                    do
                    {
                        length = requestStream.Read(buf, 0, length);
                        fs.Write(buf, 0, length);
                    } while (length != 0);
                }
            }
            Console.WriteLine("Get object succeeded");
        }
        catch (OssException ex)
        {
            Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed with error info: {0}", ex.Message);
        }
    }


}
