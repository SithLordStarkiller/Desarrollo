namespace BlobsUploadFiles.Site.Models
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    public class UserViewModel
    {
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Fotografia")]
        [DataType(DataType.Upload)]
        public IFormFile Photo { get; set; }
    }
}
