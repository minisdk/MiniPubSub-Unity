#if UNITY_EDITOR && UNITY_IOS
using MiniPubSub.Editor;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace Sample.Editor
{
    public static class IOSPostProcessBuild
    {
        [PostProcessBuild(1001)]
        public static void PostProcessBuild(BuildTarget target, string pathToBuiltProject)
        {
            if(target == BuildTarget.iOS)
            {
                string pbxProjectPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
                PBXProject pbxProject = new PBXProject();
                pbxProject.ReadFromFile(pbxProjectPath);
                string unityFrameworkTargetGuid = pbxProject.GetUnityFrameworkTargetGuid();
                
                // copy framework
                pbxProject.AddStaticFramework(pathToBuiltProject, "Assets/Plugins/iOS", "sample-1.0.xcframework");
                
                pbxProject.WriteToFile(pbxProjectPath);
            }
        }
    }
}

#endif