namespace OnePF.OPFPush
{
	public interface IOPFPushHelper
	{
		void Register ();

		void Unregister ();

		string GetRegistrationId ();

		string GetProviderName ();

		bool IsRegistered ();

		bool IsRegistering ();
	}
}