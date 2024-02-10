using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfieAWookie.API.UI.Application.Commands;
using SelfieAWookie.API.UI.Application.DTOs;
using SelfieAWookie.API.UI.Application.Queries;
using SelfieAWookie.API.UI.ExtensionMethods;
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Data;

namespace SelfieAWookie.API.UI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class SelfiesController : ControllerBase
    {
        #region Fields
        private readonly ISelfieRepository _repository = null;
        private readonly IWebHostEnvironment _webHostEnvironment = null;
        private readonly IMediator _mediator = null;
        #endregion

        #region Constructors
        public SelfiesController(IMediator mediator, ISelfieRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            this._repository = repository;
            this._webHostEnvironment = webHostEnvironment;
            this._mediator = mediator;
        }
        #endregion

        #region Public methods    
        
        [HttpGet]       
        public IActionResult GetAll([FromQuery] int wookieId = 0)
        {
            var param = this.Request.Query["wookyId"];          

            var model = this._mediator.Send(new SelectAllSelfiesQuery() { WookieId = wookieId });

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
        public async Task<IActionResult> AddOne(SelfieDto dto)
        {
            IActionResult result = this.BadRequest();          

            var item = await this._mediator.Send(new AddSelfieCommand() { Item = dto });

            if (item != null)
            {
                result = this.Ok(item);
            }

            return result;
        }
        #endregion
    }
}
