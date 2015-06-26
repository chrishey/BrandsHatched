using System;
using System.Configuration;

namespace BrandsHatched.CircuitBreaker.Configuration
{
	public class Config : IConfig
	{
		public string this[SettingValue value]
		{
			get
			{
				return this[value.ToString()];
			}
		}

		public string this[string key]
		{
			get { return ConfigurationManager.AppSettings[key]; }
		}

		public bool Is(SettingValue value, bool defaultValue = false)
		{
			bool result;
			return Boolean.TryParse(this[value], out result)
				? result
				: defaultValue;
		}

		public bool Is(string value, bool defaultValue = false)
		{
			SettingValue settingValue;

			if (!Enum.TryParse(value, false, out settingValue))
			{
				return defaultValue;
			}

			return Is(settingValue, defaultValue);
		}


		public T Get<T>(SettingValue value, T defaultValue)
		{
			var text = ConfigurationManager.AppSettings[value.ToString()];

			if (string.IsNullOrEmpty(text))
				return defaultValue;

			try
			{
				return (T)Convert.ChangeType(text, typeof(T));
			}
			catch (FormatException fe)
			{
				throw new FormatException("App Setting '" + value + "' cannot be converted to a " + typeof(T), fe);
			}
		}
	}
}
