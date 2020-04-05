using DALTheBookBusinessAccounting.Interfaces;
using DALTheBookBusinessAccounting.Repositories;
using Ninject.Modules;

namespace CompositionRoot.NinjectRegistrations
{
    public class RegistrationDAL : NinjectModule
    {
        public override void Load()
        {
            Bind<ICategoryRepository>().To<CategoryRepository>();
            Bind<IImageRepository>().To<ImageRepository>();
            Bind<IStatusRepository>().To<StatusRepository>();
            Bind<IItemRepository>().To<ItemRepository>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IRoleRepository>().To<RoleRepository>();
        }
    }
}
