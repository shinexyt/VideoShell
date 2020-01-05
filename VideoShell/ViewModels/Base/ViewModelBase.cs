using VideoShell.Services;
using System.Threading.Tasks;
using VideoShell.Models;
using VideoShell.ViewModels.Base;
using VideoShell.Extension.Abstraction;
using System.Composition;
using VideoShell.Extension.Abstraction.Models;
using System.Composition.Hosting;
using System.Reflection;

namespace VideoShell.ViewModels.Base
{
    public abstract class ViewModelBase : ExtendedBindableObject
    {
        protected IDialogService DialogService { get; }
        protected INavigationService NavigationService { get; }
        protected IDataSource<Video> DataSource { get;  }
        private bool _isBusy;

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public ViewModelBase()
        {
            DialogService = ViewModelLocator.Resolve<IDialogService>();
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
            using (var host = new ContainerConfiguration().WithAssembly(Assembly.GetExecutingAssembly()).CreateContainer())
            {
                DataSource = host.GetExport<IDataSource<Video>>();
            }
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}