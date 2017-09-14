using System;
using System.IO;

namespace Lab2_MPP
{
    public class ParallelDirCopy
    {
        private string origin_path;
        private string target_path;
        private int threads_capacity;

        private int copies_file_capacity;

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
                copies_file_capacity = 0;
            }
            else
                throw new DirectoryNotFoundException("Адрес исходного и целевого каталога идентичен.");
        }

        public void Execute()
        {
            string[] files = Directory.GetFiles(origin_path);
            foreach (string temp in files)
            {
                string tmp = temp.Substring(temp.LastIndexOf("\\"));
                try
                {
                    File.Copy(temp, target_path + temp.Substring(temp.LastIndexOf("\\")));
                    copies_file_capacity++;
                }
                catch(Exception)
                {
                    IOException e = new IOException("Файл " + temp.Substring(temp.LastIndexOf("\\")) + " уже существует.");
                    throw e;
                }
                
            }
        }

        public string GetInfo()
        {
            return "Из директории " + origin_path + " в директорию " + target_path + " скопировано " + copies_file_capacity + " файлов.";
        }
    }
}
