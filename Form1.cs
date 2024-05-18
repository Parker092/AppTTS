using System;
using System.Windows.Forms;
using NAudio.Wave;
using System.Speech.Synthesis;
using System.Globalization;

namespace AppTTS
{
    public partial class Form1 : Form
    {
        private WaveOutEvent outputDevice;
        private MixingWaveProvider32 mixer;

        public Form1()
        {
            InitializeComponent();
            InitializeAudio();
        }

        private void InitializeAudio()
        {
            outputDevice = new WaveOutEvent();
            outputDevice.PlaybackStopped += OnPlaybackStopped;
            mixer = new MixingWaveProvider32();
            outputDevice.Init(mixer);
        }

        private void AddMp3FileToMixer(string fileName)
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                var reader = new Mp3FileReader(filePath);
                var mp3Stream = new WaveChannel32(reader);
                mixer.AddInputStream(mp3Stream);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load audio file: " + ex.Message);
            }
        }

        private void SpeakText(string text)
        {
            using (var synth = new SpeechSynthesizer())
            {
                synth.SetOutputToDefaultAudioDevice();
                synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult, 0, CultureInfo.GetCultureInfo("es-ES"));
                synth.Speak(text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Recreate the mixer to clear previous audio streams
            mixer = new MixingWaveProvider32();
            outputDevice.Init(mixer);

            // Add local MP3 files to the mixer
            AddMp3FileToMixer("Now_serving_customer.mp3");
            AddMp3FileToMixer("at_window_number.mp3");

            // Play the mixed audio
            outputDevice.Play();
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            // Check if there are texts to be spoken after playback
            this.Invoke((MethodInvoker)delegate
            {
                if (!string.IsNullOrEmpty(txtCustomer.Text))
                {
                    SpeakText(txtCustomer.Text);
                }

                if (!string.IsNullOrEmpty(txtWindow.Text))
                {
                    SpeakText(txtWindow.Text);
                }
            });
        }

        private void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            // Your code here, if needed.
        }
    }
}
