#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

/// <summary>
/// Ensures Cloud Build never reaches BuildPlayer with an empty Android architecture mask.
/// </summary>
public sealed class AndroidBuildSettings : IPreprocessBuildWithReport
{
    public int callbackOrder => -1000;

    public void OnPreprocessBuild(BuildReport report)
    {
        if (report.summary.platform != BuildTarget.Android)
            return;

        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
        Debug.Log("Paperents Android build: forced ARM64 target architecture.");
    }
}
#endif
