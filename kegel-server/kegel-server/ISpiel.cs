/*
 * Created by SharpDevelop.
 * User: alex
 * Date: 23.01.2014
 * Time: 22:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using kegel_server.Dto;

namespace kegel_server
{
	/// <summary>
	/// Description of ISpiel.
	/// </summary>
	public interface ISpiel
	{
		void Start(List<UserData> listOfUser);
		
		UserData GetAktuellenSpieler();
		
		bool SetWurf(WurfData wurf);
		
		SpielData GetDaten();
		string GetErklaerung();
		string GetName();
	}
}
