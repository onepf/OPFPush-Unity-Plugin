namespace OnePF.OPFPush
{
	public class PushError
	{
		public string AvailabilityErrorCode { get; set; }

		public string Type { get; set; }

		public string OriginalError { get; set; }

		public PushError(string availabilityErrorCode, string type, string originalError) {
			this.AvailabilityErrorCode = availabilityErrorCode;
			this.Type = type;
			this.OriginalError = originalError;
		}
	}
}
