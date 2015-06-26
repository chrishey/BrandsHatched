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
		private Dictionary<string, object> _brokenCircuits;

		public BrokenCircuits()
		{
			_brokenCircuits = new Dictionary<string, object>();
		}

		public void AddBrokenCircuit(string key, object value)
		{
			throw new System.NotImplementedException();
		}

		public void RemoveBrokenCircuit(string key)
		{
			throw new System.NotImplementedException();
		}

		public bool CircuitBrokenForUser(string key)
		{
			throw new System.NotImplementedException();
		}
	}
}