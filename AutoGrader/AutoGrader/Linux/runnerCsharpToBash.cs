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

        public static CompileCheckOutput Compile(this SubmissionInput obj){ //need to make it into an object
            WriteToFile((obj.SubmissionInputId).ToString()+"."+obj.Language, obj.Code);

            CompileCheckOutput Compiled = new CompileCheckOutput();

            if(obj.Language == "cpp"){
                Compiled.CompilerOutput = obj.CompileCpp();
            }
            else if(obj.Language == "java"){
                Compiled.CompilerOutput = obj.CompileJava();
            }

            //determine if the file was compiled
            if ( System.Convert.ToInt32( ("ls | grep " + obj.SubmissionInputId + " | wc -l").Bash() ) == 2){
                Compiled.Compiled = true;
            }
            else {
                Compiled.Compiled = false;
            }


            return Compiled;
        }

        public static string CompileCpp(this SubmissionInput obj){
            return ("g++ " + obj.SubmissionInputId + ".cpp -o " + obj.SubmissionInputId +".out").Bash();
        }

        public static string CompileJava(this SubmissionInput obj){
            return (("java " + obj.SubmissionInputId.ToString()).Bash());
        }

        public static SubmissionOutput Run(this SubmissionOutput obj){
            //need to pull input list from server
            string input = "hello world";
            string CmdLineInput = ("./"+obj.SubmissionInputId +".out < " + input);
            obj.Language = CmdLineInput.Bash();
            return obj;
        }

        public static void WriteToFile(string name, string text){
             System.IO.File.WriteAllText(name, text);
        }

        public static void Compare(this SubmissionInput obj, string input, string output){
            WriteToFile(obj.SubmissionInputId+".txt", obj.Code);
        }

        public static int GetInputList(this int ID){

            return 1;

        }

    }
}