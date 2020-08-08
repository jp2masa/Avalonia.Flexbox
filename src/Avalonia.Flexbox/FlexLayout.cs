using System;
using System.Collections.Generic;
using System.Linq;

using Avalonia.Layout;

namespace Avalonia.Flexbox
{
    public sealed partial class FlexLayout : NonVirtualizingLayout
    {
        public static readonly StyledProperty<FlexDirection> DirectionProperty =
            AvaloniaProperty.Register<FlexLayout, FlexDirection>(nameof(FlexDirection));

        public static readonly StyledProperty<JustifyContent> JustifyContentProperty =
            AvaloniaProperty.Register<FlexLayout, JustifyContent>(nameof(JustifyContent));

        public static readonly StyledProperty<AlignItems> AlignItemsProperty =
            AvaloniaProperty.Register<FlexLayout, AlignItems>(nameof(AlignItems));

        public static readonly StyledProperty<AlignContent> AlignContentProperty =
            AvaloniaProperty.Register<FlexLayout, AlignContent>(nameof(AlignContent));

        public static readonly StyledProperty<FlexWrap> WrapProperty =
            AvaloniaProperty.Register<FlexLayout, FlexWrap>(nameof(Wrap), FlexWrap.Wrap);

        public static readonly StyledProperty<double> ColumnSpacingProperty =
            AvaloniaProperty.Register<FlexLayout, double>(nameof(ColumnSpacing));

        public static readonly StyledProperty<double> RowSpacingProperty =
            AvaloniaProperty.Register<FlexLayout, double>(nameof(RowSpacing));

        static FlexLayout()
        {
            AffectsMeasure(
                DirectionProperty,
                JustifyContentProperty,
                AlignItemsProperty,
                AlignContentProperty,
                WrapProperty,
                ColumnSpacingProperty,
                RowSpacingProperty);
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

        protected override Size MeasureOverride(NonVirtualizingLayoutContext context, Size availableSize)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var isColumn = Direction == FlexDirection.Column || Direction == FlexDirection.ColumnReverse;
            var even = JustifyContent == JustifyContent.SpaceEvenly ? 2 : 0;

            var max = Uv.FromSize(availableSize, isColumn);
            var spacing = Uv.FromSize(ColumnSpacing, RowSpacing, isColumn);

            var u = 0.0;
            var maxU = 0.0;
            var m = 0;

            var v = 0.0;
            var maxV = 0.0;
            var n = 0;

            var sections = new List<Section>();
            var first = 0;

            var i = 0;

            foreach (var element in context.Children)
            {
                element.Measure(availableSize);

                var size = Uv.FromSize(element.DesiredSize, isColumn);

                if (Wrap != FlexWrap.NoWrap && u + size.U + (m + even) * spacing.U > max.U)
                {
                    sections.Add(new Section(first, i - 1, u, maxV));

                    if (u > maxU)
                    {
                        maxU = u;
                    }

                    u = 0.0;
                    m = 0;

                    v += maxV;
                    maxV = 0.0;
                    n++;

                    first = i;
                }

                if (size.V > maxV)
                {
                    maxV = size.V;
                }

                u += size.U;
                m++;

                i++;
            }

            if (m != 0)
            {
                sections.Add(new Section(first, first + m - 1, u, maxV));

                if (u > maxU)
                {
                    maxU = u;
                }
            }

            if (Wrap == FlexWrap.WrapReverse)
            {
                sections.Reverse();
            }

            context.LayoutState = new FlexLayoutState(sections);

            return Uv.ToSize(new Uv(maxU + sections.Max(s => s.Last - s.First) * spacing.U, v + maxV + (sections.Count - 1) * spacing.V), isColumn);
        }

        protected override Size ArrangeOverride(NonVirtualizingLayoutContext context, Size finalSize)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var isColumn = Direction == FlexDirection.Column || Direction == FlexDirection.ColumnReverse;
            var isReverse = Direction == FlexDirection.RowReverse || Direction == FlexDirection.ColumnReverse;

            var state = (FlexLayoutState)context.LayoutState;
            var n = state.Sections.Count;

            var size = Uv.FromSize(finalSize, isColumn);
            var spacing = Uv.FromSize(ColumnSpacing, RowSpacing, isColumn);

            var totalSectionV = 0.0;

