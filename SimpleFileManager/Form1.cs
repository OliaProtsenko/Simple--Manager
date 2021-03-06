﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleFileManager
{
    public partial class Form1 : Form
    {
        private string filePath1 = @"E:\";
        private string lastFilePath1 = "";
        private string filePath2 = @"E:\";
        private string lastFilePath2 = "";
        private bool isFile =false ;
        private string currentlySelectedItemName = "";
        List<FileInfo> search = new List<FileInfo>();
        private bool isSearch = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filePathTextBox.Text = filePath1;
            filePathTextBox2.Text = filePath2;
            loadFilesAndDirectories(filePath1,listView1);
            loadFilesAndDirectories(filePath2, listView2);
           
        }


        public void loadFilesAndDirectories(string filePath,ListView listView1)
        {
           
            string tempFilePath = "";
            FileAttributes fileAttr;
            try
            {

                if (isFile)
                {
                    tempFilePath = filePath;
                    FileInfo fileDetails = new FileInfo(tempFilePath);
                    fileNameLabel.Text = fileDetails.Name;
                    fileTypeLabel.Text = fileDetails.Extension;
                    fileAttr = File.GetAttributes(tempFilePath);
                    Process.Start(tempFilePath);
                }
                else
                {
                    fileAttr = File.GetAttributes(filePath);
                    
                }

                if((fileAttr & FileAttributes.Directory ) == FileAttributes.Directory)
                {
                    Output(filePath,listView1);
                }
                else
                {
                    fileNameLabel.Text = this.currentlySelectedItemName;
                }
            }
            catch(Exception e)
            {

            }
            filePath1 = filePathTextBox.Text;
            //filePath2 = filePathTextBox2.Text;
        }

        private void Output(string filePath,ListView listView1)
        {
            DirectoryInfo fileList;
            fileList = new DirectoryInfo(filePath);
            FileInfo[] files = fileList.GetFiles(); // GET ALL THE FILES
            DirectoryInfo[] dirs = fileList.GetDirectories(); // GET ALL THE DIRS
            Signs(files,listView1);
               for (int i = 0; i < dirs.Length; i++)
            {
                listView1.Items.Add(dirs[i].Name, 10);
            }
        }
        private void Signs(FileInfo[] files,ListView listView1)
        {
            string fileExtension = "";
            listView1.Items.Clear();
            for (int i = 0; i < files.Length; i++)
            {
                fileExtension = files[i].Extension.ToUpper();
                switch (fileExtension)
                {
                    case ".MP3":
                    case ".MP2":
                        listView1.Items.Add(files[i].Name, 5);
                        break;
                    case ".EXE":
                    case ".COM":
                        listView1.Items.Add(files[i].Name, 7);
                        break;

                    case ".MP4":
                    case ".AVI":
                    case ".MKV":
                        listView1.Items.Add(files[i].Name, 6);
                        break;
                    case ".PDF":
                        listView1.Items.Add(files[i].Name, 4);
                        break;
                    case ".DOC":
                    case ".DOCX":
                        listView1.Items.Add(files[i].Name, 3);
                        break;
                    case ".PNG":
                    case ".JPG":
                    case ".JPEG":
                        listView1.Items.Add(files[i].Name, 9);
                        break;

                    default:
                        listView1.Items.Add(files[i].Name, 8);
                        break;
                }

            }
        }
        public void loadButtonAction(string filePath,string lastFilePath,ListView listView1,TextBox filePathTextBox,bool left)
        {
            // removeBackSlash();
            if (left)
            {
                if ((isSearch == true) && (isFile == true))
                {
                    lastFilePath = filePath;
                    filePath = filePathTextBox.Text;
                    loadFilesAndDirectories(filePath, listView1);
                    filePath = lastFilePath;
                }
                else
                {
                    if (isSearch == false)
                    {
                        filePath = filePathTextBox.Text;
                    }
                    loadFilesAndDirectories(filePath, listView1);

                }
            }
            else
            {
                filePath = filePathTextBox.Text;
            loadFilesAndDirectories(filePath, listView1);
        }
            
          
        }

        public void removeBackSlash(TextBox filePathTextBox)
        {
            string path = filePathTextBox.Text;
            if (path.LastIndexOf(@"\") == path.Length - 1);
            {
                filePathTextBox.Text = path.Substring(0, path.Length - 1);
            }
        }

        public void goBack(TextBox filePathTextBox)
        {
            try
            {
                removeBackSlash(filePathTextBox);
                string path = filePathTextBox.Text;
                path = path.Substring(0, path.LastIndexOf(@"\")+1);
                this.isFile = false;
                filePathTextBox.Text = path;
                removeBackSlash(filePathTextBox);
            }
            catch(Exception e)
            {

            }
        }

        private void goButton_Click(object sender, EventArgs e)
        {
           
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            currentlySelectedItemName = e.Item.Text;
            FileAttributes fileAttr=new FileAttributes();
            if (isSearch == false)
            {
                fileAttr  = File.GetAttributes(filePathTextBox.Text + @"\" + currentlySelectedItemName);
                filePathTextBox.Text = filePathTextBox.Text + @"\" + currentlySelectedItemName;
            }
            else
            {
                foreach(FileInfo file in search)
                {
                    if (file.Name == currentlySelectedItemName)
                    {
                        fileAttr = File.GetAttributes(file.FullName);
                        filePathTextBox.Text = file.FullName;
                    }

                }
            }
                if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    isFile = false;
                   
                }
                else
                {
                    isFile = true;
                    
                }
            
           
        }
        private void listView2_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            
            currentlySelectedItemName = e.Item.Text;
            FileAttributes fileAttr = new FileAttributes();
           
                fileAttr = File.GetAttributes(filePathTextBox2.Text + @"\" + currentlySelectedItemName);
                filePathTextBox2.Text = filePathTextBox2.Text + @"\" + currentlySelectedItemName;
            if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                isFile = false;

            }
            else
            {
                isFile = true;

            }


        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            loadButtonAction(filePath1,lastFilePath1,listView1,filePathTextBox,true);
        }
        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            loadButtonAction(filePath2, lastFilePath2, listView2, filePathTextBox2,false);
        }
        private void backButton_Click(object sender, EventArgs e)
        {  if (isSearch == false)
            {
                goBack(filePathTextBox);
                loadButtonAction(filePath1, lastFilePath1, listView1, filePathTextBox,true);
            }
            else
            {
                isFile = false;
                loadButtonAction(filePath1, lastFilePath1, listView1, filePathTextBox,true);
                filePathTextBox.Text = filePath1;
            }
            isSearch = false;
        }

      

       

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            
        }
        private void searchToolStripMenuItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }
        private void changeASequenceOfLetterToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Facade.Delete(filePathTextBox.Text);
            goBack(filePathTextBox);
            loadButtonAction(filePath1, lastFilePath1, listView1, filePathTextBox,true);
        }

        private void rightRegistrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Facade.RightRegister(filePathTextBox.Text);
        }

        private void goToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            string property = toolStripTextBox1.Text;
            search.Clear();
            Facade.Search(search, property, filePathTextBox.Text);
            FileInfo[] files = search.ToArray();
            listView1.Clear();
            Signs(files,listView1);
            isSearch = true;
        }

        private void goToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Facade.ChangeSequenceOfLetter(filePathTextBox.Text, toolStripTextBox2.Text, toolStripTextBox3.Text);
            isFile = true;
        }

        private void goToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (isFile == true)
            {
                goBack(filePathTextBox);
            }
            Facade.Create(filePath1, toolStripTextBox4.Text);
            loadButtonAction(filePath1, lastFilePath1, listView1, filePathTextBox,true); 
        }

        private void back1_Click(object sender, EventArgs e)
        {
            isFile = false;
            goBack(filePathTextBox2);
            loadButtonAction(filePath2, lastFilePath2, listView2, filePathTextBox2, false);
           // filePathTextBox2.Text = filePath2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Facade.CutAndPaste(filePathTextBox.Text, filePathTextBox2.Text);
            isSearch = false;
            isFile = false;
            loadButtonAction(filePath1, lastFilePath1, listView1, filePathTextBox, true);

            loadButtonAction(filePath2, lastFilePath2, listView2, filePathTextBox2, false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Facade.CutAndPaste(filePathTextBox2.Text, filePathTextBox.Text);
            isSearch = false;
            isFile = false;
            loadButtonAction(filePath1, lastFilePath1, listView1, filePathTextBox, true);
            loadButtonAction(filePath2, lastFilePath2, listView2, filePathTextBox, false);
        }
    }
}
