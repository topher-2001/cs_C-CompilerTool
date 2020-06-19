using System;
using System.Diagnostics;
using System.IO;

namespace BuildHelper {
     class BuildHelperMain {

          //Vars
          public string[] lines;
          public bool makeExist;
          public string exparms;
          public string cscLocation;
          public string srcFiles;


          public void run(string[] args) {
               //Save the text from "make.txt" if it exist
               if(args.Length > 0) {
                    lines = File.ReadAllLines(@args[0]);
               } else {
                    string[] dirs = Directory.GetFiles(@"./");
                    foreach(string dir in dirs) {
                         if(dir == "./make.txt") {
                              lines = File.ReadAllLines(dir);
                              makeExist = true;
                         }
                    }
               }
          }

          public void getCSC() {
               string[] posCscFiles = Directory.GetFiles("C:/Windows/Microsoft.NET/Framework", "csc.exe", SearchOption.AllDirectories);
               cscLocation = posCscFiles[posCscFiles.Length - 1];
          }

          public void getParms() {
               bool exparmsExist = false;
               string[] dirs = Directory.GetFiles(@"./");
               foreach(string dir in dirs) {
                    //Add icon if its available
                    if(dir == "./icon.ico") {
                         exparms += " -win32icon:icon.ico ";
                         Console.WriteLine("Added icon");
                    }
                    //Add extra parameters if the "exparms.txt" file is available
                    if(dir == "./exparms.txt") {
                         exparmsExist = true;
                         string[] exParmLines = File.ReadAllLines(@"./exparms.txt");
                         for(int i = 0; i < exParmLines.Length; i++) {
                              exparms += " " + exParmLines[i] + " ";
                         }
                         //Print out the extra parameters
                         Console.WriteLine("Added Extra Parms:");
                         for(int i = 0; i < exParmLines.Length; i++) {
                              Console.WriteLine("\t{0}", exParmLines[i]);
                         }
                    }
               }

               if(!exparmsExist) {
                    exparms += " -out:./output.exe ";
               }
          }

          public void getSrcFiles() {
               if(makeExist) {
                    Console.WriteLine("make.txt file exist");
                    for(int i = 0; i < lines.Length; i++) {
                         string[] srcDirs = {""};
                         try {
                              srcDirs = Directory.GetFiles(@"" + lines[i]);
                              foreach(string srcDir in srcDirs) {
                                   Console.WriteLine(srcDir);
                                   srcFiles += srcDir + " ";
                              }
                         } catch (Exception e) {
                              Console.WriteLine(e.ToString());
                              Console.WriteLine("Press ENTER to exit");
                              Console.ReadLine();
                         }
                    }
               } else {
                    Console.WriteLine("make.txt file dose not exist");
                    string[] srcDirs = Directory.GetFiles(@".\");
                    try {
                         srcDirs = Directory.GetFiles(@".\", "*.cs", SearchOption.AllDirectories);
                         foreach(string srcDir in srcDirs) {
                              Console.WriteLine(srcDir);
                              srcFiles += srcDir + " ";
                         }
                    } catch (Exception e) {
                         Console.WriteLine(e.ToString());
                         Console.WriteLine("Press ENTER to exit");
                         Console.ReadLine();
                    }
               }
          }

          public void compile() {
               Console.WriteLine(cscLocation);
               Console.WriteLine(exparms);
               Console.WriteLine(srcFiles);
               //Compile the program with the provided parameters
               Process process = new Process();
               ProcessStartInfo startInfo = new ProcessStartInfo();
               startInfo.WindowStyle = ProcessWindowStyle.Normal;
               //Get the location of "CSC.exe from the make.txt file fall back to base if the make.txt is unavailable"
               startInfo.FileName = "cmd.exe";
               startInfo.Arguments = "/k" + cscLocation + exparms + srcFiles;
               process.StartInfo = startInfo;
               process.Start();
               process.WaitForExit();
          }


          static void Main(string[] args) {
               BuildHelperMain comp = new BuildHelperMain();
               comp.run(args);
               comp.getCSC();
               comp.getParms();
               comp.getSrcFiles();
               comp.compile();
          }
     }
}
