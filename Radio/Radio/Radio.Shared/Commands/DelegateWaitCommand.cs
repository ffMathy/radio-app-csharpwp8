using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Radio.Commands
{
    public class DelegateWaitCommand : ICommand
    {
        private readonly Func<object, Task> _execute;

        private bool _canExecute;

        public DelegateWaitCommand(Func<object, Task> execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;

            _canExecute = true;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute;
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
            _canExecute = canExecute;
            OnCanExecuteChanged();
        }
    }
}