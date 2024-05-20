using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using System.Threading.Tasks;
using Firebase.Auth;
using medicalmty.Helpers;
using medicalmty.Models;
using medicalmty.ViewModels;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;
using Plugin.Toast;

namespace medicalmty.Views
{	
	public partial class EditarMetodoPago : ContentPage
	{
        string p_email1 = "";
        string p_idTarjeta = "";
        public EditarMetodoPago ()
		{

			InitializeComponent ();

            var p_email = JsonConvert.DeserializeObject<string>(Settings.Email);
            consultarTarjeta(p_email);
            
            p_email1 = p_email;

            
        }


        private async void RegistrarDatosTarjeta(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtNumeroTarjeta.Text) || string.IsNullOrWhiteSpace(txtFechaMessExpiracion.Text) || string.IsNullOrWhiteSpace(txtFechaAñoExpiracion.Text) || string.IsNullOrWhiteSpace(txtCodigoCVV.Text) || string.IsNullOrWhiteSpace(txtNombrePersona.Text))
            {
                await DisplayAlert("Error", "Es necesario completar todos los campos", "Volver");
                return;
            }
            else if (Convert.ToInt32(txtFechaMessExpiracion.Text) > 12)
            {
                CrossToastPopUp.Current.ShowToastError("Mes de expiración No Valido");
            }

