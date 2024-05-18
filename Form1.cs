using System;
using System.Collections.Generic;
using System.IO;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Lame;

namespace AppTTS
{
    public partial class Form1 : Form
    {
        private SpeechSynthesizer synthesizer;

        public Form1()
        {
            InitializeComponent();
            synthesizer = new SpeechSynthesizer();
            InitializeSpeechSynthesizer();
        }

        private void InitializeSpeechSynthesizer()
        {
            foreach (var voice in synthesizer.GetInstalledVoices())
            {
                var info = voice.VoiceInfo;
                if (info.Gender == VoiceGender.Female && info.Age == VoiceAge.Adult)
                {
                    synthesizer.SelectVoice(info.Name);
                    break;
                }
            }

            // Ajustar la velocidad de la síntesis de voz
            synthesizer.Rate = -8; // Reducir la velocidad de la voz aún más
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var sequence = new List<string>
            {
                "Now_serving_customer.mp3",
                txtCustomer.Text,
                "at_window_number.mp3",
                txtWindow.Text
            };

            string tempFilePath = Path.Combine(Path.GetTempPath(), "temp_output.mp3");
            await CreateCombinedAudioFile(sequence, tempFilePath);
            PlayAudio(tempFilePath);
        }

        private async Task CreateCombinedAudioFile(List<string> sequence, string outputFilePath)
        {
            string tempWavPath = Path.Combine(Path.GetTempPath(), "temp_output.wav");

            using (var waveFileWriter = new WaveFileWriter(tempWavPath, new WaveFormat(44100, 16, 1)))
            {
                foreach (var item in sequence)
                {
                    if (item.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
                    {
                        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, item);
                        await AppendMp3ToWaveFile(filePath, waveFileWriter);
                    }
                    else
                    {
                        AppendTextToWaveFile(item, waveFileWriter);
                        await Task.Delay(500); 
                    }
                }
            }

            // Convertir WAV a MP3
            using (var reader = new AudioFileReader(tempWavPath))
            using (var writer = new LameMP3FileWriter(outputFilePath, reader.WaveFormat, LAMEPreset.VBR_90))
            {
                reader.CopyTo(writer);
            }

            // Eliminar el archivo WAV temporal
            File.Delete(tempWavPath);
        }

        private async Task AppendMp3ToWaveFile(string mp3FilePath, WaveFileWriter waveFileWriter)
        {
            using (var reader = new Mp3FileReader(mp3FilePath))
            using (var resampler = new MediaFoundationResampler(reader, waveFileWriter.WaveFormat))
            {
                resampler.ResamplerQuality = 60; 
                byte[] buffer = new byte[1024];
                int bytesRead;
                while ((bytesRead = resampler.Read(buffer, 0, buffer.Length)) > 0)
                {
                    waveFileWriter.Write(buffer, 0, bytesRead);
                    await Task.Yield(); 
                }
            }
        }

        private void AppendTextToWaveFile(string text, WaveFileWriter waveFileWriter)
        {
            using (var stream = new MemoryStream())
            {
                synthesizer.SetOutputToWaveStream(stream);
                synthesizer.Speak(text);

                stream.Position = 0;
                byte[] buffer = new byte[1024];
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    waveFileWriter.Write(buffer, 0, bytesRead);
                }
            }
        }

        private void PlayAudio(string filePath)
        {
            using (var audioFile = new AudioFileReader(filePath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
        }

        private void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            // Manejar el evento de texto cambiado si es necesario
        }

        private void InitializeComponent()
        {
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.txtWindow = new System.Windows.Forms.TextBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtCustomer
            // 
            this.txtCustomer.Location = new System.Drawing.Point(12, 12);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(260, 20);
            this.txtCustomer.TabIndex = 0;
            this.txtCustomer.PlaceholderText = "Cliente";
            // 
            // txtWindow
            // 
            this.txtWindow.Location = new System.Drawing.Point(12, 38);
            this.txtWindow.Name = "txtWindow";
            this.txtWindow.Size = new System.Drawing.Size(260, 20);
            this.txtWindow.TabIndex = 1;
            this.txtWindow.PlaceholderText = "Ventana";
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(12, 64);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 2;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.txtWindow);
            this.Controls.Add(this.txtCustomer);
            this.Name = "Form1";
            this.Text = "TTS and Audio Player";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.TextBox txtWindow;
        private System.Windows.Forms.Button btnPlay;
    }
}
