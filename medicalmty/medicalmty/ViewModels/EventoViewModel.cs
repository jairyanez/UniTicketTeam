using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using medicalmty.Models;
using medicalmty.Servicios;

namespace medicalmty.ViewModels
{
	public class EventoViewModel
	{

        List<EventoModel> ListaEventos = new List<EventoModel>();
        
        public async Task<List<EventoModel>> GetEventosFirebase()
        {
            var evento = await ConexionFirebase.firebase
                .Child("Evento")
                .OnceAsync<EventoModel>();

            foreach (var ev in evento)
            {
                EventoModel eventos = new EventoModel();
                eventos.eventKey = ev.Key;
                eventos.idEvento = ev.Object.idEvento;
                eventos.nombreEvento = ev.Object.nombreEvento;
                eventos.descripcion = ev.Object.descripcion;
                //eventos.fechaEvento = ev.Object.fechaEvento;
                eventos.hora = ev.Object.hora;
                eventos.img = ev.Object.img;
                eventos.tipo = ev.Object.tipo;

                ListaEventos.Add(eventos);

            }

            return ListaEventos;
        }


        public ObservableCollection<ZonaModel> ObtenerZonas()
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









    }
}

