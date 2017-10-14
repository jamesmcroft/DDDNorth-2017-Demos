namespace MyEnterpriseUWPApp.Services.Notifications
{
    using System;
    using System.Linq;

    using Microsoft.Toolkit.Uwp.Notifications;

    using Windows.UI.Notifications;

    /// <summary>
    /// Defines a system notification service for Windows devices.
    /// </summary>
    public class NotificationService : INotificationService
    {
        private static int notificationId;

        private static NotificationService current;

        public static NotificationService Current => current ?? (current = new NotificationService());

        /// <inheritdoc />
        public int Show(string title, string body, string action, string iconReference)
        {
            return Show(GenerateNotificationId(), title, body, action, iconReference, null);
        }

        /// <inheritdoc />
        public int Show(
            string title,
            string body,
            string action,
            string iconReference,
            params NotificationButton[] buttons)
        {
            return Show(GenerateNotificationId(), title, body, action, iconReference, buttons);
        }

        /// <inheritdoc />
        public int ShowLongRunningNotification(string title, string iconReference)
        {
            return Show(GenerateNotificationId(), title, string.Empty, null, iconReference, null);
        }

        /// <inheritdoc />
        public int ShowLongRunningNotification(int id, string title, string iconReference)
        {
            return Show(id, title, string.Empty, null, iconReference, null);
        }

        /// <inheritdoc />
        public void ClearNotification(int id)
        {
            try
            {
                ToastNotificationManager.History.Remove(id.ToString());
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif
            }
        }

        private static int Show(
            int id,
            string title,
            string body,
            string action,
            string iconReference,
            params NotificationButton[] buttons)
        {
            ToastVisual visual = SetupToastVisualComponent(title, body, iconReference);

            ToastContent toastContent = new ToastContent { Visual = visual };

            if (!string.IsNullOrWhiteSpace(action?.ToString()))
            {
                toastContent.Launch = action.ToString();
            }

            if (buttons != null && buttons.Any())
            {
                ToastActionsCustom actions = SetupToastActionsComponent(buttons);
                toastContent.Actions = actions;
            }

            ToastNotification toast = new ToastNotification(toastContent.GetXml()) { Tag = id.ToString() };

            ToastNotificationManager.CreateToastNotifier().Show(toast);

            return id;
        }

        private static int GenerateNotificationId()
        {
            if (notificationId == 65535)
            {
                notificationId = 0;
            }

            return notificationId++;
        }

        private static ToastActionsCustom SetupToastActionsComponent(NotificationButton[] buttons)
        {
            ToastActionsCustom actions = new ToastActionsCustom();
            foreach (NotificationButton button in buttons)
            {
                ToastButton toastButton =
                    new ToastButton(button.Label, button.Action.ToString())
                    {
                        ActivationType = ToastActivationType
                                .Foreground
                    };

                if (!string.IsNullOrWhiteSpace(button.IconReference))
                {
                    toastButton.ImageUri = button.IconReference;
                }

                actions.Buttons.Add(toastButton);
            }
            return actions;
        }

        private static ToastVisual SetupToastVisualComponent(string title, string body, string iconReference)
        {
            ToastVisual visual = new ToastVisual();

            ToastBindingGeneric bindingGeneric = new ToastBindingGeneric();

            bindingGeneric.Children.Add(new AdaptiveText { Text = title });

            if (!string.IsNullOrWhiteSpace(body))
            {
                bindingGeneric.Children.Add(new AdaptiveText { Text = body });
            }

            if (!string.IsNullOrWhiteSpace(iconReference))
            {
                bindingGeneric.AppLogoOverride =
                    new ToastGenericAppLogo { Source = iconReference, HintCrop = ToastGenericAppLogoCrop.Circle };
            }

            visual.BindingGeneric = bindingGeneric;
            return visual;
        }
    }
}