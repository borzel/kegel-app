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
		public Guid Id {get; set;}
		public String Name {get; set;}
		public int MaxWuerfeJeSpielzug {get; set;}
		public int MaxSpielzuege {get; set;}
	}
}
