using System;
using System.Diagnostics;
using AutoGrader.Models.Submission;

namespace ShellHelper
{
    public static class Shell
    {
        public static string Bash(this string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");
            
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }

        public static bool Compile(this Submission obj){ //need to make it into an object


            bool Compiled;
            string CompiledFeedback;


            if(obj.Input.Language == Language.C){
                CompiledFeedback = obj.CompileC();
            }
            else if(obj.Input.Language == Language.Cpp){
                CompiledFeedback = obj.CompileCpp();
            }
            else if(obj.Input.Language == Language.Java){
                CompiledFeedback = obj.CompileJava();
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
            return ("g++ " + obj.SubmissionId + ".cpp -o " + obj.SubmissionId +".out").Bash();
        }

        private static string CompileJava(this Submission obj){
            WriteToFile(obj.SubmissionId+".jar", obj.Input.SourceCode);
            ("javac " + obj.SubmissionId + ".jar").Bash();
            return ("java " + obj.SubmissionId).Bash();
        }

        public static bool IsCompiled(this Submission obj){
            bool Compiled;
            if ( System.Convert.ToInt32( ("ls | grep " + obj.SubmissionId + " | wc -l").Bash() ) >= 2){
                Compiled = true;
            }
            else {
                Compiled = false;
            }
            return Compiled;
        }

        public static Submission Run(this Submission obj){
            //need to pull input list from server
            
            string CmdLineInput = ("./"+obj.SubmissionId +".out");
            //obj.Output.TestCases[0].CodeOutput = CmdLineInput.Bash();
            System.Console.WriteLine(CmdLineInput.Bash());
            return obj;
        }

        public static Submission CompileAndRun(this Submission obj){
            bool Compiled = obj.Compile();
            if(Compiled){
                obj.Run();
            }
            return obj;
        }

        public static void WriteToFile(string name, string text){
             System.IO.File.WriteAllText(name, text);
        }

        public static void Compare(this Submission obj, string input, string output){
            WriteToFile(obj.SubmissionId+".txt", obj.Input.SourceCode);
        }

        public static int GetInputList(this int ID){

            return 1;

        }

    }
}