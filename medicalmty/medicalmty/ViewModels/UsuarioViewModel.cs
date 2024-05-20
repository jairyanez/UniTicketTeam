using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database.Query;
using medicalmty.Models;
using medicalmty.Servicios;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;

namespace medicalmty.ViewModels
{
	public class UsuarioViewModel
	{
        string WebAPIKey = "AIzaSyBOzPHZf6mmS9fsj2FYBYHif8ynNoRqdBw";
        
        List<UsuarioModel> ListaUsuario = new List<UsuarioModel>();
		string idUser;
		public async Task<List<UsuarioModel>> GetAllUsuarios()
		{
			var usuario = await ConexionFirebase.firebase
				.Child("Usuario")
				.OnceAsync<UsuarioModel>();

			foreach(var user in usuario)
			{
				UsuarioModel usuarios = new UsuarioModel();
                usuarios.userKey = user.Key;
				usuarios.idUsuario = user.Object.idUsuario;
                usuarios.email = user.Object.email;
                usuarios.nombreUsuario = user.Object.nombreUsuario;
                usuarios.apellidoP = user.Object.apellidoP;
                usuarios.apellidoM = user.Object.apellidoM;
                usuarios.imagenPerfil = user.Object.imagenPerfil;

				ListaUsuario.Add(usuarios);

            }

			return ListaUsuario;
		}

        public async Task<List<UsuarioModel>> GetUsuario(string p_email)
        {
            var usuario = await ConexionFirebase.firebase
                .Child("Usuario")
                .OnceAsync<UsuarioModel>();

            foreach (var user in usuario)
            {
                if (user.Object.email == p_email)
                {
                    UsuarioModel usuarios = new UsuarioModel();
                    usuarios.userKey = user.Key;
                    usuarios.idUsuario = user.Object.idUsuario;
                    usuarios.email = user.Object.email;
                    usuarios.nombreUsuario = user.Object.nombreUsuario;
                    usuarios.apellidoP = user.Object.apellidoP;
                    usuarios.apellidoM = user.Object.apellidoM;
                    usuarios.imagenPerfil = user.Object.imagenPerfil;

                    ListaUsuario.Add(usuarios);
                }

            }

            return ListaUsuario;
        }



        public async Task<string> RegistrarUsuario(UsuarioModel parametro)
		{
			var data = await ConexionFirebase.firebase
                .Child("Usuario")
                .PostAsync(new UsuarioModel()
                {
                    idUsuario = parametro.idUsuario,
                    email = parametro.email,
                    nombreUsuario = parametro.nombreUsuario,
                    apellidoP = parametro.apellidoP,
                    apellidoM = parametro.apellidoM,
                    imagenPerfil = parametro.imagenPerfil

                });
            idUser = data.Key;
			return idUser;


        }

        public async Task<string> UploadImg(Stream img, string filename)
        {
            var image = await ConexionFirebase.firebaseStorage
                .Child("Images")
                .Child(filename)
                .PutAsync(img);
            return image;
        }

        public async Task<bool> ActualizarInfoUsuario(UsuarioModel usuario)
        {
            await ConexionFirebase.firebase
                .Child("Usuario/" + usuario.userKey)
                .PutAsync(JsonConvert.SerializeObject(usuario));

            return true;
        }


	}
}

