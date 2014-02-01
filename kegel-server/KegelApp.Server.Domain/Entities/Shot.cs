/*
 * Created by SharpDevelop.
 * User: alex
 * Date: 23.01.2014
 * Time: 22:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace KegelApp.Server.Domain.Entities
{
	[Serializable]
	public class Shot
	{	
		public virtual int Id {get; protected set;}
		public virtual bool NullShot {get; set;}
		public virtual bool Fault {get; set;}
		public virtual int Value {get; set;}

	   // public virtual Move InMove { get; set; }
	}
}
