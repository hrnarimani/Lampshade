﻿using ShopManagement.Application.Contracts.ProductCategory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Product
{
    public  class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Code { get;  set; }

     

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string ShortDescription { get;  set; }

        public string Description { get;  set; }
        public string Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }

        [Range(1,100000,ErrorMessage = ValidationMessages.IsRequired)]
        public long CategoryId { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string KeyWords { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get;  set; }
       

    }
}
