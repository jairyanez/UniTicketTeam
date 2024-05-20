using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Firebase.Auth;
using medicalmty.Helpers;
using medicalmty.Models;
using medicalmty.Servicios;
using medicalmty.ViewModels;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace medicalmty.Views
{	
	public partial class HistorialBoletosPage : ContentPage
	{
        //public Task<ObservableCollection<BoletoModel>> BoletosDB { get; set; }
        public ObservableCollection<BoletoModel> BoletosDB { get; set; }

        public BoletoModel BoletoSeleccionado { get; set; }

        BoletoViewModel metodoB = new BoletoViewModel();


        string p_email = "";
        EventoModel eventoM;

		public HistorialBoletosPage ()
		{
            InitializeComponent();
            ObtenerBoletos();
            this.BindingContext = this;

            eventoM = JsonConvert.DeserializeObject<EventoModel>(Settings.EventoS);
            p_email = JsonConvert.DeserializeObject<string>(Settings.Email);
        }


        public async void ObtenerBoletos()
        {
            var tickets = await metodoB.ObtenerTodo();
            tickets[0].precio = $"${tickets[0].precio}";
            var ev = tickets[0].evento;
            BoletosList.ItemsSource = tickets;
        }

        private async void GetBoletos()
        {

            BoletoViewModel metodo = new BoletoViewModel();
            var dt = await metodo.GetAllBoletos(p_email);

            ObservableCollection<BoletoModel> listaBoletos = new ObservableCollection<BoletoModel>();

            foreach (var bol in dt)
            {
                listaBoletos.Add(new BoletoModel()
                {
                    idBoleto = bol.idBoleto,
                    email = bol.email,
                    GUID = bol.GUID,
                    cantBoletos = bol.cantBoletos,
                    asientos = bol.asientos,
                    evento = bol.evento,
                    precio = bol.precio
                });

            }

            BoletosList.ItemsSource = listaBoletos;
            
        }

        private void volverHomepage(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage(p_email));
        }

        private void C_BoletoSelected(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Settings.Boleto = JsonConvert.SerializeObject(BoletoSeleccionado);

                this.Navigation.PushAsync(new BoletoQRPage());

            }
        }
    }
}

