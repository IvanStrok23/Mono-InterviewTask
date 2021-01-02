using MonoTask.Core.Entities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTask.Core.Entities.Interfaces
{
    public interface IViewEntity
    {
        List<SortByType> SortableBy();
    }
}
