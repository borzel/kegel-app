/*
 * Created by SharpDevelop.
 * User: alex
 * Date: 23.01.2014
 * Time: 22:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace kegel_server.Dto
{
	/// <summary>
	/// Description of Spiel.
	/// </summary>
	[Serializable]
	public class SpielData
	{
		public SpielData()
		{
			_spielzuege = new List<SpielzugData>();
		}
		
		public List<UserData> Spieler { get; set; }
		
		private List<SpielzugData> _spielzuege;
		public List<SpielzugData> Spielzuege {get {return _spielzuege;}}
	}
}
