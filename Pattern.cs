using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Cabinet
{
	public class Pattern
	{
		public class PatternItem
		{
			public Color Color { get; set; }
			public TimeSpan TimeSpan { get; set; }
		}
		List<PatternItem> items = new List<PatternItem>();

		public Pattern(string pattern)
		{
			var time = default(TimeSpan);
			foreach (var item in pattern.Split('|'))
			{
				var itemData = item.Split(',');
				var itemTime = TimeSpan.FromMilliseconds(int.Parse(itemData[0]));
				var itemColor = (Color)ColorConverter.ConvertFromString($"#{itemData[1]}");
				time += itemTime;
				items.Add(new PatternItem { Color = itemColor, TimeSpan = time });
			}
		}

		Color MixColor(Color start, Color end, double percent)
		{
			Func<byte, byte, byte> calc = (startv, endv) => (byte)(startv * (1 - percent) + endv * percent);
			return Color.FromArgb(calc(start.A, end.A), calc(start.R, end.R), calc(start.G, end.G), calc(start.B, end.B));
		}

		public Color GetColor(TimeSpan time)
		{
			var itemIndex = items.LastIndexFor(x => x.TimeSpan <= time);
			if (itemIndex == -1)
				return items[items.Count - 1].Color;
			var start = items[itemIndex];
			var end = items[itemIndex + 1];
			var percent = (time - start.TimeSpan).TotalMilliseconds / (end.TimeSpan - start.TimeSpan).TotalMilliseconds;
			var color = MixColor(start.Color, end.Color, percent);
			return color;
		}
	}
}
