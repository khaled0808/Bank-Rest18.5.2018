using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BankData
{
    [DataContract]
    public class Person
    {

        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string address { get; set; }

        [DataMember]
        public int AcountID { get; set; }

        [DataMember]

        public string Occupation { get; set; }
        [DataMember]
        public string Salary { get; set; }
        [DataMember]
        public string Maritalstatus { get; set; }
        [DataMember]
        public string Healthstatus { get; set; }
        [DataMember]
        public string Noofchildren { get; set; }
        [DataMember]
        public object BankID { get; set; }
        [DataMember]

        public string Tel { get; set; }

        public DateTime DateofBirth { get; set; }
    }
}
