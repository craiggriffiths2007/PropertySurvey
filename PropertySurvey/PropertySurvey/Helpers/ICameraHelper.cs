using System.IO;

namespace PropertySurvey
{
    public interface ICameraHelper
    {
        string GetIdentifier();
        byte[] GenerateThumbImage(string path, int usecond);
        void StartVideoCamera();
        void StartCamera();
        void StartCameraLandscape();
        void StartCameraOrientated();
        void StartCameraLandscapeFixed();
    }
}
