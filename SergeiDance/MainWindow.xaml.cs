using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace SergeiDance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window1 win2 = new Window1();
            win2.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.IO.File.Delete("audio.mp3");
        }

        private void button_import_audio_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".mp3";
            dialog.Filter = "MP3 Files (*.mp3)|*.mp3";
            dialog.ShowDialog();
            string audioName = dialog.FileName;
            audio_path.Text = audioName;

            string destFile = "audio.mp3";
            string targetPath = "\\Resources\\audio";
            System.IO.Directory.CreateDirectory(targetPath);
            System.IO.File.Copy(audioName, destFile, true);
        }

        private void button_export_video_Click(object sender, RoutedEventArgs e)
        {
            string ffmpegCommand = "/C ffmpeg -stream_loop -1 -i dance_sergei_default.mp4 -c copy -v 0 -f nut - | ffmpeg -thread_queue_size 10K -i - -i audio.mp3 -c copy -map 0:v -map 1:a -shortest -y output.mp4";
            System.Diagnostics.Process.Start("CMD.exe", ffmpegCommand);
            output_info.Text = "Output video saved as output.mp4!";
        }
    }
}
