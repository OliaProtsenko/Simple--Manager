using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SimpleFileManager
{class TxtAnalizator
    {
        public  void Analize(List<FileInfo>search,string property,string filePath)
        {
            DirectoryInfo fileList = new DirectoryInfo(filePath);
            FileInfo[] files = fileList.GetFiles(); // GET ALL THE FILES
            DirectoryInfo[] dirs = fileList.GetDirectories();// GET ALL THE DIRS
            for(int i = 0; i < files.Length; i++)
            {
                if ((files[i].Extension == ".txt")|| (files[i].Extension == ".doc")||((files[i].Extension == ".docx")))
                {
                    if (FindProperty(files[i], property))
                        search.Add(files[i]);
                }
            }
            for(int i = 0; i < dirs.Length; i++)
            {
                Analize(search, property, dirs[i].FullName);
            }
        }
        static bool FindProperty(FileInfo file,string property)
        {
            StreamReader sr = file.OpenText();
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                if (s.Contains(property))
                {
                    return true;
                }
            }
            return false;
        }
        public  void ChangeSequenceOfLetter(string filePath,string what, string forwhat)
        {
            string text = File.ReadAllText(filePath);
            text = text.Replace(what, forwhat);
            File.WriteAllText(filePath, text);
        }
        public  void RightRegister(string filePath)
        {
            string text = File.ReadAllText(filePath);
            char[] arr1 = text.ToCharArray();
            arr1[0] = Char.ToUpper(arr1[0]);
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] == '.')
                {
                    arr1[i + 1] = Char.ToUpper(arr1[i + 1]);
                }     
            }
            text = arr1.ToString();
            File.WriteAllText(filePath, text);
        }
    }
 class FileManager
    {
        public void Delete (string filePath){ }
        public void Create(string filePath,string type) { }
        
    }  
 class Facade
    {
        static TxtAnalizator t;
        static FileManager manager;
        public static void Delete(string filepath)
        {
            manager.Delete(filepath);
        }
        public static void Create(string filePath, string type)
        {
            manager.Create(filePath, type);
        }
        public static void Search(List<FileInfo>search, string property, string filePath)
        {
            t.Analize(search, property, filePath);
        }
        public static void ChangeSequenceOfLetter(string filePath, string what, string forwhat)
        {
            t.ChangeSequenceOfLetter(filePath, what, forwhat);
        }
        public static void RightRegister(string filePath)
        {
            t.RightRegister(filePath);
        }
    }
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
