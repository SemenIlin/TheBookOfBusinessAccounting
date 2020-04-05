using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.Services;
using Ninject.Modules;

namespace CompositionRoot.NinjectRegistrations
{
    public class RegistrationBLL : NinjectModule
    {
        public override void Load()
        {
            Bind<ICategoryService>().To<CategoryService>();
            Bind<IImageService>().To<ImageService>();
            Bind<IStatusService>().To<StatusService>();
            Bind<IItemService>().To<ItemService>();
            Bind<IUserService>().To<UserService>();
            Bind<IRoleService>().To<RoleService>();
        }
    }
}
