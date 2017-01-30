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

        public ConfigurationPage()
        {
            Title = "Config";
            var randomSelectedColor = new Button()
            {
                Text = "Randomize selected color"
            };
            var randomBarBackgroundColor = new Button()
            {
                Text = "Randomize bar background color"
            };
            randomSelectedColor.Clicked += (sender, args) =>
            {
                var ran = r.Next(0, App.SelectedColors.Length);
                App.HomeTabbedPage.SelectedColor = App.SelectedColors[ran];
            };
            randomBarBackgroundColor.Clicked += (sender, args) =>
            {
                var ran = r.Next(0, App.BarBackgroundColors.Length);
                App.HomeTabbedPage.BarBackgroundColor = App.BarBackgroundColors[ran];
            };

            var applyToDdl = new Picker();
            var list = ApplyTo.Keys.ToList();
            foreach (var applyToKey in list)
            {
                applyToDdl.Items.Add(applyToKey);
            }
            applyToDdl.SelectedIndexChanged += (sender, args) =>
            {
                App.HomeTabbedPage.BarBackgroundApplyTo = ApplyTo[list[applyToDdl.SelectedIndex]];
            };

            Content = new StackLayout
            {
                Children = {
                    randomSelectedColor,
                    randomBarBackgroundColor,
                    applyToDdl
                }
            };
        }
    }
}
