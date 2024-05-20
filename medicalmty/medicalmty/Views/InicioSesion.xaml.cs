using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Acr.UserDialogs;
using Firebase.Auth;
using medicalmty.Helpers;
using medicalmty.Models;
using medicalmty.ViewModels;
using medicalmty.Views;
using Newtonsoft.Json;
using Plugin.Toast;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace medicalmty
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InicioSesion : ContentPage
    {
        string WebAPIKey = "AIzaSyBOzPHZf6mmS9fsj2FYBYHif8ynNoRqdBw";

        public InicioSesion()
        {
            InitializeComponent();

        }            
        async void EnviarDatos_LogIn(object sender, System.EventArgs e)
        {
            UsuarioModel userModel = new UsuarioModel();
            UsuarioViewModel metodo = new UsuarioViewModel();

            userModel.email = txtCorreoCelular.Text;
            string p_pass = txtpassword.Text;
            
            var dt = await metodo.GetUsuario(userModel.email);
            string p_name = "";

            foreach (var usuario in dt)
            {
                p_name = usuario.nombreUsuario;
            }
            
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIKey));

            try
            {
                
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(userModel.email, p_pass);

                
                var content = await auth.GetFreshAuthAsync();

                UserDialogs.Instance.ShowLoading("Iniciando Sesión");
                await Task.Delay(6000);
                UserDialogs.Instance.HideLoading();
                

                Settings.Email = JsonConvert.SerializeObject(userModel.email);

                await Application.Current.MainPage.Navigation.PushAsync(new AdentroAplicacion(userModel.email));
                
            }
            catch (Exception ex)
            {
                CrossToastPopUp.Current.ShowToastError("El usuario o contraseña incorrectos");
                //await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Volver");
                // await App.Current.MainPage.DisplayAlert("Error", "Usuario o Contraseña Incorrectos", "Volver");
            }


        }

        private async void OlvideMiPass(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new OlvideMiContrasena());

        }

        private void Registrar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreacionCuenta());
        }

        private async Task mostrarUser()
        {
            UsuarioViewModel metodo = new UsuarioViewModel();
            var dt = await metodo.GetAllUsuarios();

        }



    }
}