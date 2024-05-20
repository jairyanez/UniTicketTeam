using System;
using Firebase.Auth;
using medicalmty.Models;
using medicalmty.Servicios;
using System.Threading.Tasks;
using Firebase.Database.Query;
using System.Collections.Generic;
using medicalmty.Helpers;
using Newtonsoft.Json;

namespace medicalmty.ViewModels
{
	public class TarjetaViewModel
	{
        List<TarjetaModel> ListaTarjeta = new List<TarjetaModel>();
        string idTarjeta1;


        public async Task<string> RegistrarTarjeta(TarjetaModel p_tarjeta)
        {
            var data = await ConexionFirebase.firebase
                .Child("Tarjeta")
                .PostAsync(new TarjetaModel()
                {
                    email = p_tarjeta.email,
                    idTarjeta = idTarjeta1,
                    nombrePersona = p_tarjeta.nombrePersona,
                    numeroTarjeta = p_tarjeta.numeroTarjeta,
                    fechaExpiracion = p_tarjeta.fechaExpiracion,
                    CVV = p_tarjeta.CVV
                });
            idTarjeta1 = data.Key;
            return idTarjeta1;


        }

        public async Task<List<TarjetaModel>> GetTarjeta(string p_email)
        {
            var card = await ConexionFirebase.firebase
                .Child("Tarjeta")
                .OnceAsync<TarjetaModel>();

            foreach (var c in card)
            {
                if (c.Object.email == p_email)
                {
                    TarjetaModel tarjetaN = new TarjetaModel();
                    tarjetaN.idTarjeta = c.Key;
                    tarjetaN.email = c.Object.email;
                    tarjetaN.nombrePersona = c.Object.nombrePersona;
                    tarjetaN.numeroTarjeta = c.Object.numeroTarjeta;
                    tarjetaN.fechaExpiracion = c.Object.fechaExpiracion;
                    tarjetaN.CVV = c.Object.CVV;

                    ListaTarjeta.Add(tarjetaN);
                }
                
            }

            return ListaTarjeta;
        }

        public async Task<bool> ActualizarInfoTarjeta(TarjetaModel card)
        {
            await ConexionFirebase.firebase
                .Child("Tarjeta/" + card.idTarjeta)
                .PutAsync(JsonConvert.SerializeObject(card));

            return true;
        }

        public async Task<bool> EliminarTarjeta(TarjetaModel card)
        {
            await ConexionFirebase.firebase
                .Child("Tarjeta/" + card.idTarjeta)
                .DeleteAsync();

            return true;
        }


    }


}

