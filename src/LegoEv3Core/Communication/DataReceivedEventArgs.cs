﻿using System;

namespace LegoEv3Core.Communication
{
	/// <summary>
	/// Event arguments for the ReportReceived event.
	/// </summary>
	public sealed class ReportReceivedEventArgs : EventArgs
	{
		/// <summary>
		/// Byte array of the data received from the EV3 brick.
		/// </summary>
		public byte[] Report { get; set; }
	}
}