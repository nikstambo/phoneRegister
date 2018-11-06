using PhoneRegister.DataModel;
using PhoneRegister.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneRegister.ViewModels {
    public class AddPhoneRecordViewModel : BaseINPC {

        #region Properties

        private bool editMode;
        private EditableRecord recordForEditing;
        private PhoneRecord recordForSaving;

        public bool EditMode {
            get { return editMode; }
            set {
                editMode = value;
                RaisePropertyChanged("EditMode");
            }
        }

        public EditableRecord EditablePhoneRecord {
            get { return recordForEditing; }
            set { SetProperty(ref recordForEditing, value); }
        }

        #endregion

        public RelayCommand AddCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public event Action<string> Done = delegate { };
        private PhoneRecordRepository _repo;

        public AddPhoneRecordViewModel(PhoneRecordRepository repo) {
            _repo = repo;
            AddCommand = new RelayCommand(OnAdd, CanSave);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            CancelCommand = new RelayCommand(OnCancel);
        }

        private async void OnAdd() {
            ConvertEditableRecord();

            var errors = await IsEntryValid(recordForSaving);

            if (!errors.Any()) {
                await _repo.AddRecordAsync(recordForSaving);
                Done(string.Empty);
            } else {
                Done(errors.Values.ElementAt(0));
            }

        }

        private async void OnSave() {
            ConvertEditableRecord();

            var errors = await IsEntryValid(recordForSaving);
            if (!errors.Any()) {
                await _repo.UpdateRecordAsync(recordForSaving);
                Done(string.Empty);
            } else {
                Done(errors.Values.ElementAt(0));
            }
        }

        private void OnCancel() {
            Done(string.Empty);
        }

        private bool CanSave() {
            if (EditablePhoneRecord != null) {
                return !EditablePhoneRecord.HasErrors;
            }
            return false;
        }

        // Creating the record which will be validated and passing any record details if any (editing)
        public void SetRecord(PhoneRecord record) {
            if (EditablePhoneRecord != null) EditablePhoneRecord.ErrorsChanged -= RaiseCanExecuteChanged;
            EditablePhoneRecord = new EditableRecord();
            EditablePhoneRecord.ErrorsChanged += RaiseCanExecuteChanged;
            CopyRecordDetails(record, EditablePhoneRecord);
        }

        private void CopyRecordDetails(PhoneRecord sourceRecord, EditableRecord targetRecord) {

            if (EditMode) {
                targetRecord.PhoneRecordId = sourceRecord.PhoneRecordId;
            }

            targetRecord.Name = sourceRecord.Name;
            targetRecord.Surname = sourceRecord.Surname;
            targetRecord.PhoneNumber = sourceRecord.PhoneNumber;
            targetRecord.IdentificationNumber = sourceRecord.IdentificationNumber;
        }

        private void ConvertEditableRecord() {
            recordForSaving = new PhoneRecord();

            if (EditMode) {
                recordForSaving.PhoneRecordId = recordForEditing.PhoneRecordId;
            }

            recordForSaving.Name = recordForEditing.Name;
            recordForSaving.Surname = recordForEditing.Surname;
            recordForSaving.PhoneNumber = recordForEditing.PhoneNumber;
            recordForSaving.IdentificationNumber = recordForEditing.IdentificationNumber;
        }

        private async Task<Dictionary<bool,string>> IsEntryValid(PhoneRecord record) {

            Dictionary<bool,string> errorMsgs = new Dictionary<bool, string> ();

            List<PhoneRecord> allRecords = await _repo.GetAllPhoneRecords();

            if (EditMode) {
                if (allRecords.Select(r => r.PhoneRecordId).ToList().Contains(record.PhoneRecordId)) {
                    var recordToRemove = allRecords.Where(r => r.PhoneRecordId == record.PhoneRecordId).SingleOrDefault();
                    allRecords.Remove(recordToRemove);
                }
            }

            bool invalidIdNumber = allRecords
                .Where(r => r.IdentificationNumber == record.IdentificationNumber).Any();

            bool duplicateName = allRecords
                .Where(r => r.Name == record.Name && r.Surname == record.Surname)
                .Any();

            if(invalidIdNumber) {
                errorMsgs.Add(false, "Record was not saved. Invalid Identification Number.");
            } else if (duplicateName) {
                errorMsgs.Add(false, "Record was not saved. Duplicate name.");
            }
            return errorMsgs;
        }

        private void RaiseCanExecuteChanged(object sender, EventArgs e) {
            SaveCommand.RaiseCanExecuteChanged();
            AddCommand.RaiseCanExecuteChanged();
        }
    }
}