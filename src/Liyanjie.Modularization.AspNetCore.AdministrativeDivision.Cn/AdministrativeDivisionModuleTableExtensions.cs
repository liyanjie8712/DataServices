﻿using System;

using Microsoft.Extensions.DependencyInjection;

namespace Liyanjie.Modularization.AspNetCore
{
    /// <summary>
    /// 
    /// </summary>
    public static class AdministrativeDivisionModuleTableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleTable"></param>
        /// <param name="configureOptions"></param>
        /// <param name="getRouteTemplate"></param>
        /// <param name="getChildrenRouteTemplate"></param>
        /// <param name="findRouteTemplate"></param>
        /// <returns></returns>
        public static ModularizationModuleTable AddAdministrativeDivisionCn(this ModularizationModuleTable moduleTable,
            Action<AdministrativeDivisionModuleOptions> configureOptions,
            string getRouteTemplate= "AD/Get",
            string getChildrenRouteTemplate = "AD/GetChildren",
            string findRouteTemplate = "AD/Find")
        {
            moduleTable.Services.AddSingleton<AdministrativeDivisionFindMiddleware>();
            moduleTable.Services.AddSingleton<AdministrativeDivisionGetMiddleware>();
            moduleTable.Services.AddSingleton<AdministrativeDivisionGetChildrenMiddleware>();

            moduleTable.AddModule("AdministrativeDivisionModule", new[]
            {
                new ModularizationModuleMiddleware
                {
                    HttpMethods = new[] { "GET" },
                    RouteTemplate = findRouteTemplate,
                    HandlerType = typeof(AdministrativeDivisionFindMiddleware),
                },
                new ModularizationModuleMiddleware
                {
                    HttpMethods = new[] { "GET" },
                    RouteTemplate = getRouteTemplate,
                    HandlerType = typeof(AdministrativeDivisionGetMiddleware),
                },
                new ModularizationModuleMiddleware
                {
                    HttpMethods = new[] { "GET" },
                    RouteTemplate = getChildrenRouteTemplate,
                    HandlerType = typeof(AdministrativeDivisionGetChildrenMiddleware),
                },
            }, configureOptions);

            return moduleTable;
        }
    }
}
