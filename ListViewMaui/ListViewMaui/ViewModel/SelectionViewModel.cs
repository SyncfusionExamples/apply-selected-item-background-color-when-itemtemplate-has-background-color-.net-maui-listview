using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListViewMaui
{
    public class SelectionViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<MusicInfo> musicInfo;

        #endregion

        #region Constructor

        public SelectionViewModel()
        {
            GenerateSource();
        }

        #endregion

        #region Properties

        public ObservableCollection<MusicInfo> MusicInfo
        {
            get { return musicInfo; }
            set { this.musicInfo = value; }
        }

        #endregion

        #region Generate Source

        private void GenerateSource()
        {
            var random = new Random();
            musicInfo = new ObservableCollection<MusicInfo>();

            for (int i = 0; i < SongsNames.Count(); i++)
            {
                var info = new MusicInfo()
                {
                    SongTitle = SongsNames[i],
                    SongAuther = SongAuthers[i],
                    SongSize = random.Next(50, 600).ToString() + "." + random.Next(1, 10) / 2 + "KB",
                    SongThumbnail = "songthumbnail.png",
                    SelectedImage = "selected.png",
                    NotSelectedImage = "notselected.png",
                };
                musicInfo.Add(info);
            }
        }

        #endregion

        #region SongInfo

        string[] SongsNames = new string[]
        {
            "Adventure of a lifetime",
            "Blue moon of Kentucky",
            "I don't care if tomorrow never comes",
            "You are the first, my last, my everything",
            "Words don't come easy to me",
            "Everybody's free to wear sunscreen",
            "Before the next teardrop falls",
            "You've lost that lovin' feeling",
            "Underneath your clothes",
            "Try to remember",
            "The hanging tree",
            "Somewhere over the rainbow",
            "Return to innocence",
            "I say a little prayer for you",
            "I believe I can fly",
            "House every weekend",
            "Heal the world",
            "Green green grass of home",
            "God only knows",
            "Five hundred miles",
            "Earth song",
            "Down to the river to pray",
            "Come away with me",
            "Boulevard of broken dreams",
            "Heart is a drum",
            "I'm so lonesome I could cry",
        };

        string[] SongAuthers = new string[]
        {
            "Coldplay",
            "Bill Monroe",
            "Hank Williams & George Jones",
            "Barry White",
            "F. R. David",
            "Baz Luhrmann",
            "Freddy Fender",
            "Righteous Brothers",
            "Shakira",
            "Andy Williams",
            "James Newton Howard ft. Jennifer Lawrence",
            "I. Kamakawiwo'ole",
            "Enigma",
            "Dionne Warwick",
            "R. Kelly",
            "David Zowie",
            "Michael Jackson",
            "Tom Jones",
            "The Beach Boys",
            "The Brothers Four",
            "Michael Jackson",
            "Alison Krauss",
            "Norah Jones",
            "Green Day",
            "Beck",
            "Hank Williams",
        };

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
