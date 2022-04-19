using NPointeuse.Infra.Client;
using NPointeuse.Infra.XF;
using NPointeuse.Services;

namespace NPointeuse.XF.Views.EditReportedTime
{
    internal class EditReportedTimeNavigationToken : INavigationToken
    {
        public EditReportedTimeNavigationToken(DateRange dateRange )
        {
            DateRange = dateRange;
        }

        public string Title => "Edit specific expected time";

        public DateRange DateRange { get; }
    }
}