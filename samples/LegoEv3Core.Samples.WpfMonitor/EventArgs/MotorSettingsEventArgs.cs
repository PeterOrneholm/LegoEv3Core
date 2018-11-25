using SampleApp.Enums;

namespace SampleApp.EventArgs
{
	public class MotorSettingsEventArgs : System.EventArgs
	{
		public MotorMovementTypes MotorMovementType { get; set; }
		public int DegreeMovement { get; set; }
		public int TimeToMoveInSeconds { get; set; }
		public int PowerRatingMovement { get; set; }
	}
}
