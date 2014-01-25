/*
 * Created by SharpDevelop.
 * User: alex
 * Date: 23.01.2014
 * Time: 22:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace kegel_server.Dto
{
	[Serializable]
	public class SpielzugData
	{		
		public Guid Id {get; set;}
		public Guid Spiel {get; set;}
		public Guid Spieler {get; set;}
		public int Spielzugnummer {get; set;}
		public int Punktzahl {get; set;}
	}
}
