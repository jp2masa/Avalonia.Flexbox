using Avalonia.Controls;
using Avalonia.Layout;

namespace Avalonia.Flexbox
{
    public sealed class FlexPanel : Panel
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

        private readonly FlexLayout _layout = new FlexLayout();
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

            _layout[~FlexLayout.DirectionProperty] = this[~DirectionProperty];
            _layout[~FlexLayout.JustifyContentProperty] = this[~JustifyContentProperty];
            _layout[~FlexLayout.AlignItemsProperty] = this[~AlignItemsProperty];
            _layout[~FlexLayout.AlignContentProperty] = this[~AlignContentProperty];
            _layout[~FlexLayout.WrapProperty] = this[~WrapProperty];
            _layout[~FlexLayout.ColumnSpacingProperty] = this[~ColumnSpacingProperty];
            _layout[~FlexLayout.RowSpacingProperty] = this[~RowSpacingProperty];
        }

        public FlexDirection Direction
        {
            get => _layout.Direction;
            set => _layout.Direction = value;
        }

        public JustifyContent JustifyContent
        {
            get => _layout.JustifyContent;
            set => _layout.JustifyContent = value;
        }

        public AlignItems AlignItems
        {
            get => _layout.AlignItems;
            set => _layout.AlignItems = value;
        }

        public AlignContent AlignContent
        {
            get => _layout.AlignContent;
            set => _layout.AlignContent = value;
        }

        public FlexWrap Wrap
        {
            get => _layout.Wrap;
            set => _layout.Wrap = value;
        }

        public double ColumnSpacing
        {
            get => _layout.ColumnSpacing;
            set => _layout.ColumnSpacing = value;
        }

        public double RowSpacing
        {
            get => _layout.RowSpacing;
            set => _layout.RowSpacing = value;
        }
        protected override Size MeasureOverride(Size availableSize) => _layout.Measure(_layoutContext, availableSize);

        protected override Size ArrangeOverride(Size finalSize) => _layout.Arrange(_layoutContext, finalSize);
    }
}
