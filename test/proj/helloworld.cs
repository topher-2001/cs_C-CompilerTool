using System;
using System.Diagnostics;
using System.IO;
using HelloWorldAppa;

namespace HelloWorldApp {
     class HelloMain {
          static void Main(string[] args) {
               Console.WriteLine("Hello World");
               Secnd s = new Secnd();
               s.PrintOne();


               // if(args.Length > 0) {
               //      for(int i = 0; i < args.Length; i++) {
               //           Console.WriteLine("arg {0} = {1}", i, args[i]);
               //      }
               //
               //      if(args[2] != null) {
               //           Process.Start(args[2]);
               //      }
               // }

               // String input;
               //
               // Console.Write("How old are you: ");
               // input = Console.ReadLine();
               //
               // Console.WriteLine("You are {0} years old", Convert.ToInt32(input));



               Console.WriteLine("Press any key to exit");
               Console.ReadKey();
          }
     }
}
