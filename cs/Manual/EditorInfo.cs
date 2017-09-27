using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumix
{
    [AttributeUsage(AttributeTargets.All)]
    public class EditorInfo : Attribute
    {
        string name_;
        string description_;
        bool isVisible_;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public virtual string Name
        {
            get { return name_; }
            set { name_ = value; }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public virtual string Description
        {
            get { return description_; }
            set { description_ = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is visible; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsVisible
        {
            get { return isVisible_; }
            set { isVisible_ = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorInfo"/> class.
        /// </summary>
        /// <param name="_name">The name.</param>
        public EditorInfo()
        {
            name_ = "";
            description_ = "";
            isVisible_ = true;
        }
    }
}
