using DAL.Interface.Interfaces;
using DAL.Interface.DTO;
using DAL.Repositories.ADONET;
using DAL.Repositories.EntityFramework;
using BLL.Interface.Interfaces;
using BLL.Interface.Entities;
using BLL.Services;
using DAL;
using Ninject;
using Ninject.Modules;

namespace DependencyResolver
{
    public static class ResolverConfig// : NinjectModule
    {
        public static void ConfigureResolver(this IKernel kernel)
        {
            kernel.Bind<IAccountRepository>().To<ADONETAccountRepository>();
            kernel.Bind<IEmployeeRepository>().To<ADONETEmployeeRepository>();
            kernel.Bind<ITaskRepository>().To<ADONETTaskRepository>();

            kernel.Bind<IFileSystemRepository>().To<FileSystemRepository>();
            kernel.Bind<IBlobCreator>().To<BlobCreator>();

            kernel.Bind<IAccountService>().To<AccountService>()
                .WithConstructorArgument("repository", kernel.Get<IAccountRepository>());

            kernel.Bind<IEmployeeService>().To<EmployeeInfoService>()
                .WithConstructorArgument("repository", kernel.Get<IEmployeeRepository>());

            kernel.Bind<ITaskService>().To<TaskService>()
                .WithConstructorArgument("taskRepository", kernel.Get<ITaskRepository>())
                .WithConstructorArgument("employeeRepository", kernel.Get<IEmployeeRepository>())
                .WithConstructorArgument("fileSystemRepository", kernel.Get<IFileSystemRepository>())
                .WithConstructorArgument("blob", kernel.Get<IBlobCreator>());
        }
    }
}
