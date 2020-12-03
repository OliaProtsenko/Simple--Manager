using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace SimpleFileManager
{
    class Facade
    {
        public static void Delete(string filepath)
        {
            FileManager.Delete(filepath);
        }
        public static void Create(string filePath, string type)
        {
            FileManager.Create(filePath, type);
        }
        public static void Search(List<FileInfo> search, string property, string filePath)
        {
            TxtAnalizator.Analize(search, property, filePath);
        }
        public static void ChangeSequenceOfLetter(string filePath, string what, string forwhat)
        {
            TxtAnalizator.ChangeSequenceOfLetter(filePath, what, forwhat);
        }
        public static void RightRegister(string filePath)
        {
            TxtAnalizator.RightRegister(filePath);
        }
    }
}
