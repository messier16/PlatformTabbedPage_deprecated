using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Messier16.Forms.Controls;
using Plugin.Badge.Abstractions;
using Xamarin.Forms;

namespace TabbedPage
{
    public class BadgesConfigurationPage : ContentPage
    {

        public BadgesConfigurationPage()
        {
            Title = "Badges";
            var applyToDdl = new Picker();
            applyToDdl.Items.Add("Tab one");
            applyToDdl.Items.Add("Tab two");
            applyToDdl.Items.Add("Tab three");
            applyToDdl.Items.Add("Tab four");
            applyToDdl.SelectedIndex = 0;

            var randomSelectedColor = new Button()
            {
                Text = "Change badge color"
            };
            randomSelectedColor.Clicked += (sender, args) =>
            {
                _currentTabColor = ++_currentTabColor % App.SelectedColors.Length;
                var selectedColor = App.SelectedColors[_currentTabColor];
                var page = App.HomeTabbedPage.Children[applyToDdl.SelectedIndex];
                page.SetValue(TabBadge.BadgeColorProperty, selectedColor);
            };

            randomSelectedColor.Clicked += (sender, args) =>
            {
                _currentTabColor = ++_currentTabColor % App.SelectedColors.Length;
                var selectedColor = App.SelectedColors[_currentTabColor];
                var page = App.HomeTabbedPage.Children[applyToDdl.SelectedIndex];
                page.SetValue(TabBadge.BadgeColorProperty, selectedColor);
            };

            var increaseBadgeValue = new Button()
            {
                Text = "Increment badge value"
            };
            increaseBadgeValue.Clicked += (s, a) =>
            {
                var page = App.HomeTabbedPage.Children[applyToDdl.SelectedIndex];
                _badgeValues[applyToDdl.SelectedIndex]++;
                page.SetValue(TabBadge.BadgeTextProperty,
                    _badgeValues[applyToDdl.SelectedIndex] == 0
                        ? null
                        : _badgeValues[applyToDdl.SelectedIndex].ToString());
            };

            var decrementBadgeValue = new Button()
            {
                Text = "Decrement badge value"
            };
            decrementBadgeValue.Clicked += (s, a) =>
            {
                var page = App.HomeTabbedPage.Children[applyToDdl.SelectedIndex];
                _badgeValues[applyToDdl.SelectedIndex]--;
                page.SetValue(TabBadge.BadgeTextProperty,
                    _badgeValues[applyToDdl.SelectedIndex] == 0
                        ? null
                        : _badgeValues[applyToDdl.SelectedIndex].ToString());
            };

            Content = new StackLayout
            {
                Children = {
                    applyToDdl,
                    randomSelectedColor,
                    increaseBadgeValue,
                    decrementBadgeValue
                }
            };
        }

        private int _currentTabColor = 0;
        private int[] _badgeValues = new int[4];
    }
}
