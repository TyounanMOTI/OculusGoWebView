#include <jni.h>
#include <memory>
#include "IUnityGraphics.h"
#include "debug.h"

JavaVM* jvm;
std::unique_ptr<debug> g_debug;
jclass webview_class;
jmethodID update_method;

extern "C" {

void set_debug_log_func(debug::log_func func) {
    g_debug.reset(new debug(func));
    g_debug->log("Debug Log Set!");
}

jint JNI_OnLoad(JavaVM* vm, void* reserved) {
    jvm = vm;
    JNIEnv* env = nullptr;
    vm->AttachCurrentThread(&env, nullptr);

    webview_class = reinterpret_cast<jclass>(env->NewGlobalRef(env->FindClass("com/tyounanmoti/oculus/go/unity/webview/UnityWebView")));
    update_method = env->GetMethodID(webview_class, "update", "()V");

    return JNI_VERSION_1_6;
}

void update_texture(int event_id, void* webview) {
    JNIEnv* env;
    jvm->AttachCurrentThread(&env, nullptr);
    env->CallVoidMethod(reinterpret_cast<jobject>(webview), update_method);
}

UnityRenderingEventAndData get_update_texture_func() {
    return update_texture;
}

}
