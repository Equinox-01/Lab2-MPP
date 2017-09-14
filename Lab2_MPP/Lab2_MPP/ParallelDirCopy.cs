using System;
using System.IO;

namespace Lab2_MPP
{
    public class ParallelDirCopy
    {
        private string origin_path;
        private string target_path;
        private int threads_capacity;

        private string Origin_Path
        {
            get
            {
                return origin_path;
            }
            set
            {
                if (Directory.Exists(value))
                {
                    origin_path = value;
                }
                else
                {
                    origin_path = null;
                    throw new DirectoryNotFoundException("Папка " + value + "не существует");
                }
            }
        }

        private string Target_Path
        {
            get
            {
                return target_path;
            }
            set
            {
                if (Directory.Exists(value))
                {
                    target_path = value;
                }
                else
                {
                    target_path = null;
                    throw new DirectoryNotFoundException("Папка " + value + " не существует");
                }
            }
        }


        public ParallelDirCopy(string origin, string target, int capacity)
        {
            if (origin != target)
            {
                threads_capacity = capacity;
                Origin_Path = origin;
                Target_Path = target;
            }
            else
                throw new DirectoryNotFoundException("Адрес исходного и целевого каталога идентичен.");
        }

        public void Execute()
        {
            //Выполнение
        }

        public string GetInfo()
        {
            //Формат вывода
            return "";
        }
    }
}
