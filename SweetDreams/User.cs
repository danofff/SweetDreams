using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetDreams
{
    public enum gender {male, female}
    public class User
    {
        public Guid PersonalKey { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public gender Gender { get; set; }
        public gender LookingFor { get; set; }
    }
}
