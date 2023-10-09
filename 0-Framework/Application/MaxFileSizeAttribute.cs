using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;

namespace _0_Framework.Application
{
    public  class MaxFileSizeAttribute:ValidationAttribute 
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file == null)
                return true;
            return file.Length <= _maxFileSize;
        }

      
    }
}
