using MediatR;
using Microsoft.AspNetCore.Authentication;
using SelfieAWookie.API.UI.Application.DTOs;

namespace SelfieAWookie.API.UI.Application.Queries
{
    /// <summary>
    /// Query to select all selfies for a wookie (with dto class)
    /// </summary>
    public class SelectAllSelfiesQuery : IRequest<List<SelfieResumeDto>>
    {
        #region Properties
        public int WookieId { get; set; }
        #endregion
    }
}
