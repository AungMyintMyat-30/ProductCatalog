using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogCore.Interfaces
{
    public interface IImageService
    {
        public bool Upload(IFormFile file, string name, string folder);
    }
}
