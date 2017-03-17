using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Messier16.Forms.Controls;
using Xamarin.Forms;

namespace TabbedPage
{
    public class ConfigurationPage : ContentPage
    {
        Random r = new Random();

        private Dictionary<string, BarBackgroundApplyTo> ApplyTo = new Dictionary<string, BarBackgroundApplyTo>
        {
            {"None", BarBackgroundApplyTo.None},
            {"Android", BarBackgroundApplyTo.Android},
            {"iOS", BarBackgroundApplyTo.iOS},
            {"Both", BarBackgroundApplyTo.iOS |BarBackgroundApplyTo.Android }
        };

        Label SelectedColorLabel;
        Label BarbackgroundColorLabel;

        private int _currentTabColor = 0;
        private int _currentBackgroundTabColor = 0;

        public ConfigurationPage()
        {
            Title = "Config";
            var randomSelectedColor = new Button()
            {
                Text = "Change selected color"
            };
            SelectedColorLabel = new Label { HorizontalTextAlignment = TextAlignment.Center };
            randomSelectedColor.Clicked += (sender, args) =>
            {
                _currentTabColor = ++_currentTabColor % App.SelectedColors.Length;
                App.HomeTabbedPage.SelectedColor = App.SelectedColors[_currentTabColor];
                UpdateSelectedColors();
            };

            var randomBarBackgroundColor = new Button()
            {
                Text = "Change bar background color"
            };
            BarbackgroundColorLabel = new Label { HorizontalTextAlignment = TextAlignment.Center };
            randomBarBackgroundColor.Clicked += (sender, args) =>
            {

                _currentBackgroundTabColor = ++_currentBackgroundTabColor % App.BarBackgroundColors.Length;
                App.HomeTabbedPage.BarBackgroundColor = App.BarBackgroundColors[_currentBackgroundTabColor];
                UpdateSelectedColors();
            };

            var applyToDdl = new Picker();
            var list = ApplyTo.Keys.ToList();
            foreach (var applyToKey in list)
            {
                applyToDdl.Items.Add(applyToKey);
            }
            applyToDdl.SelectedIndex = 1; // Android only
            applyToDdl.SelectedIndexChanged += (sender, args) =>
            {
                App.HomeTabbedPage.BarBackgroundApplyTo = ApplyTo[list[applyToDdl.SelectedIndex]];
            };

            Content = new StackLayout
            {
                Children = {
                    randomSelectedColor,
                    SelectedColorLabel,
                    randomBarBackgroundColor,
                    BarbackgroundColorLabel,
                    applyToDdl
                }
            };
        }

        protected override void OnAppearing()
        {
            App.HomeTabbedPage.SelectedColor = App.SelectedColors[_currentTabColor];
            App.HomeTabbedPage.BarBackgroundColor = App.BarBackgroundColors[_currentBackgroundTabColor];
            UpdateSelectedColors();
        }

        void UpdateSelectedColors()
        {
            var selected = App.HomeTabbedPage.SelectedColor;
            var background = App.HomeTabbedPage.BarBackgroundColor;

            BarbackgroundColorLabel.Text = $"Background R:{background.R * 255:000} G:{background.G * 255:000} B:{background.B * 255:000}";
            SelectedColorLabel.Text = $"Selected R:{selected.R * 255:000} G:{selected.G * 255:000} B:{selected.B * 255:000}";
        }
    }
}
