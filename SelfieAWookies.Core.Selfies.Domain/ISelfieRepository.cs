using SelfieAWookies.Core.Selfies.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfieAWookies.Core.Selfies.Domain
{
    /// <summary>
    /// Repository to manage Selfies
    /// </summary>
    public interface ISelfieRepository : IRepository
    {
        ICollection<Selfie> GetAll();

        /// <summary>
        /// Add a new Selfie in database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Selfie AddOne(Selfie item);
    }
}
