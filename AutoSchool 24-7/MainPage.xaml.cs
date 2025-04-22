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
        private async void OnLogoLoaded(object sender, EventArgs e)
        {
            LogoImage.Opacity = 0;
            LogoImage.Scale = 0.5;

            // Анимация появления логотипа
            await LogoImage.FadeTo(1, 600, Easing.CubicIn);
            await LogoImage.ScaleTo(1, 500, Easing.SpringOut);
        }
    }

}
