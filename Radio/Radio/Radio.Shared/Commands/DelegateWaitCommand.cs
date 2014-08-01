using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Radio.Commands
{
    public class DelegateWaitCommand : ICommand
    {
        private readonly Func<object, Task> _execute;
        private readonly Func<object, bool> _canExecute;

        private bool _isIdle;

        public DelegateWaitCommand(Func<object, Task> execute, Func<object, bool> canExecute = null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _isIdle = true;

            _execute = execute;

            if (canExecute == null)
            {
                _canExecute = (obj) => true;
            }
            else
            {
                _canExecute = canExecute;
            }
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _isIdle && _canExecute(parameter);
        }

        public async void Execute(object parameter)
        {
            ChangeCanExecute(false);

            await _execute(parameter);

            ChangeCanExecute(true);
        }

        protected virtual void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void ChangeCanExecute(bool canExecute)
        {
            _isIdle = canExecute;
            OnCanExecuteChanged();
        }
    }
}