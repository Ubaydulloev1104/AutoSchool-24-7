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
            try
            {
            await Navigation.PushAsync(new Menu());

            }
            catch (Exception ex)
            {

                DisplayAlert("Ошибка",$"Error: {ex}", "OK"); 
            }
        }

    }

}
