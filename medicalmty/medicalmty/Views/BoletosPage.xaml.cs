using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using medicalmty.Helpers;
using medicalmty.Models;
using medicalmty.ViewModels;
using medicalmty.Views;
using Newtonsoft.Json;
using Xamarin.Forms;
namespace medicalmty.Views
{	
	public partial class BoletosPage : ContentPage
	{
        public ObservableCollection<ZonaModel> ZonasList { get; set; }


        public BoletosPage()
        {
            InitializeComponent();
        
            var pet = JsonConvert.DeserializeObject<EventoModel>(Settings.EventoS);
            precioStr.Text = $"Precio: ${pet.precio}";
            if (pet.tipo == "Partido")
            {
                ZonasList = ObtenerZonas();
                TablaZonas.IsVisible = true;
                
                
            }
            else
            {
                BotonCantBoletos.IsVisible = true;
            }
            BindingContext = this;
            
        }

        public ZonaModel ZonaSeleccionada { get; set; }

        
        private ObservableCollection<ZonaModel> ObtenerZonas()
        {
            return new ObservableCollection<ZonaModel>
            {
                new ZonaModel{ idZona = "1", nombreZona = "SOL9B", precio = 320, preciostr = "$320" },
                new ZonaModel{ idZona = "2", nombreZona = "SOL6B", precio = 320, preciostr = "$320" },
                new ZonaModel{ idZona = "3", nombreZona = "GRANUM", precio = 490, preciostr = "$490" },
                new ZonaModel{ idZona = "4", nombreZona = "GNTE5A", precio = 500, preciostr = "$500" },
                new ZonaModel{ idZona = "5", nombreZona = "PRE7A", precio = 630, preciostr = "$630" },
                new ZonaModel{ idZona = "6", nombreZona = "PRENUM", precio = 1050, preciostr = "$1050" },
                new ZonaModel{ idZona = "7", nombreZona = "VIPNTE", precio = 2660, preciostr = "$2660" },
                new ZonaModel{ idZona = "8", nombreZona = "VIPSUR", precio = 2660, preciostr = "$2660"  },
            };

        }
        
        private void C_EventoSelected(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                this.Navigation.PushAsync(new startPage());
            }
        }


        private void TapMenos_Tapped(object sender, EventArgs e)
        {
            int cantidad = Convert.ToInt32(txtCantidad.Text);
            cantidad -= 1;
            if (cantidad <= 0)
            {
                txtCantidad.Text = "0";
                BtnContinuar.BackgroundColor = Xamarin.Forms.Color.FromHex("DBDBDB");
                BtnContinuar.IsEnabled = false;

                
            }
            else
            {
                BtnContinuar.IsEnabled = true;
                BtnContinuar.BackgroundColor = Xamarin.Forms.Color.FromHex("FF8036");
                
                txtCantidad.Text = cantidad.ToString();
            }
            

        }

        private void TapMas_Tapped(object sender, EventArgs e)
        {
            int cantidad = Convert.ToInt32(txtCantidad.Text);
            
            BtnContinuar.IsEnabled = true;
            BtnContinuar.BackgroundColor = Xamarin.Forms.Color.FromHex("FF8036");
            cantidad += 1;
            txtCantidad.Text = cantidad.ToString();


        }

        private void Btn_Continuar(System.Object sender, System.EventArgs e)
        {
            int cantidad = Convert.ToInt32(txtCantidad.Text);
            
            Navigation.PushAsync(new ResumenPage(cantidad));
        }

    }
}

