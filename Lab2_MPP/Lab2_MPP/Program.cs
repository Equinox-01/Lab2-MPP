using System;
using System.IO;
using System.Threading;

namespace Lab2_MPP
{
    class Program
    {
        static void Main(string[] args)
        {
            /*string original_dir = Environment.GetCommandLineArgs()[0];
            string target_dir = Environment.GetCommandLineArgs()[1];*/

            string original_dir = "C:\\MPP2\\origin";
            string target_dir = "C:\\MPP2\\target";

            try
            {
                var pDirCopy = new ParallelDirCopy(original_dir, target_dir);
                pDirCopy.Execute();
                Console.WriteLine(pDirCopy.GetInfo());
            }
            catch (OverflowException)
            {
                Console.WriteLine("Количество пулов в потоке слишком велико.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Количество заданно некорректно.");
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
