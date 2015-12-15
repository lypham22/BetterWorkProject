﻿using BW.Repository.Data.Infrastructure;
using BW.Repository.Data.Repositories;
using BW.Services.Api.Resolver;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace BW.Services.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Web API configuration and services
            var container = new UnityContainer();
            //container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<IDatabaseFactory, DatabaseFactory>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            //Ioc.Instance.<IDatabaseFactory>(new DatabaseFactory());
            //Ioc.Instance.RegisterType<IDatabaseSessionFactory, DBSessionFactoryEager>();
            //Ioc.Instance.RegisterType<IDatabaseFactory, DatabaseFactory>();
            //Ioc.Instance.RegisterType<IUnitOfWork, UnitOfWork>();
            //Ioc.Instance.RegisterType<IUserRepository, UserRepository>();
        }
    }
}
