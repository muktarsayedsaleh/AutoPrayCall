using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CSCore.CoreAudioAPI;

namespace AthanManager
{
    public partial class Form2 : Form
    {
        private string msg;
        private decimal minutes;
        WMPLib.WindowsMediaPlayer Player;

        public Form2(string msg)
        {
            InitializeComponent();
            this.msg = msg;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = msg;
            // mute all apps.
            using (var sessionManager = GetDefaultAudioSessionManager2(DataFlow.Render))
            {
                try
                {
                    var sessionEnumerator = sessionManager.GetSessionEnumerator();
                    if (sessionEnumerator != null)
                    {
                        foreach (var session in sessionEnumerator)
                        {
                            //MessageBox.Show(session.DisplayName);
                            using (var simpleVolume = session.QueryInterface<SimpleAudioVolume>())
                            {
                                //MessageBox.Show(simpleVolume.ToString());
                                simpleVolume.SetMuteNative(true, new Guid());

                            }
                        }
                    }
                }
                catch (NullReferenceException)
                {
                   
                }
                
            }

            // run athan call from this app
            Player = new WMPLib.WindowsMediaPlayer();
            Player.URL = Application.StartupPath + "\\athan.mp3";
            Player.controls.play();

            try
            {
                minutes = Decimal.Parse(Properties.Settings.Default["minutes"].ToString());
            }
            catch(Exception)
            {
                minutes = 4;
            }
            timer1.Interval = 60000 * (int)minutes;
            timer1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("هل أنت متأكّد؟", "مذكّر أوقات الصلاة الإصدار الأول", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dr == DialogResult.Yes)
            {
                timer1_Tick(sender, e);
            }
        }

        private static AudioSessionManager2 GetDefaultAudioSessionManager2(DataFlow dataFlow)
        {
            using (var enumerator = new MMDeviceEnumerator())
            {
                using (var device = enumerator.GetDefaultAudioEndpoint(dataFlow, Role.Multimedia))
                {
                    //if(device.FriendlyName.Contains("Speakers / Headphones"))
                    //{
                    //    return null;
                    //}
                    //else
                    //{
                        //MessageBox.Show(device.FriendlyName);
                        var sessionManager = AudioSessionManager2.FromMMDevice(device);
                        return sessionManager;
                    //}
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // unmute all apps.
            using (var sessionManager = GetDefaultAudioSessionManager2(DataFlow.Render))
            {
                try
                {
                    var sessionEnumerator = sessionManager.GetSessionEnumerator();
                    if (sessionEnumerator != null)
                    {
                        foreach (var session in sessionEnumerator)
                        {
                            //MessageBox.Show(session.DisplayName);
                            using (var simpleVolume = session.QueryInterface<SimpleAudioVolume>())
                            {
                                //MessageBox.Show(simpleVolume.ToString());
                                simpleVolume.SetMuteNative(false, new Guid());

                            }
                        }
                    }
                }
                catch (NullReferenceException)
                {

                }

                timer1.Enabled = false;
                myVars.isAthanTime = false;
                Player.controls.stop();
                this.Close();
            }
        }
    }
}
