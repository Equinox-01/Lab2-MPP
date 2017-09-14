using System;
using System.IO;

namespace Lab2_MPP
{
    class Program
    {
        static void Main(string[] args)
        {
            string original_dir = "C:\\MPP2\\origin";
            string target_dir = "C:\\MPP2\\target";
            Console.Write("Количество потоков в пуле - ");
            try
            {
                var pDirCopy = new ParallelDirCopy(original_dir, target_dir, int.Parse(Console.ReadLine()));
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
            catch (IOException e)
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
