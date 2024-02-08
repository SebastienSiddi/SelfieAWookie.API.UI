using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfieAWookies.Core.Selfies.Framework
{
    public interface IUnitOfWork
    {
        int SaveChanges();
    }
}
