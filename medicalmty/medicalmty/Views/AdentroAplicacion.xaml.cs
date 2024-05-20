using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using medicalmty.Models;
using medicalmty.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using medicalmty.Servicios;
using medicalmty.Helpers;
using Newtonsoft.Json;

namespace medicalmty.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdentroAplicacion : ContentPage
    {
        public ObservableCollection<EventoModel> EventosDB { get; set; }

        string p_email_1 = "";
        string p_name = "";

        

        public AdentroAplicacion(string p_email)
        {
            InitializeComponent();
            EventosDB = GetEventos();
            BindingContext = this;

            consultarUsuario(p_email);
            Settings.Email = JsonConvert.SerializeObject(p_email);

            consultarTarjeta(p_email);
            p_email_1 = p_email;

        }

        public EventoModel EventoSeleccionado { get; set; }


        private ObservableCollection<EventoModel>GetEventos()
        {

            return new ObservableCollection<EventoModel>
            {
                new EventoModel
                { idEvento = "48328",
                    nombreEvento = "Tigres vs Rayados",
                    descripcion = "Clasico Regio 136, Los Tigres de la UANL son uno de los equipos más ganadores del futbol mexicano, y actualmente cuentan con ocho títulos de Liga, tres Campeón de Campeones, tres Copas MX, una Liga de Campeones de la Concacaf y ha sido finalista del Mundial de Clubes y de la Copa Libertadores.",
                    img = "clasico1.png",
                    imgDesc = "https://pbs.twimg.com/media/D1gqy2NWkAA4prE.jpg",
                    fechaEvento = new DateTime(2024,05,08).ToString("dd/MM/yyyy"),
                    hora = "19:00",
                    tipo = "Evento",
                    concat_evento = "Partido | Tigres vs Rayados",
                    lugar = "Estadio Universitario UANL"

                },
                new EventoModel
                { idEvento = "493284",
                    nombreEvento = "Saiyan Fest 2024",
                    descripcion = "Saiyan Fest es el primer evento enfocado en el anime, manga y en los fans de Dragón Ball. Es presentado por AkibaraXpress. Se realizará en mayo en Pabellón",
                    img = "https://www.klandmexico.com/wp-content/uploads/2023/05/FuWie1-XoAUg7De.jpg",
                    imgDesc = "https://www.klandmexico.com/wp-content/uploads/2023/05/BANNER-PRINCIPAL-1-1024x576.png",
                    fechaEvento = new DateTime(2024,10,10).ToString("dd/MM/yyyy"),
                    hora = "13:00",
                    tipo = "Exposición",
                    precio = "140",
                    concat_evento = "Exposición | Saiyan Fest 2024",
                    lugar = "Nave Lewis - Parque Fundidora"

                },
                new EventoModel
                { idEvento = "654323",
                    nombreEvento = "ExpoFest Pokemon",
                    descripcion = "¿Eres fanático del anime, Pokémon o de la galaxia muy, muy lejana? ¡Entonces esta feria temática es para ti! Te invitamos a la Expofest Pokémon",
                    img = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQiE-qOAo6zLtQkyeyEj8VSpYMrid96tEDSWwyCESkgzKhWGEB7",
                    imgDesc = "https://www.planetagaming.com/wp-content/uploads/2018/10/pokemon-go-video-games-pikachu-pok%C3%A9mon-wallpaper.jpg",
                    fechaEvento = new DateTime(2025,02,15).ToString("dd/MM/yyyy"),
                    hora = "11:00",
                    tipo = "Exposición",
                    precio = "100",
                    concat_evento = "Exposición | ExpoFest Pokemon",
                    lugar = "Cintermex"

                },
                new EventoModel
                { idEvento = "123923",
                    nombreEvento = "Ferxxo Monterrey Woo",
                    descripcion = "FEID ANUNCIA SU GIRA EN LATINOAMÉRICA\n\n¡SE ACERCA EL FERXXOCALIPSOS TOUR 2024!\n\nFeid no pasa desapercibido. Con más de 17.6 billones de reproducciones y alcanzando el #6 como artista global en Spotify, su música resuena entre el público de todo el mundo. Éxitos como “LUNA” el cual está en el top 10 global, y “PERRO NEGRO” (con Bad Bunny) han acumulado cientos de millones de reproducciones, consolidando su estatus como un artista que encabeza las listas. ",
                    img = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRY_2H1dsnKs22NPMFurkje5tQ_nFS7UDYovvN5Fqup9_H_gdn3",
                    imgDesc = "https://cloudfront-us-east-1.images.arcpublishing.com/elespectador/UFN65WNF3COEKXDWPHRBQQQPFE.jpg",
                    fechaEvento = new DateTime(2024,11,04).ToString("dd/MM/yyyy"),
                    hora = "20:00",
                    tipo = "Concierto",
                    precio = "800",
                    concat_evento = "Concierto | Ferxxo Monterrey Woo",
                    lugar = "Arena Monterrey"

                },
                new EventoModel
                { idEvento = "23415",
                    nombreEvento = "Dale Mix",
                    descripcion = "Una colección de artistas que evidencian al presencia de reguetón, trap, rap e incluso corridos tumbados. El Dale Mixx 2024 se llevará a cabo el próximo 17 de agosto, en el Parque Fundidora de Monterrey.",
                    img = "https://media.ticketmaster.com/tm/en-us/dam/a/5a1/8a2b2555-f152-4fa6-8ef0-9713f6bc25a1_CUSTOM.jpg",
                    imgDesc = "https://cdn.ntmx.me/media/2024/03/05/_hde6a60c3b5a65250847316b81e48d9aff1cd07ab3.webp",
                    fechaEvento = new DateTime(2024,08,17).ToString("dd/MM/yyyy"),
                    hora = "16:00",
                    tipo = "Evento",
                    precio = "400",
                    concat_evento = "Evento | Dale Mix",
                    lugar = "General - Parque Fundidora"

                }
            };

        }
        

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AdentroAplicacion(p_name));
        }

        private void ImageButton_Clicked_6(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage(p_email_1));
        }

        async void consultarUsuario(string p_email)
        {
            UsuarioViewModel metodo = new UsuarioViewModel();
            var dt = await metodo.GetUsuario(p_email);

            foreach (var usuario in dt)
            {
                imgFile.Source = usuario.imagenPerfil;
                
            }
        }


        async void consultarTarjeta(string p_email)
        {
            TarjetaViewModel metodo = new TarjetaViewModel();
            var dt = await metodo.GetTarjeta(p_email);
            var tarjeta = JsonConvert.DeserializeObject<TarjetaModel>(Settings.Tarjeta);
            
            if (dt.Count > 0)
            {
                //uli.Text = "Existe Tarjeta";
                foreach (var card in dt)
                {
                    tarjeta.email = card.email;
                    tarjeta.nombrePersona = card.nombrePersona;
                    tarjeta.numeroTarjeta = card.numeroTarjeta;
                    tarjeta.fechaExpiracion = card.fechaExpiracion;
                    tarjeta.CVV = card.CVV;
                    tarjeta.idTarjeta = card.idTarjeta;
                    
                }

                Settings.Tarjeta = JsonConvert.SerializeObject(tarjeta);
            }
        }

        async Task<ObservableCollection<EventoModel>> consultarEventos()
        {
            EventoViewModel metodo = new EventoViewModel();
            var dt = await metodo.GetEventosFirebase();

            ObservableCollection<EventoModel> ListaEventos = new ObservableCollection<EventoModel>();

            foreach (var evento in dt)
            {
                ListaEventos.Add(new EventoModel
                {
                    idEvento = evento.idEvento,
                    nombreEvento = evento.nombreEvento,
                    descripcion = evento.descripcion,
                    img = evento.img,
                    //fechaEvento = evento.fechaEvento,
                    hora = evento.hora,
                    tipo = evento.tipo,
                    concat_evento = $"{evento.tipo} | {evento.nombreEvento}",
                });
            }

            return ListaEventos;
        }


        private void C_EventoSelected(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Settings.EventoS = JsonConvert.SerializeObject(EventoSeleccionado);

                this.Navigation.PushAsync(new EventoPage(EventoSeleccionado, p_email_1));

            }
        }





    }
}