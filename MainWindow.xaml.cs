using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Cabinet
{
	partial class MainWindow
	{
		static ObservableCollection<Color> GetDefaultColors()
		{
			var lights = new ObservableCollection<Color>();
			for (var ctr = 0; ctr < 100; ++ctr)
				lights.Add(Colors.Purple);
			return lights;
		}

		public static DependencyProperty LightsProperty = DependencyProperty.Register(nameof(Lights), typeof(ObservableCollection<Color>), typeof(MainWindow), new PropertyMetadata(GetDefaultColors()));

		public ObservableCollection<Color> Lights { get { return (ObservableCollection<Color>)GetValue(LightsProperty); } set { SetValue(LightsProperty, value); } }

		public MainWindow()
		{
			InitializeComponent();
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.G:
					for (var ctr = 0; ctr < 100; ++ctr)
						Lights[ctr] = Colors.Green;
					break;
				case Key.R:
					Lights[36] = Lights[37] = Lights[38] = Lights[39] = Lights[52] = Lights[53] = Lights[54] = Lights[55] = Lights[68] = Lights[69] = Lights[70] = Lights[71] = Lights[84] = Lights[85] = Lights[86] = Lights[87] = Color.FromRgb(0, 0, 255);
					Lights[16] = Lights[17] = Lights[18] = Lights[19] = Lights[32] = Lights[33] = Lights[34] = Lights[35] = Lights[48] = Lights[49] = Lights[50] = Lights[51] = Lights[64] = Lights[65] = Lights[66] = Lights[67] = Lights[80] = Lights[81] = Lights[82] = Lights[83] = Color.FromRgb(0, 255, 0);
					Lights[56] = Lights[57] = Lights[58] = Lights[59] = Lights[72] = Lights[73] = Lights[74] = Lights[75] = Lights[88] = Lights[89] = Lights[90] = Lights[91] = Color.FromRgb(75, 0, 130);
					Lights[76] = Lights[77] = Lights[78] = Lights[79] = Lights[92] = Lights[93] = Lights[94] = Lights[95] = Lights[96] = Lights[97] = Lights[98] = Lights[99] = Color.FromRgb(148, 0, 211);
					Lights[00] = Lights[01] = Lights[02] = Lights[03] = Lights[04] = Lights[05] = Lights[06] = Lights[07] = Lights[20] = Lights[21] = Lights[22] = Lights[23] = Color.FromRgb(255, 0, 0);
					Lights[08] = Lights[09] = Lights[10] = Lights[11] = Lights[24] = Lights[25] = Lights[26] = Lights[27] = Lights[40] = Lights[41] = Lights[42] = Lights[43] = Color.FromRgb(255, 127, 0);
					Lights[12] = Lights[13] = Lights[14] = Lights[15] = Lights[28] = Lights[29] = Lights[30] = Lights[31] = Lights[44] = Lights[45] = Lights[46] = Lights[47] = Lights[60] = Lights[61] = Lights[62] = Lights[63] = Color.FromRgb(255, 255, 0);
					break;
			}
		}
	}
}
