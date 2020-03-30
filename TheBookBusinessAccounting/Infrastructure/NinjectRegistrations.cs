using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using BLLTheBookOfBusinessAccounting.Services;
using DALTheBookBusinessAccounting.Entities;
using DALTheBookBusinessAccounting.Interfaces;
using DALTheBookBusinessAccounting.Repositories;
using Ninject.Modules;

namespace TheBookBusinessAccounting.Infrastructure
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IReadAndEditRepository<Category>>().To<CategoryRepository>();
            Bind<IReadAndEditRepository<Image>>().To<ImageRepository>();
            Bind<IStatusRepository>().To<StatusRepository>();
            Bind<IItemRepository>().To<ItemRepository>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IRoleRepository>().To<RoleRepository>();

            Bind<IReadAndEditService<CategoryDto>>().To<CategoryService>();
            Bind<IReadAndEditService<ImageDto>>().To<ImageService>();
            Bind<IStatusService>().To<StatusService>();
            Bind<IItemService>().To<ItemService>();
            Bind<IUserService>().To<UserService>();
            Bind<IRoleService>().To<RoleService>();
        }
    }
}