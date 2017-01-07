using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KMOL.Data
{
    public class GetNinject
    {
        StandardKernel _kernel;
        public GetNinject()
        {
            _kernel = new StandardKernel();
            _kernel.Load(Assembly.GetExecutingAssembly());
        }
        public T Get<T>()
        {
            return _kernel.Get<T>();
        }
    }
}
