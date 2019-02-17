using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketTest.Models
{
    public class PostmanLocationViewModel
    {
        public PostmanModel PostmanModel { get; set; }
        public Socket Socket { get; set; }
    }

    public class PostmanModel
    {
        public string Token { get; set; }
        public string Latitude { get; set; }
        public string Longitiude { get; set; }
    }
}
