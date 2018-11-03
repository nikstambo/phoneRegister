using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneRegister.Models {
    abstract class PhoneRecord {
        abstract protected int RecordId { get; set; }
        abstract protected string PhoneNumber { get; set; }
        abstract protected string Name { get; set; }
        virtual protected string Surname { get; set; }
        abstract protected string IdentificationNum { get; set; }
    }
}
