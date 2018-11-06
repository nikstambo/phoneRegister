using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneRegister.DataModel;
using PhoneRegister.Commands;

namespace PhoneRegister.ViewModels {
    class PhoneRecordsViewModel : BaseINPC{

        #region Properties

        private ObservableCollection<PhoneRecord> records;
        private List<PhoneRecord> allRecords;
        private PhoneRecord selectedRecord;
        private string name;
        private string surname;
        private string phone;
        private int identificationNumber;
        private string searchInput;
        private string errorMessage;
        private bool hasErrorMessage;

        public ObservableCollection<PhoneRecord> Records {
            get { return records; }
            set { if (value != null) {
                    records = value;
                    RaisePropertyChanged("Records");
                }
            }
        }

        public PhoneRecord SelectedRecord {
            get { return selectedRecord; }
            set {
                selectedRecord = value;
                RaisePropertyChanged("SelectedRecord");
                EditRecordCommand.RaiseCanExecuteChanged();
                DeleteRecordCommand.RaiseCanExecuteChanged();
            }
        }

        public string Name {
            get { return name; }
            set {
                name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string Surname {
            get { return surname; }
            set {
                surname = value;
                RaisePropertyChanged("Surname");
            }
        }

        public string Phone {
            get { return phone; }
            set {
                phone = value;
                RaisePropertyChanged("Phone");
            }
        }
        public int IdentificationNumber {
            get { return identificationNumber; }
            set {
                identificationNumber = value;
                RaisePropertyChanged("IdentificationNumber");
            }
        }

        public string SearchInput {
            get { return searchInput; }
            set {
                searchInput = value;
                RaisePropertyChanged("SearchInput");
                FilterRecords(searchInput);
            }
        }

        public string ErrorMessage {
            get { return errorMessage; }
            set {
                errorMessage = value;
                RaisePropertyChanged("ErrorMessage");
            }
        }

        public bool HasErrorMessage {
            get { return hasErrorMessage; }
            set {
                hasErrorMessage = value;
                RaisePropertyChanged("HasErrorMessage");
            }
        }

        #endregion

        #region Commands

        public RelayCommand AddRecordCommand { get; private set; }
        public RelayCommand EditRecordCommand { get; private set; }
        public RelayCommand DeleteRecordCommand { get; private set; }
        public RelayCommand ClearSearchCommand { get; private set; }

        #endregion

        private PhoneRecordRepository _repo;

        public PhoneRecordsViewModel(PhoneRecordRepository repo) {
            _repo = repo;
            AddRecordCommand = new RelayCommand(OnAddRecord);
            EditRecordCommand = new RelayCommand(OnEditRecord, IsRecordSelected);
            DeleteRecordCommand = new RelayCommand(OnDeleteRecord, IsRecordSelected);
            ClearSearchCommand = new RelayCommand(OnClearSearch);
        }

        private bool IsRecordSelected() {
            return SelectedRecord != null ;
        }

        public event Action<PhoneRecord> AddRecordRequested = delegate { };
        public event Action<PhoneRecord> EditRecordRequested = delegate { };
        public event Action<PhoneRecord> DeleteRecordRequested = delegate { };

        private void OnAddRecord() {
            AddRecordRequested(new PhoneRecord());
        }

        private void OnEditRecord() {
            EditRecordRequested(SelectedRecord);
        }

        private async void OnDeleteRecord() {
            await _repo.DeleteRecordAsync(SelectedRecord.PhoneRecordId);
            Records.Remove(SelectedRecord);
            SetSelectionToNull();
        }

        private void OnClearSearch() {
            SearchInput = null;
        }

        public async void LoadRecords() {
            allRecords = await _repo.GetAllPhoneRecords();
            Records = new ObservableCollection<PhoneRecord>(allRecords);
        }

        private void FilterRecords(string searchInput) {
            if (string.IsNullOrWhiteSpace(searchInput)) {
                Records = new ObservableCollection<PhoneRecord>(allRecords);
                return;
            } else {
                Records = new ObservableCollection<PhoneRecord>(allRecords
                    .Where(r => r.Name.ToLower().Contains(searchInput.ToLower()) ||
                            r.Surname.ToLower().Contains(searchInput.ToLower()) ||
                            r.PhoneNumber.ToLower().Contains(searchInput.ToLower())));
            }
        }

        public void AddErrorMessage(string error) {
            if (!string.IsNullOrEmpty(error)) {
                ErrorMessage = error;
                HasErrorMessage = true;
                RaisePropertyChanged("ErrorMessage");
                RaisePropertyChanged("HasErrorMessage");
            } else {
                HasErrorMessage = false;
                RaisePropertyChanged("HasErrorMessage");
            }
        }

        public void SetSelectionToNull() {
            SelectedRecord = null;
        }
    }
}
