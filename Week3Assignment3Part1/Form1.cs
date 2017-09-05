//Paxston Gulledge
//August 22, 2017, Week 3 Assignment 3 Part 1
//Simple program to display current working directory and then list each file and its size
//Writes to a file and reads from the file. 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; //allows for working with files

namespace Week3Assignment3Part1
{
    public partial class Form1 : Form
    {
        private StreamWriter fil; // declare a file string object
        private StreamReader inFile; //Declaring a file stream object

        public Form1()
        {
            InitializeComponent();
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            lstDisplay.Items.Clear();
        }

        private void btnGetFiles_Click(object sender, EventArgs e)
        {
            lstDisplay.Items.Clear();
            //Try to read from current directory
            try
            {
                //Create a new directory class at the current working directory
                DirectoryInfo dir = new DirectoryInfo(".");
                //Update our label to display the information and current directory, and a line break
                lstDisplay.Items.Add("The current directory is " + Directory.GetCurrentDirectory());
                lstDisplay.Items.Add("The files contained inside are:");
                lstDisplay.Items.Add("");
                //For each file in the current directory we are going to add the file name, its size and a line break
                foreach (FileInfo fil in dir.GetFiles("."))
                {
                    lstDisplay.Items.Add(fil.Name);
                    lstDisplay.Items.Add("Size: " + fil.Length.ToString("N0") + "KB");
                }
            }
            //If we couldn't we let them the user know it failed.
            catch
            {
                lstDisplay.Items.Clear();
                lstDisplay.Items.Add("The process failed.");
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            //String to use as our inValue for current line being read
            string inValue;
            //clear our display
            lstDisplay.Items.Clear();
            //If the file is there we will proceed, if not we will let the user know there is no file
            if (File.Exists("fileinfo.txt"))
            {
                //Try catch to make sure we can read the file, if something goes wrong we display an error message.
                try
                {
                    inFile = new StreamReader("fileinfo.txt");
                    //While our string for current line is not null, we will do our actions, null being end of file
                    while ((inValue = inFile.ReadLine()) != null)
                    {
                        //Each line is added to our list box
                        lstDisplay.Items.Add(inValue);
                    }
                    inFile.Close();
                }
                catch (System.IO.IOException exc)
                {
                    lstDisplay.Items.Clear();
                    lstDisplay.Items.Add(exc.Message);
                }
            }
            else
            {
                lstDisplay.Items.Clear();
                lstDisplay.Items.Add("File unavailable");
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                fil = new StreamWriter("fileinfo.txt");
                //For each item in the list box we are going to write it to a new line, that way when we read we can just read by line
                foreach (var listBoxItem in lstDisplay.Items)
                {
                    fil.WriteLine(listBoxItem);
                }
                MessageBox.Show("File info written to file fileinfo.txt");
                fil.Close();
            }
            catch (DirectoryNotFoundException exc)
            {
                lstDisplay.Items.Clear();
                lstDisplay.Items.Add("Invalid directory" + exc.Message);
            }

            catch (System.IO.IOException exc)
            {
                lstDisplay.Items.Clear();
                lstDisplay.Items.Add(exc.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Close the files when we close the form, placed in a try so no error is produced
            //incase no files were messed with 
            try
            {
                fil.Close();
                inFile.Close();
            }
            catch
            {

            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            //Populate listbox with help information
            lstDisplay.Items.Clear();
            lstDisplay.Items.Add("Click Get Files to list the current directory and all files within the directory.");
            lstDisplay.Items.Add("Click Write to File to save the information in the listbox to a file (even this help information!).");
            lstDisplay.Items.Add("Click Read File to display the information located within the text file.");
            lstDisplay.Items.Add("Click click Clear Display to clear all information in this box.");
        }
    }
}
