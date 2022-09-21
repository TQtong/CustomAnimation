using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Wpfnet5
{
    [DefaultProperty("Items")]
    [ContentProperty("Items")]
    [TemplatePart(Name = ItemHostPanel, Type = typeof(Panel))]
    public class Carouse : ContentControl
    {
        private const string ItemHostPanel = "PART_ItemHost";//约定图片控件的窗口x:Name
        private const string PreButton = "PART_PreButton";//约定上一张按钮的x:Name
        private const string NextButton = "PART_NextButton";//约定下一张按钮的x:Name
        private List<double> _widthList = new List<double>();//记录每个图片到容器左侧的距离
        private DispatcherTimer _updateTimer;//自动切换图片的定时器
        public Panel ItemHost { get; set; }//图片容器控件
        private Button _preButton { get; set; }//上一张按钮
        private Button _nextButton { get; set; }//下一张按钮

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Bindable(true)]
        public ObservableCollection<object> Items { get; set; } = new ObservableCollection<object>();//在xaml里面添加图片后，会自动添加到这个集合里面
        private int _currentIndex = 0;//当前是第几张图片
        public int CurrentIndex
        {
            get
            {
                _currentIndex = Math.Max(0, _currentIndex);
                _currentIndex = Math.Min(Items.Count - 1, _currentIndex);
                return _currentIndex;
            }
            set
            {
                if (value == _currentIndex) return;
                if (value < 0) _currentIndex = Items.Count - 1;
                else if (value >= Items.Count) _currentIndex = 0;
                else _currentIndex = value;
                UpdateWidths();//更新_widthList
                UpdateMargin();//执行动画更新当前显示的图片
            }
        }
        //自动切换图片的时间间隔
        public TimeSpan Interval
        {
            get { return (TimeSpan)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(TimeSpan), typeof(Carouse), new PropertyMetadata(TimeSpan.FromSeconds(4)));

        //是否自动切换图片
        public bool AutoChange
        {
            get { return (bool)GetValue(AutoChangeProperty); }
            set { SetValue(AutoChangeProperty, value); }
        }
        public static readonly DependencyProperty AutoChangeProperty =
            DependencyProperty.Register("AutoChange", typeof(bool), typeof(Carouse), new PropertyMetadata(false, (o, args) =>
            {
                var ctl = (Carouse)o;
                ctl.SetTimer((bool)args.NewValue);
            }));


        private int _animationDelay = 1000;
        //每次动画执行时长
        public int AnimationDelay
        {
            get { return _animationDelay; }
            set { _animationDelay = value; }
        }
        //设置自动切换图片的定时器
        public void SetTimer(bool isOpen)
        {
            if (_updateTimer != null)
            {
                _updateTimer.Tick -= UpdateTimer_Tick;
                _updateTimer.Stop();
                _updateTimer = null;
            }
            if (!isOpen) return;
            _updateTimer = new DispatcherTimer()
            {
                Interval = Interval
            };
            _updateTimer.Tick += UpdateTimer_Tick;
            _updateTimer.Start();
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (IsMouseOver) return;
            CurrentIndex++;
        }
        private void UpdateWidths()
        {
            _widthList.Clear();
            _widthList.Add(0); var width = .0;
            foreach (FrameworkElement item in ItemHost.Children)
            {
                item.Measure(new Size(ActualWidth, ActualHeight));
                width += item.DesiredSize.Width;
                _widthList.Add(width);
            }
        }
        //执行动画
        public void UpdateMargin()
        {
            if (ItemHost == null) return;
            if (ItemHost.Children.Count == 0) return;
            var marginAnimation = new ThicknessAnimation(new Thickness(-_widthList[CurrentIndex], 0, 0, 0),
                new Duration(TimeSpan.FromMilliseconds(AnimationDelay)))
            {
                EasingFunction = new PowerEase { EasingMode = EasingMode.EaseOut }
            };
            ItemHost.BeginAnimation(MarginProperty, marginAnimation);
        }
        //初始化控件，添加按钮点击事件
        public override void OnApplyTemplate()
        {
            ItemHost?.Children.Clear();
            base.OnApplyTemplate();
            ItemHost = GetTemplateChild(ItemHostPanel) as Panel;
            _preButton = GetTemplateChild(PreButton) as Button;
            if (_preButton != null)
            {
                _preButton.Click -= Pre;
                _preButton.Click += Pre;
            }
            _nextButton = GetTemplateChild(NextButton) as Button;
            if (_nextButton != null)
            {
                _nextButton.Click -= Next;
                _nextButton.Click += Next;
            }
            RefreshItems();//将每个图片添加到容器中
        }
        private void Pre(object sender, RoutedEventArgs e) => CurrentIndex--;
        private void Next(object sender, RoutedEventArgs e) => CurrentIndex++;
        private void RefreshItems()
        {
            if (ItemHost == null) return;
            ItemHost.Children.Clear();
            if (Items.Count == 0) return;
            foreach (var item in Items)
            {
                if (item is UIElement element)
                    ItemHost.Children.Add(element);
            }
        }
    }
}