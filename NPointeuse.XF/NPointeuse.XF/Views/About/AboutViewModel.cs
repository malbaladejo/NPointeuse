namespace NPointeuse.XF.ViewModels
{
    internal class AboutViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            this.Version = this.GetType().Assembly.GetName().Version.ToString();
        }

        public string Title { get; set; }

        public string Version { get; }
    }
}