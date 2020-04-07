using CompositionRoot.NinjectRegistrations;
using Ninject.Modules;

namespace TheBookBusinessAccounting.Infrastructure
{
    public class NinjectRegistrations : NinjectModule
    {
        private readonly RegistrationDAL _registrationDAL;
        private readonly RegistrationBLL _registrationBLL;

        public NinjectRegistrations()
        {
            _registrationDAL = new RegistrationDAL();
            _registrationBLL = new RegistrationBLL();
        }

        public override void Load()
        {
            _registrationDAL.OnLoad(this.Kernel);
            _registrationBLL.OnLoad(this.Kernel);
        }
    }
}