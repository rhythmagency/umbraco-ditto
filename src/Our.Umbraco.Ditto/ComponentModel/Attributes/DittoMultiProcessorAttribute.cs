﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Our.Umbraco.Ditto
{
    /// <summary>
    /// Represents a multi-ditto processor capable of wrapping multiple attributes into a single attribute definition
    /// </summary>
    [AttributeUsage(Ditto.ProcessorAttributeTargets, AllowMultiple = true, Inherited = false)]
    [DittoProcessorMetaData(ValueType = typeof(object), ContextType = typeof(DittoMultiProcessorContext))]
    public abstract class DittoMultiProcessorAttribute : DittoProcessorAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DittoMultiProcessorAttribute" /> class.
        /// </summary>
        protected DittoMultiProcessorAttribute()
        {
            this.Attributes = new List<DittoProcessorAttribute>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DittoMultiProcessorAttribute" /> class.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        protected DittoMultiProcessorAttribute(IEnumerable<DittoProcessorAttribute> attributes)
            : this()
        {
            this.Attributes.AddRange(attributes);
        }

        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        protected List<DittoProcessorAttribute> Attributes { get; set; }

        /// <summary>
        /// Processes the value.
        /// </summary>
        /// <returns>
        /// The <see cref="object" /> representing the processed value.
        /// </returns>
        public override object ProcessValue()
        {
            var ctx = (DittoMultiProcessorContext)this.Context;

            foreach (var processorAttr in this.Attributes)
            {
                // Process value, Pass MultiProcessor as context
                this.Value = processorAttr.ProcessValue(this.Value, ctx);
            }

            return this.Value;
        }
    }
}