using MediatR;
using SelfieAWookie.API.UI.Application.DTOs;
using SelfieAWookies.Core.Selfies.Domain;

namespace SelfieAWookie.API.UI.Application.Queries
{
    public class SelectAllSelfiesHandler : IRequestHandler<SelectAllSelfiesQuery, List<SelfieResumeDto>>
    {
        #region Fields
        private readonly ISelfieRepository _repository = null;
        #endregion

        #region Constructors
        public SelectAllSelfiesHandler(ISelfieRepository repository)
        {
            this._repository = repository;
        }
        #endregion

        #region Public methods
        public Task<List<SelfieResumeDto>> Handle(SelectAllSelfiesQuery request, CancellationToken cancellationToken)
        {

            var selfiesList = _repository.GetAll(request.WookieId);
            var result =  selfiesList.Select(item => new SelfieResumeDto() { 
                Title = item.Title, 
                WookieId = item.Wookie.Id, 
                NbSelfiesFromWookie = (item.Wookie?.Selfies?.Count).GetValueOrDefault(0) }).ToList();

            return Task.FromResult(result);
        }
        #endregion
    }
}
