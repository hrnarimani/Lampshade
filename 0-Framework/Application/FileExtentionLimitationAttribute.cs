using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application
{
    public class FileExtentionLimitationAttribute:ValidationAttribute
    {
        private readonly string[] validExtentions;

        public FileExtentionLimitationAttribute(string[] validExtentions)
        {
            this.validExtentions = validExtentions;
        }

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file == null)
                return true;
            var fileExtention = Path.GetExtension(file.FileName);
            return validExtentions.Contains(fileExtention);
        }
    }
}
