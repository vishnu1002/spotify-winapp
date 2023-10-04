using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlayer
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // No Form Loader is present
        }

        // Home Button Tab
        private void homeBtn_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(0);
        }

        // Search Button Tab
        private void searchBtn_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(1);
        }

        // Library Button Tab
        private void libraryBtn_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(2);
        }

        string[] paths, files;

        // Import Button 
        private void importBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;


            if(ofd.ShowDialog() == DialogResult.OK)
            {
                files = ofd.FileNames;
                paths = ofd.FileNames;
                for(int i=0; i<files.Length; i++)
                {
                    listBox1.Items.Add(files[i]);

                    var tfile = TagLib.File.Create(files[i]);
                    string title = tfile.Tag.Title;
                    string album = tfile.Tag.Album;
                    TimeSpan duration = tfile.Properties.Duration;

                    // Printing album details in Console
                    Console.WriteLine("\nTitle   : {0}", title);
                    Console.WriteLine("Duration: {0}", (duration.TotalMinutes.ToString()).Remove(4, 2).Replace(".", ":"));
                    Console.WriteLine("Album   : {0}", album);
                }
            }
        }

        // List Box View
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            player.URL = paths[listBox1.SelectedIndex];
            player.Ctlcontrols.play();
        }

        // Play Button
        private void playBtn_Click_1(object sender, EventArgs e)
        {
            player.Ctlcontrols.pause();
        }

        // Next Button 
        private void nextBtn_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex < listBox1.Items.Count - 1)
            {
                listBox1.SelectedIndex += 1;
            }
        }

        // Previous Button
        private void previousBtn_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex > 0)
            {
                listBox1.SelectedIndex -= 1;
            }
        }

        // Volume slider panel
        private void volumeSlider_Scroll(object sender, Utilities.BunifuSlider.BunifuHScrollBar.ScrollEventArgs e)
        {
            player.settings.volume = volumeSlider.Value;
        }

        // Timer component for accessing track play time
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(player.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                songSlider.Maximum = (int)player.Ctlcontrols.currentItem.duration;
                songSlider.Value = (int)player.Ctlcontrols.currentPosition;
            }
            try
            {
                minPlayingTime.Text = player.Ctlcontrols.currentPositionString;
                maxPlayingTime.Text = player.Ctlcontrols.currentItem.durationString.ToString();
            }
            catch
            {
                
            }
        }
    }
}
