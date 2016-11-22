using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class Song
    {
        public string Artist { get; set; }
        public string Title { get; set; }

        public Guid Id { get; set; }
        public Song(string artist, string title)
        {
            this.Artist = artist;
            this.Title = title;
            this.Id = Guid.NewGuid();
        }

        public string Info { get { return Artist + ", " + Title; } }
    }
    public class SongEventArgs : EventArgs
    {
        public Song Song { get; set; }
    }
    public class JukeBox
    {
        public event EventHandler<SongEventArgs> OnSongAddedEvent;
        public event EventHandler<SongEventArgs> OnSongDeletedEvent;
        public void AddSong(Song s)
        {
            // saving to database.... ;)
            this.OnSongAdded(s);
        }
        public void DeleteSong(Song s)
        {
            // saving to database.... ;)
            this.OnSongDeleted(s);
        }

        public void OnSongAdded(Song s)
        {
            if (OnSongAddedEvent != null)
            {
                OnSongAddedEvent(this, new SongEventArgs { Song = s });
            }
        }
        public void OnSongDeleted(Song s)
        {
            if (OnSongDeletedEvent != null)
            {
                OnSongDeletedEvent(this, new SongEventArgs { Song = s });
            }
        }
    }
}
