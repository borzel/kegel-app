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
		public WurfData()
		{
		}
		
		private int _punktzahl;
		public int Punktzahl
		{
			get {return _punktzahl;}
			set
			{
				if (value <= 9)
					_punktzahl = value;
				else
					throw new FormatException("Punktzahl muss zwischen NULL und 9 liegen");
			}
		}
	}
}
