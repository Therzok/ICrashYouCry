using System;
using AppKit;

namespace ICrashYouCry
{
    public abstract class ButtonViewController : NSViewController
    {
        public override void LoadView()
        {
            // base.LoadView(); This causes a native exception

            // Create a simple button
            var button = new NSButton(new CGRect(0, 0, 40, 50))
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Title = "dotnet sauce",
                Target = this,
                Action = new ObjCRuntime.Selector("clicked:"),
            };

            View = button;
        }

        [Export("clicked:")]
        public abstract void Clicked(NSObject sender);
    }

    [Register("ButtonViewControllerNativeAuto")]
    public class ButtonViewControllerNativeAuto : ButtonViewController
    {
        public override void LoadView()
        {
            base.LoadView();

            var btn = (NSButton)View;
            btn.Title = "Crash native autorelease";
        }

        public override void Clicked(NSObject sender)
        {
            View.Superview.DangerousAutorelease();
        }
    }

    [Register("ButtonViewControllerNative")]
    public class ButtonViewControllerNative : ButtonViewController
    {
        public override void LoadView()
        {
            base.LoadView();

            var btn = (NSButton)View;
            btn.Title = "Crash native";
        }

        public override void Clicked(NSObject sender)
        {
            View.Superview.DangerousRelease();
        }
    }

    [Register("ButtonViewControllerManaged")]
    public class ButtonViewControllerManaged : ButtonViewController
    {
        public override void LoadView()
        {
            base.LoadView();

            var btn = (NSButton)View;
            btn.Title = "Crash managed";
        }

        public override void Clicked(NSObject sender)
        {
            new Thread(() => { throw new Exception(); }).Start();
        }
    }
}

