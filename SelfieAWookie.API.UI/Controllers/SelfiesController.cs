using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfieAWookies.Core.Selfies.Infrastructures.Data;

namespace SelfieAWookie.API.UI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SelfiesController : ControllerBase
    {
        #region Fields
        private readonly SelfiesContext _context = null;
        #endregion

        #region Constructors
        public SelfiesController(SelfiesContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public methods    
        
        [HttpGet]
        public IActionResult Get()
        {
            var model = this._context.Selfies.Include(item => item.Wookie).Select(item => new { Title = item.Title, WookieId = item.Wookie.Id, NbSelfiesFromWookie = item.Wookie.Selfies.Count}).ToList();

            return this.Ok(model);
        }
        #endregion
    }
}
