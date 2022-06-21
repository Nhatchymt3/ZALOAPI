using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XProject.Core.Utils
{
    public class LanguageService
    {
        private readonly IStringLocalizer _localizer;
        private readonly IViewLocalizer localizer;


        public LanguageService(IStringLocalizerFactory factory)
        {
            var type = typeof(ShareResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResource", assemblyName.Name);
        }

        public LocalizedString Getkey(string key)
        {
            var a = _localizer.GetString(key);
            return _localizer.GetString(key);
        }

    }
}
