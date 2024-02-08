using Microsoft.AspNetCore.Authentication;

namespace SelfieAWookie.API.UI.Application.DTOs
{
    public class SelfieResumeDto
    {
        #region Properties
        public int NbSelfiesFromWookie { get; set; }

        public string Title { get; set; }

        public int WookieId { get; set; }
        #endregion
    }
}
