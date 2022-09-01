using System;
using AppKit;

namespace ICrashYouCry
{
    [Register("ButtonViewController")]
    public class ButtonViewController : NSViewController
    {
        public override void LoadView()
        {
            base.LoadView();

            // Create a simple button
            var button = new NSButton
            {
                Title = "dotnet sauce",
                Target = this,
                Action = new ObjCRuntime.Selector("clicked:"),
            };

            View = button;
        }

        [Export("clicked:")]
        public void Clicked(NSObject sender)
        {
            // Triggers a crash at the end of the UI loop, in native.
            View.Superview.DangerousAutorelease();
        }
    }
}

