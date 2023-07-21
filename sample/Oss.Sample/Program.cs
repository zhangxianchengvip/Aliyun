using Aliyun.OSS.Sdk.AspNetCore;
namespace Oss.Sample;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddOSSAliyun(conn =>
        {

            conn.Endpoint = "oss-cn-beijing.aliyuncs.com";
            conn.AccessKeySecret = "R28E5Exxxxxxxxxxxqa7terCWfGIVVIh";
            conn.AccessKeyId = "LTxxxxxxxx5oAG";
        });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
