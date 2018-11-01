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
            var escapedArgs = cmd.Replace("\"", "\\\"");

            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.Arguments =  $"-c \"{escapedArgs}\"";
            startInfo.FileName = "/bin/bash";

            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;

            
            StringBuilder error = new StringBuilder();
            StringBuilder output = new StringBuilder();

            using (Process myProcess = Process.Start(startInfo))
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


        public static Submission RunProcess(this Submission obj, string cmd){

                    var escapedArgs = cmd.Replace("\"", "\\\"");
            
                    var process = new Process()
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "/bin/bash",
                            Arguments = $"-c \"{escapedArgs}\"",
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            UseShellExecute = false,
                            CreateNoWindow = true,
                        }
                    };
                    
                    process.Start();
                    string error   = process.StandardError.ReadToEnd();
                    string output  = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    string result = error + output; 
                    return obj;
        }

        public static Submission RunC(this Submission obj, int TestCaseNumber){
            string CmdLineInput = ("./"+obj.SubmissionId +".out < " + obj.SubmissionId+"input.txt");
            obj.Output.TestCases[TestCaseNumber].CodeOutput = CmdLineInput.Bash();
            return obj;
        }

        public static Submission RunCpp(this Submission obj, int TestCaseNumber){
            string CmdLineInput = ("./"+obj.SubmissionId +".out < " + obj.SubmissionId+"input.txt");
            obj.Output.TestCases[TestCaseNumber].CodeOutput = CmdLineInput.Bash();
            return obj;
        }

        public static Submission RunJava(this Submission obj, int TestCaseNumber){
            string CmdLineInput = ("java "+obj.SubmissionId +" < " + obj.SubmissionId+"input.txt");
            obj.Output.TestCases[TestCaseNumber].CodeOutput = CmdLineInput.Bash();
            return obj;
        }

        public static void WriteToFile(string name, string text){
             System.IO.File.WriteAllText(name, text);
        }


        public static Submission Compare (this Submission obj, int TestCaseNumber){
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
