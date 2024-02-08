using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Data;

namespace SelfieAWookie.API.UI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SelfiesController : ControllerBase
    {
        #region Fields
        private readonly ISelfieRepository _repository = null;
        #endregion

        #region Constructors
        public SelfiesController(ISelfieRepository repository)
        {
            this._repository = repository;
        }
        #endregion

        #region Public methods    
        
        [HttpGet]
        public IActionResult Get()
        {
            var selfiesList = _repository.GetAll();
            var model = selfiesList.Select(item => new { Title = item.Title, WookieId = item.Wookie.Id, NbSelfiesFromWookie = item.Wookie.Selfies.Count});

            return this.Ok(model);
        }
        #endregion
    }
}