            foreach (var section in state.Sections)
            {
                totalSectionV += section.V;
            }

            var totalSpacingV = (n - 1) * spacing.V;

            var totalV = totalSectionV + totalSpacingV;

            var spacingV = AlignContent switch
            {
                AlignContent.FlexStart => spacing.V,
                AlignContent.FlexEnd => spacing.V,
                AlignContent.Center => spacing.V,
                AlignContent.SpaceBetween => spacing.V + (size.V - totalV) / (n - 1),
                AlignContent.SpaceAround => (size.V - totalSectionV) / n,
                AlignContent.SpaceEvenly => (size.V - totalSectionV) / (n + 1),
                _ => throw new NotImplementedException()
            };

            var scaleV = AlignContent == AlignContent.Stretch ? ((size.V - totalSpacingV) / totalSectionV) : 1.0;

            var v = AlignContent switch
            {
                AlignContent.FlexStart => 0.0,
                AlignContent.FlexEnd => size.V - totalV,
                AlignContent.Center => (size.V - totalV) / 2,
                AlignContent.Stretch => 0,
                AlignContent.SpaceBetween => 0.0,
                AlignContent.SpaceAround => spacingV / 2,
                AlignContent.SpaceEvenly => spacingV,
                _ => throw new NotImplementedException()
            };

            foreach (var section in state.Sections)
            {
                var sectionV = scaleV * section.V;

                var (spacingU, u) = JustifyContent switch
                {
                    JustifyContent.FlexStart => (spacing.U, 0.0),
                    JustifyContent.FlexEnd => (spacing.U, size.U - section.U - (section.Last - section.First) * spacing.U),
                    JustifyContent.Center => (spacing.U, (size.U - section.U - (section.Last - section.First) * spacing.U) / 2),
                    JustifyContent.SpaceBetween => ((size.U - section.U) / (section.Last - section.First), 0.0),
                    JustifyContent.SpaceAround => (spacing.U, (size.U - section.U - (section.Last - section.First) * spacing.U) / 2),
                    JustifyContent.SpaceEvenly => ((size.U - section.U) / (section.Last - section.First + 2), (size.U - section.U) / (section.Last - section.First + 2)),
                    _ => throw new NotImplementedException()
                };

                for (int i = section.First; i <= section.Last; i++)
                {
                    var element = context.Children[i];
                    var elementSize = Uv.FromSize(element.DesiredSize, isColumn);

                    double finalV = AlignItems switch
                    {
                        AlignItems.FlexStart => v,
                        AlignItems.FlexEnd => v + sectionV - elementSize.V,
                        AlignItems.Center => v + (sectionV - elementSize.V) / 2,
                        AlignItems.Stretch => v,
                        _ => throw new NotImplementedException()
                    };

                    if (AlignItems == AlignItems.Stretch)
                    {
                        elementSize = new Uv(elementSize.U, sectionV);
                    }

                    var position = new Uv(isReverse ? (size.U - elementSize.U - u) : u, finalV);

                    element.Arrange(new Rect(Uv.ToPoint(position, isColumn), Uv.ToSize(elementSize, isColumn)));

                    u += elementSize.U + spacingU;
                }

                v += sectionV + spacingV;
            }

            return finalSize;
        }

        // Adapted from Avalonia: https://github.com/AvaloniaUI/Avalonia/blob/17d4ae9e4ea0c99dc9cfe951d6e1cbcf64f628dc/src/Avalonia.Layout/Layoutable.cs
        private static void AffectsMeasure(params AvaloniaProperty[] properties)
        {
            void Invalidate(AvaloniaPropertyChangedEventArgs e)
            {
                (e.Sender as FlexLayout)?.InvalidateMeasure();
            }

            foreach (var property in properties)
            {
                property.Changed.Subscribe(Invalidate);
            }
        }

        private struct FlexLayoutState
        {
            public FlexLayoutState(IReadOnlyList<Section> sections)
            {
                Sections = sections;
            }

            public IReadOnlyList<Section> Sections { get; }
        }

        private struct Section
        {
            public Section(int first, int last, double u, double v)
            {
                First = first;
                Last = last;
                U = u;
                V = v;
            }

            public int First { get; }

            public int Last { get; }

            public double U { get; }

            public double V { get; }
        }
    }
}
