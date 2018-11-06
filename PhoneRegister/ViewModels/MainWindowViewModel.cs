using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PhoneRegister.Commands;
using PhoneRegister.DataModel;

namespace PhoneRegister.ViewModels
{
    class MainWindowViewModel: BaseINPC {
        #region Properties

        public BaseINPC CurrentViewModel {
            get { return currentViewModel; }
            set {
                currentViewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }
        }

        #endregion

        private BaseINPC currentViewModel;
        private PhoneRecordRepository repo = new PhoneRecordRepository();
        private PhoneRecordsViewModel phoneRecordsViewModel;
        private AddPhoneRecordViewModel addPhoneRecordViewModel;


        public MainWindowViewModel() {

            phoneRecordsViewModel = new PhoneRecordsViewModel(repo);
            addPhoneRecordViewModel = new AddPhoneRecordViewModel(repo);

            CurrentViewModel = phoneRecordsViewModel;

            phoneRecordsViewModel.AddRecordRequested += NavToAddPhoneRecord;
            phoneRecordsViewModel.EditRecordRequested += NavToEditPhoneRecord;
            addPhoneRecordViewModel.Done += NavToViewAllRecords;
        }

        #region Navigation

        private void NavToAddPhoneRecord(PhoneRecord record) {
            addPhoneRecordViewModel.EditMode = false;
            addPhoneRecordViewModel.SetRecord(record);
            CurrentViewModel = addPhoneRecordViewModel;
        }

        private void NavToEditPhoneRecord(PhoneRecord record) {
            addPhoneRecordViewModel.EditMode = true;
            addPhoneRecordViewModel.SetRecord(record);
            CurrentViewModel = addPhoneRecordViewModel;
        }

        private void NavToViewAllRecords(string errorMessage) {
            phoneRecordsViewModel.AddErrorMessage(errorMessage);
            CurrentViewModel = phoneRecordsViewModel;
        }

        #endregion
    }
}
