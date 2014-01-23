/*
 * Created by SharpDevelop.
 * User: alex
 * Date: 23.01.2014
 * Time: 22:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Nancy;

namespace kegel_server.Module
{
	/// <summary>
	/// Description of DebugModule.
	/// </summary>
	public class DebugModule : NancyModule
	{
		public DebugModule() : base("/debug")
		{
			Get["/"] = _ =>
			{
				
				
				
				
				return "DEBUG";
			};
		}
	}
}
