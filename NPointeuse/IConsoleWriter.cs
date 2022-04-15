using NPointeuse.Services;

namespace NPointeuse
{
    internal interface IConsoleWriter
    {
        void WriteHeader();
        void WriteQuestion(IBusinessTimeService timeService);
        void WriteStatus(IBusinessTimeService timeDataService);
        void WriteTimes(IBusinessTimeService timeService);
    }
}