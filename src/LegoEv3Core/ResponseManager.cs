﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LegoEv3Core
{
	internal static class ResponseManager
	{
		private static ushort _nextSequence = 0x0001;
		internal static readonly Dictionary<int, Response> Responses = new Dictionary<int, Response>();

		private static ushort GetSequenceNumber()
		{
			if(_nextSequence == ushort.MaxValue)
				_nextSequence++;

			return _nextSequence++;
		}

		public static Response CreateResponse()
		{
			var sequence = GetSequenceNumber();

		    var r = new Response(sequence);
			Responses[sequence] = r;
			return r;
		}

	    public static async Task WaitForResponseAsync(Response r)
		{
			await Task.Run(() =>
			{
				if(r.Event.WaitOne(1000))
					Responses.Remove(r.Sequence);
				else
					r.ReplyType = ReplyType.DirectReplyError;
			});
		}

	    public static void HandleResponse(byte[] report)
		{
			if (report == null || report.Length < 3)
				return;

			var sequence = (ushort) (report[0] | (report[1] << 8));
		    var replyType = report[2];

		    if (sequence <= 0)
		    {
		        return;
		    }

		    var r = Responses[sequence];

		    if (Enum.IsDefined(typeof (ReplyType), replyType))
		        r.ReplyType = (ReplyType) replyType;

		    if (r.ReplyType == ReplyType.DirectReply || r.ReplyType == ReplyType.DirectReplyError)
		    {
		        r.Data = new byte[report.Length - 3];
		        Array.Copy(report, 3, r.Data, 0, report.Length - 3);
		    }
		    else if (r.ReplyType == ReplyType.SystemReply || r.ReplyType == ReplyType.SystemReplyError)
		    {
		        if (Enum.IsDefined(typeof (SystemOpcode), (int) report[3]))
		            r.SystemCommand = (SystemOpcode) report[3];

		        if (Enum.IsDefined(typeof (SystemReplyStatus), (int) report[4]))
		            r.SystemReplyStatus = (SystemReplyStatus) report[4];

		        r.Data = new byte[report.Length - 5];
		        Array.Copy(report, 5, r.Data, 0, report.Length - 5);
		    }

		    r.Event.Set();
		}
	}
}
