using System;
using System.Web.Mail;
using System.IO;
using System.Text;
using System.Diagnostics;
//using System.ComponentModel;
namespace cyclone
{
	/// <summary>
	/// Class1 的摘要描述。
	/// </summary>
	class Class1
	{
		/// <summary>
		/// 應用程式的主進入點。
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			//
			// TODO: 在此加入啟動應用程式的程式碼
			//
			//open emp1/edi/GEIS_SendLog and load log file"
				mail mymail=new mail();
			
			try
			{
				Class1 cyclone =new Class1();
				string Sourcefloderpath;
				string Movefloderppath;
				string Backupfloderpath;
			
				//			
				Sourcefloderpath= args[0];
				Movefloderppath= args[1];
				Backupfloderpath= args[2];
				StreamWriter sw1 = new StreamWriter(args[3], true,Encoding.Default);
				cyclone.copyfile(Sourcefloderpath,Movefloderppath,Backupfloderpath,args[4],sw1);
			}
			catch (Exception e) 
			{
				mymail.writeMail("HUB move file error",e.ToString()+args[0].ToString());
			}



		}
		public void copyfile(string frompath,string topath,string backupfloderpath,string subject,StreamWriter sw)
		{
			TimeSpan diff=new TimeSpan();
			TimeSpan diff1=new TimeSpan();
			mail mymail=new mail();
		try
		{
			
			string movepath ;
			string buckuppath ;
			DateTime starttime=DateTime.Now;//開始時間每處理一個檔案先覆置至Movefloderppath再移動Backupfloderpath
			 
			DirectoryInfo di = new DirectoryInfo(frompath);
			FileInfo  movefi;
			FileInfo[] fi = di.GetFiles();
			Console.WriteLine(fi.Length.ToString());
			int countfilenumber=0;	
			long totalsize=0;
			foreach (FileInfo fiTemp in fi)
			{
				DateTime now=DateTime.Now;
				string timestring=now.Year.ToString()+now.Month.ToString()+now.Day.ToString()+now.Hour.ToString()+now.Minute.ToString()+now.Second.ToString();	
				//movepath=topath+timestring+fiTemp.Name+"#NEC_CI";
				movepath=topath+fiTemp.Name;
				//buckuppath=backupfloderpath+timestring+fiTemp.Name;
				buckuppath=backupfloderpath+fiTemp.Name;
				diff=DateTime.Now-fiTemp.LastWriteTime;
				if(diff.TotalSeconds>100)//處理大於30秒的檔案
				{
					Console.WriteLine(fiTemp.FullName.ToString()+movepath.ToString()+buckuppath.ToString());      
                          
						
					try 
					{
							
						if (!File.Exists(fiTemp.FullName)) 
						{
							// This statement ensures that the file is created,
							// but the handle is not kept.
							using (FileStream fs1 = File.Create(fiTemp.FullName)) {}
							mymail.writeMail(subject,"檔案不存在"+fiTemp.FullName);
						}

						// Ensure that the target does not exist.
						if (File.Exists(movepath))    
							File.Delete(movepath);
						//參數backuppath=none不執行
						if (backupfloderpath!="none")
						{
							if (File.Exists(buckuppath))    
								File.Delete(buckuppath);
						}

						// Move the file.
						if (backupfloderpath!="none")
						{
							File.Copy(fiTemp.FullName, movepath);
						}
						else
						{
						    File.Move(fiTemp.FullName, movepath);
						}
						if (backupfloderpath!="none")
						{
							File.Move(fiTemp.FullName, buckuppath);
						}
						movefi=new FileInfo(movepath);
						sw.WriteLine(fiTemp.Name+"   "+DateTime.Now.ToString()+"File size:"+fiTemp.Length+"Move to size"+movefi.Length);
						if(movefi.Length==0)
						{
							//mymail.writeMail("0 byte error","from:"+fiTemp.FullName+";btye:"+fiTemp.Length+"to:"+movepath+";btye:"+movefi.Length);
						}
						else
						{
						    string is940=fiTemp.Name.Substring(0,3);
							//if(is940=="940")
							//{
							//	mymail.writeMail("EI940",fiTemp.FullName+";btye:"+fiTemp.Length+"to:"+movepath+";btye:"+movefi.Length);
							//}
						}
						Console.WriteLine(fiTemp.Name+"   "+DateTime.Now.ToString()+"File size:"+fiTemp.Length);
						totalsize=totalsize+fiTemp.Length;
						countfilenumber++;
						diff1=DateTime.Now-starttime;
						if(diff1.TotalSeconds>150)//處理大於30秒的檔案
						{
							//mymail.writeMail("apple move file","跳出時間是"+diff1.TotalSeconds);
							break;
						}
						// See if the original exists now.
						if (File.Exists(fiTemp.FullName)) 
						{
								
							mymail.writeMail(subject,"The original file still exists, which is unexpected."+fiTemp.FullName);
						} 
						else 
						{
							Console.WriteLine("The original file no longer exists, which is expected.");
						}            

					} 
					catch (Exception e) 
					{
						mymail.writeMail(subject,"檔案從"+fiTemp.FullName.ToString()+"移至"+movepath.ToString()+"發生錯誤"+"錯誤訊息是"+e.ToString());
					}

				}
			}
			try
			{
				diff1=DateTime.Now-starttime;
				if(countfilenumber>0)
				{
					sw.WriteLine("*******處理"+countfilenumber.ToString()+"個檔案"+"大小是"+totalsize.ToString()+"共花"+diff1.TotalSeconds+"秒******");
				}
				sw.Close();
			}
			catch (Exception e) 
			{
				mymail.writeMail(subject,"錯誤訊息是"+e.ToString());
			}
		}
		catch (Exception e) 
	{
		mymail.writeMail(subject,"錯誤訊息是"+e.ToString());
		string error=e.ToString().Substring(0,42);
		
			StreamReader process=new StreamReader(@"C:\C#EXCUTE\rebuiltconnection.txt");
			string Line="";
			while (process.Peek() >= 0) 
			{
				Line=process.ReadLine();
				string [] split=Line.Split(',');
				Process.Start(split[0],split[1]);
			}   
			process.Close();

				
	}
		
		
		}
	}
}

