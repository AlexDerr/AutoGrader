using System;
using System.Text;
using System.Diagnostics;
using AutoGrader.Models.Submission;

namespace ShellHelper
{
    public static class Shell
    {
        public static string Bash(this string cmd)
        {
        
            ProcessStartInfo StartInfo = BashProcess(cmd);
            
            StringBuilder error = new StringBuilder();
            StringBuilder output = new StringBuilder();

            using (Process myProcess = Process.Start(StartInfo))
            {
                myProcess.OutputDataReceived += delegate(object sender, DataReceivedEventArgs e)
                {
                    output.Append(e.Data);
                };
                myProcess.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs e)
                {
                    error.Append(e.Data);            
                };

                myProcess.BeginErrorReadLine();
                myProcess.BeginOutputReadLine();

                myProcess.WaitForExit();

            }

            string result = output.ToString() + error.ToString();

            return result;
        }

        private static ProcessStartInfo BashProcess(string cmd){
            var escapedArgs = cmd.Replace("\"", "\\\"");

            ProcessStartInfo StartInfo = new ProcessStartInfo();

            StartInfo.Arguments =  $"-c \"{escapedArgs}\"";
            StartInfo.FileName = "/bin/bash";

            StartInfo.UseShellExecute = false;
            StartInfo.RedirectStandardOutput = true;
            StartInfo.RedirectStandardError = true;
            StartInfo.CreateNoWindow = true;
            return StartInfo;
        }

        public static bool Compile(this Submission obj){ //need to make it into an object


            bool Compiled;

            if(obj.Input.Language == "C"){
                obj.Output.CompileOutput = obj.CompileC();
            }
            else if(obj.Input.Language == "C++"){
                obj.Output.CompileOutput = obj.CompileCpp();
            }
            else if(obj.Input.Language == "Java"){
                obj.Output.CompileOutput = obj.CompileJava();
            }

            //determine if the file was compiled
            Compiled = obj.IsCompiled();

            return Compiled;
        }

        private static string CompileC(this Submission obj){
            WriteToFile(obj.SubmissionId+".c", obj.Input.SourceCode);
            return ("gcc " + obj.SubmissionId + ".c -o " + obj.SubmissionId +".out").Bash();
        }

        private static string CompileCpp(this Submission obj){
            WriteToFile(obj.SubmissionId+".cpp", obj.Input.SourceCode);
            string test =  (("g++ " + obj.SubmissionId + ".cpp -o " + obj.SubmissionId +".out").Bash());
            return (test);
        }

        private static string CompileJava(this Submission obj){
            WriteToFile(obj.SubmissionId+".jPava", obj.Input.SourceCode);
            return ("javac " + obj.SubmissionId + ".java").Bash();
        }

        public static bool IsCompiled(this Submission obj){
            if ( System.Convert.ToInt32( ("ls | grep " + obj.SubmissionId + " | wc -l").Bash() ) >= 2){
                obj.Output.Compiled = true;
            }
            else {
                obj.Output.Compiled = false;
            }

            return obj.Output.Compiled;
        }

        public static Submission RunAndCompare(this Submission obj){
            for(int TestCaseNumber = 0; TestCaseNumber < obj.Output.TestCases.Count; TestCaseNumber++){
                obj.Run(TestCaseNumber);
                obj.Compare(TestCaseNumber);
            }

            return obj;
            
        }

        public static Submission Run(this Submission obj, int TestCaseNumber = 0){
            WriteToFile(obj.SubmissionId+"input.txt", obj.Output.TestCases[TestCaseNumber].CodeInput);
            if(obj.Input.Language == "C"){
                return obj.RunC(TestCaseNumber);
            }
            else if(obj.Input.Language == "C++"){
                return obj.RunCpp(TestCaseNumber);
            }
            else if(obj.Input.Language == "Java"){
                return obj.RunJava(TestCaseNumber);
            }
            return obj;
        }


        public static Submission RunProcess(this Submission obj, int TestCaseNumber, string cmd){
            ProcessStartInfo StartInfo = BashProcess(cmd);
            
            StringBuilder error = new StringBuilder();
            StringBuilder output = new StringBuilder();

            using (Process myProcess = Process.Start(StartInfo))
            {
                myProcess.OutputDataReceived += delegate(object sender, DataReceivedEventArgs e)
                {
                    output.Append(e.Data);
                };

                myProcess.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs e)
                {
                    error.Append(e.Data);            
                };

                myProcess.BeginErrorReadLine();
                myProcess.BeginOutputReadLine();
/*
                do
                {
                    if (!myProcess.HasExited)
                    {
                        // Refresh the current process property values.
                        myProcess.Refresh();

                        // Display current process statistics.
                        if(myProcess. > obj.){
                        
                        }
                        Console.WriteLine("  base priority: {0}",
                            myProcess.BasePriority);
                        Console.WriteLine("  priority class: {0}",
                            myProcess.PriorityClass);
                        Console.WriteLine("  user processor time: {0}",
                            myProcess.UserProcessorTime);
                        Console.WriteLine("  privileged processor time: {0}",
                            myProcess.PrivilegedProcessorTime);
                        Console.WriteLine("  total processor time: {0}",
                            myProcess.TotalProcessorTime);
                        Console.WriteLine("  PagedSystemMemorySize64: {0}",
                            myProcess.PagedSystemMemorySize64);
                        Console.WriteLine("  PagedMemorySize64: {0}",
                           myProcess.PagedMemorySize64);

                        // Update the values for the overall peak memory statistics.
                        peakPagedMem = myProcess.PeakPagedMemorySize64;
                        peakWorkingSet = myProcess.PeakWorkingSet64;
                    }
                }
                while (!myProcess.WaitForExit(1000));
*/

                myProcess.WaitForExit();


            }


            obj.Output.TestCases[TestCaseNumber].CodeOutput =  output.ToString() + error.ToString();
            return obj;
        }

        public static Submission RunC(this Submission obj, int TestCaseNumber){
            string CmdLineInput = ("./"+obj.SubmissionId +".out < " + obj.SubmissionId+"input.txt");
            obj.RunProcess(TestCaseNumber, CmdLineInput);
            return obj;
        }

        public static Submission RunCpp(this Submission obj, int TestCaseNumber){
            string CmdLineInput = ("./"+obj.SubmissionId +".out < " + obj.SubmissionId+"input.txt");
            obj.RunProcess(TestCaseNumber, CmdLineInput);
            return obj;
        }

        public static Submission RunJava(this Submission obj, int TestCaseNumber){
            string CmdLineInput = ("java "+obj.SubmissionId +" < " + obj.SubmissionId+"input.txt");
            obj.RunProcess(TestCaseNumber, CmdLineInput);
            return obj;
        }

        public static void WriteToFile(string name, string text){
             System.IO.File.WriteAllText(name, text);
        }


        public static Submission Compare (this Submission obj, int TestCaseNumber = 0){
            if(obj.Output.TestCases[TestCaseNumber].CodeOutput.Trim() == obj.Output.TestCases[TestCaseNumber].ExpectedOutput.Trim()){
                obj.Output.TestCases[TestCaseNumber].Pass = true;
            }
            else{
                obj.Output.TestCases[TestCaseNumber].Pass = false;
            }

            return obj;
        }

    }
}
