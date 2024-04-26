using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PropertySurvey
{
    public interface ISaveAndLoad
    {
        Task SaveTextAsync(string filename, string text);
        Task<string> LoadTextAsync(string filename);

        long FileSize(string filepath);
        string GetImageRotation(string fname);

        bool SaveStream(string filename, System.IO.Stream stream);

        bool SaveBinary(string filename, byte[] bin);

        bool SaveMemoryStream(string filename, MemoryStream ms);
        byte[] LoadBinary(string filename);

        byte[] LoadBinaryRange(string filename, int start, int to);
        bool SaveBinaryToDownloads(string filename, byte[] byteArray);
        byte[] LoadBinaryFromDownloads(string filename);

        string GetLocalFilePath(string filename);

        string GetDownloadsFilePath(string filename);

        List<string> GetFileList(string directory, string wildc, string prefix = "");
        List<string> GetFileListSegments(string directory, string wildc, string prefix = "");

        void CreateDirectory(string dir);
        string CreatePathToFile(string filename);

        bool FileExists(string filename);
        void DeleteFile(string fname);

        void CopyFile(string sfname, string dfname);

        string GetLocalDataPath();
    }
}

