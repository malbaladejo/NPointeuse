using System;

namespace NPointeuse
{
    internal interface IConsoleProgressBar {

        void Write(TimeSpan currentTime, TimeSpan expectedTime);
    }
}
