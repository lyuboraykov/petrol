namespace PetrolWindowsPhone
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Windows.Data.Xml.Dom;
    using Windows.UI.Notifications;
    using Windows.UI.Popups;

   public class ToastManager
    {
       private ToastTemplateType toastTemplate;
       private XmlDocument toastTemplateXml;

       public ToastManager()
       {
           this.toastTemplate = ToastTemplateType.ToastText02;
           this.toastTemplateXml = ToastNotificationManager.GetTemplateContent(this.toastTemplate);
       }

       public void SendToast(string heading, string content)
       {
           this.FillToast(heading, content);
           ToastNotification toast = new ToastNotification(this.toastTemplateXml);
           toast.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(3600);
           ToastNotificationManager.CreateToastNotifier().Show(toast);
       }

       public  void SendScheduledToast(string heading, string content, DateTimeOffset trigger)
       {
           this.FillToast(heading, content);
           ScheduledToastNotification toast = new ScheduledToastNotification(this.toastTemplateXml, trigger);
           ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
          //MessageDialog msg = new MessageDialog("Известието и запазено.");
          //await msg.ShowAsync();

       }

       private void FillToast(string heading, string content)
       {
           XmlNodeList toastTextElements = this.toastTemplateXml.GetElementsByTagName("text");
           toastTextElements[0].AppendChild(this.toastTemplateXml.CreateTextNode(heading));
           toastTextElements[1].AppendChild(this.toastTemplateXml.CreateTextNode(content));

           IXmlNode toastNode = this.toastTemplateXml.SelectSingleNode("/toast");
           ((XmlElement)toastNode).SetAttribute("duration", "long");
       }
    }
}