            else
            {
                TarjetaModel tarjeta = new TarjetaModel()
                {
                    numeroTarjeta = txtNumeroTarjeta.Text,
                    email = p_email1,
                    fechaExpiracion = string.Format("{0}/{1}", txtFechaMessExpiracion.Text, txtFechaAñoExpiracion.Text),
                    CVV = txtCodigoCVV.Text,
                    nombrePersona = txtNombrePersona.Text
                };

                try
                {
                    TarjetaViewModel tarjetaVM = new TarjetaViewModel();
                    var res = await tarjetaVM.RegistrarTarjeta(tarjeta);

                    tarjeta.idTarjeta = res;

                    await tarjetaVM.ActualizarInfoTarjeta(tarjeta);

                    Settings.Tarjeta = JsonConvert.SerializeObject(tarjeta);

                    UserDialogs.Instance.ShowLoading("Verificando");
                    await Task.Delay(3000);
                    UserDialogs.Instance.ShowLoading("Registrando Tarjeta");
                    await Task.Delay(3000);
                    UserDialogs.Instance.HideLoading();

                    if (!string.IsNullOrEmpty(res))
                    {
                        await DisplayAlert("Registro Exitoso", "La Tarjeta ha sido registrada y verificada correctamente", "Aceptar");

                        await Navigation.PopModalAsync();
                    }
                    else
                    {
                        await DisplayAlert("Mensaje", "A ocurrido un problema con el metodo de pago, vuelve a intentarlo", "Volver");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "Aceptar");
                }
            }

            


        }

        private async void ActualizarDatosTarjeta(System.Object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumeroTarjeta.Text) || string.IsNullOrWhiteSpace(txtFechaMessExpiracion.Text) || string.IsNullOrWhiteSpace(txtFechaAñoExpiracion.Text) || string.IsNullOrWhiteSpace(txtCodigoCVV.Text) || string.IsNullOrWhiteSpace(txtNombrePersona.Text))
            {
                await DisplayAlert("Error", "Es necesario completar todos los campos", "Volver");
                return;
            }
            else if (Convert.ToInt32(txtFechaMessExpiracion.Text) > 12)
            {
                CrossToastPopUp.Current.ShowToastError("Mes de expiración No Valido");
            }
            else
            {
                TarjetaViewModel metodo = new TarjetaViewModel();
                var tarjeta = JsonConvert.DeserializeObject<TarjetaModel>(Settings.Tarjeta);


                tarjeta.CVV = txtCodigoCVV.Text;
                tarjeta.fechaExpiracion = string.Format("{0}/{1}", txtFechaMessExpiracion.Text, txtFechaAñoExpiracion.Text);
                tarjeta.nombrePersona = txtNombrePersona.Text;
                tarjeta.numeroTarjeta = txtNumeroTarjeta.Text;


                Settings.Tarjeta = JsonConvert.SerializeObject(tarjeta);

                try
                {
                    bool isActualizar = await metodo.ActualizarInfoTarjeta(tarjeta);

                    if (isActualizar)
                    {
                        UserDialogs.Instance.ShowLoading("Actualizando Tarjeta");
                        await Task.Delay(2000);
                        UserDialogs.Instance.HideLoading();

                        await DisplayAlert("Actualizado", "Los datos de la tarjeta fueron actualizados con exito!", "Aceptar");


                        await Navigation.PopModalAsync();
                    }
                }

                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "Aceptar");
                }
            }

        }


        private void VolverAtras(System.Object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }


 
        async void consultarTarjeta(string p_email)
        {
            TarjetaViewModel metodo = new TarjetaViewModel();
            var tarjetaUser = JsonConvert.DeserializeObject<TarjetaModel>(Settings.Tarjeta);
            var dt = await metodo.GetTarjeta(p_email);

            if (dt.Count > 0)
            {

                txtNumeroTarjeta.Text = tarjetaUser.numeroTarjeta;
                txtNombrePersona.Text = tarjetaUser.nombrePersona;
                txtFechaMessExpiracion.Text = tarjetaUser.fechaExpiracion.Substring(0, 2);
                txtFechaAñoExpiracion.Text = tarjetaUser.fechaExpiracion.Substring(3);
                txtCodigoCVV.Text = tarjetaUser.CVV;

                btnActualizarTarjeta.Clicked += ActualizarDatosTarjeta;


            }
            else
            {
                btnActualizarTarjeta.Text = "Agregar Tarjeta";
                //btnCancelarCambios.Text = "Volver";
                btnActualizarTarjeta.Clicked += RegistrarDatosTarjeta;

                await DisplayAlert("Error", "No se encontró el metodo de pago, porfavor agregue uno", "Aceptar");
            }


        }

        private async void Btn_eliminarTarjeta(System.Object sender, System.EventArgs e)
        {
            TarjetaViewModel metodo = new TarjetaViewModel();
            var tarjeta = JsonConvert.DeserializeObject<TarjetaModel>(Settings.Tarjeta);
            try
            {
                bool opcion = await DisplayAlert("Borrar Tarjeta", "¿Estas seguro de eliminar esta tarjeta?", "Si", "No");

                if (opcion)
                {
                    var res = await metodo.EliminarTarjeta(tarjeta);
                    Settings.Tarjeta = JsonConvert.SerializeObject(new TarjetaModel());

                    if (res)
                    {
                        UserDialogs.Instance.ShowLoading("Eliminando Tarjeta");
                        await Task.Delay(1500);
                        UserDialogs.Instance.HideLoading();

                        await DisplayAlert("Tarjeta Eliminada", "Se ha elimando la tarjeta con exito", "Aceptar");
                        await Navigation.PopModalAsync();

                    }
                    else 
                    {
                        await DisplayAlert("Error", "La tarjeta no se ha podido elimanar","Volver");
                    }

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Volver");
            }
            
        }


        /*
         async void consultarTarjeta(string p_email)
        {
            TarjetaViewModel metodo = new TarjetaViewModel();
            var dt = await metodo.GetTarjeta(p_email);

            if (dt.Count > 0)
            {
                //uli.Text = "Existe Tarjeta";
                foreach (var card in dt)
                {
                    //var tarjeta = JsonConvert.DeserializeObject<TarjetaModel>(Settings.Tarjeta);
                    // uli.Text = tarjeta.email;

                    txtNumeroTarjeta.Text = card.numeroTarjeta;
                    txtNombrePersona.Text = card.nombrePersona;
                    txtFechaMessExpiracion.Text = card.fechaExpiracion.Substring(0, 2);
                    txtFechaAñoExpiracion.Text = card.fechaExpiracion.Substring(3);
                    txtCodigoCVV.Text = card.CVV;
                }

                //Button btnRegistrarTarjeta = new Button();

                btnActualizarTarjeta.Clicked += ActualizarDatosTarjeta;
                

            }
            else
            {
                //uli.Text = "No existen Tarjetas";
                btnActualizarTarjeta.Text = "Agregar Tarjeta";
                btnCancelarCambios.Text = "Volver";
                
                btnActualizarTarjeta.Clicked += RegistrarDatosTarjeta;
            }

        }
         */

    }
}

