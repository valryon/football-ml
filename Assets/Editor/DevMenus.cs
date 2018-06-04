using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class DevMenus  {

  [MenuItem("FootML/Player mode (test in Unity)")]
  public static void SetPlayer()
  {
    foreach(var b in GameObject.FindObjectsOfType<Brain>())
    {
      b.brainType = BrainType.Player;
    }
    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "");
  }

  [MenuItem("FootML/Set external mode (build, train and learn)")]
  public static void SetExternal()
  {
    foreach (var b in GameObject.FindObjectsOfType<Brain>())
    {
      b.brainType = BrainType.External;
    }
    PlayerSettings.displayResolutionDialog = ResolutionDialogSetting.Disabled;
    PlayerSettings.runInBackground = true;
    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "");
  }

  [MenuItem("FootML/Set internal mode (use a computed graph)")]
  public static void SetInternal()
  {
    foreach (var b in GameObject.FindObjectsOfType<Brain>())
    {
      b.brainType = BrainType.Internal;
    }
    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "ENABLE_TENSORFLOW");
  }
}
