using System;
namespace medicalmty.Models
{
	public class BoletoModel
	{
		public string idBoleto { get; set; }
        public string GUID { get; set; }
        public object evento { get; set; }
        public string email { get; set; }
        public string cantBoletos { get; set; }
        public string asientos { get; set; }
        public string precio { get; set; }
    }
}

