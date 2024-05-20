using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using medicalmty.Models;
using medicalmty.ViewModels;
using medicalmty.Views;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace medicalmty.Views
{	
	public partial class ProfilePage : ContentPage
	{
        MediaFile file;
		public ProfilePage (string p_email)
		{
			InitializeComponent ();
            consultarUsuario(p_email);

            
        }

        private void editarInfo(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new EditarInfoUsuario(txtEmail.Text));
        }

        private void volverHomepage(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AdentroAplicacion(txtEmail.Text));
        }

        async void consultarUsuario(string p_email)
		{
            UsuarioViewModel metodo = new UsuarioViewModel();   
            var dt = await metodo.GetUsuario(p_email);

            foreach (var usuario in dt)
            {
                // imagenPerfil.Source = usuario.imagenPerfil;
                imagenPerfil.Source = usuario.imagenPerfil;
                txtNombre.Text = $"{usuario.nombreUsuario} {usuario.apellidoP} {usuario.apellidoM}";
                txtEmail.Text = usuario.email;
                txtId.Text = "ID: " + usuario.userKey;
            }
        }


        public async void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            UsuarioViewModel metodo = new UsuarioViewModel();
            UsuarioModel user = new UsuarioModel();

            await CrossMedia.Current.Initialize();

            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize=PhotoSize.Medium
                });
                if (file==null)
                {
                    return;
                }
                
                imagenPerfil.Source = ImageSource.FromStream(() =>
                {
                    return file.GetStream();
                });
            }
            catch
            {


            }
            if (file != null)
            {
                string image = await metodo.UploadImg(file.GetStream(), Path.GetFileName(file.Path));
                user.imagenPerfil = image;
                
            }

            bool isActualizar = await metodo.ActualizarInfoUsuario(user);

            //Navigation.PushModalAsync(new startPage());
        }

        private async void CerrarSesion(System.Object sender, System.EventArgs e)
        {
            try
            {
                bool session = await DisplayAlert("Cerrar Sesión", "¿Estas seguro que deseas salir?", "Si", "No");

                if (session)
                {
                    var existingPages = Navigation.NavigationStack ;
                   
                    foreach (var page in existingPages.ToList())
                    {
                        if (existingPages.ToList().Count() <= 1)
                        {
                            await Navigation.PushAsync(new startPage());
                            
                        }
                        else
                        {
                            Navigation.RemovePage(page);
                        }


                    }
                    
            }   }

            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Aceptar");
            }
            
        }

        private void EditarMetodoPago(System.Object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new EditarMetodoPago());

        }

        private void historialBoletos(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new HistorialBoletosPage());
        }

    }
}

