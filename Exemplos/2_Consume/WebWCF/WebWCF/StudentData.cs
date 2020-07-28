using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebWCF
{
    [DataContract]
    public class StudentData
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember(Name = "Nome")]
        public string Name { get; set; }
    }
}