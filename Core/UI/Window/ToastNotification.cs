using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace TimeX.Core.UI.Window
{
    public class ToastNotification
    {
        private static List<Notification> notifs = new List<Notification>();

        public static void ShowNotification(string Title, string Message)
        {
            Notification not = new Notification()
            {
                Number = notifs.Count,
            };

            not.ShowNotification(Title, Message);
        }

        public static void MoveNotification(Notification notif, Point point, double duration)
        {
            notif.toaster.Dispatcher.BeginInvoke(new Action(() =>
            {
                DoubleAnimation locX = new DoubleAnimation()
                {
                    From = notif.toaster.Left,
                    To = point.X,
                    Duration = TimeSpan.FromMilliseconds(duration),
                };

                DoubleAnimation locY = new DoubleAnimation()
                {
                    From = notif.toaster.Top,
                    To = point.Y,
                    Duration = TimeSpan.FromMilliseconds(duration),
                };

                DoubleAnimation opacityAnim = new DoubleAnimation()
                {
                    From = 0.0f,
                    To = 1.0f,
                    Duration = TimeSpan.FromMilliseconds(duration)
                };

                Storyboard.SetTarget(locX, notif.toaster);
                Storyboard.SetTarget(locY, notif.toaster);
                Storyboard.SetTarget(opacityAnim, notif.toaster);


                Storyboard.SetTargetProperty(locX, new PropertyPath(System.Windows.Window.LeftProperty));
                Storyboard.SetTargetProperty(locY, new PropertyPath(System.Windows.Window.TopProperty));
                Storyboard.SetTargetProperty(opacityAnim, new PropertyPath(UIElement.OpacityProperty));

                Storyboard board = new Storyboard();
                board.Children.Add(locX);
                board.Children.Add(locY);

                board.Children.Add(opacityAnim);

                locY.Completed += (s, g) =>
                {
                    DoubleAnimation opaAnim = new DoubleAnimation()
                    {
                        From = 1.0f,
                        To = 0.0f,
                        Duration = TimeSpan.FromMilliseconds(duration),
                    };

                    notif.toaster.BeginAnimation(UIElement.OpacityProperty, opaAnim);

                };

                board.Begin();
            }), DispatcherPriority.Normal);
        }

        public class Notification
        {
            public ToastWindow.Toast toaster { get; private set; }

            public int Number { get; set; }

            public string Title { get; set; }

            public string Message { get; set; }


            public async void ShowNotification(string title, string message)
            {
                toaster = new ToastWindow.Toast();

                await Task.Run(() =>
                {
                    Title = title;
                    Message = message;

                    toaster.Dispatcher.BeginInvoke(new Action(() => 
                    {
                        toaster.Opacity = 0.0f;
                        toaster.LB_Message.Text = message;
                        toaster.LB_Title.Text = title;

                        toaster.Show();
                    }));

                    MoveNotification(this, new Point(0, 0), 1000);
                });
            }
        }
    }
}
