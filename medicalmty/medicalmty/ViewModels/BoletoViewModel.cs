using System;
using medicalmty.Models;
using medicalmty.Servicios;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Firebase.Database.Query;
using medicalmty.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace medicalmty.ViewModels
{
	public class BoletoViewModel
	{
        string idboleto = "";
        public async Task<string> RegistrarBoleto(BoletoModel p_boleto)
        {
            BoletoModel boleto = new BoletoModel()
            {
                email = p_boleto.email,
                evento = p_boleto.evento,
                asientos = p_boleto.asientos,
                cantBoletos = p_boleto.cantBoletos,
                GUID = p_boleto.GUID,
                precio = p_boleto.precio

            };

            var data = await ConexionFirebase.firebase
                .Child("Boleto")
                .PostAsync(boleto);
            boleto.idBoleto = data.Key;

            await ConexionFirebase.firebase
                .Child("Boleto/" + data.Key)
                .PutAsync(boleto);

            idboleto = data.Key;
            return idboleto;


        }

        List<BoletoModel> BoletosList = new List<BoletoModel>();
        
        public async Task<List<BoletoModel>> GetAllBoletos(string p_email)
        {

            p_email = JsonConvert.DeserializeObject<string>(Settings.Email);


            var boleto = await ConexionFirebase.firebase
                .Child("Boleto")
                .OnceAsync<BoletoModel>();

            foreach (var b in boleto)
            {

                if (b.Object.email == p_email)
                {
                    BoletoModel boletos = new BoletoModel();
                    boletos.idBoleto = b.Object.idBoleto;
                    boletos.cantBoletos = b.Object.cantBoletos;
                    boletos.asientos = b.Object.asientos;
                    boletos.GUID = b.Object.GUID;
                    boletos.email = b.Object.email;
                    boletos.evento = b.Object.precio;
                    boletos.precio = b.Object.precio;

                    BoletosList.Add(boletos);

                }

            }

            return BoletosList;
        }





        public async Task<List<BoletoModel>> ObtenerTodo()
        {
            return (await ConexionFirebase.firebase.Child("Boleto").OnceAsync<BoletoModel>()).Select(item => new BoletoModel
            {
                idBoleto = item.Key,
                cantBoletos = item.Object.cantBoletos,
                GUID = item.Object.GUID,
                email = item.Object.email,
                evento = item.Object.evento,
                precio = item.Object.precio,
                asientos = item.Object.asientos

            }).ToList();


        }
    }
}

