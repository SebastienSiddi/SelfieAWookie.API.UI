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
        private readonly IWebHostEnvironment _webHostEnvironment = null;
        #endregion

        #region Constructors
        public SelfiesController(ISelfieRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            this._repository = repository;
            this._webHostEnvironment = webHostEnvironment;
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

        [Route("photos")]
        [HttpPost]
        public async Task<IActionResult> AddPicture(IFormFile picture)
        {
            string filePath = Path.Combine(this._webHostEnvironment.ContentRootPath, @"images\selfies");

            if (! Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            filePath = Path.Combine(filePath, picture.FileName);

            using var stream = new FileStream(filePath, FileMode.OpenOrCreate);          
            await picture.CopyToAsync(stream);

            var itemFile = this._repository.AddOnePicture(filePath);
            this._repository.UnitOfWork.SaveChanges();

            return this.Ok(itemFile);
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
