using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acr.UserDialogs;
using medicalmty.Helpers;
using medicalmty.Models;
using medicalmty.Servicios;
using medicalmty.ViewModels;
using Newtonsoft.Json;
using Plugin.Toast;
using Xamarin.Forms;

namespace medicalmty.Views
{	
	public partial class PagoPage : ContentPage
	{
        string p_email1 = "";
        public PagoPage ()
		{
			InitializeComponent ();
            var p_email = JsonConvert.DeserializeObject<string>(Settings.Email);
            p_email1 = p_email;
        }


        private async void ComprarConTarjeta(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNumeroTarjeta.Text) || string.IsNullOrWhiteSpace(txtFechaMessExpiracion.Text) || string.IsNullOrWhiteSpace(txtFechaAñoExpiracion.Text) || string.IsNullOrWhiteSpace(txtCodigoCVV.Text) || string.IsNullOrWhiteSpace(txtNombrePersona.Text))
                {
                    await DisplayAlert("Error", "Es necesario completar todos los campos", "Volver");
                    return;
                }else if (Convert.ToInt32(txtFechaMessExpiracion.Text) > 12)
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

            
                    TarjetaViewModel tarjetaVM = new TarjetaViewModel();
                    // var res = await tarjetaVM.RegistrarTarjeta(tarjeta);

                    // tarjeta.idTarjeta = res;

                    // await tarjetaVM.ActualizarInfoTarjeta(tarjeta);

                    //Settings.Tarjeta = JsonConvert.SerializeObject(tarjeta);

                    UserDialogs.Instance.ShowLoading("Verificando Tarjeta");
                    await Task.Delay(3000);
                    UserDialogs.Instance.ShowLoading("Realizando Compra");
                    await Task.Delay(4000);
                    UserDialogs.Instance.HideLoading();

                    await Navigation.PushAsync(new BoletoQRPage());
                }
                    //await DisplayAlert("GUID QR", System.Guid.NewGuid().ToString(), "Aceptar");
                    //await Navigation.PushAsync(new BoletoQRPage());

                

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Aceptar");
            }


        }

    }
}

