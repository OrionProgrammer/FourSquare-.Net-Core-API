using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Services.Helpers
{
    public class FourSquareAuthModel
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
    }
}
