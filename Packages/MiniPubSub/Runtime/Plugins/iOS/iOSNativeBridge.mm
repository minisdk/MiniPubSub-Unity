#import "MiniPubSub/ObjcSide.h"

extern "C"
{
    void __iOSInitialize(NativeMessageCallback messageCallback)
    {
        [[ObjcSide sharedInstance] initializeWith:messageCallback];
    }

    void __iOSSend(const char* infoCStr, const char* jsonCStr)
    {
        [[ObjcSide sharedInstance] sendToNativeWithInfo:infoCStr andData:jsonCStr];
    }
    
    const char* __iOSSendSync(const char* infoCStr, const char* jsonCStr)
    {
        return [[ObjcSide sharedInstance] sendSyncToNativeWithInfo:infoCStr andData:jsonCStr];
    }
    
    const void __iOSFreeCString(const char* ptr)
    {
        [[ObjcSide sharedInstance] freeCString:ptr];
    }
}
