using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PhoneRegister.Commands {
    public class RelayCommand : ICommand {

        public Action TargetExecuteMethod;
        Func<bool> TargetCanExecuteMethod;

        public RelayCommand(Action executeMethod) {
            TargetExecuteMethod = executeMethod;
        }

        public RelayCommand(Action executeMethod, Func<bool> canExecuteMethod) {
            TargetCanExecuteMethod = canExecuteMethod;
            TargetExecuteMethod = executeMethod;
        }

        public bool CanExecute(object parameter) {
            if (TargetCanExecuteMethod != null) {
                return TargetCanExecuteMethod();
            }

            if (TargetExecuteMethod != null) {
                return true;
            }
            return false;
        }

        public void Execute(object parameter) {
            if (TargetExecuteMethod != null) {
                TargetExecuteMethod();
            }
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public void RaiseCanExecuteChanged() {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
