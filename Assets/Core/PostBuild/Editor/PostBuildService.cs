using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
#if UNITY_IOS
#endif

namespace Core.PostBuild.Editor {
	public class PostBuildService {
		// Set the IDFA request description:
		const string k_TrackingDescription = "Your data will be used to provide you a better and personalized ad experience.";
 
		[PostProcessBuild(0)]
		public static void OnPostProcessBuild(BuildTarget buildTarget, string pathToXcode) {
			if (buildTarget == BuildTarget.iOS) {
				AddPListValues(pathToXcode);
			}
		}
 
		// Implement a function to read and write values to the plist file:
		static void AddPListValues(string pathToXcode) {
			// Retrieve the plist file from the Xcode project directory:
			var plistPath = pathToXcode + "/Info.plist";
			var plistObj = new PlistDocument();
 
 
			// Read the values from the plist file:
			plistObj.ReadFromString(File.ReadAllText(plistPath));
 
			// Set values from the root object:
			var plistRoot = plistObj.root;
 
			// Set the description key-value in the plist:
			plistRoot.SetString("NSUserTrackingUsageDescription", k_TrackingDescription);
 
			// Save changes to the plist:
			File.WriteAllText(plistPath, plistObj.WriteToString());
		}
	}
}
