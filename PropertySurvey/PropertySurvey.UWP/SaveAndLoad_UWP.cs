using PropertySurvey;
using PropertySurvey.UWP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(SaveAndLoad_UWP))]

namespace PropertySurvey.UWP
{
    public class SaveAndLoad_UWP : ISaveAndLoad
    {
        #region ISaveAndLoad implementation

        public long FileSize(string filepath)
        {
            FileInfo fi = new FileInfo(GetLocalFilePath(filepath));
            return fi.Length;
        }
        public string GetImageRotation(string fname)
        {
            /*
            ExifInterface newExif = new ExifInterface(CreatePathToFile(fname));
            //newExif.SetAttribute(ExifInterface.TagOrientation, "90");
            return newExif.GetAttribute(ExifInterface.TagOrientation);
            */
            return "90";
        }

        public async Task SaveTextAsync(string filename, string text)
        {
            var path = CreatePathToFile(filename);
            using (StreamWriter sw = File.CreateText(path))
                await sw.WriteAsync(text);
        }

        public async Task<string> LoadTextAsync(string filename)
        {
            var path = CreatePathToFile(filename);
            using (StreamReader sr = File.OpenText(path))
                return await sr.ReadToEndAsync();
        }

        public bool SaveStream(string filename, System.IO.Stream stream)
        {
            try
            {
                using (var fs = new FileStream(CreatePathToFile(filename), FileMode.Create, FileAccess.Write))
                {
                    byte[] bytesInStream = new byte[stream.Length];
                    stream.Read(bytesInStream, 0, bytesInStream.Length);
                    // Use write method to write to the file specified above
                    fs.Write(bytesInStream, 0, bytesInStream.Length);
                    //Close the filestream
                    fs.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }

        public bool SaveBinary(string filename, byte[] byteArray)
        {
            try
            {
                using (var fs = new FileStream(CreatePathToFile(filename), FileMode.Create, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }

        public byte[] LoadBinaryFromDownloads(string filename)
        {
            /*
            string directory = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);

            byte[] buffer;
            FileStream fileStream = new FileStream(Path.Combine(Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads), filename), FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            */
            byte[] buffer = null;
            return buffer;
        }

        public bool SaveBinaryToDownloads(string filename, byte[] byteArray)
        {
            /*
            string directory = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);

            try
            {
                using (var fs = new FileStream(Path.Combine(directory, filename), FileMode.Create, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
            */
            return false;
        }
        public string GetDownloadsFilePath(string filename)
        {
            /*
            string path = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);
            */
            return "null"; // Path.Combine(path, filename);
        }

        public string GetLocalFilePath(string filename)
        {
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return ApplicationData.Current.LocalFolder.Path + "\\" + filename; //Path.Combine(path, filename);
        }

        public byte[] LoadBinaryRange(string filename, int start, int to)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(CreatePathToFile(filename), FileMode.Open, FileAccess.Read);
            try
            {
                int length = to - start;// (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, start + sum, to)) > 0 && (sum + start) < to)
                {
                    sum += count;  // sum is a buffer offset for next reading
                    //to -= count;
                }
                if (sum < length)
                    Array.Resize(ref buffer, sum);
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }
        public byte[] LoadBinary(string filename)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(CreatePathToFile(filename), FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }

        public void DeleteFile(string fname)
        {
            File.Delete(CreatePathToFile(fname));
        }

        public void CreateDirectory(string dir)
        {
            Directory.CreateDirectory(CreatePathToFile(dir));
        }

        public List<string> GetFileList(string directory, string wildc, string prefix = "")
        {
            List<string> filenames = new List<string>();
            try
            {
                DirectoryInfo d = new DirectoryInfo(CreatePathToFile(directory));//Assuming Test is your Folder
                FileInfo[] Files = d.GetFiles(wildc); //Getting Text files

                foreach (FileInfo file in Files)
                {
                    filenames.Add(prefix + file.Name);
                }
                return filenames;
            }
            catch (Exception e)
            {
                return filenames;
            }
        }


        public List<string> GetFileListSegments(string directory, string wildc, string prefix = "")
        {
            List<string> filenames = new List<string>();
            try
            {
                DirectoryInfo d = new DirectoryInfo(CreatePathToFile(directory));//Assuming Test is your Folder
                FileInfo[] Files = d.GetFiles(wildc); //Getting Text files

                foreach (FileInfo file in Files)
                {
                    // Chop into segments and append at the other side
                    int segments = (int)(file.Length / 200000) + 1;
                    for (int i = 0; i < segments; i++)
                    {
                        filenames.Add(prefix + file.Name);
                    }
                }
                return filenames;
            }
            catch (Exception e)
            {
                return filenames;
            }
        }

        public string GetLocalDataPath()
        {
            //return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return ApplicationData.Current.LocalFolder.Path;// + "\\" + filename;
        }

        public bool FileExists(string filename)
        {
            return File.Exists(CreatePathToFile(filename));
        }

        public void CopyFile(string sfname, string dfname)
        {
            File.Copy(CreatePathToFile(sfname), CreatePathToFile(dfname));
        }

        public string CreatePathToFile(string filename)
        {
            //string isthis = ApplicationData.Current.RoamingFolder.Path;
            //var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //return ApplicationData.Current.LocalFolder.Path + "\\" + filename;// Path.Combine(docsPath, filename);
            return ApplicationData.Current.LocalFolder.Path + "\\" + filename;// Path.Combine(docsPath, filename);
        }

        #endregion

    }
}
