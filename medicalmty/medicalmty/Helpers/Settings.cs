using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace medicalmty.Helpers
{
	public static class Settings
	{
		private const string _email = "Email";
		private static readonly string _settingsDefault = string.Empty;

		private static ISettings AppSettings => CrossSettings.Current;

		public static string Email
		{
			get => AppSettings.GetValueOrDefault(_email, _settingsDefault);
			set => AppSettings.AddOrUpdateValue(_email, value);
		}


        private const string _evento = "EventoS";
        public static string EventoS
        {
            get => AppSettings.GetValueOrDefault(_evento, _settingsDefault);
            set => AppSettings.AddOrUpdateValue(_evento, value);
        }

        private const string _tarjeta = "Tarjeta";
        public static string Tarjeta
        {
            get => AppSettings.GetValueOrDefault(_tarjeta, _settingsDefault);
            set => AppSettings.AddOrUpdateValue(_tarjeta, value);
        }

        private const string _boleto = "Boleto";
        public static string Boleto
        {
            get => AppSettings.GetValueOrDefault(_boleto, _settingsDefault);
            set => AppSettings.AddOrUpdateValue(_boleto, value);
        }
    }
}

