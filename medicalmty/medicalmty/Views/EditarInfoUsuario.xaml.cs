using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using Firebase.Auth;
using medicalmty.Models;
using medicalmty.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
namespace medicalmty.Views
{
    public partial class EditarInfoUsuario : ContentPage
	{
        string txtEmail = "";
        string txtKey = "";
        string txtId = "";
        string p_img = "";
        MediaFile file;
        
        public EditarInfoUsuario (string p_email)
		{
			InitializeComponent ();
            consultarUsuario(p_email);
            txtEmail = p_email;

        }

        async void consultarUsuario(string p_email)
        {
            UsuarioViewModel metodo = new UsuarioViewModel();
            var dt = await metodo.GetUsuario(p_email);

            foreach (var usuario in dt)
            {
                txtKey = usuario.userKey;
                txtId = usuario.idUsuario;
                p_img = usuario.imagenPerfil;
                imgFile.Source = usuario.imagenPerfil;
                txtNombre.Text = usuario.nombreUsuario;
                txtApellidoP.Text = usuario.apellidoP;
                txtApellidoM.Text = usuario.apellidoM;
            }
        }

        public async void subirImagen(System.Object sender, System.EventArgs e)
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
            }
            catch
            {


            }
        }

        public async void Btn_ActualizarInfo(System.Object sender, System.EventArgs e)
        {
            UsuarioViewModel metodo = new UsuarioViewModel();
            UsuarioModel user = new UsuarioModel();

            user.idUsuario = txtId;
            user.userKey = txtKey;
            user.email = txtEmail;
            user.nombreUsuario = txtNombre.Text;
            user.apellidoP = txtApellidoP.Text;
            user.apellidoM = txtApellidoM.Text;
            user.imagenPerfil = p_img;
            try
            {
                if (file != null)
                {
                    string image = await metodo.UploadImg(file.GetStream(), Path.GetFileName(file.Path));
                    user.imagenPerfil = image;

                }


                bool isActualizar = await metodo.ActualizarInfoUsuario(user);

                if (isActualizar)
                {
                    await DisplayAlert("Actualizado", "Los datos fueron actualizados con exito!", "Aceptar");
                    // await Navigation.PopModalAsync();
                    //await Navigation.PopAsync();
                    await Navigation.PushAsync(new ProfilePage(user.email));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Aceptar");
            }
            
        }

        
    }
}

