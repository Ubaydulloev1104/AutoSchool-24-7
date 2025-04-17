namespace AutoSchool_24_7
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
           

            MainPage = new NavigationPage(new MainPage());
        }
    }
}
