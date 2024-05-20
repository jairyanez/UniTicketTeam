using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Firebase.Auth;
using medicalmty.Models;
using medicalmty.ViewModels;
using medicalmty.Views;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace medicalmty
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreacionCuenta : ContentPage
    {
        string WebAPIKey = "AIzaSyBOzPHZf6mmS9fsj2FYBYHif8ynNoRqdBw";
        MediaFile file;
        public CreacionCuenta()
        {
            InitializeComponent();
        }

        async private void RegisterUser(object sender, EventArgs e)
        {
            
            string p_email = txtCorreo.Text;
            string p_pass = txtPassword.Text;

            if (String.IsNullOrEmpty(p_email))
            {
                await DisplayAlert("Alerta", "El Campo de Email esta vacío", "Volver");
                return;
            }
            
            if (String.IsNullOrEmpty(p_pass))
            {
                await DisplayAlert("Alerta", "El Campo de Contraseña esta vacío", "Volver");
                return;
            }

            if (p_pass.Length <6)
            {
                await DisplayAlert("Error", "La Contraseña debe ser mayor a 6 Caracteres", "Volver");
                return;
            }

            if (String.IsNullOrEmpty(txtNombre.Text))
            {
                await DisplayAlert("Alerta", "El Campo de Nombre esta vacío", "Volver");
                return;
            }

            if (String.IsNullOrEmpty(txtApellidoP.Text))
            {
                await DisplayAlert("Alerta", "El Campo de Apellido Paterno esta vacío", "Volver");
                return;
            }

            if (String.IsNullOrEmpty(txtApellidoM.Text))
            {
                await DisplayAlert("Alerta", "El Campo de Apellido Materno esta vacío", "Volver");
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                await DisplayAlert("Error", "Ambas contraseñas debe ser iguales", "Volver");
                return;
            }

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIKey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(p_email, p_pass);
                string gettoken = auth.FirebaseToken;

                await App.Current.MainPage.DisplayAlert("¡Registro Exitoso!", "Tu Usuario se ha registrado correctamente", "Continuar");

                await guardarUsuario();

                UserDialogs.Instance.ShowLoading("Iniciando Sesión");
                await Task.Delay(3000);
                UserDialogs.Instance.HideLoading();

                await Application.Current.MainPage.Navigation.PushAsync(new AdentroAplicacion(p_email));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("EMAIL_EXISTS"))
                {
                    await App.Current.MainPage.DisplayAlert("Este email ya existe", p_email + " ya esta registrado, porfavor inicia sesión","Aceptar");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alerta", ex.Message, "OK");
                }
            }

        }

        private async Task guardarUsuario()
        {
            UsuarioModel userModel = new UsuarioModel();
            UsuarioViewModel metodo = new UsuarioViewModel();
            Random aleatorio = new Random();

            userModel.nombreUsuario = txtNombre.Text;
            userModel.email = txtCorreo.Text;
            userModel.apellidoP = txtApellidoP.Text;
            userModel.apellidoM = txtApellidoM.Text;
            userModel.idUsuario = aleatorio.Next(99999).ToString();
            userModel.imagenPerfil = "iconPerfil.png";
            if (file != null)
            {
                string image = await metodo.UploadImg(file.GetStream(), Path.GetFileName(file.Path));
                userModel.imagenPerfil = image;

            }

            await metodo.RegistrarUsuario(userModel);

            

        }



        private async void subirImagen(System.Object sender, System.EventArgs e)
        {

            await CrossMedia.Current.Initialize();

            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file == null)
                {
                    return;
                }

                imgFile.Source = ImageSource.FromStream(() =>
                {
                    return file.GetStream();
                });
                imgFile.IsVisible = true;
                imgFile.WidthRequest = 120;
                imgFile.HeightRequest = 120;
            }
            catch
            {


            }
        }

    }
}