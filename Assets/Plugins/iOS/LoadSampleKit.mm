#import <Sample/SampleKitLoader.h>

extern "C"
{
    void __iOSSampleKitLoad()
    {
        [[[SampleKitLoader alloc] init] mount];
    }
}
