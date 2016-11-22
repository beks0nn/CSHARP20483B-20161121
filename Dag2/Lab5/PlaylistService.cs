using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public enum TakeAction
    {
        Add, Remove
    }
    public class PlayListUpdatedEventArgs : SongEventArgs
    {
        public TakeAction Action{ get; set; }
    }
    public class PlaylistService
    {
        public event EventHandler<PlayListUpdatedEventArgs> PlaylistUpdatedEvent;
        public void OnSongAdded(object sender, SongEventArgs e)
        {
            MessageBox.Show("Song Added " + e.Song.Artist + ", " +e.Song.Title);
            if (PlaylistUpdatedEvent != null)
            {
                PlaylistUpdatedEvent(this, new PlayListUpdatedEventArgs() { Song = e.Song, Action = TakeAction.Add });
            }
        }

        public void OnSongDeleted(object sender, SongEventArgs e)
        {
            MessageBox.Show("Song Deleted " + e.Song.Artist + ", " + e.Song.Title);
            if (PlaylistUpdatedEvent != null)
            {
                PlaylistUpdatedEvent(this, new PlayListUpdatedEventArgs() { Song = e.Song, Action = TakeAction.Remove });
            }
        }
    }
}
