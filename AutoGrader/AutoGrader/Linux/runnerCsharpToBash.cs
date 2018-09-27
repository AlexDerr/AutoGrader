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
            WriteToFile((obj.SubmissionId).ToString()+"."+obj.Input.Language, obj.Input.SourceCode);

            bool Compiled;
            string CompiledFeedback;

            if(obj.Input.Language == Language.Cpp){
                CompiledFeedback = obj.CompileCpp();
            }
            else if(obj.Input.Language == Language.Java){
                CompiledFeedback = obj.CompileJava();
            }

            //determine if the file was compiled
            if ( System.Convert.ToInt32( ("ls | grep " + obj.SubmissionId + " | wc -l").Bash() ) == 2){
                Compiled = true;
            }
            else {
                Compiled = false;
            }

            return Compiled;
        }

        private static string CompileCpp(this Submission obj){
            return ("g++ " + obj.SubmissionId + ".cpp -o " + obj.SubmissionId +".out").Bash();
        }

        private static string CompileJava(this Submission obj){
            return (("java " + obj.SubmissionId.ToString()).Bash());
        }

        public static Submission Run(this Submission obj){
            //need to pull input list from server
            string input = "hello world";
            string CmdLineInput = ("./"+obj.SubmissionId +".out < " + input);
            //obj.Input.Language = CmdLineInput.Bash();
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