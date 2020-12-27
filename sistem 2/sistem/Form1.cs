using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace sistem
{
   
    public partial class Form1 : Form
    {
        Dictionary<int, string> forbidden;
        Dictionary<int, string> exercises;
        public Form1()
        {
            InitializeComponent();
            
            parserExercises("exercises.txt");
            parserForbidden("forbidden.txt");

        }

        private void parserForbidden(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string line;
            forbidden = new Dictionary<int, string>();
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                forbidden.Add(int.Parse(line.Split('@')[0]), line.Split('@')[1]);
            }
            sr.Close();
        }

        private void parserExercises(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string line;
            exercises = new Dictionary<int, string>();
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                exercises.Add(int.Parse(line.Split('@')[0]), line.Split('@')[1]);
            }
            sr.Close();
        }
      

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> listForbidden = new List<int>();
            List<int> listExercises = new List<int>();
            textBox3.Text = "";
            for (int i = 0; i< checkedListBox1.CheckedIndices.Count; i++)
            {
                int idDisease = checkedListBox1.CheckedIndices[i];
                textBox3.Text += "Выбрана болезнь: " + checkedListBox1.CheckedItems[i] + "\r\n";
                textBox3.Text += "\r\n";
                var list1 = parserDiseaseForbiddenOrExercises("disease forbidden.txt", idDisease);
                textBox3.Text += "Список противопоказанных упражнений к данной болезни:\r\n";
                foreach (int j in list1)
                    textBox3.Text += forbidden[j] + "\r\n";
                textBox3.Text +="\r\n";
                listForbidden = listForbidden.Union(list1).ToList();
                var list2 = parserDiseaseForbiddenOrExercises("disease exercises.txt", idDisease);
                textBox3.Text += "Список рекомендуемых упражнений к данной болезни:\r\n";
                foreach (int j in list2)
                    textBox3.Text += exercises[j] + "\r\n";
                textBox3.Text +="\r\n";
                listExercises = listExercises.Union(list2).ToList();       
            }
            textBox3.Text += "Список рекомендуемых упражнений к выбранным болезням:\r\n";
            foreach (int j in listExercises)
                textBox3.Text += exercises[j] + "\r\n";
            textBox3.Text += "\r\n";
            textBox3.Text += "Список противопоказанных упражнений к выбранным болезням:\r\n";
            foreach (int j in listForbidden)
                textBox3.Text += forbidden[j] + "\r\n";
            textBox3.Text += "\r\n";
            List<int> prohibitedExercises = parserForbiddenOrExercises("forbidden exercises.txt", listForbidden);
            List<int> resultExercises = listExercises.Except(prohibitedExercises).ToList();
            textBox1.Text = "";
            textBox2.Text = "";
            foreach (int i in resultExercises)
                textBox1.Text += exercises[i]+ "\r\n\r\n";
            foreach (int i in listForbidden)
                textBox2.Text += forbidden[i] + "\r\n";
        }

        private List<int> parserForbiddenOrExercises(string filename, List<int> listForbidden)
        {
            StreamReader sr = new StreamReader(filename);
            string line;
            List<int> result = new List<int>();
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                if (listForbidden.Contains(int.Parse(line.Split(' ')[1])))
                {
                    result.Add(int.Parse(line.Split(' ')[0]));
                }
            }
            sr.Close();
            return result;
        }
        private List<int> parserDiseaseForbiddenOrExercises(string filename, int idDisease)
        {
            StreamReader sr = new StreamReader(filename);
            string line;
            List<int> result = new List<int>();
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                if (int.Parse(line.Split(' ')[0])==idDisease)
                {
                    result.Add(int.Parse(line.Split(' ')[1]));

                }
            }
            sr.Close();
            return result;
        }
 


        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

}
