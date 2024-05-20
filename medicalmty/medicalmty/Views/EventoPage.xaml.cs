using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using medicalmty.Helpers;
using medicalmty.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace medicalmty.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventoPage : TabbedPage
    {
        string p_email_1 = "";
       
        public EventoPage(EventoModel evento, string p_email)
        {
            InitializeComponent();
            EventoSeleccionado = evento;
            this.BindingContext = this;
            var pet = JsonConvert.DeserializeObject<EventoModel>(Settings.EventoS);
            p_email_1 = p_email;

        }


        public EventoModel EventoSeleccionado { get; set; }

        private void volverHomepage(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AdentroAplicacion(p_email_1));
        }

    }

}
