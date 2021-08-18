using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Visuals.Media.Imaging;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace TestCatalogAvalonia.Converters
{
    /// <summary>
    /// <para>
    /// Converts a string path to a bitmap asset.
    /// </para>
    /// <para>
    /// The asset must be in the same assembly as the program. If it isn't,
    /// specify "avares://<assemblynamehere>/" in front of the path to the asset.
    /// </para>
    /// </summary>
    public class BitmapConverter : IValueConverter
    {
        public static BitmapConverter Instance = new BitmapConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
                return null;

            else if (value is string rawUri)
            {
                try
                {
                    var path = Path.Combine(Environment.CurrentDirectory, rawUri);
                    return new Bitmap(path).CreateScaledBitmap(PixelSize.FromSizeWithDpi(new Size(300, 250), 72), BitmapInterpolationMode.LowQuality);
                }
                catch
                {
                    Uri uri;

                    if (rawUri.StartsWith("avares://"))
                    {
                        uri = new Uri(rawUri);
                    }
                    else
                    {
                        string assemblyName = Assembly.GetEntryAssembly().GetName().Name;
                        uri = new Uri($"avares://{assemblyName}{rawUri}");
                    }
                    var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
                    Stream asset;
                    try { asset = assets.Open(uri); }
                    catch { asset = assets.Open(new Uri("Assets/ImageNotFound.jpg")); }
                    return new Bitmap(asset);
                }
            }
            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
