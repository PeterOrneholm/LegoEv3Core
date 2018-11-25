using System;
using System.Text;
using System.Threading.Tasks;

namespace LegoEv3Core
{
	/// <summary>
	/// Direct commands for the EV3 brick
	/// </summary>
	public sealed class DirectCommand
	{
		private readonly Brick _brick;

		internal DirectCommand(Brick brick)
		{
			_brick = brick;
		}

		/// <summary>
		/// Turn the motor connected to the specified port or ports at the specified power.
		/// </summary>
		/// <param name="ports">A specific port or Ports.All.</param>
		/// <param name="power">The power at which to turn the motor (-100 to 100).</param>
		/// <returns></returns>
		public async Task TurnMotorAtPowerAsync(OutputPort ports, int power)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.TurnMotorAtPower(ports, power);
		    c.StartMotor(ports);
		    await _brick.SendCommandAsyncInternal(c);
		}

		/// <summary>
		/// Turn the specified motor at the specified speed.
		/// </summary>
		/// <param name="ports">Port or ports to apply the command to.</param>
		/// <param name="speed">The speed to apply to the specified motors (-100 to 100).</param>
		public async Task TurnMotorAtSpeedAsync(OutputPort ports, int speed)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.TurnMotorAtSpeed(ports, speed);
		    c.StartMotor(ports);
		    await _brick.SendCommandAsyncInternal(c);
		}

		/// <summary>
		/// Step the motor connected to the specified port or ports at the specified power for the specified number of steps.
		/// </summary>
		/// <param name="ports">A specific port or Ports.All.</param>
		/// <param name="power">The power at which to turn the motor (-100 to 100).</param>
		/// <param name="steps"></param>
		/// <param name="brake">Apply brake to motor at end of routine.</param>
		public async Task StepMotorAtPowerAsync(OutputPort ports, int power, uint steps, bool brake)
		{
		    await StepMotorAtPowerAsync(ports, power, 0, steps, 0, brake);
		}

		/// <summary>
		/// Step the motor connected to the specified port or ports at the specified power for the specified number of steps.
		/// </summary>
		/// <param name="ports">A specific port or Ports.All.</param>
		/// <param name="power">The power at which to turn the motor (-100 to 100).</param>
		/// <param name="rampUpSteps"></param>
		/// <param name="constantSteps"></param>
		/// <param name="rampDownSteps"></param>
		/// <param name="brake">Apply brake to motor at end of routine.</param>
		public async Task StepMotorAtPowerAsync(OutputPort ports, int power, uint rampUpSteps, uint constantSteps, uint rampDownSteps, bool brake)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.StepMotorAtPower(ports, power, rampUpSteps, constantSteps, rampDownSteps, brake);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Step the motor connected to the specified port or ports at the specified speed for the specified number of steps.
        /// </summary>
        /// <param name="ports">A specific port or Ports.All.</param>
        /// <param name="speed">The speed at which to turn the motor (-100 to 100).</param>
        /// <param name="steps"></param>
        /// <param name="brake">Apply brake to motor at end of routine.</param>
        public async Task StepMotorAtSpeedAsync(OutputPort ports, int speed, uint steps, bool brake)
		{
			await StepMotorAtSpeedAsync(ports, speed, 0, steps, 0, brake);
		}

        /// <summary>
        /// Step the motor connected to the specified port or ports at the specified speed for the specified number of steps.
        /// </summary>
        /// <param name="ports">A specific port or Ports.All.</param>
        /// <param name="speed">The speed at which to turn the motor (-100 to 100).</param>
        /// <param name="rampUpSteps"></param>
        /// <param name="constantSteps"></param>
        /// <param name="rampDownSteps"></param>
        /// <param name="brake">Apply brake to motor at end of routine.</param>
        public async Task StepMotorAtSpeedAsync(OutputPort ports, int speed, uint rampUpSteps, uint constantSteps, uint rampDownSteps, bool brake)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.StepMotorAtSpeed(ports, speed, rampUpSteps, constantSteps, rampDownSteps, brake);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Turn the motor connected to the specified port or ports at the specified power for the specified times.
        /// </summary>
        /// <param name="ports">A specific port or Ports.All.</param>
        /// <param name="power">The power at which to turn the motor (-100 to 100).</param>
        /// <param name="milliseconds">Number of milliseconds to run at constant power.</param>
        /// <param name="brake">Apply brake to motor at end of routine.</param>
        /// <returns></returns>
        public async Task TurnMotorAtPowerForTimeAsync(OutputPort ports, int power, uint milliseconds, bool brake)
		{
			await TurnMotorAtPowerForTimeAsync(ports, power, 0, milliseconds, 0, brake);
		}

        /// <summary>
        /// Turn the motor connected to the specified port or ports at the specified power for the specified times.
        /// </summary>
        /// <param name="ports">A specific port or Ports.All.</param>
        /// <param name="power">The power at which to turn the motor (-100 to 100).</param>
        /// <param name="msRampUp">Number of milliseconds to get up to power.</param>
        /// <param name="msConstant">Number of milliseconds to run at constant power.</param>
        /// <param name="msRampDown">Number of milliseconds to power down to a stop.</param>
        /// <param name="brake">Apply brake to motor at end of routine.</param>
        /// <returns></returns>
        public async Task TurnMotorAtPowerForTimeAsync(OutputPort ports, int power, uint msRampUp, uint msConstant, uint msRampDown, bool brake)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.TurnMotorAtPowerForTime(ports, power, msRampUp, msConstant, msRampDown, brake);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Turn the motor connected to the specified port or ports at the specified speed for the specified times.
        /// </summary>
        /// <param name="ports">A specific port or Ports.All.</param>
        /// <param name="speed">The power at which to turn the motor (-100 to 100).</param>
        /// <param name="milliseconds">Number of milliseconds to run at constant speed.</param>
        /// <param name="brake">Apply brake to motor at end of routine.</param>
        /// <returns></returns>
        public async Task TurnMotorAtSpeedForTimeAsync(OutputPort ports, int speed, uint milliseconds, bool brake)
		{
			await TurnMotorAtSpeedForTimeAsync(ports, speed, 0, milliseconds, 0, brake);
		}

        /// <summary>
        /// Turn the motor connected to the specified port or ports at the specified speed for the specified times.
        /// </summary>
        /// <param name="ports">A specific port or Ports.All.</param>
        /// <param name="speed">The power at which to turn the motor (-100 to 100).</param>
        /// <param name="msRampUp">Number of milliseconds to get up to speed.</param>
        /// <param name="msConstant">Number of milliseconds to run at constant speed.</param>
        /// <param name="msRampDown">Number of milliseconds to slow down to a stop.</param>
        /// <param name="brake">Apply brake to motor at end of routine.</param>
        /// <returns></returns>
        public async Task TurnMotorAtSpeedForTimeAsync(OutputPort ports, int speed, uint msRampUp, uint msConstant, uint msRampDown, bool brake)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.TurnMotorAtSpeedForTime(ports, speed, msRampUp, msConstant, msRampDown, brake);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Set the polarity (direction) of a motor.
        /// </summary>
        /// <param name="ports">Port or ports to change polarity</param>
        /// <param name="polarity">The new polarity (direction) value</param>
        /// <returns></returns>
        public async Task SetMotorPolarityAsync(OutputPort ports, Polarity polarity)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.SetMotorPolarity(ports, polarity);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Start motors on the specified ports.
        /// </summary>
        /// <param name="ports">The port or ports to which the stop command will be sent.</param>
        /// <returns></returns>
        public async Task StartMotorAsync(OutputPort ports)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.StartMotor(ports);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Synchronize stepping of motors.
        /// </summary>
        /// <param name="ports">The port or ports to which the stop command will be sent.</param>
        /// <param name="speed">Speed to turn the motor(s).</param>
        /// <param name="turnRatio">The turn ratio to apply.</param>
        /// <param name="step">The number of steps to turn the motor(s).</param>
        /// <param name="brake">Brake or coast at the end.</param>
        /// <returns></returns>
        public async Task StepMotorSyncAsync(OutputPort ports, int speed, short turnRatio, uint step, bool brake)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.StepMotorSync(ports, speed, turnRatio, step, brake);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Synchronize timing of motors.
        /// </summary>
        /// <param name="ports">The port or ports to which the stop command will be sent.</param>
        /// <param name="speed">Speed to turn the motor(s).</param>
        /// <param name="turnRatio">The turn ratio to apply.</param>
        /// <param name="time">The time to turn the motor(s).</param>
        /// <param name="brake">Brake or coast at the end.</param>
        /// <returns></returns>
        public async Task TimeMotorSyncAsync(OutputPort ports, int speed, short turnRatio, uint time, bool brake)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.TimeMotorSync(ports, speed, turnRatio, time, brake);
		    await _brick.SendCommandAsyncInternal(c);
		}


        /// <summary>
        /// Stops motors on the specified ports.
        /// </summary>
        /// <param name="ports">The port or ports to which the stop command will be sent.</param>
        /// <param name="brake">Apply brake to motor at end of routine.</param>
        /// <returns></returns>
        public async Task StopMotorAsync(OutputPort ports, bool brake)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.StopMotor(ports, brake);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Resets all ports and devices to defaults.
        /// </summary>
        /// <returns></returns>
        public async Task ClearAllDevicesAsync()
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.ClearAllDevices();
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Clears changes on specified port
        /// </summary>
        ///	<param name="port">The port to clear</param>
        /// <returns></returns>
        public async Task ClearChangesAsync(InputPort port)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.ClearChanges(port);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Plays a tone of the specified frequency for the specified time.
        /// </summary>
        /// <param name="volume">Volume of tone (0-100).</param>
        /// <param name="frequency">Frequency of tone, in hertz.</param>
        /// <param name="duration">Duration to play tone, in milliseconds.</param>
        /// <returns></returns>
        public async Task PlayToneAsync(int volume, ushort frequency, ushort duration)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.PlayTone(volume, frequency, duration);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Play a sound file stored on the EV3 brick
        /// </summary>
        /// <param name="volume">Volume of the sound (0-100)</param>
        /// <param name="filename">Filename of sound stored on brick, without the .RSF extension</param>
        /// <returns></returns>
        public async Task PlaySoundAsync(int volume, string filename)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.PlaySound(volume, filename);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Return the current version number of the firmware running on the EV3 brick.
        /// </summary>
        /// <returns>Current firmware version.</returns>
        public async Task<string> GetFirmwareVersionAsync()
		{
		    var c = new Command(CommandType.DirectReply, 0x10, 0);
		    c.GetFirwmareVersion(0x10, 0);
		    await _brick.SendCommandAsyncInternal(c);
		    if (c.Response.Data == null)
		        return null;

		    var index = Array.IndexOf(c.Response.Data, (byte)0);
		    return Encoding.UTF8.GetString(c.Response.Data, 0, index);
		}

        /// <summary>
        /// Returns whether the specified BrickButton is pressed
        /// </summary>
        /// <param name="button">Button on the face of the EV3 brick</param>
        /// <returns>Whether or not the button is pressed</returns>
        public async Task<bool> IsBrickButtonPressedAsync(BrickButton button)
		{
		    var c = new Command(CommandType.DirectReply, 1, 0);
		    c.IsBrickButtonPressed(button, 0);
		    await _brick.SendCommandAsyncInternal(c);
		    return false; //TODO: Read value?
		}

		/// <summary>
		/// Set EV3 brick LED pattern
		/// </summary>
		/// <param name="ledPattern">Pattern to display on LED</param>
		/// <returns></returns>
		public async Task SetLedPatternAsync(LedPattern ledPattern)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.SetLedPattern(ledPattern);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Draw a line on the EV3 LCD screen
        /// </summary>
        /// <param name="color">Color of the line</param>
        /// <param name="x0">X start</param>
        /// <param name="y0">Y start</param>
        /// <param name="x1">X end</param>
        /// <param name="y1">Y end</param>
        /// <returns></returns>
        public async Task DrawLineAsync(Color color, ushort x0, ushort y0, ushort x1, ushort y1)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.DrawLine(color, x0, y0, x1, y1);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Draw a single pixel
        /// </summary>
        /// <param name="color">Color of the pixel</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <returns></returns>
        public async Task DrawPixelAsync(Color color, ushort x, ushort y)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.DrawPixel(color, x, y);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Draw a rectangle
        /// </summary>
        /// <param name="color">Color of the rectangle</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="width">Width of rectangle</param>
        /// <param name="height">Height of rectangle</param>
        /// <param name="filled">Filled or empty</param>
        /// <returns></returns>
        public async Task DrawRectangleAsync(Color color, ushort x, ushort y, ushort width, ushort height, bool filled)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.DrawRectangle(color, x, y, width, height, filled);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Draw a filled rectangle, inverting the pixels underneath it
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        /// <returns></returns>
        public async Task DrawInverseRectangleAsync(ushort x, ushort y, ushort width, ushort height)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.DrawInverseRectangle(x, y, width, height);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Draw a circle
        /// </summary>
        /// <param name="color">Color of the circle</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="radius">Radius of the circle</param>
        /// <param name="filled">Filled or empty</param>
        /// <returns></returns>
        public async Task DrawCircleAsync(Color color, ushort x, ushort y, ushort radius, bool filled)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.DrawCircle(color, x, y, radius, filled);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Write a string to the screen
        /// </summary>
        /// <param name="color">Color of the text</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="text">Text to draw</param>
        /// <returns></returns>
        public async Task DrawTextAsync(Color color, ushort x, ushort y, string text)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.DrawText(color, x, y, text);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Draw a dotted line
        /// </summary>
        /// <param name="color">Color of dotted line</param>
        /// <param name="x0">X start</param>
        /// <param name="y0">Y start</param>
        /// <param name="x1">X end</param>
        /// <param name="y1">Y end</param>
        /// <param name="onPixels">Number of pixels the line is drawn</param>
        /// <param name="offPixels">Number of pixels the line is empty</param>
        /// <returns></returns>
        public async Task DrawDottedLineAsync(Color color, ushort x0, ushort y0, ushort x1, ushort y1, ushort onPixels, ushort offPixels)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.DrawDottedLine(color, x0, y0, x1, y1, onPixels, offPixels);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Fills the width of the screen between the provided Y coordinates
        /// </summary>
        /// <param name="color">Color of the fill</param>
        /// <param name="y0">Y start</param>
        /// <param name="y1">Y end</param>
        /// <returns></returns>
        public async Task DrawFillWindowAsync(Color color, ushort y0, ushort y1)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.DrawFillWindow(color, y0, y1);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Draw an image to the LCD screen
        /// </summary>
        /// <param name="color">Color of the image pixels</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="devicePath">Path to the image on the EV3 brick</param>
        /// <returns></returns>
        public async Task DrawImageAsync(Color color, ushort x, ushort y, string devicePath)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.DrawImage(color, x, y, devicePath);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Enable or disable the top status bar
        /// </summary>
        /// <param name="enabled">Enabled or disabled</param>
        /// <returns></returns>
        public async Task EnableTopLineAsync(bool enabled)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.EnableTopLine(enabled);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Select the font for text drawing
        /// </summary>
        /// <param name="fontType">Type of font to use</param>
        /// <returns></returns>
        public async Task SelectFontAsync(FontType fontType)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.SelectFont(fontType);
		    await _brick.SendCommandAsyncInternal(c);
		}

        /// <summary>
        /// Clear the entire screen
        /// </summary>
        /// <returns></returns>
        public async Task CleanUiAsync()
        {
            var c = new Command(CommandType.DirectNoReply);
            c.CleanUi();
            await _brick.SendCommandAsyncInternal(c);
        }

        /// <summary>
        /// Refresh the EV3 LCD screen
        /// </summary>
        /// <returns></returns>
        public async Task UpdateUIAsync()
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.UpdateUI();
		    await _brick.SendCommandAsyncInternal(c);
		}

		/// <summary>
		/// Get the type and mode of the device attached to the specified port
		/// </summary>
		/// <param name="port">The input port to query</param>
		/// <returns>2 bytes, index 0 being the type, index 1 being the mode</returns>
		public async Task<byte[]> GetTypeModeAsync(InputPort port)
		{
		    var c = new Command(CommandType.DirectReply, 2, 0);
		    c.GetTypeMode(port, 0, 1);
		    await _brick.SendCommandAsyncInternal(c);
		    return c.Response.Data;
		}

		/// <summary>
		/// Read the SI value from the specified port in the specified mode
		/// </summary>
		/// <param name="port">The port to query</param>
		/// <param name="mode">The mode used to read the data</param>
		/// <returns>The SI value</returns>
		public async Task<float> ReadySIAsync(InputPort port, int mode)
		{
		    var c = new Command(CommandType.DirectReply, 4, 0);
		    c.ReadySI(port, mode, 0);
		    await _brick.SendCommandAsyncInternal(c);
		    return BitConverter.ToSingle(c.Response.Data, 0);
		}

		/// <summary>
		/// Read the raw value from the specified port in the specified mode
		/// </summary>
		/// <param name="port">The port to query</param>
		/// <param name="mode">The mode used to read the data</param>
		/// <returns>The Raw value</returns>
		public async Task<int> ReadyRawAsync(InputPort port, int mode)
		{
		    var c = new Command(CommandType.DirectReply, 4, 0);
		    c.ReadyRaw(port, mode, 0);
		    await _brick.SendCommandAsyncInternal(c);
		    return BitConverter.ToInt32(c.Response.Data, 0);
		}

		/// <summary>
		/// Read the percent value from the specified port in the specified mode
		/// </summary>
		/// <param name="port">The port to query</param>
		/// <param name="mode">The mode used to read the data</param>
		/// <returns>The percentage value</returns>
		public async Task<int> ReadyPercentAsync(InputPort port, int mode)
		{
		    var c = new Command(CommandType.DirectReply, 1, 0);
		    c.ReadyRaw(port, mode, 0);
		    await _brick.SendCommandAsyncInternal(c);
		    return c.Response.Data[0];
		}

		/// <summary>
		/// Get the name of the device attached to the specified port
		/// </summary>
		/// <param name="port">Port to query</param>
		/// <returns>The name of the device</returns>
		public async Task<string> GetDeviceNameAsync(InputPort port)
		{
		    var c = new Command(CommandType.DirectReply, 0x7f, 0);
		    c.GetDeviceName(port, 0x7f, 0);
		    await _brick.SendCommandAsyncInternal(c);
		    var index = Array.IndexOf(c.Response.Data, (byte)0);
		    return Encoding.UTF8.GetString(c.Response.Data, 0, index);
		}

		/// <summary>
		/// Get the mode of the device attached to the specified port
		/// </summary>
		/// <param name="port">Port to query</param>
		/// <param name="mode">Mode of the name to get</param>
		/// <returns>The name of the mode</returns>
		public async Task<string> GetModeNameAsync(InputPort port, int mode)
		{
		    var c = new Command(CommandType.DirectReply, 0x7f, 0);
		    c.GetModeName(port, mode, 0x7f, 0);
		    await _brick.SendCommandAsyncInternal(c);
		    var index = Array.IndexOf(c.Response.Data, (byte)0);
		    return Encoding.UTF8.GetString(c.Response.Data, 0, index);
		}

        /// <summary>
        /// Wait for the specificed output port(s) to be ready for the next command
        /// </summary>
        /// <param name="ports">Port(s) to wait for</param>
        /// <returns></returns>
        public async Task OutputReadyAsync(OutputPort ports)
		{
		    var c = new Command(CommandType.DirectNoReply);
		    c.OutputReady(ports);
		    await _brick.SendCommandAsyncInternal(c);
		}
	}
}
