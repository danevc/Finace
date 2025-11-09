using LiveChartsCore;
using LiveChartsCore.Kernel;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Themes;
using SkiaSharp;

namespace Finace;

public static partial class CustomLiveChartsExtensions
{
    public static LiveChartsSettings AddLiveChartsAppSettings(this LiveChartsSettings settings) =>
        settings
            .AddSkiaSharp()
            .AddDefaultMappers()
            .AddDefaultTheme(theme => theme
            .OnInitialized(() =>
            {
                theme.AnimationsSpeed = TimeSpan.FromSeconds(1);
                theme.Colors = ColorPalletes.MaterialDesign500;
                theme.VirtualBackroundColor = new LiveChartsCore.Drawing.LvcColor(33, 32, 29);
            })
            .HasRuleForBarSeries(series =>
            {
                series.Rx = 10;
                series.Ry = 10;
            }));

}
