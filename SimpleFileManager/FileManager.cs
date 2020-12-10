using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SimpleFileManager
{
    class FileManager
    {
        static public void Delete(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                else
                {
                    MessageBox.Show("This File is not exist");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }

        }
        static public void CutAndPaste(string pathFrom,string pathTo)
        {
            FileInfo f = new FileInfo(pathFrom);
            
            System.IO.File.Move(pathFrom, pathTo+@"\"+f.Name);
            
        }
        
        static public void Create(string filePath, string name)
        {
            string fileName = filePath + @"\" + name + ".txt";
            try
            {
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    DialogResult result = MessageBox.Show("File with same name is already exist.Are you sure?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.OK)
                    { File.Delete(fileName); }
                    else
                    {
                        return;
                    }

                }

                // Create a new file     
                using (StreamWriter sw = File.CreateText(fileName));


                // Open the stream and read it back.    

            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }

        }

    }
}
