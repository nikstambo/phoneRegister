using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneRegister;

namespace PhoneRegister.DataModel
{
    public class PRContext : DbContext {
        public DbSet<PhoneRecord> PhoneRecords { get; set; }
    }
}
