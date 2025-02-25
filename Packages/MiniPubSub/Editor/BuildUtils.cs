#if UNITY_EDITOR
using System.IO;

namespace MiniPubSub.Editor
{
    public static class BuildUtils
    {
        public static void CopyDirectory(string sourcePath, string targetPath)
        {
            Directory.CreateDirectory(targetPath);
            
            foreach (string fileName in Directory.EnumerateFiles(sourcePath))
            {
                if (fileName.EndsWith(".meta"))
                {
                    continue;
                }
                string destFile = Path.Combine(targetPath, Path.GetFileName(fileName));
                File.Copy(fileName, destFile, true);
            }
            
            foreach (string subDirName in Directory.EnumerateDirectories(sourcePath))
            {
                string subDir = Path.Combine(targetPath, Path.GetFileName(subDirName));
                CopyDirectory(subDirName, subDir);
            }
        }
    }
}

#endif