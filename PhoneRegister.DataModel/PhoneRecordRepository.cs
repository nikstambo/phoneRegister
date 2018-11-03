using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneRegister.DataModel {
    class PhoneRecordRepository {

        PRContext context = new PRContext();

        public Task<List<PhoneRecord>> GetAllRecords() {
            return context.PhoneRecords.ToListAsync();
        }

        public Task<PhoneRecord> GetRecord(int id) {
            return context.PhoneRecords.FirstOrDefaultAsync(r => r.RecordId == id);
        }

        public async Task<PhoneRecord> AddRecord(PhoneRecord record) {
            context.PhoneRecords.Add(record);
            await context.SaveChangesAsync();
            return record;
        }

        public async Task DeleteRecord(int id) {
            var record = context.PhoneRecords.FirstOrDefault(r => r.RecordId == id);

            if (record != null) {
                context.PhoneRecords.Remove(record);
            }

            await context.SaveChangesAsync();
        }
    }
}
