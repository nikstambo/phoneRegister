using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneRegister.Models {
    class PersonRecord : Record {
        protected override int RecordId { get; set; }
        protected override string PhoneNumber { get; set; }
        protected override string Name { get; set; }
        protected override string Surname { get; set; }
        protected override string IdentificationNum { get; set; }
    }
}
