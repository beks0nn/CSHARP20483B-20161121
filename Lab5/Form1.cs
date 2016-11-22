using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        public JukeBox JukeBox;
        public PlaylistService PlaylistService;
        public Form1()
        {
            InitializeComponent();
            this.listBox1.DisplayMember = "Info";
            this.listBox1.ValueMember = "Id";
            this.JukeBox = new JukeBox();
            this.PlaylistService = new PlaylistService();

            this.PlaylistService.PlaylistUpdatedEvent += PlaylistService_PlaylistUpdatedEvent;
            this.JukeBox.OnSongAddedEvent += this.PlaylistService.OnSongAdded;
            this.JukeBox.OnSongDeletedEvent += this.PlaylistService.OnSongDeleted;
        }

        void PlaylistService_PlaylistUpdatedEvent(object sender, PlayListUpdatedEventArgs e)
        {
            if(e.Action == TakeAction.Add)
                this.listBox1.Items.Add(e.Song);
            else
                this.listBox1.Items.Remove(e.Song);
        }

        private void buttonAddSong_Click(object sender, EventArgs e)
        {
            this.JukeBox.AddSong(new Song(this.textBoxArtist.Text, this.textBoxSongTitle.Text));
        }

        private void buttonPlaySong_Click(object sender, EventArgs e)
        {
            Thread.Sleep(15000);
        }

        private void buttonDeleteSong_Click(object sender, EventArgs e)
        {
            var selected = (Song)this.listBox1.SelectedItem;
            this.JukeBox.DeleteSong(selected);
        }
    }
}
