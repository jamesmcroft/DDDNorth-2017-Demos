namespace MyEnterpriseUWPApp.Services.Notifications
{
    /// <summary>
    /// Defines an action button to be used with toast notifications.
    /// </summary>
    public class NotificationButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationButton"/> class.
        /// </summary>
        /// <param name="label">
        /// The label for the button.
        /// </param>
        /// <param name="action">
        /// The action to run when the button is pressed.
        /// </param>
        /// <param name="iconReference">
        /// The reference to the icon to display.
        /// </param>
        public NotificationButton(string label, string action, string iconReference)
        {
            this.Label = label;
            this.Action = action;
            this.IconReference = iconReference;
        }

        /// <summary>
        /// Gets the action to run when the button is pressed.
        /// </summary>
        public string Action { get; }

        /// <summary>
        /// Gets the reference to the icon to display.
        /// </summary>
        public string IconReference { get; }

        /// <summary>
        /// Gets the label for the button.
        /// </summary>
        public string Label { get; }
    }
}