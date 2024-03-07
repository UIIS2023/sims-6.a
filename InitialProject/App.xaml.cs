using InitialProject.Domen.CustomClasses;
using InitialProject.Serializer;
using InitialProject.Services;
using InitialProject.Services.IServices;
using System.Collections.Generic;
using System.Threading;
using System;
using System.Windows;
using System.Windows.Input;
using InitialProject.CustomClasses;

namespace InitialProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        /*
        private Thread _notificationThread;
        private bool _notificationThreadRunning;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _notificationThreadRunning = true;
            _notificationThread = new Thread(NotificationThreadMethod);
            _notificationThread.Start();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            _notificationThreadRunning = false;
            _notificationThread.Join();
        }

        private void NotificationThreadMethod()
        {
            var notificationService = new NotificationService();

            while (_notificationThreadRunning)
            {
                try
                {
                    MessageBox.Show("PROVJERA!");
                    Thread.Sleep(4000); 
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred in notification thread: " + ex.Message);
                }
            }
        }

        private void ShowNotification(Notification notification)
        {
            // Implement logic to show notification to user
            // Example: Use notification data to display a notification in the UI
            // or perform any other action as needed
        }
        */
    }
}