using System;
using System.Diagnostics;
using System.ComponentModel;
using ShellHelper;

namespace MyProcessSample
{
    class MyProcess
    {
        public static void Main()
        {
            string args = Console.ReadLine();
            Console.WriteLine(args.Bash());
        }
    }
}