using ReactiveUI;
using System;

namespace TestCatalogAvalonia.Views
{
    public class ViewModelBase : ReactiveObject, IDisposable
    {
        public virtual void Dispose() { }
    }
}
