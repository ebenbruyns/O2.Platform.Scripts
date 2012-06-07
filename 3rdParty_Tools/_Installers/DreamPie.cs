﻿using System;
using System.Diagnostics; 
using O2.Kernel; 
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;  
using O2.XRules.Database.Utils;
//O2File:Tool_API.cs 
 
namespace O2.XRules.Database.APIs 
{
	public class testInstall
	{
		public static void test()  
		{
			new DreamPie_Install().start(); 
		}
	}
	 
	public class DreamPie_Install : Tool_API    
	{				
		public DreamPie_Install()
		{			
			Install_Uri = "http://launchpad.net/dreampie/trunk/1.1.1/+download/dreampie-1.1.1-setup.exe".uri();
			Install_File = "dreampie-1.1.1-setup.exe";
			Install_Dir = ProgramFilesFolder;
			Install_File = 
			Executable = ProgramFilesFolder.pathCombine("DreamPie//dreampie.exe");
				   			
			startInstaller_FromMsi_Web();			
		}	
		//
		
		public Process start()
		{
			if (this.isInstalled())
				return this.Executable.startProcess(@"C:\Python27\python.exe"); 
			return null;
		}		
	}
}