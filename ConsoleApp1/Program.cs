using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        string identation = "";
        string sourcePath = @"D:\Posao";
        string targetPath = @"E:\BackupPosao\" + DateTime.Today.ToShortDateString().Replace("/", "_");
        List<string> blacklist = new List<string> { "eclipse", "apache" };
        List<string> extensions = new List<string> { ".docx", ".doc", ".txt", ".pdf", ".xls", ".xlsx" };

        static void Main(string[] args)
        {
            
            Program p = new Program();
            p.GoDeeper(p.sourcePath);
            Console.ReadLine();
        }

        private bool IsLeaf(string dir)
        {
            return Directory.GetDirectories(dir).Count() == 0;
        }

        private bool GoDeeper(string dir)
        {
            identation += "_";
            Console.WriteLine(identation + dir);
            
            foreach(string subdir in Directory.GetDirectories(dir))
            {                
                if (IsLeaf(subdir) || IsBlackListed(subdir))
                {
                    CopyFiles(subdir);
                    continue;
                }
                else
                {
                    CopyFiles(subdir);
                    GoDeeper(subdir);
                }
            }
            identation = identation.Remove(0, 1);
            return false;
        }

        private void CopyFiles(string subdir)
        {
            foreach (string file in Directory.GetFiles(subdir))
            {
                if (extensions.Contains(Path.GetExtension(file)))
                {
                    string newFolderPath = targetPath + subdir.Replace("D:\\Posao", "");
                    if (!Directory.Exists(newFolderPath))
                    {
                        Directory.CreateDirectory(newFolderPath);
                    }
                    string newFileName = Path.GetFileName(file);
                    string newFileNameFullPath = Path.Combine(newFolderPath, newFileName);
                    System.IO.File.Copy(file, newFileNameFullPath, true);
                }
            }
        }

        private bool IsBlackListed(string dir)
        {
            foreach (string s in blacklist)
            {
                if (dir.Contains(s))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
