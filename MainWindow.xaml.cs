﻿using System;
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
			Loaded += MainWindow_Loaded;
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			Left = -1920;
			Top = 0;
			WindowState = WindowState.Maximized;
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.D1:
					SetPatterns(Tuple.Create(new Pattern("0,00ff00"), Enumerable.Range(0, 100).ToList()));
					break;
				case Key.D2:
					SetPatterns(@"
						0,0000ffx36,37,38,39,52,53,54,55,68,69,70,71,84,85,86,87
						0,00ff00x16,17,18,19,32,33,34,35,48,49,50,51,64,65,66,67,80,81,82,83
						0,4b0082x56,57,58,59,72,73,74,75,88,89,90,91
						0,9400d3x76,77,78,79,92,93,94,95,96,97,98,99
						0,ff0000x00,01,02,03,04,05,06,07,20,21,22,23
						0,ff7f00x08,09,10,11,24,25,26,27,40,41,42,43
						0,ffff00x12,13,14,15,28,29,30,31,44,45,46,47,60,61,62,63
					");
					break;
				case Key.D3:
					SetPatterns(@"
						0,000000|500,ff0000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,4b0082|500,9400d3|500,ff0000|500,ff0000|500,000000x00,01,02,03,04,05,06,07,20,21,22,23
						0,000000|500,ff7f00|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,4b0082|500,9400d3|500,ff0000|500,ff7f00|500,ff7f00|500,000000x08,09,10,11,24,25,26,27,40,41,42,43
						0,000000|500,ffff00|500,ffff00|500,00ff00|500,0000ff|500,4b0082|500,9400d3|500,ff0000|500,ff7f00|500,ffff00|500,ffff00|500,000000x12,13,14,15,28,29,30,31,44,45,46,47,60,61,62,63
						0,000000|500,00ff00|500,00ff00|500,0000ff|500,4b0082|500,9400d3|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,00ff00|500,000000x16,17,18,19,32,33,34,35,48,49,50,51,64,65,66,67,80,81,82,83
						0,000000|500,0000ff|500,0000ff|500,4b0082|500,9400d3|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,0000ff|500,000000x36,37,38,39,52,53,54,55,68,69,70,71,84,85,86,87
						0,000000|500,4b0082|500,4b0082|500,9400d3|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,4b0082|500,4b0082|500,000000x56,57,58,59,72,73,74,75,88,89,90,91
						0,000000|500,9400d3|500,9400d3|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,4b0082|500,9400d3|500,9400d3|500,000000x76,77,78,79,92,93,94,95,96,97,98,99
					");
					break;
				case Key.D4:
					SetPatterns(@"
						0,000000|0006,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x00
						0,000000|0106,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x01,02
						0,000000|0109,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x04,20
						0,000000|0206,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x03
						0,000000|0209,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x05,06,21,22
						0,000000|0212,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x08,24,40
						0,000000|0309,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x07,23
						0,000000|0312,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x09,10,25,26,41,42
						0,000000|0315,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x12,28,44,60
						0,000000|0412,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x11,27,43
						0,000000|0415,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x13,14,29,30,45,46,61,62
						0,000000|0418,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x16,32,48,64,80
						0,000000|0515,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x15,31,47,63
						0,000000|0518,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x17,18,33,34,49,50,65,66,81,82
						0,000000|0521,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x36,52,68,84
						0,000000|0618,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x19,35,51,67,83
						0,000000|0621,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x37,38,53,54,69,70,85,86
						0,000000|0624,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x56,72,88
						0,000000|0721,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x39,55,71,87
						0,000000|0724,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x57,58,73,74,89,90
						0,000000|0727,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x76,92
						0,000000|0824,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x59,75,91
						0,000000|0827,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x77,78,93,94
						0,000000|0830,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x96
						0,000000|0927,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x79,95
						0,000000|0930,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x97,98
						0,000000|1030,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x99
					");
					break;
				case Key.D5:
					SetPatterns(@"
						0,000000|000,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x48,49,50,51
						0,000000|006,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x30,31,45,47,52,54,68,69
						0,000000|012,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x27,34,65,72
						0,000000|206,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x28,29,44,46,53,55,70,71
						0,000000|212,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x10,11,25,26,32,35,41,43,56,58,64,67,73,74,88,89
						0,000000|218,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x07,14,23,38,61,76,85,92
						0,000000|412,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x08,09,24,33,40,42,57,59,66,75,90,91
						0,000000|418,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x05,06,12,15,21,22,36,39,60,63,77,78,84,87,93,94
						0,000000|424,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x03,18,81,96
						0,000000|618,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x04,13,20,37,62,79,86,95
						0,000000|624,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x01,02,16,19,80,83,97,98
						0,000000|824,000000|500,ff0000|500,ff7f00|500,ffff00|500,00ff00|500,0000ff|500,8b00ff|500,000000x00,17,82,99
					");
					break;
				case Key.D6:
					SetPatterns(@"
						0,000000|0000,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x09
						0,000000|0002,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x12
						0,000000|0020,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x11
						0,000000|0021,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x29
						0,000000|0023,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x14
						0,000000|0024,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x32
						0,000000|0059,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x13
						0,000000|0060,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x16
						0,000000|0096,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x31
						0,000000|0099,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x15
						0,000000|0100,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x17,18,33,34,49
						0,000000|0102,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x36
						0,000000|0105,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x52
						0,000000|0141,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x19
						0,000000|0142,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x37
						0,000000|0177,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x35
						0,000000|0178,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x38
						0,000000|0180,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x53
						0,000000|0181,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x56
						0,000000|0199,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x39
						0,000000|0201,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x57
						0,000000|0266,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x59
						0,000000|0268,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x77
						0,000000|0286,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x58
						0,000000|0287,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x55
						0,000000|0289,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x76
						0,000000|0290,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x73
						0,000000|0325,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x79
						0,000000|0326,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x97
						0,000000|0362,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x54
						0,000000|0365,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x78
						0,000000|0367,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x51,72,75,96,99
						0,000000|0368,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x93
						0,000000|0372,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x69
						0,000000|0407,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x98
						0,000000|0409,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x95
						0,000000|0443,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x74
						0,000000|0444,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x92
						0,000000|0446,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x71
						0,000000|0447,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x89
						0,000000|0465,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x94
						0,000000|0467,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x91
						0,000000|0533,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x90
						0,000000|0535,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x87
						0,000000|0553,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x88
						0,000000|0554,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x70
						0,000000|0556,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x85
						0,000000|0557,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x67
						0,000000|0591,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x86
						0,000000|0593,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x83
						0,000000|0628,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x68
						0,000000|0632,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x84
						0,000000|0633,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x50,65,66,81,82
						0,000000|0635,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x63
						0,000000|0638,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x47
						0,000000|0674,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x80
						0,000000|0675,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x62
						0,000000|0710,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x64
						0,000000|0711,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x61
						0,000000|0713,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x46
						0,000000|0714,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x43
						0,000000|0732,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x60
						0,000000|0734,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x42
						0,000000|0799,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x40
						0,000000|0801,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x22
						0,000000|0819,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x41
						0,000000|0820,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x44
						0,000000|0822,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x23
						0,000000|0823,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x26
						0,000000|0858,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x20
						0,000000|0859,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x02
						0,000000|0895,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x45
						0,000000|0898,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x21
						0,000000|0900,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x00,03,24,27,48
						0,000000|0901,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x06
						0,000000|0904,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x30
						0,000000|0940,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x01
						0,000000|0941,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x04
						0,000000|0976,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x25
						0,000000|0977,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x07
						0,000000|0979,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x28
						0,000000|0980,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x10
						0,000000|0998,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x05
						0,000000|1000,000000|400,ff0000|800,ff0000|400,ff7f00|800,ff7f00|400,ffff00|800,ffff00|400,00ff00|800,00ff00|400,0000ff|800,0000ff|400,8b00ff|800,8b00ff|400,000000x08
					");
					break;
				case Key.D7:
					SetPatterns(@"
						0,000000|2000,0000ff|3000,0000ff|2000,0000ff|3000,0000ff|2000,ffff00|3000,ffff00|2000,ffff00|3000,ffff00|2000,000000x68,69,70,71
						0,000000|2000,0000ff|3000,0000ff|2000,9400d3|3000,9400d3|2000,ffff00|3000,ffff00|2000,ff0000|3000,ff0000|2000,000000x84,85,86,87
						0,000000|2000,0000ff|3000,0000ff|2000,ff0000|3000,ff0000|2000,ffff00|3000,ffff00|2000,9400d3|3000,9400d3|2000,000000x36,37,38,39
						0,000000|2000,0000ff|3000,0000ff|2000,ffff00|3000,ffff00|2000,ffff00|3000,ffff00|2000,0000ff|3000,0000ff|2000,000000x52,53,54,55
						0,000000|2000,00ff00|3000,00ff00|2000,ff0000|3000,ff0000|2000,00ff00|3000,00ff00|2000,9400d3|3000,9400d3|2000,000000x16,17,18,19
						0,000000|2000,00ff00|3000,00ff00|2000,ff7f00|3000,ff7f00|2000,00ff00|3000,00ff00|2000,4b0082|3000,4b0082|2000,000000x32,33,34,35
						0,000000|2000,00ff00|3000,00ff00|2000,00ff00|3000,00ff00|2000,00ff00|3000,00ff00|2000,00ff00|3000,00ff00|2000,000000x48,49,50,51
						0,000000|2000,00ff00|3000,00ff00|2000,4b0082|3000,4b0082|2000,00ff00|3000,00ff00|2000,ff7f00|3000,ff7f00|2000,000000x64,65,66,67
						0,000000|2000,00ff00|3000,00ff00|2000,9400d3|3000,9400d3|2000,00ff00|3000,00ff00|2000,ff0000|3000,ff0000|2000,000000x80,81,82,83
						0,000000|2000,4b0082|3000,4b0082|2000,ff7f00|3000,ff7f00|2000,ff7f00|3000,ff7f00|2000,4b0082|3000,4b0082|2000,000000x56,57,58,59
						0,000000|2000,4b0082|3000,4b0082|2000,4b0082|3000,4b0082|2000,ff7f00|3000,ff7f00|2000,ff7f00|3000,ff7f00|2000,000000x88,89,90,91
						0,000000|2000,4b0082|3000,4b0082|2000,00ff00|3000,00ff00|2000,ff7f00|3000,ff7f00|2000,00ff00|3000,00ff00|2000,000000x72,73,74,75
						0,000000|2000,9400d3|3000,9400d3|2000,ffff00|3000,ffff00|2000,ff0000|3000,ff0000|2000,0000ff|3000,0000ff|2000,000000x76,77,78,79
						0,000000|2000,9400d3|3000,9400d3|2000,00ff00|3000,00ff00|2000,ff0000|3000,ff0000|2000,00ff00|3000,00ff00|2000,000000x96,97,98,99
						0,000000|2000,9400d3|3000,9400d3|2000,0000ff|3000,0000ff|2000,ff0000|3000,ff0000|2000,ffff00|3000,ffff00|2000,000000x92,93,94,95
						0,000000|2000,ff0000|3000,ff0000|2000,ffff00|3000,ffff00|2000,9400d3|3000,9400d3|2000,0000ff|3000,0000ff|2000,000000x04,05,06,07
						0,000000|2000,ff0000|3000,ff0000|2000,0000ff|3000,0000ff|2000,9400d3|3000,9400d3|2000,ffff00|3000,ffff00|2000,000000x20,21,22,23
						0,000000|2000,ff0000|3000,ff0000|2000,00ff00|3000,00ff00|2000,9400d3|3000,9400d3|2000,00ff00|3000,00ff00|2000,000000x00,01,02,03
						0,000000|2000,ff7f00|3000,ff7f00|2000,ff7f00|3000,ff7f00|2000,4b0082|3000,4b0082|2000,4b0082|3000,4b0082|2000,000000x08,09,10,11
						0,000000|2000,ff7f00|3000,ff7f00|2000,00ff00|3000,00ff00|2000,4b0082|3000,4b0082|2000,00ff00|3000,00ff00|2000,000000x24,25,26,27
						0,000000|2000,ff7f00|3000,ff7f00|2000,4b0082|3000,4b0082|2000,4b0082|3000,4b0082|2000,ff7f00|3000,ff7f00|2000,000000x40,41,42,43
						0,000000|2000,ffff00|3000,ffff00|2000,ff0000|3000,ff0000|2000,0000ff|3000,0000ff|2000,9400d3|3000,9400d3|2000,000000x12,13,14,15
						0,000000|2000,ffff00|3000,ffff00|2000,ffff00|3000,ffff00|2000,0000ff|3000,0000ff|2000,0000ff|3000,0000ff|2000,000000x28,29,30,31
						0,000000|2000,ffff00|3000,ffff00|2000,0000ff|3000,0000ff|2000,0000ff|3000,0000ff|2000,ffff00|3000,ffff00|2000,000000x44,45,46,47
						0,000000|2000,ffff00|3000,ffff00|2000,9400d3|3000,9400d3|2000,0000ff|3000,0000ff|2000,ff0000|3000,ff0000|2000,000000x60,61,62,63
					");
					break;
				case Key.D8:
					SetPatterns(@"
						0,000000|000,000000|300,ff0000|900,ff0000|375,000000|300,ffff00|525,ffff00|250,000000|300,00ff00|650,00ff00|375,000000|300,0000ff|525,0000ff|300,000000x00
						0,000000|025,000000|300,ff0000|875,ff0000|400,000000|300,ffff00|500,ffff00|275,000000|300,00ff00|625,00ff00|400,000000|300,0000ff|500,0000ff|300,000000x01,04
						0,000000|050,000000|300,ff0000|850,ff0000|425,000000|300,ffff00|475,ffff00|300,000000|300,00ff00|600,00ff00|425,000000|300,0000ff|475,0000ff|300,000000x05,08
						0,000000|075,000000|300,ff0000|825,ff0000|450,000000|300,ffff00|450,ffff00|325,000000|300,00ff00|575,00ff00|450,000000|300,0000ff|450,0000ff|300,000000x09,12
						0,000000|100,000000|300,ff0000|800,ff0000|475,000000|300,ffff00|425,ffff00|350,000000|300,00ff00|550,00ff00|475,000000|300,0000ff|425,0000ff|300,000000x13,16
						0,000000|125,000000|300,ff0000|775,ff0000|000,000000|300,ffff00|900,ffff00|375,000000|300,00ff00|525,00ff00|000,000000|300,0000ff|900,0000ff|300,000000x17
						0,000000|475,000000|300,ff0000|425,ff0000|350,000000|300,ffff00|550,ffff00|225,000000|300,00ff00|675,00ff00|350,000000|300,0000ff|550,0000ff|300,000000x02,20
						0,000000|500,000000|300,ff0000|400,ff0000|725,000000|300,ffff00|175,ffff00|650,000000|300,00ff00|250,00ff00|725,000000|300,0000ff|175,0000ff|300,000000x03,06,21,24
						0,000000|525,000000|300,ff0000|375,ff0000|750,000000|300,ffff00|150,ffff00|675,000000|300,00ff00|225,00ff00|750,000000|300,0000ff|150,0000ff|300,000000x07,10,25,28
						0,000000|550,000000|300,ff0000|350,ff0000|775,000000|300,ffff00|125,ffff00|700,000000|300,00ff00|200,00ff00|775,000000|300,0000ff|125,0000ff|300,000000x11,14,29,32
						0,000000|575,000000|300,ff0000|325,ff0000|500,000000|300,ffff00|400,ffff00|725,000000|300,00ff00|175,00ff00|500,000000|300,0000ff|400,0000ff|300,000000x15,18,33,36
						0,000000|150,000000|300,ff0000|750,ff0000|025,000000|300,ffff00|875,ffff00|400,000000|300,00ff00|500,00ff00|025,000000|300,0000ff|875,0000ff|300,000000x19,37
						0,000000|450,000000|300,ff0000|450,ff0000|325,000000|300,ffff00|575,ffff00|200,000000|300,00ff00|700,00ff00|325,000000|300,0000ff|575,0000ff|300,000000x22,40
						0,000000|775,000000|300,ff0000|125,ff0000|700,000000|300,ffff00|200,ffff00|625,000000|300,00ff00|275,00ff00|700,000000|300,0000ff|200,0000ff|300,000000x23,26,41,44
						0,000000|800,000000|300,ff0000|100,ff0000|875,000000|300,ffff00|025,ffff00|850,000000|300,00ff00|050,00ff00|875,000000|300,0000ff|025,0000ff|300,000000x27,30,45,48
						0,000000|825,000000|300,ff0000|075,ff0000|800,000000|300,ffff00|100,ffff00|875,000000|300,00ff00|025,00ff00|800,000000|300,0000ff|100,0000ff|300,000000x31,34,49,52
						0,000000|600,000000|300,ff0000|300,ff0000|525,000000|300,ffff00|375,ffff00|750,000000|300,00ff00|150,00ff00|525,000000|300,0000ff|375,0000ff|300,000000x35,38,53,56
						0,000000|175,000000|300,ff0000|725,ff0000|050,000000|300,ffff00|850,ffff00|425,000000|300,00ff00|475,00ff00|050,000000|300,0000ff|850,0000ff|300,000000x39,57
						0,000000|425,000000|300,ff0000|475,ff0000|300,000000|300,ffff00|600,ffff00|175,000000|300,00ff00|725,00ff00|300,000000|300,0000ff|600,0000ff|300,000000x42,60
						0,000000|750,000000|300,ff0000|150,ff0000|675,000000|300,ffff00|225,ffff00|600,000000|300,00ff00|300,00ff00|675,000000|300,0000ff|225,0000ff|300,000000x43,46,61,64
						0,000000|875,000000|300,ff0000|025,ff0000|850,000000|300,ffff00|050,ffff00|825,000000|300,00ff00|075,00ff00|850,000000|300,0000ff|050,0000ff|300,000000x47,50,65,68
						0,000000|850,000000|300,ff0000|050,ff0000|825,000000|300,ffff00|075,ffff00|800,000000|300,00ff00|100,00ff00|825,000000|300,0000ff|075,0000ff|300,000000x51,54,69,72
						0,000000|625,000000|300,ff0000|275,ff0000|550,000000|300,ffff00|350,ffff00|775,000000|300,00ff00|125,00ff00|550,000000|300,0000ff|350,0000ff|300,000000x55,58,73,76
						0,000000|200,000000|300,ff0000|700,ff0000|075,000000|300,ffff00|825,ffff00|450,000000|300,00ff00|450,00ff00|075,000000|300,0000ff|825,0000ff|300,000000x59,77
						0,000000|400,000000|300,ff0000|500,ff0000|275,000000|300,ffff00|625,ffff00|150,000000|300,00ff00|750,00ff00|275,000000|300,0000ff|625,0000ff|300,000000x62,80
						0,000000|725,000000|300,ff0000|175,ff0000|650,000000|300,ffff00|250,ffff00|575,000000|300,00ff00|325,00ff00|650,000000|300,0000ff|250,0000ff|300,000000x63,66,81,84
						0,000000|700,000000|300,ff0000|200,ff0000|625,000000|300,ffff00|275,ffff00|550,000000|300,00ff00|350,00ff00|625,000000|300,0000ff|275,0000ff|300,000000x67,70,85,88
						0,000000|675,000000|300,ff0000|225,ff0000|600,000000|300,ffff00|300,ffff00|525,000000|300,00ff00|375,00ff00|600,000000|300,0000ff|300,0000ff|300,000000x71,74,89,92
						0,000000|650,000000|300,ff0000|250,ff0000|575,000000|300,ffff00|325,ffff00|500,000000|300,00ff00|400,00ff00|575,000000|300,0000ff|325,0000ff|300,000000x75,78,93,96
						0,000000|225,000000|300,ff0000|675,ff0000|100,000000|300,ffff00|800,ffff00|475,000000|300,00ff00|425,00ff00|100,000000|300,0000ff|800,0000ff|300,000000x79,97
						0,000000|375,000000|300,ff0000|525,ff0000|250,000000|300,ffff00|650,ffff00|125,000000|300,00ff00|775,00ff00|250,000000|300,0000ff|650,0000ff|300,000000x82
						0,000000|350,000000|300,ff0000|550,ff0000|225,000000|300,ffff00|675,ffff00|100,000000|300,00ff00|800,00ff00|225,000000|300,0000ff|675,0000ff|300,000000x83,86
						0,000000|325,000000|300,ff0000|575,ff0000|200,000000|300,ffff00|700,ffff00|075,000000|300,00ff00|825,00ff00|200,000000|300,0000ff|700,0000ff|300,000000x87,90
						0,000000|300,000000|300,ff0000|600,ff0000|175,000000|300,ffff00|725,ffff00|050,000000|300,00ff00|850,00ff00|175,000000|300,0000ff|725,0000ff|300,000000x91,94
						0,000000|275,000000|300,ff0000|625,ff0000|150,000000|300,ffff00|750,ffff00|025,000000|300,00ff00|875,00ff00|150,000000|300,0000ff|750,0000ff|300,000000x95,98
						0,000000|250,000000|300,ff0000|650,ff0000|125,000000|300,ffff00|775,ffff00|000,000000|300,00ff00|900,00ff00|125,000000|300,0000ff|775,0000ff|300,000000x99
					");
					break;
			}
		}

		void SetPatterns(params Tuple<Pattern, List<int>>[] patterns)
		{
			time = default(TimeSpan);
			Patterns = patterns.ToList();
		}

		void SetPatterns(string pattern) => SetPatterns(pattern.Split('\r', '\n').Select(str => str.Trim()).Where(str => str.Length != 0).Select(str => str.Split('x')).Select(x => Tuple.Create(new Pattern(x[0]), x[1].Split(',').Select(str => int.Parse(str)).ToList())).ToArray());

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
