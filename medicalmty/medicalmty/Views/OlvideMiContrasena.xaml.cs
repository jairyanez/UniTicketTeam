using System;
using System.Collections.Generic;
using Firebase.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace medicalmty
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OlvideMiContrasena : ContentPage
	{
        string WebAPIKey = "AIzaSyBOzPHZf6mmS9fsj2FYBYHif8ynNoRqdBw";
        public OlvideMiContrasena ()
		{
			InitializeComponent ();
		}


        private async void Reset_Password(System.Object sender, System.EventArgs e)
        {

            string p_email = txtCorreo.Text;
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIKey));
            try
            {
                await authProvider.SendPasswordResetEmailAsync(p_email);

                await App.Current.MainPage.DisplayAlert("Solicitud Enviada", "Se ha enviado un Link de restablecimiento de la contraseña. Porfavor, entra a tu correo y restablecela", "Aceptar");

                await Navigation.PushAsync(new InicioSesion());

                //await Application.Current.MainPage.Navigation.PushAsync(new InicioSesion());

            }
            catch (Exception ex)
            {
                //await DisplayAlert("¡Error!", "El usuario no existe o no es correcto", "Intentalo de nuevo");
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Volver");
            }
        }
    }
}

