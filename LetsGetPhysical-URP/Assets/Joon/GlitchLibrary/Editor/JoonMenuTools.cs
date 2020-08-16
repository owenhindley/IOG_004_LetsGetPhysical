using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEditor;
using Debug = UnityEngine.Debug;

public class JoonMenuTools {
	[MenuItem("Joon/Align/Align Scene To Camera")]
    public static void AlignToCamera()
     {
        var mainCamera = Camera.main;
        //SceneView.lastActiveSceneView.pivot = target.position;
        SceneView.lastActiveSceneView.rotation = mainCamera.transform.rotation;
        SceneView.lastActiveSceneView.Repaint ();
     }

    [MenuItem("Joon/Align/Align Scene To Selection (Back)")]
    public static void AlignToSelectionBack()
     {
        var mainCamera = Camera.main;
        //SceneView.lastActiveSceneView.pivot = target.position;
        var go = Selection.activeObject as GameObject;
        if(go != null)
        {
            SceneView.lastActiveSceneView.rotation = go.transform.rotation;
            SceneView.lastActiveSceneView.Repaint ();
        }
     }

    [MenuItem("Joon/Align/Align Scene To Selection (Front)")]
    public static void AlignToSelectionFront()
    {
        var mainCamera = Camera.main;
        //SceneView.lastActiveSceneView.pivot = target.position;
        var go = Selection.activeObject as GameObject;
        if(go != null)
        {
            SceneView.lastActiveSceneView.rotation = go.transform.rotation * Quaternion.Euler(go.transform.up * 180);
            SceneView.lastActiveSceneView.Repaint ();
        }
    }
    
    [MenuItem("Joon/Align/Align Scene To Selection (Left)")]
    public static void AlignToSelectionLeft()
    {
        var mainCamera = Camera.main;
        //SceneView.lastActiveSceneView.pivot = target.position;
        var go = Selection.activeObject as GameObject;
        if(go != null)
        {
            SceneView.lastActiveSceneView.rotation = Quaternion.LookRotation(go.transform.right, go.transform.up) ;
            SceneView.lastActiveSceneView.Repaint ();
        }
    }

    [MenuItem("Joon/Align/Align Scene To Selection (Right)")]
    public static void AlignToSelectionRight()
    {
        var mainCamera = Camera.main;
        //SceneView.lastActiveSceneView.pivot = target.position;
        var go = Selection.activeObject as GameObject;
        if(go != null)
        {
            SceneView.lastActiveSceneView.rotation = Quaternion.LookRotation(-go.transform.right, go.transform.up) ;
            SceneView.lastActiveSceneView.Repaint ();
        }
    }
    
    [MenuItem("Joon/Align/Align Scene To Selection (Top)")]
    public static void AlignToSelectionTop()
    {
        var mainCamera = Camera.main;
        //SceneView.lastActiveSceneView.pivot = target.position;
        var go = Selection.activeObject as GameObject;
        if(go != null)
        {
            SceneView.lastActiveSceneView.rotation = Quaternion.LookRotation(-go.transform.up, go.transform.right) ;
            SceneView.lastActiveSceneView.Repaint ();
        }
    }        
    
    [MenuItem("Joon/Android/List Devices")]
    public static void AndroidListDevices()
    {
        var thread = new Thread(delegate () {Command("adb devices");});
        thread.Start();
    }   
    
    [MenuItem("Joon/Android/Uninstall")]
    public static void AndroidUninstall()
    {
        Debug.Log("Uninstalling");
        var id = Application.identifier;
        var thread = new Thread(delegate () {Command("adb uninstall " + id);});
        thread.Start();
    }   
    
    [MenuItem("Joon/Android/Reinstall last")]
    public static void AndroidReinstall()
    {
        Debug.Log("Reinstalling");
        var path = Application.dataPath + "/../";
        var thread = new Thread(delegate () {Command("adb install Build/nuts_android.apk",  path);});
        thread.Start();
    }
    
    [MenuItem("Joon/Git/SSH Initialize")]
    public static void SSHInit()
    {
        Debug.Log("Init Git");
        var thread = new Thread(delegate () {Command("ssh-add -A");});
        thread.Start();
    }
    
    static void Command (string input, string path="/")
    {
        //Debug.Log("Executing " + input);
        var processInfo = new ProcessStartInfo("/bin/bash");
        processInfo.WorkingDirectory = path;
        processInfo.CreateNoWindow = false;
        processInfo.WindowStyle = ProcessWindowStyle.Normal;
        processInfo.UseShellExecute = false;
        processInfo.RedirectStandardInput = true;
        processInfo.RedirectStandardOutput = true;
        processInfo.RedirectStandardError = true;
        
        var process = new Process();
        process.OutputDataReceived += (sender, args) => Debug.Log(args.Data.ToString());
        process.Exited += (sender, args) => Debug.Log("exited");  
        process.ErrorDataReceived += (sender, args) => Debug.Log(args.Data.ToString());
        process.StartInfo = processInfo;
        process.Start();
        
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        process.StandardInput.WriteLine (@"PATH=/opt/local/bin:/opt/local/sbin:/usr/local/bin:/usr/bin:/bin:/usr/sbin:/sbin:/usr/local/share/dotnet:~/.dotnet/tools:/Library/Frameworks/Mono.framework/Versions/
       ");
        process.StandardInput.WriteLine(input);
        process.StandardInput.WriteLine("exit");  // if no exit then WaitForExit will lockup your program
        process.StandardInput.Flush();
        
        process.WaitForExit();
        process.Close();
    }

    
 }