#import <sample/sample-Swift.h>

extern "C"
{
    void __iOSSampleKitLoad()
    {
        [[SampleKit shared] prepare];
    }
}
