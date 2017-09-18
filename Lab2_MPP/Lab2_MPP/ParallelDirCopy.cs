using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2_MPP
{
    delegate void CopyFolder(DirectoryInfo origin, DirectoryInfo target);

    public class ParallelDirCopy
    {
        private string origin_path;
        private string target_path;
        private volatile int copies_file_capacity;

        private List<Task> task_list = new List<Task>();

        private string Origin_Path
        {
            get
            {
                return origin_path;
            }
            set
            {
                if (Directory.Exists(value))
                    origin_path = value;
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
                    target_path = value;
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
            CopyFolder(new DirectoryInfo(origin_path), new DirectoryInfo(target_path));
            for (int i = 0; i < task_list.Count; i++)
                task_list[i].Wait();
        }

        private void CopyFolder(DirectoryInfo origin, DirectoryInfo target)
        {
            if (!Directory.Exists(target.FullName))
                Directory.CreateDirectory(target.FullName);
            foreach (var fi in origin.GetFiles())
            {
                if (!File.Exists(Path.Combine(target.ToString(), fi.Name)))
                {
                    Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                    fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
                    copies_file_capacity++;
                }
                else
                    Console.WriteLine("Copying " + target.FullName + fi.Name + " cancelled.\nReason: File already exist.");
            }
            foreach (DirectoryInfo subDir in origin.GetDirectories())
                task_list.Add(Task.Run(() => CopyFolder(subDir, target.CreateSubdirectory(subDir.Name))));
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
