using Autofac;
using MonitoringClient.Repository;
using MonitoringClient.Services;
using MonitoringClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringClient.IoC
{
    public class Injector
    {
        public LogEntryRepository InjectLogEntryRepository()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<LogEntryRepository>();
            builder.RegisterType<LogEntryRepository>().As<ILogEntryRepository>();
            IContainer container = builder.Build();
            return container.Resolve<LogEntryRepository>();
        }

        public CustomerRepository InjectCustomerRepository()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CustomerRepository>();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            IContainer container = builder.Build();
            return container.Resolve<CustomerRepository>();
        }

        public LocationRepository InjectLocationRepository()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<LocationRepository>();
            builder.RegisterType<LocationRepository>().As<ILocationRepository>();
            IContainer container = builder.Build();
            return container.Resolve<LocationRepository>();
        }
    }
}
