using System;

namespace NPointeuse.Services
{
    public interface ISpecificExpectedTime
    {
        DateTime Date { get; }
        TimeSpan Duration { get; }
    }
}