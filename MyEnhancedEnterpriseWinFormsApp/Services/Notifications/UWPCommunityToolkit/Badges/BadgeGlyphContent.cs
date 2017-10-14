﻿// ******************************************************************
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

using System;
using Windows.Data.Xml.Dom;

namespace Microsoft.Toolkit.Uwp.Notifications
{
    /// <summary>
    /// Notification content object to display a glyph on a Tile's badge.
    /// </summary>
    public sealed class BadgeGlyphContent : INotificationContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadgeGlyphContent"/> class.
        /// Default constructor to create a glyph badge content object.
        /// </summary>
        public BadgeGlyphContent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadgeGlyphContent"/> class.
        /// Constructor to create a glyph badge content object with a glyph.
        /// </summary>
        /// <param name="glyph">The glyph to be displayed on the badge.</param>
        public BadgeGlyphContent(BadgeGlyphValue glyph)
        {
            _glyph = glyph;
        }

        /// <summary>
        /// Gets or sets the glyph to be displayed on the badge.
        /// </summary>
        public BadgeGlyphValue Glyph
        {
            get
            {
                return _glyph;
            }

            set
            {
                if (!Enum.IsDefined(typeof(BadgeGlyphValue), value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _glyph = value;
            }
        }

        /// <summary>
        /// Retrieves the notification Xml content as a string.
        /// </summary>
        /// <returns>The notification Xml content as a string.</returns>
        public string GetContent()
        {
            if (!Enum.IsDefined(typeof(BadgeGlyphValue), _glyph))
            {
                throw new NotificationContentValidationException("The badge glyph property was left unset.");
            }

            string glyphString = _glyph.ToString();

            // lower case the first character of the enum value to match the Xml schema
            glyphString = string.Format("{0}{1}", char.ToLowerInvariant(glyphString[0]), glyphString.Substring(1));
            return string.Format("<badge value='{0}'/>", glyphString);
        }

        /// <summary>
        /// Retrieves the notification XML content as a string.
        /// </summary>
        /// <returns>The notification XML content as a string.</returns>
        public override string ToString()
        {
            return GetContent();
        }

        /// <summary>
        /// Retrieves the notification XML content as a WinRT Xml document.
        /// </summary>
        /// <returns>The notification XML content as a WinRT Xml document.</returns>
        public XmlDocument GetXml()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(GetContent());
            return xml;
        }

        private BadgeGlyphValue _glyph = (BadgeGlyphValue)(-1);
    }
}
