using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo10
{
    class Program
    {
        static void Main(string[] args)
        {
            var imageService = new ImageService();

            var smsService = new SmsService();
            imageService.OnImageCompletedEvent += smsService.OnImageCompleted;
            
            var mailService = new MailService();
            imageService.OnImageCompletedEvent += mailService.OnImageCompleted;

            imageService.ProcessImage();
            Console.ReadLine();
        }
    }

    public class ImageCompletedEventArgs : EventArgs
    {
        public string ImageName { get; set; }
        public int ImageSize { get; set; }
    }
    public class ImageService
    {
        public event EventHandler<ImageCompletedEventArgs> OnImageCompletedEvent;
        public void ProcessImage()
        {
            //fake something...
            Thread.Sleep(5000);
            //Notify subscribers
            OnImageCompleted(new ImageCompletedEventArgs { ImageName ="fake.jpg",ImageSize = 100 });  
        }
        public void OnImageCompleted(ImageCompletedEventArgs e)
        {
            if (OnImageCompletedEvent != null)
            {
                OnImageCompletedEvent(this, e);
            }
        }
    }
    public class SmsService
    {
        public void OnImageCompleted(object sender, ImageCompletedEventArgs e)
        {
            Console.WriteLine("SmsService Notified About ImageCompleted Name {0}, Size {1}", e.ImageName, e.ImageSize);
        }
    }
    public class MailService
    {
        public void OnImageCompleted(object sender, ImageCompletedEventArgs e)
        {
            Console.WriteLine("MailService Notified About ImageCompleted Name {0}, Size {1}", e.ImageName, e.ImageSize);
        }
    }
}
