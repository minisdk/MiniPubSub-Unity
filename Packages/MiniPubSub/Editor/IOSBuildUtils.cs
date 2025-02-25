using System.IO;
using UnityEditor.iOS.Xcode;

namespace MiniPubSub.Editor
{
    public static class IOSBuildUtils
    {
        public static void AddStaticFramework(this PBXProject pbxProject, string pathToBuiltProject, string iosPluginsPath, string frameworkName)
        {
            string unityFrameworkTargetGuid = pbxProject.GetUnityFrameworkTargetGuid();
            
            string xcodeFrameworkPath = Path.Combine("Frameworks", frameworkName);
            string frameworkSourcePath = Path.Combine(iosPluginsPath, frameworkName);
            string frameworkTargetPath = Path.Combine(pathToBuiltProject, xcodeFrameworkPath);
            BuildUtils.CopyDirectory(frameworkSourcePath, frameworkTargetPath);

            string frameworkGuid = pbxProject.AddFile(frameworkTargetPath, xcodeFrameworkPath);
            string frameworkBuildPhaseGuid = pbxProject.GetFrameworksBuildPhaseByTarget(unityFrameworkTargetGuid);
            pbxProject.AddFileToBuildSection(unityFrameworkTargetGuid, frameworkBuildPhaseGuid, frameworkGuid);
        }
    }
}