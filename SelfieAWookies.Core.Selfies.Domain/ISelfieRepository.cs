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
        ICollection<Selfie> GetAll(int wookieId);

        /// <summary>
        /// Add a new Selfie in database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Selfie AddOne(Selfie item);

        /// <summary>
        /// Add a new Picture in database
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Picture AddOnePicture(string url);        
    }
}
