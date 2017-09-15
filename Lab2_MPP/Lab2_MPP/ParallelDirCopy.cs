using System;
using System.IO;
using System.Threading;

namespace Lab2_MPP
{
    public class ParallelDirCopy
    {
        private string origin_path;
        private string target_path;

        private volatile int copies_file_capacity;

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
                    throw new DirectoryNotFoundException("Папка " + value + " не существует");
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


        public ParallelDirCopy(string origin, string target)
        {
            if (origin != target)
            {
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
                object outdata = (new CopyInfo(temp, target_path + temp.Substring(temp.LastIndexOf("\\")))) as object;
                object locker = new object();
                lock (locker)
                {
                    ThreadPool.QueueUserWorkItem(CopyData, outdata);
                }
            }
            Thread.Sleep(100);
        }

        private void CopyData(object indata)
        {
            var data = (CopyInfo)indata;

            try
            {
                File.Copy(data.origin, data.direction);
                copies_file_capacity++;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public string GetInfo()
        {
            return "Из директории " + origin_path + " в директорию " + target_path + " скопировано " + copies_file_capacity + " файлов.";
        }

        public class CopyInfo
        {
            public string origin;
            public string direction;

            public CopyInfo(string origin, string direction)
            {
                this.origin = origin;
                this.direction = direction;
            }
        }
    }
}
