using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Klondike
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            sa();
        }
        int a1 = 0;
        
        private void sa()
        {
            
            List<string> arr = new List<string>();
            int i = 0;
            List<string> s = new List<string>();
            StreamReader sr = new StreamReader(new FileStream("rank.txt", FileMode.OpenOrCreate));
            int minus = 0;
            //  string[] s = new string[1000];
            while (sr.EndOfStream == false)
            {
                s.Add(sr.ReadLine());
                string ase = s[i];
                if (s[i].Equals("") == false)
                {
                    arr.Add(s[i].Split()[1]);
           
                }
                
                

                i++;
       
            }

           
            
            sr.Close();
            su(arr, s);
            StreamWriter sw = new StreamWriter(new FileStream("rank.txt", FileMode.Create));
            //minus = 0;
            for (i = 0; i < arr.Count; i++)
            {
               


                //if (s[i].Equals("") == false)
                //{
                    
                    ListViewItem item = new ListViewItem((i + 1).ToString());
                    item.SubItems.Add(s[i].Split()[0]);
                    item.SubItems.Add(s[i].Split()[1]);

                    listView1.Items.Add(item);    
                    sw.WriteLine(s[i]);
                //}
                //else minus++;

            }
            sw.Close();


        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void su(List<String> arr, List<string> s)
        {

            //int test = s.Count;
            //arr.Clear();
            //arr.AddRange(s);
            for (int i = s.Count - 1; i >= 0; i--)
            {
                if (s[i].Equals(""))
                {

                    s.RemoveAt(i);
                }


            }


            for (int x = 0; x < arr.Count; x++)
            {
                for (int i = 0; i < arr.Count - 1; i++)
                {
                    if (Convert.ToInt32(arr[i]) > Convert.ToInt32(arr[i + 1]))
                    {
                        string ar = s[i];
                        s[i] = s[i + 1];
                        s[i + 1] = ar;
                        ar = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = ar;
                    }
                }
            }

            
            
        }
    }
}
