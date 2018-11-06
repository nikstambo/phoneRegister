using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneRegister.DataModel {
    public class PhoneRecordRepository {
        PRContext context = new PRContext();

        public Task<List<PhoneRecord>> GetAllPhoneRecords() {
            return context.PhoneRecords.ToListAsync();
        }

        public Task<PhoneRecord> GetRecordById(int id) {
            return context.PhoneRecords.Where(p => p.PhoneRecordId == id).SingleOrDefaultAsync();
        }

        public async Task<PhoneRecord> AddRecordAsync(PhoneRecord record) {
            context.PhoneRecords.Add(record);
            await context.SaveChangesAsync();
            return record;
        }

        public Task<int> GetLastRecordId() {
            return context.PhoneRecords
                .OrderByDescending(r => r.PhoneRecordId)
                .Select(r => r.PhoneRecordId)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteRecordAsync(int id) {
            var record = context.PhoneRecords.FirstOrDefault(r => r.PhoneRecordId == id);
            if (record != null) {
                context.PhoneRecords.Remove(record);
            }
            await context.SaveChangesAsync();
            context.PhoneRecords.Local.Remove(record);
        }

        public async Task<PhoneRecord> UpdateRecordAsync(PhoneRecord record) {
            var temp = context.ChangeTracker.Entries();

            if (!context.PhoneRecords.Local.Any(r => r.PhoneRecordId == record.PhoneRecordId)) {
                context.PhoneRecords.Attach(record);
            }

            var existingEntity = context.PhoneRecords.Local.SingleOrDefault(r => r.PhoneRecordId == record.PhoneRecordId);

            if (existingEntity != null) {
                context.Entry(existingEntity).CurrentValues.SetValues(record);
            } else {
                context.Entry(record).State = EntityState.Modified;
            }

            await context.SaveChangesAsync();
            return record;
        }
    }
}
