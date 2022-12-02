using Microsoft.Maui.Controls;
using Syncfusion.Maui.ListView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListViewMaui
{
    public class Behavior:Behavior<SfListView>
    {
        private SfListView listView;

        protected override void OnAttachedTo(SfListView bindable)
        {
          listView = bindable;
          listView.SelectionChanged += ListView_SelectionChanged;
          base.OnAttachedTo(bindable);
        }

        #region CallBacks
        private void ListView_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            for (int i = 0; i < e.AddedItems.Count; i++)
            {
                var item = e.AddedItems[i];
                (item as MusicInfo).IsSelected = true;
            }
            for (int i = 0; i < e.RemovedItems.Count; i++)
            {
                var item = e.RemovedItems[i];
                (item as MusicInfo).IsSelected = false;
            }
        }
        #endregion

        protected override void OnDetachingFrom(SfListView bindable)
        {
            listView.SelectionChanged -= ListView_SelectionChanged;
            listView = null;
            base.OnDetachingFrom(bindable);
        }
    }
}
