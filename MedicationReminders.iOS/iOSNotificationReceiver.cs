using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using UserNotifications;
using Xamarin.Forms;

namespace MedicationReminders.iOS
{
    public class iOSNotificationReceiver : UNUserNotificationCenterDelegate
    {
        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            ProcessNotification(notification);
            completionHandler(UNNotificationPresentationOptions.Alert);
        }

        void ProcessNotification(UNNotification notification)
        {
            string title = notification.Request.Content.Title;
            string message = notification.Request.Content.Body;

            DependencyService.Get<INotificationManager>().ReceiveNotification(title, message);
        }
    }
}