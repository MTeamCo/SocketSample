//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SocketTest.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PostmanLocations
    {
        public int Id { get; set; }
        public int PostmanId { get; set; }
        public string Latitude { get; set; }
        public string Longitiude { get; set; }
    
        public virtual Postmen Postmen { get; set; }
    }
}