using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleFileManager
{
    class TxtAnalizator
    {
        static public void Analize(List<FileInfo> search, string property, string filePath)
        {
            DirectoryInfo fileList = new DirectoryInfo(filePath);
            FileInfo[] files = fileList.GetFiles(); // GET ALL THE FILES
            DirectoryInfo[] dirs = fileList.GetDirectories();// GET ALL THE DIRS
            for (int i = 0; i < files.Length; i++)
            {
                if ((files[i].Extension == ".txt") || (files[i].Extension == ".doc") || ((files[i].Extension == ".docx")))
                {
                    if (FindProperty(files[i], property))
                        search.Add(files[i]);
                }
            }
            for (int i = 0; i < dirs.Length; i++)
            {
                Analize(search, property, dirs[i].FullName);
            }
        }
        static bool FindProperty(FileInfo file, string property)
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
        static public void ChangeSequenceOfLetter(string filePath, string what, string forwhat)
        {
            string text = File.ReadAllText(filePath);
            text = text.Replace(what, forwhat);
            File.WriteAllText(filePath, text);
        }
        static public void RightRegister(string filePath)
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
            text = new string(arr1);
            File.WriteAllText(filePath, text);
        }


    }
}

