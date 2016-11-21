using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form1 : Form
    {
        Dictionary<Guid, string> Animals;
        public Form1()
        {
            InitializeComponent();
            this.listBoxAnimals.DisplayMember = "Name"; //visar Name
            this.listBoxAnimals.ValueMember = "Id"; // value är Id
            this.Animals = new Dictionary<Guid, string>();
            this.Animals.Add(Guid.NewGuid(),"Zebra");

            foreach (var animal in this.Animals)
            {
                //bind både UI och Id, se ovan valuemember och displaymember
                this.listBoxAnimals.Items.Add(new { Name = animal.Value, Id = animal.Key });
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var a = new {Id = Guid.NewGuid(), Name=this.textBoxAdd.Text };
            this.Animals.Add(a.Id, a.Name);
            this.listBoxAnimals.Items.Add(a);
            this.textBoxAdd.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dynamic selected = this.listBoxAnimals.SelectedItem;

            if (this.Animals.ContainsKey(selected.Id))
            {
                this.Animals.Remove(selected.Id);
                this.listBoxAnimals.Items.Remove(selected);
            }
        }
    }
}
