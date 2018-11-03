using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneRegister.DataModel {
    class PRContext: DbContext {
        public DbSet<PhoneRecord> PhoneRecords { get; set; }
    }
}
