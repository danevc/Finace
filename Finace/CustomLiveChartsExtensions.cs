using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.Themes;

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
