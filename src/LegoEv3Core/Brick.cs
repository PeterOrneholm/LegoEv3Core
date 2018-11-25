﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LegoEv3Core.Communication;

namespace LegoEv3Core
{
	/// <summary>
	/// Main EV3 brick interface
	/// </summary>
	public sealed class Brick
	{
		/// <summary>
		/// Width of LCD screen
		/// </summary>
		public ushort LcdWidth => 178;

	    /// <summary>
		/// Height of LCD screen
		/// </summary>
		public ushort LcdHeight => 128;

	    /// <summary>
		/// Height of status bar
		/// </summary>
		public ushort TopLineHeight => 10;

	    private readonly SynchronizationContext _context = SynchronizationContext.Current;
		private readonly ICommunication _comm;
		private CancellationTokenSource _tokenSource;
		private readonly bool _alwaysSendEvents;

        /// <summary>
        /// Input and output ports on LEGO EV3 brick
        /// </summary>
        public IDictionary<InputPort,Port> Ports { get; set; }

		/// <summary>
		/// Buttons on the face of the LEGO EV3 brick
		/// </summary>
		public BrickButtons Buttons { get; set; }

        /// <summary>
        /// Send "direct commands" to the EV3 brick.  These commands are executed instantly and are not batched.
        /// </summary>
        public DirectCommand DirectCommand { get; }

        /// <summary>
        /// Send "system commands" to the EV3 brick.  These commands are executed instantly and are not batched.
        /// </summary>
        public SystemCommand SystemCommand { get; }

        /// <summary>
        /// Send a batch command of multiple direct commands at once.  Call the <see cref="Command.Initialize(LegoEv3Core.CommandType)"/> method with the proper <see cref="CommandType"/> to set the type of command the batch should be executed as.
        /// </summary>
        public Command BatchCommand { get; }

