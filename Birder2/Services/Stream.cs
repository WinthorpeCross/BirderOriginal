using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public class Stream : IStream
    {
        private readonly MemoryStream _stream;
        public Stream(MemoryStream stream)
        {
            _stream = stream;
        }
        public Byte[] GetPic(IFormFile f)
        {
            byte[] h;
            f.CopyToAsync(_stream);
            return _stream.ToArray();
            //return h;
        }
    }
}
