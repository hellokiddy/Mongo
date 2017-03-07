using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mongo.Common.Utility;

namespace Mongo.Common.Event
{
	public interface IEvent
	{
		/// <summary>
		/// Gets or sets the event ID.
		/// </summary>
		/// <value>The event ID.</value>
		int EventID{ get; set; }

		/// <summary>
		/// Gets or sets the delay.
		/// </summary>
		/// <value>The delay time.</value>
		float Delay{ get; set; }

		/// <summary>
		/// Gets or sets the data.
		/// </summary>
		/// <value>The event data.</value>
		Bundle EventArgs{ get; set; }

		/// <summary>
		/// Reset this instance.
		/// </summary>
		void Reset ();
	}
}


