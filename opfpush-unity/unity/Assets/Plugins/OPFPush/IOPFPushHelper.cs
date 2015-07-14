namespace OnePF.OPFPush
{
	/// <summary>
	/// The helper class to manage push providers.
	/// 
	/// Use <c>register()</c> to start a registration and <c>unregister()</c> for start an unregistration.
	/// The registration and unregistration operations are asynchronous. You can handle results of these operations
	/// via adding functions which handles <c>OPFPush</c> events.
	/// 
	/// </summary>
	public interface IOPFPushHelper
	{
		/// <summary>
		/// If the <c>IOPFPushHelper</c> is unregistered, it chooses a push provider with the highest priority and starts the registration. 
		/// Does nothing in another case.
		/// The registration result can be handled via adding functions which handles <c>OPFPush.OnRegistered</c> event.
		/// 
		/// The priority of providers corresponds to the order in which they was added to the opfpush_config.json.
		/// If you set <c>true</c> as the value of <c>selectSystemPreferred</c> field, the system push provider will get the highest priority.
		/// </summary>
		void Register ();

		/// <summary>
		/// If the <c>IOPFPushHelper</c> is registered or registration is in process, starts the asynchronous unregistration of the current push provider. 
		/// Does nothing if the <c>IOPFPushHelper</c> has already been unregistered.
		/// 
		/// You should rarely (if ever) need to call this method. Not only is it expensive in terms of resources,
		/// but it invalidates your registration ID, which you should never change unnecessarily.
		/// A better approach is to simply have your server stop sending messages.
		/// Only use unregister if you want to change your sender ID.
		/// </summary>
		void Unregister ();

		/// <summary>
		/// Returns the registration ID if there's the registered push provider, null otherwise.
		/// </summary>
		/// <returns>The registration ID if there's the registered push provider, null otherwise.</returns>
		string GetRegistrationId ();

		/// <summary>
		/// Returns the current provider name if there's the registered push provider, null otherwise.
		/// </summary>
		/// <returns>The current provider name if there's the registered push provider, null otherwise.</returns>
		string GetProviderName ();

		/// <summary>
		/// Returns <c>true</c> if the <c>IOPFPushHelper</c> is registered, false otherwise.
		/// </summary>
		/// <returns><c>true</c> if <c>IOPFPushHelper</c> is registered; otherwise, <c>false</c>.</returns>
		bool IsRegistered ();

		/// <summary>
		/// Returns <c>true</c> if the registration operation is being performed at the moment.
		/// </summary>
		/// <returns><c>true</c> if the registration operation is being performed at the moment; otherwise, <c>false</c>.</returns>
		bool IsRegistering ();
	}
}