#include <jni.h>
#include <memory>
#include "IUnityGraphics.h"
#include "debug.h"

JavaVM* jvm;
std::unique_ptr<debug> g_debug;
jclass update_texture_class;
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

    update_texture_class = reinterpret_cast<jclass>(env->NewGlobalRef(env->FindClass("com/tyounanmoti/oculus/go/unity/webview/UpdateTextureKt")));
    update_method = env->GetStaticMethodID(update_texture_class, "Update", "(I)V");

    return JNI_VERSION_1_6;
}

void update_texture(int event_id) {
    JNIEnv* env;
    jvm->AttachCurrentThread(&env, nullptr);
    env->CallStaticVoidMethod(update_texture_class, update_method, event_id);
}

UnityRenderingEvent get_update_texture_func() {
    return update_texture;
}

}
