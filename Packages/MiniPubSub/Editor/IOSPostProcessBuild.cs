#if UNITY_IOS && UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;


namespace MiniPubSub.Editor
{
    public static class IOSPostProcessBuild
    {
        [PostProcessBuild(1000)]
        public static void PostProcessBuild(BuildTarget target, string pathToBuiltProject)
        {
            if (target == BuildTarget.iOS)
            {
                string pbxProjectPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
                PBXProject pbxProject = new PBXProject();
                pbxProject.ReadFromFile(pbxProjectPath);
                
                string unityFrameworkTargetGuid = pbxProject.GetUnityFrameworkTargetGuid();
                
                // copy framework
                pbxProject.AddStaticFramework(pathToBuiltProject, "Packages/MiniPubSub/Runtime/Plugins/iOS", "MiniPubSub-1.0.xcframework");

                // enable swift runtime
                pbxProject.SetBuildProperty(unityFrameworkTargetGuid, "SWIFT_VERSION", "5.0");
                pbxProject.SetBuildProperty(unityFrameworkTargetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");
                
                pbxProject.WriteToFile(pbxProjectPath);
            }
        }

        
    }
    
}

#endif