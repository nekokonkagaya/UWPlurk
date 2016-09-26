using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace UWPlurk.Components
{
    public static class PlurkAuthericationBroker
    {
        public static Task AuthenticateAsync(Uri uri)
        {
            var tcs = new TaskCompletionSource<string>();

            var w = new WebView
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Margin = new Thickness(30.0),
            };

            var b = new Border
            {
                Background =
                new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                Width = Window.Current.Bounds.Width,
                Height = Window.Current.Bounds.Height,
                Child = w
            };

            var p = new Popup
            {
                Width = Window.Current.Bounds.Width,
                Height = Window.Current.Bounds.Height,
                Child = b,
                HorizontalOffset = 0.0,
                VerticalOffset = 0.0
            };

            Window.Current.SizeChanged += (s, e) =>
            {
                p.Width = e.Size.Width;
                p.Height = e.Size.Height;
                b.Width = e.Size.Width;
                b.Height = e.Size.Height;
            };

            w.Source = uri;

            w.NavigationCompleted += async (sender, args) =>
            {
                if (args.Uri != null)
                {
                    if (args.Uri.OriginalString.Contains("authorizeDone"))
                    {
                        // Get the oauth verifier in HTML elements
                        var rawHtml = await w.InvokeScriptAsync("eval", new string[] { "document.documentElement.outerHTML;" });
                        var regex = new Regex("<span id=\"oauth_verifier\">(.*?)</span>");
                        if (regex.IsMatch(rawHtml))
                        {
                            MatchCollection collection = regex.Matches(rawHtml);
                            foreach(Match match in collection)
                            {
                                string val = match.Groups[1].Value;
                                tcs.SetResult(val);
                            }
                        }
                        else
                        {
                            tcs.SetResult(null);
                        }
                        p.IsOpen = false;
                    }
                    if (args.Uri.OriginalString.Contains("error=access_denied"))
                    {
                        tcs.SetResult(null);
                        p.IsOpen = false;
                    }
                }
            };

            p.IsOpen = true;
            return tcs.Task;

        }
    }
}