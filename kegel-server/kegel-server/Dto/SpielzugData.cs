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
		public SpielzugData()
		{
			_wuerfe = new List<WurfData>();
		}
		
		private List<WurfData> _wuerfe;
		public List<WurfData> Wuerfe {get{return _wuerfe;}}
	}
}
