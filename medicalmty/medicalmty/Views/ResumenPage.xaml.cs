using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using System.Threading.Tasks;
using medicalmty.Helpers;
using medicalmty.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using medicalmty.ViewModels;

namespace medicalmty.Views
{	
	public partial class ResumenPage : ContentPage
	{
        string p_email = "";
        int p_cantidadBoletos = 1;
        EventoModel eventoS;
        BoletoModel boletoCompra;
        public ResumenPage (int cantidadBoletos)
		{
			InitializeComponent ();
            p_email = JsonConvert.DeserializeObject<string>(Settings.Email);
            eventoS = JsonConvert.DeserializeObject<EventoModel>(Settings.EventoS);
            

            this.BindingContext = this;
            string s = "";
            p_cantidadBoletos = cantidadBoletos;
            

            int precioTotal = Convert.ToInt32(eventoS.precio) * cantidadBoletos;

            if (eventoS.tipo != "Partido")
            {
                txtasientos.IsVisible = false;
                txtasientosCant.IsVisible = false;

            }

            if (cantidadBoletos > 1)
            {
                s = "s";
            }

            Btn_comprar.Text = $"Comprar {cantidadBoletos} Boleto{s} | ${precioTotal}";

            txtlugar.Text = eventoS.lugar;
            txtevento.Text = eventoS.nombreEvento;
            txtfecha.Text = eventoS.fechaEvento;
            txthora.Text = eventoS.hora;
            txtCantidadBoletos.Text = cantidadBoletos.ToString();

            txtPrecioTotalCant.Text = $"${precioTotal}";

            boletoCompra = new BoletoModel()
            {
                email = p_email,
                evento = eventoS,
                cantBoletos = p_cantidadBoletos.ToString(),
                precio = precioTotal.ToString()
            };
        }

        private void Btn_volver(System.Object sender, System.EventArgs e)
        {
            Navigation.RemovePage(this);
            var email = JsonConvert.DeserializeObject<string>(Settings.Email);
            var eventoS = JsonConvert.DeserializeObject<EventoModel>(Settings.EventoS);

            Navigation.PushAsync(new EventoPage(eventoS,email));
        }

        private async void RealizarCompra(System.Object sender, System.EventArgs e)
        {
            var tarjeta = JsonConvert.DeserializeObject<TarjetaModel>(Settings.Tarjeta);

            if (tarjeta.email == p_email)
            {

                //var boletoCompra = JsonConvert.DeserializeObject<BoletoModel>(Settings.Boleto);
                UserDialogs.Instance.ShowLoading("Verificando Tarjeta");
                await Task.Delay(3000);

                RegistraBoleto();
                Settings.Boleto = JsonConvert.SerializeObject(boletoCompra);

                UserDialogs.Instance.ShowLoading("Realizando Compra");
                await Task.Delay(4000);
                UserDialogs.Instance.HideLoading();

                //await DisplayAlert("GUID QR",System.Guid.NewGuid().ToString(),"Aceptar");
                //await Navigation.PushAsync(new AdentroAplicacion(p_email));

                await Navigation.PushAsync(new BoletoQRPage());
            }
            else
            {
                await Navigation.PushAsync(new PagoPage());

            }
        }

        private async void RegistraBoleto()
        {
            string guid = System.Guid.NewGuid().ToString();
            BoletoViewModel metodo = new BoletoViewModel();

            boletoCompra.GUID = guid;

            await metodo.RegistrarBoleto(boletoCompra);
        }
    }
}

