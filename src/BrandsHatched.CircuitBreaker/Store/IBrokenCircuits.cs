using System.Collections.Generic;

namespace BrandsHatched.CircuitBreaker.Store
{
	public interface IBrokenCircuits
	{
		void AddBrokenCircuit(string key, object value);
		void RemoveBrokenCircuit(string key);
		bool CircuitBrokenForUser(string key);
	}

	public class BrokenCircuits : IBrokenCircuits
	{
		private readonly Dictionary<string, object> _brokenCircuits;

		public BrokenCircuits()
		{
			_brokenCircuits = new Dictionary<string, object>();
		}

		public void AddBrokenCircuit(string key, object value)
		{
			_brokenCircuits.Add(key, value);
		}

		public void RemoveBrokenCircuit(string key)
		{
			_brokenCircuits.Remove(key);
		}

		public bool CircuitBrokenForUser(string key)
		{
			return _brokenCircuits.ContainsKey(key);
		}
	}
}