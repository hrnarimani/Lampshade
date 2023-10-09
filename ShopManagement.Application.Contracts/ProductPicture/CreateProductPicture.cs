using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public  class CreateProductPicture
    {
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
        [FileExtentionLimitation(new string[] { ".jpg", ".png", ".jpeg" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
        public IFormFile Picture { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get;  set; }

        [Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get;  set; }
       
    }
}
