using Invedia.Core.ConfigUtils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XProject.Core.Configs;

namespace XProject.Core.Extension
{
    public static class ServiceExt
    {
        public static IServiceCollection AddSystemHelper(this IServiceCollection serviceProvider, IConfiguration configuration)
        {
            SystemSettingModel.Instance = configuration.GetSection<SystemSettingModel>("SystemSetting") ?? new SystemSettingModel();
            MailSettings.Default = configuration.GetSection<MailSettings>("MailSettings") ?? new MailSettings();

            return serviceProvider;
        }

        public static IApplicationBuilder UseSystemSetting(this IApplicationBuilder app)
        {
            SystemSettingModel.Configs = app.ApplicationServices.GetService<IConfiguration>();

            return app;
        }
    }
}
