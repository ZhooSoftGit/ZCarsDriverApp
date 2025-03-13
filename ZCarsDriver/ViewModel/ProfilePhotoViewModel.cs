using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class ProfilePhotoViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ImageSource profilePhoto;

        public ICommand UploadPhotoCommand { get; }
        public ICommand TakePhotoCommand { get; }
        public ICommand RemovePhotoCommand { get; }
        public ICommand SaveCommand { get; }

        public ProfilePhotoViewModel()
        {
            UploadPhotoCommand = new Command(async () => await UploadPhotoAsync());
            TakePhotoCommand = new Command(async () => await TakePhotoAsync());
            RemovePhotoCommand = new Command(RemovePhoto);
            SaveCommand = new Command(SaveProfilePhoto);
        }

        private async Task UploadPhotoAsync()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    using var stream = await result.OpenReadAsync();
                    ProfilePhoto = ImageSource.FromStream(() => stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error picking photo: {ex.Message}");
            }
        }

        private async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                if (photo != null)
                {
                    using var stream = await photo.OpenReadAsync();
                    ProfilePhoto = ImageSource.FromStream(() => stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error capturing photo: {ex.Message}");
            }
        }

        private void RemovePhoto()
        {
            ProfilePhoto = null;
        }

        private void SaveProfilePhoto()
        {
            if (ProfilePhoto == null)
            {
                Console.WriteLine("Please upload or capture a photo before saving.");
                return;
            }

            Console.WriteLine("Profile photo saved successfully.");
            // Implement actual save logic (e.g., upload to server)
        }
    }
}
