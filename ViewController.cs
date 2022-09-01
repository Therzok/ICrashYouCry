using AppKit;
using ObjCRuntime;

namespace ICrashYouCry;

public partial class ViewController : NSViewController
{
    readonly ButtonViewController buttonViewController;
    protected ViewController(NativeHandle handle) : base(handle)
    {
        buttonViewController = new ButtonViewController();
        AddChildViewController(buttonViewController);
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

        stackView.AddArrangedSubview(buttonViewController.View);
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
            Orientation = NSUserInterfaceLayoutOrientation.Vertical
        };
        scrollView.AddSubview(stackView);

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
