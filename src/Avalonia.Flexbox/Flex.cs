using System;

using Avalonia.Layout;

namespace Avalonia.Flexbox
{
    public static class Flex
    {
        public static readonly AttachedProperty<AlignItems?> AlignSelfProperty =
            AvaloniaProperty.RegisterAttached<Layoutable, AlignItems?>("AlignSelf", typeof(Flex));

        public static AlignItems? GetAlignSelf(Layoutable layoutable)
        {
            if (layoutable is null)
            {
                throw new ArgumentNullException(nameof(layoutable));
            }

            return layoutable.GetValue(AlignSelfProperty);
        }

        public static void SetAlignSelf(Layoutable layoutable, AlignItems? value)
        {
            if (layoutable is null)
            {
                throw new ArgumentNullException(nameof(layoutable));
            }

            layoutable.SetValue(AlignSelfProperty, value);
        }
    }
}
