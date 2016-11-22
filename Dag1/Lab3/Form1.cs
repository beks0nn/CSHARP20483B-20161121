using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{

    public partial class Form1 : Form
    {
        //vårat repository
        Dictionary<Guid, Animal> Animals;
        public Form1()
        {
            InitializeComponent();
            this.listBoxAnimals.DisplayMember = "Name"; //för att visa animal-Name i listbox
            this.listBoxAnimals.ValueMember = "Id"; // value för listbox är Animal-Id
            this.Animals = this.Animals.ReadFromDisk();

            //lägg till djuren i listbox
            foreach (var animal in this.Animals.Values)
            {
                this.listBoxAnimals.Items.Add(animal);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (this.textBoxAdd.Text == string.Empty)
            {
                MessageBox.Show("You have to provide a name for the animal!", "Error");
                return;
            }
            var a = new Animal(this.textBoxAdd.Text);
            this.Animals.Add(a.Id, a);
            this.listBoxAnimals.Items.Add(a);
            this.textBoxAdd.Text = string.Empty;

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Animal selected = (Animal)this.listBoxAnimals.SelectedItem;
            if (selected == null)
            {
                MessageBox.Show("You have to select something to delete!", "Error");
                return;
            }
            if (this.Animals.ContainsKey(selected.Id))
            {
                this.Animals.Remove(selected.Id);
                this.listBoxAnimals.Items.Remove(selected);
            }

        }

        private void onClosing(object sender, FormClosingEventArgs e)
        {
            this.Animals.SaveToDisk();
        }
    }
}
