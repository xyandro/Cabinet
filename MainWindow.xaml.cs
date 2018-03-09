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
						Tuple.Create(new Pattern("0,ff0000|1000,ff7f00|1000,ffff00|1000,00ff00|1000,0000ff|1000,4b0082|1000,9400d3|1000,ff0000"), new List<int> { 00, 01, 02, 03, 04, 05, 06, 07, 20, 21, 22, 23 }),
						Tuple.Create(new Pattern("0,ff7f00|1000,ffff00|1000,00ff00|1000,0000ff|1000,4b0082|1000,9400d3|1000,ff0000|1000,ff7f00"), new List<int> { 08, 09, 10, 11, 24, 25, 26, 27, 40, 41, 42, 43 }),
						Tuple.Create(new Pattern("0,ffff00|1000,00ff00|1000,0000ff|1000,4b0082|1000,9400d3|1000,ff0000|1000,ff7f00|1000,ffff00"), new List<int> { 12, 13, 14, 15, 28, 29, 30, 31, 44, 45, 46, 47, 60, 61, 62, 63 }),
						Tuple.Create(new Pattern("0,00ff00|1000,0000ff|1000,4b0082|1000,9400d3|1000,ff0000|1000,ff7f00|1000,ffff00|1000,00ff00"), new List<int> { 16, 17, 18, 19, 32, 33, 34, 35, 48, 49, 50, 51, 64, 65, 66, 67, 80, 81, 82, 83 }),
						Tuple.Create(new Pattern("0,0000ff|1000,4b0082|1000,9400d3|1000,ff0000|1000,ff7f00|1000,ffff00|1000,00ff00|1000,0000ff"), new List<int> { 36, 37, 38, 39, 52, 53, 54, 55, 68, 69, 70, 71, 84, 85, 86, 87 }),
						Tuple.Create(new Pattern("0,4b0082|1000,9400d3|1000,ff0000|1000,ff7f00|1000,ffff00|1000,00ff00|1000,0000ff|1000,4b0082"), new List<int> { 56, 57, 58, 59, 72, 73, 74, 75, 88, 89, 90, 91 }),
						Tuple.Create(new Pattern("0,9400d3|1000,ff0000|1000,ff7f00|1000,ffff00|1000,00ff00|1000,0000ff|1000,4b0082|1000,9400d3"), new List<int> { 76, 77, 78, 79, 92, 93, 94, 95, 96, 97, 98, 99 })
					);
					break;
				case Key.D:
					SetPatterns(
						Tuple.Create(new Pattern("0,000000|0005,ff0000|0505,ff7f00|0505,ffff00|0505,00ff00|0505,0000ff|0505,4b0082|0505,9400d3|0505,ff0000"), new List<int> { 00 }),
						Tuple.Create(new Pattern("0,000000|0102,ff0000|0602,ff7f00|0602,ffff00|0602,00ff00|0602,0000ff|0602,4b0082|0602,9400d3|0602,ff0000"), new List<int> { 01, 02 }),
						Tuple.Create(new Pattern("0,000000|0105,ff0000|0605,ff7f00|0605,ffff00|0605,00ff00|0605,0000ff|0605,4b0082|0605,9400d3|0605,ff0000"), new List<int> { 04, 20 }),
						Tuple.Create(new Pattern("0,000000|0200,ff0000|0700,ff7f00|0700,ffff00|0700,00ff00|0700,0000ff|0700,4b0082|0700,9400d3|0700,ff0000"), new List<int> { 03 }),
						Tuple.Create(new Pattern("0,000000|0202,ff0000|0702,ff7f00|0702,ffff00|0702,00ff00|0702,0000ff|0702,4b0082|0702,9400d3|0702,ff0000"), new List<int> { 05, 06, 21, 22 }),
						Tuple.Create(new Pattern("0,000000|0205,ff0000|0705,ff7f00|0705,ffff00|0705,00ff00|0705,0000ff|0705,4b0082|0705,9400d3|0705,ff0000"), new List<int> { 08, 24, 40 }),
						Tuple.Create(new Pattern("0,000000|0300,ff0000|0800,ff7f00|0800,ffff00|0800,00ff00|0800,0000ff|0800,4b0082|0800,9400d3|0800,ff0000"), new List<int> { 07, 23 }),
						Tuple.Create(new Pattern("0,000000|0302,ff0000|0802,ff7f00|0802,ffff00|0802,00ff00|0802,0000ff|0802,4b0082|0802,9400d3|0802,ff0000"), new List<int> { 09, 10, 25, 26, 41, 42 }),
						Tuple.Create(new Pattern("0,000000|0305,ff0000|0805,ff7f00|0805,ffff00|0805,00ff00|0805,0000ff|0805,4b0082|0805,9400d3|0805,ff0000"), new List<int> { 12, 28, 44, 60 }),
						Tuple.Create(new Pattern("0,000000|0400,ff0000|0900,ff7f00|0900,ffff00|0900,00ff00|0900,0000ff|0900,4b0082|0900,9400d3|0900,ff0000"), new List<int> { 11, 27, 43 }),
						Tuple.Create(new Pattern("0,000000|0402,ff0000|0902,ff7f00|0902,ffff00|0902,00ff00|0902,0000ff|0902,4b0082|0902,9400d3|0902,ff0000"), new List<int> { 13, 14, 29, 30, 45, 46, 61, 62 }),
						Tuple.Create(new Pattern("0,000000|0405,ff0000|0905,ff7f00|0905,ffff00|0905,00ff00|0905,0000ff|0905,4b0082|0905,9400d3|0905,ff0000"), new List<int> { 16, 32, 48, 64, 80 }),
						Tuple.Create(new Pattern("0,000000|0500,ff0000|1000,ff7f00|1000,ffff00|1000,00ff00|1000,0000ff|1000,4b0082|1000,9400d3|1000,ff0000"), new List<int> { 15, 31, 47, 63 }),
						Tuple.Create(new Pattern("0,000000|0502,ff0000|1002,ff7f00|1002,ffff00|1002,00ff00|1002,0000ff|1002,4b0082|1002,9400d3|1002,ff0000"), new List<int> { 17, 18, 33, 34, 49, 50, 65, 66, 81, 82 }),
						Tuple.Create(new Pattern("0,000000|0505,ff0000|1005,ff7f00|1005,ffff00|1005,00ff00|1005,0000ff|1005,4b0082|1005,9400d3|1005,ff0000"), new List<int> { 36, 52, 68, 84 }),
						Tuple.Create(new Pattern("0,000000|0600,ff0000|1100,ff7f00|1100,ffff00|1100,00ff00|1100,0000ff|1100,4b0082|1100,9400d3|1100,ff0000"), new List<int> { 19, 35, 51, 67, 83 }),
						Tuple.Create(new Pattern("0,000000|0602,ff0000|1102,ff7f00|1102,ffff00|1102,00ff00|1102,0000ff|1102,4b0082|1102,9400d3|1102,ff0000"), new List<int> { 37, 38, 53, 54, 69, 70, 85, 86 }),
						Tuple.Create(new Pattern("0,000000|0605,ff0000|1105,ff7f00|1105,ffff00|1105,00ff00|1105,0000ff|1105,4b0082|1105,9400d3|1105,ff0000"), new List<int> { 56, 72, 88 }),
						Tuple.Create(new Pattern("0,000000|0700,ff0000|1200,ff7f00|1200,ffff00|1200,00ff00|1200,0000ff|1200,4b0082|1200,9400d3|1200,ff0000"), new List<int> { 39, 55, 71, 87 }),
						Tuple.Create(new Pattern("0,000000|0702,ff0000|1202,ff7f00|1202,ffff00|1202,00ff00|1202,0000ff|1202,4b0082|1202,9400d3|1202,ff0000"), new List<int> { 57, 58, 73, 74, 89, 90 }),
						Tuple.Create(new Pattern("0,000000|0705,ff0000|1205,ff7f00|1205,ffff00|1205,00ff00|1205,0000ff|1205,4b0082|1205,9400d3|1205,ff0000"), new List<int> { 76, 92 }),
						Tuple.Create(new Pattern("0,000000|0800,ff0000|1300,ff7f00|1300,ffff00|1300,00ff00|1300,0000ff|1300,4b0082|1300,9400d3|1300,ff0000"), new List<int> { 59, 75, 91 }),
						Tuple.Create(new Pattern("0,000000|0802,ff0000|1302,ff7f00|1302,ffff00|1302,00ff00|1302,0000ff|1302,4b0082|1302,9400d3|1302,ff0000"), new List<int> { 77, 78, 93, 94 }),
						Tuple.Create(new Pattern("0,000000|0805,ff0000|1305,ff7f00|1305,ffff00|1305,00ff00|1305,0000ff|1305,4b0082|1305,9400d3|1305,ff0000"), new List<int> { 96 }),
						Tuple.Create(new Pattern("0,000000|0900,ff0000|1400,ff7f00|1400,ffff00|1400,00ff00|1400,0000ff|1400,4b0082|1400,9400d3|1400,ff0000"), new List<int> { 79, 95 }),
						Tuple.Create(new Pattern("0,000000|0902,ff0000|1402,ff7f00|1402,ffff00|1402,00ff00|1402,0000ff|1402,4b0082|1402,9400d3|1402,ff0000"), new List<int> { 97, 98 }),
						Tuple.Create(new Pattern("0,000000|1000,ff0000|1500,ff7f00|1500,ffff00|1500,00ff00|1500,0000ff|1500,4b0082|1500,9400d3|1500,ff0000"), new List<int> { 99 })
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
