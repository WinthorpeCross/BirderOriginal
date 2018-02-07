using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public interface IStream
    {
        Byte[] GetPic(IFormFile f);
    }
}
