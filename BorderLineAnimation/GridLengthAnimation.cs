using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;

namespace BorderLineAnimation
{
    public class GridLengthAnimation : AnimationTimeline
    {
        public static readonly DependencyProperty FromProperty
           = DependencyProperty.Register(
               nameof(From),
               typeof(GridLength),
               typeof(GridLengthAnimation));

        public static readonly DependencyProperty ToProperty
            = DependencyProperty.Register(
                nameof(To),
                typeof(GridLength),
                typeof(GridLengthAnimation));

        public GridLength From
        {
            get => (GridLength)GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }

        public GridLength To
        {
            get => (GridLength)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }

        public override Type TargetPropertyType => typeof(GridLength);

        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            var from = (GridLength)GetValue(FromProperty);
            var to = (GridLength)GetValue(ToProperty);
            if (from.GridUnitType != to.GridUnitType)
            {
                return to;
            }

            var fromVal = from.Value;
            var toVal = to.Value;

            if (fromVal > toVal)
            {
                return new GridLength(((1 - animationClock.CurrentProgress.Value) * (fromVal - toVal)) + toVal, GridUnitType.Star);
            }

            return new GridLength((animationClock.CurrentProgress.Value * (toVal - fromVal)) + fromVal, GridUnitType.Star);
        }

        protected override Freezable CreateInstanceCore()
        {
            return new GridLengthAnimation();
        }
    }
}
