#import "MiniPubSub/ModuleLoader.h"

extern "C"
{
    void __IOSLoadModule(const char* className)
    {
        [[ModuleLoader shared] load: className];
    }
}