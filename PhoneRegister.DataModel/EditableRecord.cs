using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneRegister.DataModel;

namespace PhoneRegister.DataModel {
    public class EditableRecord : ValidatableBase {

        private int phoneRecordId;
        private string name;
        private string surname;
        private string phoneNumber;
        private string identificationNumber;


        public int PhoneRecordId {
            get { return phoneRecordId; }
            set { SetProperty(ref phoneRecordId, value); }
        }

        [Required]     
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Must contain only letters!")]
        public string Name {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Must contain only letters!")]
        public string Surname {
            get { return surname; }
            set { SetProperty(ref surname, value); }
        }

        [Required]
        [Phone]
        public string PhoneNumber {
            get { return phoneNumber; }
            set { SetProperty(ref phoneNumber, value); }
        }

        [Required]
        [RegularExpression("^\\d+$", ErrorMessage ="Must contain positive integers only!")]
        public string IdentificationNumber {
            get { return identificationNumber; }
            set { SetProperty(ref identificationNumber, value); }
        }
    }
}
