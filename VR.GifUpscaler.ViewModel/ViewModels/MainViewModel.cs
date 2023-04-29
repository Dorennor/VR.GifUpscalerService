using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VR.GifUpscaler.ViewModel.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        public ImageSource _gifFrame;

        [RelayCommand]
        public async void RunGif()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var gifPath = currentDirectory + "\\Pigs.gif";

            Stream imageStreamSource = new FileStream(gifPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            GifBitmapDecoder decoder = new GifBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);

            //GifFrame = decoder.Frames[0];

            for (int i = 0; i < decoder.Frames.Count(); i++)
            {
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    GifFrame = decoder.Frames[i];
                    Thread.Sleep(30);
                }, DispatcherPriority.Background);
            }
        }
    }
}