using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Cabinet
{
	partial class MainWindow
	{
		const int TIMERDELAY = 25;
		static ObservableCollection<Color> GetDefaultColors()
		{
			var lights = new ObservableCollection<Color>();
			for (var ctr = 0; ctr < 100; ++ctr)
				lights.Add(Colors.Purple);
			return lights;
		}

		public static DependencyProperty LightsProperty = DependencyProperty.Register(nameof(Lights), typeof(ObservableCollection<Color>), typeof(MainWindow), new PropertyMetadata(GetDefaultColors()));

		public ObservableCollection<Color> Lights { get { return (ObservableCollection<Color>)GetValue(LightsProperty); } set { SetValue(LightsProperty, value); } }

		List<Tuple<Pattern, List<int>>> Patterns = new List<Tuple<Pattern, List<int>>>();
		TimeSpan time;

		public MainWindow()
		{
			InitializeComponent();

			var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(TIMERDELAY), };
			timer.Tick += OnTimer;
			timer.Start();
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.G:
					SetPatterns(Tuple.Create(new Pattern("0,00ff00"), Enumerable.Range(0, 100).ToList()));
					break;
				case Key.R:
					SetPatterns(
						Tuple.Create(new Pattern("0,0000ff"), new List<int> { 36, 37, 38, 39, 52, 53, 54, 55, 68, 69, 70, 71, 84, 85, 86, 87 }),
						Tuple.Create(new Pattern("0,00ff00"), new List<int> { 16, 17, 18, 19, 32, 33, 34, 35, 48, 49, 50, 51, 64, 65, 66, 67, 80, 81, 82, 83 }),
						Tuple.Create(new Pattern("0,4b0082"), new List<int> { 56, 57, 58, 59, 72, 73, 74, 75, 88, 89, 90, 91 }),
						Tuple.Create(new Pattern("0,9400d3"), new List<int> { 76, 77, 78, 79, 92, 93, 94, 95, 96, 97, 98, 99 }),
						Tuple.Create(new Pattern("0,ff0000"), new List<int> { 00, 01, 02, 03, 04, 05, 06, 07, 20, 21, 22, 23 }),
						Tuple.Create(new Pattern("0,ff7f00"), new List<int> { 08, 09, 10, 11, 24, 25, 26, 27, 40, 41, 42, 43 }),
						Tuple.Create(new Pattern("0,ffff00"), new List<int> { 12, 13, 14, 15, 28, 29, 30, 31, 44, 45, 46, 47, 60, 61, 62, 63 })
					);
					break;
				case Key.S:
					SetPatterns(
						Tuple.Create(new Pattern("0,000000|500,ff0000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,4b0082|500,9400d3|500,ff0000|500,ff0000|500,000000"), new List<int> { 00, 01, 02, 03, 04, 05, 06, 07, 20, 21, 22, 23 }),
						Tuple.Create(new Pattern("0,000000|500,ff7f00|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,4b0082|500,9400d3|500,ff0000|500,ff7f00|500,ff7f00|500,000000"), new List<int> { 08, 09, 10, 11, 24, 25, 26, 27, 40, 41, 42, 43 }),
						Tuple.Create(new Pattern("0,000000|500,ffff00|500,ffff00|500,00ff00|500,0000ff|500,4b0082|500,9400d3|500,ff0000|500,ff7f00|500,ffff00|500,ffff00|500,000000"), new List<int> { 12, 13, 14, 15, 28, 29, 30, 31, 44, 45, 46, 47, 60, 61, 62, 63 }),
						Tuple.Create(new Pattern("0,000000|500,00ff00|500,00ff00|500,0000ff|500,4b0082|500,9400d3|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,00ff00|500,000000"), new List<int> { 16, 17, 18, 19, 32, 33, 34, 35, 48, 49, 50, 51, 64, 65, 66, 67, 80, 81, 82, 83 }),
						Tuple.Create(new Pattern("0,000000|500,0000ff|500,0000ff|500,4b0082|500,9400d3|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,0000ff|500,000000"), new List<int> { 36, 37, 38, 39, 52, 53, 54, 55, 68, 69, 70, 71, 84, 85, 86, 87 }),
						Tuple.Create(new Pattern("0,000000|500,4b0082|500,4b0082|500,9400d3|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,4b0082|500,4b0082|500,000000"), new List<int> { 56, 57, 58, 59, 72, 73, 74, 75, 88, 89, 90, 91 }),
						Tuple.Create(new Pattern("0,000000|500,9400d3|500,9400d3|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,4b0082|500,9400d3|500,9400d3|500,000000"), new List<int> { 76, 77, 78, 79, 92, 93, 94, 95, 96, 97, 98, 99 })
					);
					break;
				case Key.D:
					SetPatterns(
						Tuple.Create(new Pattern("0,000000|0006,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 00 }),
						Tuple.Create(new Pattern("0,000000|0106,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 01, 02 }),
						Tuple.Create(new Pattern("0,000000|0109,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 04, 20 }),
						Tuple.Create(new Pattern("0,000000|0206,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 03 }),
						Tuple.Create(new Pattern("0,000000|0209,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 05, 06, 21, 22 }),
						Tuple.Create(new Pattern("0,000000|0212,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 08, 24, 40 }),
						Tuple.Create(new Pattern("0,000000|0309,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 07, 23 }),
						Tuple.Create(new Pattern("0,000000|0312,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 09, 10, 25, 26, 41, 42 }),
						Tuple.Create(new Pattern("0,000000|0315,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 12, 28, 44, 60 }),
						Tuple.Create(new Pattern("0,000000|0412,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 11, 27, 43 }),
						Tuple.Create(new Pattern("0,000000|0415,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 13, 14, 29, 30, 45, 46, 61, 62 }),
						Tuple.Create(new Pattern("0,000000|0418,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 16, 32, 48, 64, 80 }),
						Tuple.Create(new Pattern("0,000000|0515,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 15, 31, 47, 63 }),
						Tuple.Create(new Pattern("0,000000|0518,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 17, 18, 33, 34, 49, 50, 65, 66, 81, 82 }),
						Tuple.Create(new Pattern("0,000000|0521,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 36, 52, 68, 84 }),
						Tuple.Create(new Pattern("0,000000|0618,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 19, 35, 51, 67, 83 }),
						Tuple.Create(new Pattern("0,000000|0621,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 37, 38, 53, 54, 69, 70, 85, 86 }),
						Tuple.Create(new Pattern("0,000000|0624,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 56, 72, 88 }),
						Tuple.Create(new Pattern("0,000000|0721,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 39, 55, 71, 87 }),
						Tuple.Create(new Pattern("0,000000|0724,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 57, 58, 73, 74, 89, 90 }),
						Tuple.Create(new Pattern("0,000000|0727,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 76, 92 }),
						Tuple.Create(new Pattern("0,000000|0824,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 59, 75, 91 }),
						Tuple.Create(new Pattern("0,000000|0827,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 77, 78, 93, 94 }),
						Tuple.Create(new Pattern("0,000000|0830,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 96 }),
						Tuple.Create(new Pattern("0,000000|0927,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 79, 95 }),
						Tuple.Create(new Pattern("0,000000|0930,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 97, 98 }),
						Tuple.Create(new Pattern("0,000000|1030,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000"), new List<int> { 99 })
					);
					break;
			}
		}

		void SetPatterns(params Tuple<Pattern, List<int>>[] patterns)
		{
			time = default(TimeSpan);
			Patterns = patterns.ToList();
		}

		void OnTimer(object sender, EventArgs e)
		{
			foreach (var tuple in Patterns)
			{
				var color = tuple.Item1.GetColor(time);
				foreach (var light in tuple.Item2)
					Lights[light] = color;
			}
			time += TimeSpan.FromMilliseconds(TIMERDELAY);
		}
	}
}
