using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Radio.Models
{
    public class Program : INotifyPropertyChanged
    {
        protected bool Equals(Program other)
        {
            return _time.Equals(other._time) && string.Equals(Title, other.Title);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_time.GetHashCode()*397) ^ (Title != null ? Title.GetHashCode() : 0);
            }
        }

        private DateTime _time;
        private readonly ProgramList _programList;

        private bool _isPlaying;

        public Program(ProgramList programList)
        {
            _programList = programList;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool IsPlaying
        {
            get { return _isPlaying; }
            set
            {
                if (value.Equals(_isPlaying)) return;
                _isPlaying = value;
                OnPropertyChanged();
            }
        }

        public DateTime Time
        {
            get { return _time; }
            set
            {
                if (_time == default(DateTime))
                {
                    _time = value;
                }
            }
        }

        public string Title { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
