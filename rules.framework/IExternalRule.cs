using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rules.framework
{
    public interface IExternalRule<in T>: IRule<T>
    {
    }
}
