using Aliyun.OSS.Common;
using Aliyun.OSS.Sdk.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;

namespace Aliyun.OSS.Sdk.AspNetCore
{
    public static class AliyunOSSServiceCollectionExtensions
    {
        public static IServiceCollection AddOSSAliyun(this IServiceCollection services, Action<ClientConnectionConfiguration> connectionConfiguration, string securityToken = null)
        {
            services.AddOSSAliyun(connectionConfiguration, s => { }, securityToken);
            return services;
        }
        public static IServiceCollection AddOSSAliyun(this IServiceCollection services, Action<ClientConnectionConfiguration> connectionConfiguration, Action<ClientConfiguration> clientConfiguration, string securityToken = null)
        {
            var connection = new ClientConnectionConfiguration();

            connectionConfiguration(connection);

            var client = new ClientConfiguration();

            client.IsCname = connection.IsCname;

            clientConfiguration(client);

            if (securityToken == null)
            {
                services.AddScoped(sp =>
                {
                    return new OssClient(connection.Endpoint, connection.AccessKeyId, connection.AccessKeySecret, client);
                });
            }
            else
            {
                services.AddScoped(sp =>
                {
                    return new OssClient(connection.Endpoint, connection.AccessKeyId, connection.AccessKeySecret, securityToken, client);
                });
            }


            return services;
        }



    }
}
