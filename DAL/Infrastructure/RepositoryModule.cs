using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagementSystem.BLL.Interfaces;
using HotelManagementSystem.DAL.Repository;
using Ninject.Modules;

namespace HotelManagementSystem.DAL.Infrastructure
{
    public class RepositoryModule : NinjectModule
    {
        private readonly string connectionString;

        public RepositoryModule(string connection)
        {
            connectionString = connection;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork.UnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
