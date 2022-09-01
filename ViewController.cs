using AppKit;
using ObjCRuntime;

namespace ICrashYouCry;

public partial class ViewController : NSViewController
{
    readonly ButtonViewController buttonViewControllerManaged = new ButtonViewControllerManaged();
    readonly ButtonViewController buttonViewControllerNative = new ButtonViewControllerNative();
    readonly ButtonViewController buttonViewControllerNativeAuto = new ButtonViewControllerNativeAuto();
    protected ViewController(NativeHandle handle) : base(handle)
    {
    }

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        var view = View;
        var safeArea = view.SafeAreaLayoutGuide;

        var (scrollView, stackView) = CreateContent(safeArea);
        view.AddSubview(scrollView);

        // Fill scrollview to container
        scrollView.LeadingAnchor.ConstraintEqualTo(safeArea.LeadingAnchor).Active = true;
        scrollView.TrailingAnchor.ConstraintEqualTo(safeArea.TrailingAnchor).Active = true;
        scrollView.TopAnchor.ConstraintEqualTo(safeArea.TopAnchor).Active = true;
        scrollView.BottomAnchor.ConstraintEqualTo(safeArea.BottomAnchor).Active = true;

        stackView.WidthAnchor.ConstraintEqualTo(safeArea.WidthAnchor).Active = true;

        AddChildViewController(buttonViewControllerManaged);
        stackView.AddArrangedSubview(buttonViewControllerManaged.View);

        AddChildViewController(buttonViewControllerNative);
        stackView.AddArrangedSubview(buttonViewControllerNative.View);

        AddChildViewController(buttonViewControllerNativeAuto);
        stackView.AddArrangedSubview(buttonViewControllerNativeAuto.View);
    }

    static (NSScrollView, NSStackView) CreateContent(NSLayoutGuide safeArea)
    {
        var scrollView = new NSScrollView
        {
            TranslatesAutoresizingMaskIntoConstraints = false,
        };

        var stackView = new NSStackView
        {
            TranslatesAutoresizingMaskIntoConstraints = false,
            Orientation = NSUserInterfaceLayoutOrientation.Vertical,
            Spacing = 0,
        };
        scrollView.DocumentView = stackView;

        // Fill stackview to container, but constrain to safe area
        stackView.LeadingAnchor.ConstraintEqualTo(scrollView.LeadingAnchor).Active = true;
        stackView.TrailingAnchor.ConstraintEqualTo(scrollView.TrailingAnchor).Active = true;
        stackView.TopAnchor.ConstraintEqualTo(scrollView.TopAnchor).Active = true;
        stackView.BottomAnchor.ConstraintEqualTo(scrollView.BottomAnchor).Active = true;

        return (scrollView, stackView);
    }

    public override NSObject RepresentedObject
    {
        get => base.RepresentedObject;
        set
        {
            base.RepresentedObject = value;

            // Update the view, if already loaded.
        }
    }
}
