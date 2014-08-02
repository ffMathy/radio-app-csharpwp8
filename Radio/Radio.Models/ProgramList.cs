using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Radio.Models
{
    public abstract class ProgramList : INotifyPropertyChanged
    {
        protected LinkedList<Program> Programs;

        protected abstract Task RefreshPrograms();

        private Program _previousCurrentProgram;
        public Program CurrentProgram
        {
            get { return Programs.FirstOrDefault(p => DateTime.Now > p.Time); }
        }

        public Program NextProgram
        {
            get
            {
                if (CurrentProgram == null) return null;
                return Programs.FirstOrDefault(p => p.Time > CurrentProgram.Time);
            }
        }

        public IEnumerable<Program> RemainingPrograms
        {
            get { return Programs.Where(p => p.Time >= DateTime.Now); }
        }

        public ProgramList()
        {
            Programs = new LinkedList<Program>();
            CurrentProgramCheck();
        }

        protected void RefreshProperties()
        {
            OnPropertyChanged("CurrentProgram");
            OnPropertyChanged("RemainingPrograms");
        }

        private async void CurrentProgramCheck()
        {
            while (true)
            {
                var currentProgram = CurrentProgram;
                if (currentProgram != _previousCurrentProgram)
                {
                    if (_previousCurrentProgram != null)
                    {
                        _previousCurrentProgram.IsPlaying = false;
                    }
                    currentProgram.IsPlaying = true;

                    var nextProgram = NextProgram;
                    if (nextProgram != null)
                    {
                        nextProgram.IsPlaying = false;
                    }

                    _previousCurrentProgram = currentProgram;
                }
                await Task.Delay(10000);
            }
        }

        public static bool IgnorePrograms { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {

            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
