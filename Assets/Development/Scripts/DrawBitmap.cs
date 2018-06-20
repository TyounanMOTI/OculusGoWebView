using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System;
using System.Runtime.InteropServices;

public class DrawBitmap : MonoBehaviour {
#if UNITY_ANDROID
    [DllImport("liboculus_go_webview_native")]
    static extern IntPtr get_update_texture_func();

    delegate void logFunc([In, MarshalAs(UnmanagedType.LPStr)]string log);

    [DllImport("liboculus_go_webview_native", CharSet = CharSet.Ansi)]
    static extern void set_debug_log_func(logFunc func);

    void LogFunc(string message) {
        Debug.LogError(message);
    }

    Texture2D texture;

    void Start() {
        set_debug_log_func(LogFunc);
        Debug.Log("Log func set.");
        texture = new Texture2D(1024, 1024, TextureFormat.ARGB32, false, true);
        GetComponent<Renderer>().material.mainTexture = texture;

        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
            var webview = new AndroidJavaObject("com.tyounanmoti.oculus.go.unity.webview.UnityWebView", currentActivity, texture.GetNativeTexturePtr().ToInt32());
            StartCoroutine(UpdateTexture(webview));
        }
    }

    IEnumerator UpdateTexture(AndroidJavaObject webview) {
        while (true) {
            yield return new WaitForEndOfFrame();
            var commandBuffer = new CommandBuffer();
            commandBuffer.IssuePluginEventAndData(get_update_texture_func(), texture.GetNativeTexturePtr().ToInt32(), webview.GetRawObject());
            Graphics.ExecuteCommandBuffer(commandBuffer);
        }
    }
#endif
}
