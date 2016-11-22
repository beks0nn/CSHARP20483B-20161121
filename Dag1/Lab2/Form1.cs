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
    // objekt att visa i listbox... sparas i dictionary
    public class Animal
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Animal(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }
    }
    public partial class Form1 : Form
    {
        //vårat repository
        Dictionary<Guid, Animal> Animals;
        public Form1()
        {
            InitializeComponent();

            this.listBoxAnimals.DisplayMember = "Name"; //för att visa animal-Name i listbox
            this.listBoxAnimals.ValueMember = "Id"; // value för listbox är Animal-Id
            this.Animals = new Dictionary<Guid, Animal>();
            //starta med en zebra
            var z = new Animal("Zebra");
            this.Animals.Add(z.Id,z);

            //lägg till djuren i listbox
            foreach (var animal in this.Animals.Values)
            {
                this.listBoxAnimals.Items.Add(animal);
            }
        }

        /// <summary>
        /// Lägg till nytt djur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// ta bort valt djur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDel_Click(object sender, EventArgs e)
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
    }
}
