namespace MyEnterpriseUWPApp.Services.Notifications
{
    /// <summary>
    /// Defines an interface for a system toast notification service.
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Shows a system toast notification.
        /// </summary>
        /// <param name="title">
        /// The title of the notification.
        /// </param>
        /// <param name="body">
        /// The body of the notification.
        /// </param>
        /// <param name="action">
        /// The action to run when the notification is pressed.
        /// </param>
        /// <param name="iconReference">
        /// The reference to the icon to display.
        /// </param>
        /// <returns>
        /// Returns a reference integer to the notification.
        /// </returns>
        int Show(string title, string body, string action, string iconReference);

        /// <summary>
        /// Shows a system toast notification and a specified set of action buttons.
        /// </summary>
        /// <param name="title">
        /// The title of the notification.
        /// </param>
        /// <param name="body">
        /// The body of the notification.
        /// </param>
        /// <param name="action">
        /// The action to run when the notification is pressed.
        /// </param>
        /// <param name="iconReference">
        /// The reference to the icon to display.
        /// </param>
        /// <param name="buttons">
        /// The action buttons to display.
        /// </param>
        /// <returns>
        /// Returns a reference integer to the notification.
        /// </returns>
        int Show(string title, string body, string action, string iconReference, params NotificationButton[] buttons);

        /// <summary>
        /// Shows a long running system toast notification.
        /// </summary>
        /// <param name="title">
        /// The title of the notification.
        /// </param>
        /// <param name="iconReference">
        /// The reference to the icon to display.
        /// </param>
        /// <returns>
        /// Returns a reference integer to the notification.
        /// </returns>
        int ShowLongRunningNotification(string title, string iconReference);

        /// <summary>
        /// Shows a long running system toast notification.
        /// </summary>
        /// <param name="id">
        /// The reference integer for the notification.
        /// </param>
        /// <param name="title">
        /// The title of the notification.
        /// </param>
        /// <param name="iconReference">
        /// The reference to the icon to display.
        /// </param>
        /// <returns>
        /// Returns a reference integer to the notification.
        /// </returns>
        int ShowLongRunningNotification(int id, string title, string iconReference);

        /// <summary>
        /// Clears a notification based on the specified notification ID.
        /// </summary>
        /// <param name="id">
        /// The notification ID.
        /// </param>
        void ClearNotification(int id);
    }
}