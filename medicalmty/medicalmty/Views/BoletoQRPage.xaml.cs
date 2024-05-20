using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using medicalmty.Helpers;
using medicalmty.Models;
using medicalmty.ViewModels;
using Newtonsoft.Json;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace medicalmty.Views
{	
	public partial class BoletoQRPage : ContentPage
	{
		string p_email = "";
        EventoModel eventoS;
        BoletoModel p_boleto;


        public BoletoQRPage ()
		{
			InitializeComponent ();
            p_email = JsonConvert.DeserializeObject<string>(Settings.Email);
            eventoS = JsonConvert.DeserializeObject<EventoModel>(Settings.EventoS);
            p_boleto = JsonConvert.DeserializeObject<BoletoModel>(Settings.Boleto);

            // QRboleto();

            QRcode.Source = $"https://api.qrserver.com/v1/create-qr-code/?data={p_boleto.GUID}&size=200x200";

            if (eventoS.tipo != "Partido")
            {
                txtasientos.IsVisible = false;
                txtasientosCant.IsVisible = false;

            }

            txtlugar.Text = eventoS.lugar;
            txtevento.Text = eventoS.nombreEvento;
            txtfecha.Text = eventoS.fechaEvento;
            txthora.Text = eventoS.hora;
            txtCantidadBoletos.Text = p_boleto.cantBoletos.ToString();

        }

        private void QRboleto()
        {
            var guid = System.Guid.NewGuid().ToString();
            
            ZXingBarcodeImageView qr;

            qr = new ZXingBarcodeImageView // SE CREA LA IMAGEN DEL QR
            {
                HorizontalOptions = LayoutOptions.FillAndExpand, //POSICIÓN DE LA IMAGEN 
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            qr.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE; // ESPECIFICACIÓN DEL CODIGO, HAY DE BARRA, QR Y DE VARIOS MAS
            qr.BarcodeOptions.Width = 400; // ANCHO Y LARGO DEL QR
            qr.BarcodeOptions.Height = 400;
            qr.BarcodeValue = guid; // CONTENIDO DEL QR

            //stakQR.Children.Add(qr); // PARA QUE APAREZCA EL QR, SE AGREGA AL STACKLAYOUT. "stakQR" vendría siendo como el identificador del Stacklayout y con el add lo agregamos y así aparecera el qr
        }



        

        private void btnVolverIncio(System.Object sender, System.EventArgs e)
        {
            Navigation.RemovePage(this);
            Navigation.PushAsync(new AdentroAplicacion(p_email));
            //Navigation.PushAsync(new EventoPage(eventoS, p_email));
        }
    }
}

