/*
 * Created by SharpDevelop.
 * User: alex
 * Date: 23.01.2014
 * Time: 22:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace kegel_server.Dto
{
	[Serializable]
	public class WurfData
	{	
		public Guid Id {get; set;}
		public Guid Spieler {get; set;}
		public Guid Spielzug {get; set;}
		public int Wurfnummer {get; set;}
		public bool Ratte {get; set;}
		public bool Ungueltig {get; set;}
		public int Wurfergebniss {get; set;}
	}
}
