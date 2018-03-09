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
		const int TIMERDELAY = 100;
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
						Tuple.Create(new Pattern("0,ff0000|1000,ff7f00|1000,ffff00|1000,00ff00|1000,0000ff|1000,4b0082|1000,9400d3|1000,ff0000"), new List<int> { 00, 01, 02, 03, 04, 05, 06, 07, 20, 21, 22, 23 }),
						Tuple.Create(new Pattern("0,ff7f00|1000,ffff00|1000,00ff00|1000,0000ff|1000,4b0082|1000,9400d3|1000,ff0000|1000,ff7f00"), new List<int> { 08, 09, 10, 11, 24, 25, 26, 27, 40, 41, 42, 43 }),
						Tuple.Create(new Pattern("0,ffff00|1000,00ff00|1000,0000ff|1000,4b0082|1000,9400d3|1000,ff0000|1000,ff7f00|1000,ffff00"), new List<int> { 12, 13, 14, 15, 28, 29, 30, 31, 44, 45, 46, 47, 60, 61, 62, 63 }),
						Tuple.Create(new Pattern("0,00ff00|1000,0000ff|1000,4b0082|1000,9400d3|1000,ff0000|1000,ff7f00|1000,ffff00|1000,00ff00"), new List<int> { 16, 17, 18, 19, 32, 33, 34, 35, 48, 49, 50, 51, 64, 65, 66, 67, 80, 81, 82, 83 }),
						Tuple.Create(new Pattern("0,0000ff|1000,4b0082|1000,9400d3|1000,ff0000|1000,ff7f00|1000,ffff00|1000,00ff00|1000,0000ff"), new List<int> { 36, 37, 38, 39, 52, 53, 54, 55, 68, 69, 70, 71, 84, 85, 86, 87 }),
						Tuple.Create(new Pattern("0,4b0082|1000,9400d3|1000,ff0000|1000,ff7f00|1000,ffff00|1000,00ff00|1000,0000ff|1000,4b0082"), new List<int> { 56, 57, 58, 59, 72, 73, 74, 75, 88, 89, 90, 91 }),
						Tuple.Create(new Pattern("0,9400d3|1000,ff0000|1000,ff7f00|1000,ffff00|1000,00ff00|1000,0000ff|1000,4b0082|1000,9400d3"), new List<int> { 76, 77, 78, 79, 92, 93, 94, 95, 96, 97, 98, 99 })
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
