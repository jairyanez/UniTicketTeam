using System;
namespace medicalmty.Models
{
	public class TarjetaModel
	{
        public string idTarjeta { get; set; }
        public string email { get; set; }
        public string numeroTarjeta { get; set; }
        public string nombrePersona { get; set; }
        public string fechaExpiracion { get; set; }
        public string CVV { get; set; }
    }
}

