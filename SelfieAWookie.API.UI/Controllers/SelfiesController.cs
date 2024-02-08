using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfieAWookie.API.UI.Application.DTOs;
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
        public IActionResult GetAll([FromQuery] int wookieId = 0)
        {
            var param = this.Request.Query["wookyId"];

            var selfiesList = _repository.GetAll(wookieId);
            var model = selfiesList.Select(item => new SelfieResumeDto() { 
                Title = item.Title, 
                WookieId = item.Wookie.Id, 
                NbSelfiesFromWookie = (item.Wookie?.Selfies?.Count).GetValueOrDefault(0) }).ToList();

            return this.Ok(model);
        }

        [HttpPost]
        public IActionResult AddOne(SelfieDto dto)
        {
            IActionResult result = this.BadRequest();

            Selfie addSelfie = this._repository.AddOne(new Selfie()
            {
                ImagePath = dto.ImagePath,
                Title = dto.Title
            });
            this._repository.UnitOfWork.SaveChanges();
             
            if (addSelfie != null)
            {
                dto.Id = addSelfie.Id;
                result = this.Ok(dto);
            }

            return result;
        }
        #endregion
    }
}
