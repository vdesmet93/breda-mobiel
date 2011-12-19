using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using System.IO;
using System.Text;

namespace Model
{
    public class FileManager
    {
        private String path;

        public FileManager()
        {
            
        }

        /// <summary>
        /// /// Saves the specified data at the specified location.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="data">The data which will be stored.</param>
        /// <returns>true if data is saved, false otherwise</returns>
        public bool Save(string fileName, string data)
        {
            try
            {
                var appStorage = IsolatedStorageFile.GetUserStoreForApplication();
                if (!appStorage.FileExists(fileName))
                {
                    using (var file = appStorage.CreateFile(fileName))
                    {
                        using (var writer = new StreamWriter(file))
                        {
                            writer.WriteLine(data);
                        }
                    }
                    return true;
                }
            }
            catch {}
            // failed to save data
            return false;
        }
        /// <summary>
        /// Loads data from the specified location.
        /// </summary>
        /// <param name="location">The location where the data is stored.</param>
        /// <returns>The loaded data</returns>
        public string Load(string location)
        {
            var appStorage = IsolatedStorageFile.GetUserStoreForApplication();
            if (appStorage.FileExists(location))
            {
                using (IsolatedStorageFileStream stream = appStorage.OpenFile(location, FileMode.Open))
                {
                    long length = stream.Length;

                    byte[] decoded = new byte[length];
                    stream.Read(decoded, 0, (int)length);
                    return Encoding.UTF8.GetString(decoded, 0, (int)length);
                }
            }
            // no data
            return "";
        }

        /// <summary>
        /// Loads the file and deletes it.
        /// </summary>
        /// <param name="location">The location of the file.</param>
        /// <returns>The content of the file</returns>
        public string LoadAndDelete(string location)
        {
            string content = Load(location);
            Delete(location);
            return content;
        }

        /// <summary>
        /// Deletes the file at the specified location.
        /// </summary>
        /// <param name="location">The location of the file.</param>
        /// <returns>true if file is deleted</returns>
        public bool Delete(string location)
        {
            try
            {
                var appStorage = IsolatedStorageFile.GetUserStoreForApplication();
                appStorage.DeleteFile(location);
                return true;
            }
            catch { }
            return false;
        }
    
    }
}
