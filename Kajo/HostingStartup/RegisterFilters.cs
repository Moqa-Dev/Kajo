using Kajo.Filters;
using Kajo.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Kajo.Configuration.Startup.HostingStartup
{
    public static class RegisterFilters
    {
        public static void RegisterCustomFilters(FilterCollection filters)
        {
            filters.Add(new ExceptionHandlingFilter());
        }
    }
}
