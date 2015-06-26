namespace BrandsHatched.CircuitBreaker.Configuration
{
	public interface IConfig
	{
		string this[string key] { get; }
		string this[SettingValue value] { get; }
		bool Is(SettingValue value, bool defaultValue = false);
		bool Is(string value, bool defaultValue = false);
		T Get<T>(SettingValue value, T defaultValue);
	}
}