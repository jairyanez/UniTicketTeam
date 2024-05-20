using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace medicalmty
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class startPage : ContentPage
    {

        public startPage()
        {
            InitializeComponent();
        }

        private void RegistrarUsuario_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreacionCuenta()); //PushAsync empuja de manera asíncrona
        }

        private void InicioSesion_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InicioSesion());
        }
    }
}