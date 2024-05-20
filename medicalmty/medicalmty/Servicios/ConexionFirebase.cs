using System;
using Firebase.Database;
using Firebase.Storage;

namespace medicalmty.Servicios
{
	public class ConexionFirebase
	{
		public static FirebaseClient firebase = new FirebaseClient("https://ticketfyapp-default-rtdb.firebaseio.com/");

		public static FirebaseStorage firebaseStorage = new FirebaseStorage("ticketfyapp.appspot.com");

        
    }
}

