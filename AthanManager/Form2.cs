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
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
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
            WMPLib.WindowsMediaPlayer Player = new WMPLib.WindowsMediaPlayer();
            Player.URL = Application.StartupPath + "\\athan.mp3";
            Player.controls.play();

        }

        private void button1_Click(object sender, EventArgs e)
        {

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
    }
}
