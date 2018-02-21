using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace VisualExplorer.Model.Utility {

    public class TimerMonitor {

        public enum TimerFrequency {
            Once,
            CountDown,
            Infinity
        }

        private DispatcherTimer _timer = new DispatcherTimer();
        private TimerFrequency _frequency = TimerFrequency.Once;
        private Action _timeOutEvent = null;
        private int LeaveTimes = 0;

        public TimerFrequency Frequency => _frequency;
        public int TimesCount = 0;
        public bool HasStop = false;

        public TimeSpan Interval {
            get { return _timer.Interval; }
            set { _timer.Interval = value; }
        }
        public Action TimeOutEvent { set { _timeOutEvent = value; } }
        public bool IsRunning => _timer.IsEnabled;

        public TimerMonitor(TimerFrequency frequency) {
            _frequency = frequency;
        }

        /// <summary>
        /// 启动计时器
        /// </summary>
        public void Launch() {

            LeaveTimes = TimesCount;
            switch(_frequency) {
                case TimerFrequency.Once:
                    _timer.Tick += (_sender, _e) => {
                        _timeOutEvent();
                        _timer.Stop();
                        HasStop = true;
                    };
                    break;
                case TimerFrequency.CountDown:
                    _timer.Tick += (_sender, _e) => {

                        LeaveTimes--;
                        _timeOutEvent();
                        if (LeaveTimes == 0) {
                            _timer.Stop();
                            HasStop = true;
                        }
                    };
                    break;
                case TimerFrequency.Infinity:
                    _timer.Tick += (_sender, _e) => {
                        _timeOutEvent();
                    };
                    break;
            }

            _timer.Start();
        }

        /// <summary>
        /// 停止计时器
        /// </summary>
        public void Terminate() {
            if(_timer.IsEnabled) {
                _timer.Stop();
                HasStop = true;
            }
        }
        


    }
}
