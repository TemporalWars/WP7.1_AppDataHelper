using System;
using System.Collections.Generic;
using System.Windows;

namespace BenScharbach.WP7.Helpers.AppDataHelper
{
    /// <summary>
    /// The <see cref="AppHelper"/> class is used to store data between navigation calls.  The class uses the <see cref="Application"/>s
    /// internal Resource dictionary, which is available through the application.
    /// </summary>
    public class AppHelper
    {
        private static readonly AppHelper AppHelperSingleton = new AppHelper();
        private static Application _current;

        #region Private Constructor

        /// <summary>
        /// Default Constructor removed, since singleton.
        /// </summary>
        private AppHelper()
        {
            // Made private since Singleton.
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// Gets the <see cref="AppHelper"/> singleton instance.
        /// </summary>
        public static AppHelper Singleton
        {
            get
            {
                return AppHelperSingleton;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the Application instance from the current application.
        /// </summary>
        /// <param name="application">Use the App.Current to set instance.</param>
        public void SetApplicationInstance(Application application)
        {
            if (application == null) throw new ArgumentNullException("application");
            _current = application;
        }

        /// <summary>
        /// Sets the reusable data-names when calling 'Update' added to the internal Application's recource dictionary.
        /// </summary>
        /// <param name="dataNames">Collection of names to add.</param>
        public void SetDataNames(IList<string> dataNames)
        {
            if (dataNames == null) throw new ArgumentNullException("dataNames");

            // iterate the names and add to the recource dictionary.
            foreach (var dataName in dataNames)
            {
                _current.Resources.Add(dataName, false);
            }
        }

        /// <summary>
        /// Updates the given <paramref name="key"/> with the given <paramref name="value"/>.
        /// </summary>
        /// <param name="key">The string key of item to update.</param>
        /// <param name="value">The item value to update.</param>
        public void Update(string key, object value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("key");
            if (!_current.Resources.Contains(key)) throw new KeyNotFoundException("Key given does not exist in Application's dictionary.");

            // update data location.
            _current.Resources.Remove(key);
           _current.Resources.Add(key, value);
        }

        /// <summary>
        /// Gets the value for the given <paramref name="key"/> from the internal Application's recource dictionary.
        /// </summary>
        /// <typeparam name="TData">The class Type of your item.</typeparam>
        /// <param name="key">The string key of item to retrieve.</param>
        /// <returns>The item value casted to the given Type.</returns>
        public TData Get<TData>(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("key");
            if (!_current.Resources.Contains(key)) throw new KeyNotFoundException("Key given does not exist in Application's dictionary.");

            // get and return the value
            return (TData) _current.Resources[key];
        }

        #endregion

    }
}
