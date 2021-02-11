using System;

namespace DeVashe.CluiLib
{
    public partial class SimpleCliMenuItem
    {
        public SimpleCliMenuItem(string label, Action<SimpleCliMenuItem, Clui> handler)
        {
            Label = label;
            Handler = handler;
        }

        public SimpleCliMenuItem(string label, object data, Action<SimpleCliMenuItem, Clui> handler)
        {
            Label = label;
            Data = data;
            Handler = handler;
        }

        /// <summary>
        /// Label for displaying in menus
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// Any data associated with menu item for furhter handling
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// Handler that will be invoked when menu item is selected
        /// </summary>
        public Action<SimpleCliMenuItem, Clui> Handler { get; set; }
    }
}