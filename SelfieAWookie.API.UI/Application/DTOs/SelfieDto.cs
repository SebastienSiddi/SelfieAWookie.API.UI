using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SelfieAWookie.API.UI.Application.DTOs
{
    public class SelfieDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImagePath { get; set; }
    }
}
