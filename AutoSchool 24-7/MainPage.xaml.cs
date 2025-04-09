namespace AutoSchool_24_7
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnGoToMenuClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Menu());
        }

    }

}