        /// <summary>
        /// Event that is fired when a port is changed
        /// </summary>
        public event EventHandler<BrickChangedEventArgs> BrickChanged;
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="comm">Object implementing the <see cref="ICommunication"/> interface for talking to the brick</param>
		public Brick(ICommunication comm) : this(comm, false) { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="comm">Object implementing the <see cref="ICommunication"/> interface for talking to the brick</param>
		/// <param name="alwaysSendEvents">Send events when data changes, or at every poll</param>
		public Brick(ICommunication comm, bool alwaysSendEvents)
		{
			DirectCommand = new DirectCommand(this);
			SystemCommand = new SystemCommand(this);
			BatchCommand = new Command(this);

			Buttons = new BrickButtons();

			_alwaysSendEvents = alwaysSendEvents;

			var index = 0;

			_comm = comm;
			_comm.ReportReceived += ReportReceived;

			Ports = new Dictionary<InputPort,Port>();

			foreach(InputPort i in Enum.GetValues(typeof(InputPort)))
			{
				Ports[i] = new Port
				{
					InputPort = i,
					Index = index++,
					Name = i.ToString(),
				};
			}
		}

		/// <summary>
		/// Connect to the EV3 brick.
		/// </summary>
		/// <returns></returns>
		public Task ConnectAsync()
		{
			return ConnectAsync(TimeSpan.FromMilliseconds(100));
		}

		/// <summary>
		/// Connect to the EV3 brick with a specified polling time.
		/// </summary>
		/// <param name="pollingTime">The period to poll the device status.  Set to TimeSpan.Zero to disable polling.</param>
		/// <returns></returns>
		public async Task ConnectAsync(TimeSpan pollingTime)
		{
		    _tokenSource = new CancellationTokenSource();

		    await _comm.ConnectAsync();

		    await DirectCommand.StopMotorAsync(OutputPort.All, false);

		    if (pollingTime != TimeSpan.Zero)
		    {
		        var t = Task.Factory.StartNew(async () =>
		        {
		            while (!_tokenSource.IsCancellationRequested)
		            {
		                await PollSensorsAsync();
		                await Task.Delay(pollingTime, _tokenSource.Token);
		            }

		            await DirectCommand.StopMotorAsync(OutputPort.All, false);
		        }, _tokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Current);
		    }
		}

		/// <summary>
		/// Disconnect from the EV3 brick
		/// </summary>
		public void Disconnect()
		{
		    _tokenSource?.Cancel();
		    _comm.Disconnect();
		}

		private void ReportReceived(object sender, ReportReceivedEventArgs e)
		{
			ResponseManager.HandleResponse(e.Report);
		}

		internal async Task SendCommandAsyncInternal(Command c)
		{
			await _comm.WriteAsync(c.ToBytes());
			if(c.CommandType == CommandType.DirectReply || c.CommandType == CommandType.SystemReply)
				await ResponseManager.WaitForResponseAsync(c.Response);
		}

		private async Task PollSensorsAsync()
		{
		    var changed = false;
			const int responseSize = 11;
		    var index = 0;

			var c = new Command(CommandType.DirectReply, (8 * responseSize) + 6, 0);

			foreach(InputPort i in Enum.GetValues(typeof(InputPort)))
			{
			    var p = Ports[i];
				index = p.Index * responseSize;

				c.GetTypeMode(p.InputPort, (byte)index, (byte)(index+1));
				c.ReadySI(p.InputPort, p.Mode, (byte)(index+2));
				c.ReadyRaw(p.InputPort, p.Mode, (byte)(index+6));
				c.ReadyPercent(p.InputPort, p.Mode, (byte)(index+10));
			}

			index += responseSize;

			c.IsBrickButtonPressed(BrickButton.Back,  (byte)(index+0));
			c.IsBrickButtonPressed(BrickButton.Left,  (byte)(index+1));
			c.IsBrickButtonPressed(BrickButton.Up,    (byte)(index+2));
			c.IsBrickButtonPressed(BrickButton.Right, (byte)(index+3));
			c.IsBrickButtonPressed(BrickButton.Down,  (byte)(index+4));
			c.IsBrickButtonPressed(BrickButton.Enter, (byte)(index+5));

			await SendCommandAsyncInternal(c);
			if(c.Response.Data == null)
				return;

			foreach(InputPort i in Enum.GetValues(typeof(InputPort)))
			{
			    var p = Ports[i];

				int type = c.Response.Data[(p.Index * responseSize)+0];
				byte mode = c.Response.Data[(p.Index * responseSize)+1];
				float siValue = BitConverter.ToSingle(c.Response.Data, (p.Index * responseSize)+2);
				int rawValue = BitConverter.ToInt32(c.Response.Data, (p.Index * responseSize)+6);
				byte percentValue = c.Response.Data[(p.Index * responseSize)+10];

				if((byte)p.Type != type || Math.Abs(p.SIValue - siValue) > 0.01f || p.RawValue != rawValue || p.PercentValue != percentValue)
					changed = true;

				if(Enum.IsDefined(typeof(DeviceType), type))
					p.Type = (DeviceType)type;
				else
					p.Type = DeviceType.Unknown;

				p.SIValue = siValue;
				p.RawValue = rawValue;
				p.PercentValue = percentValue;
			}

			if(	Buttons.Back  != (c.Response.Data[index+0] == 1) ||
				Buttons.Left  != (c.Response.Data[index+1] == 1) ||
				Buttons.Up    != (c.Response.Data[index+2] == 1) ||
				Buttons.Right != (c.Response.Data[index+3] == 1) ||
				Buttons.Down  != (c.Response.Data[index+4] == 1) ||
				Buttons.Enter != (c.Response.Data[index+5] == 1)
			)
				changed = true;

			Buttons.Back	= (c.Response.Data[index+0] == 1);
			Buttons.Left	= (c.Response.Data[index+1] == 1);
			Buttons.Up		= (c.Response.Data[index+2] == 1);
			Buttons.Right	= (c.Response.Data[index+3] == 1);
			Buttons.Down	= (c.Response.Data[index+4] == 1);
			Buttons.Enter	= (c.Response.Data[index+5] == 1);

			if(changed || _alwaysSendEvents)
				OnBrickChanged(new BrickChangedEventArgs { Ports = this.Ports, Buttons = this.Buttons });
		}

		private void OnBrickChanged(BrickChangedEventArgs e)
		{
			var handler = BrickChanged;
		    if (handler == null)
		    {
		        return;
		    }

		    if(_context == SynchronizationContext.Current)
		        handler(this, e);
		    else
		        _context.Post(delegate { handler(this, e); }, null);
		}
	}
}
