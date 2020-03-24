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
            Bind<IReadRepository<Status>>().To<StatusRepository>();
            Bind<IRepository<Item>>().To<ItemRepository>();

            Bind<IReadAndEditService<CategoryDto>>().To<CategoryService>();
            Bind<IReadAndEditService<ImageDto>>().To<ImageService>();
            Bind<IReadService<StatusDto>>().To<StatusService>();
            Bind<IService<ItemDto>>().To<ItemService>();
        }
    }
}