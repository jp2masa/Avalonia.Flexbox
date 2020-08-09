using Avalonia.Controls;
using Avalonia.Layout;

namespace Avalonia.Flexbox
{
    public sealed class FlexPanel : Panel, IFlexLayout
    {
        public static readonly StyledProperty<FlexDirection> DirectionProperty =
            AvaloniaProperty.Register<FlexPanel, FlexDirection>(nameof(Direction));

        public static readonly StyledProperty<JustifyContent> JustifyContentProperty =
            AvaloniaProperty.Register<FlexPanel, JustifyContent>(nameof(JustifyContent));

        public static readonly StyledProperty<AlignItems> AlignItemsProperty =
            AvaloniaProperty.Register<FlexPanel, AlignItems>(nameof(AlignItems));

        public static readonly StyledProperty<AlignContent> AlignContentProperty =
            AvaloniaProperty.Register<FlexPanel, AlignContent>(nameof(AlignContent));

        public static readonly StyledProperty<FlexWrap> WrapProperty =
            AvaloniaProperty.Register<FlexLayout, FlexWrap>(nameof(Wrap), FlexWrap.Wrap);

        public static readonly StyledProperty<double> ColumnSpacingProperty =
            AvaloniaProperty.Register<FlexPanel, double>(nameof(ColumnSpacing));

        public static readonly StyledProperty<double> RowSpacingProperty =
            AvaloniaProperty.Register<FlexPanel, double>(nameof(RowSpacing));

        private readonly NonVirtualizingLayoutContext _layoutContext;

        static FlexPanel()
        {
            AffectsMeasure<FlexPanel>(
                DirectionProperty,
                JustifyContentProperty,
                AlignItemsProperty,
                AlignContentProperty,
                WrapProperty,
                ColumnSpacingProperty,
                RowSpacingProperty);
        }

        public FlexPanel()
        {
            _layoutContext = new PanelNonVirtualizingLayoutContext(this);
        }

        public FlexDirection Direction
        {
            get => GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }

        public JustifyContent JustifyContent
        {
            get => GetValue(JustifyContentProperty);
            set => SetValue(JustifyContentProperty, value);
        }

        public AlignItems AlignItems
        {
            get => GetValue(AlignItemsProperty);
            set => SetValue(AlignItemsProperty, value);
        }

        public AlignContent AlignContent
        {
            get => GetValue(AlignContentProperty);
            set => SetValue(AlignContentProperty, value);
        }

        public FlexWrap Wrap
        {
            get => GetValue(WrapProperty);
            set => SetValue(WrapProperty, value);
        }

        public double ColumnSpacing
        {
            get => GetValue(ColumnSpacingProperty);
            set => SetValue(ColumnSpacingProperty, value);
        }

        public double RowSpacing
        {
            get => GetValue(RowSpacingProperty);
            set => SetValue(RowSpacingProperty, value);
        }

        protected override Size MeasureOverride(Size availableSize) => FlexLayout.Measure(this, _layoutContext, availableSize);

        protected override Size ArrangeOverride(Size finalSize) => FlexLayout.Arrange(this, _layoutContext, finalSize);
    }
}
