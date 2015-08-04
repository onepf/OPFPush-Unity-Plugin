using UnityEngine;

namespace OnePF.OPFPush
{
	public class StubOPFPushHelper : IOPFPushHelper
	{
		StubOPFPushHelper _pushHelper_object;

		public void Register ()
		{
			Debug.Log("Register stub");
		}

		public void Unregister ()
		{
			Debug.Log("Unregister stub");
		}

		public string GetRegistrationId ()
		{
			Debug.Log("GetRegistrationId stub");
			return null;		
		}

		public string GetProviderName ()
		{
			Debug.Log("GetProviderName stub");
			return null;
		}

		public bool IsRegistered ()
		{
			Debug.Log("IsRegistered stub");
			return false;
		}

		public bool IsRegistering ()
		{
			Debug.Log("IsRegistering stub");
			return false;
		}
	}
}