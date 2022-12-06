# apply-selected-item-background-color-when-itemtemplate-has-background-color-.net-maui-listview
How to apply the selected item background color when the ItemTemplate view has a background color in .NET MAUI ListView (SfListView) ?

In the [.NET MAUI ListView (SfListView)](https://www.syncfusion.com/maui-controls/maui-listview), the item background color will not be displayed for the selected item if the background color is defined for the ItemTemplate, which is the actual behavior in the framework. However, it is achieved through a workaround by defining a property in the data model and using a custom converter.

In the model class, define the `IsSelected` property, which indicates whether the item is selected and will be updated when the selection is performed using the `SelectionChanged` event. Based on this property, change the background color of View defined in the ItemTemplate property by custom converter class. 

The SfListView `SelectionChanged` EventArgs contains two properties; the AddedItems and RemovedItems. Here, you can get the data of the selected item.

```c#
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
```

Bind the ItemTemplate Background using the Converter value, where the background color of the ItemTemplate will change based on the selected items. 
```xml
<ContentPage>
<ContentPage.BindingContext>
        <local:SelectionViewModel x:Name="viewModel"/>
    </ContentPage.BindingContext>
    <ContentPage.Resources>
        <ResourceDictionary>
            <local:SelectionBoolToImageConverter x:Key="BoolToImageConverter"/>
            <local:SelectionBoolToBackGroundColorConverter x:Key="BoolToColorConverter"/>
        </ResourceDictionary>
    </ContentPage.Resources>
    <Grid Margin="0">
        <Grid.RowDefinitions>
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>
        <listView:SfListView x:Name="listView" 
                             ItemsSource="{Binding MusicInfo}"
                             SelectionGesture="Tap"
                             SelectionMode="Single" 
                             AutoFitMode="Height"
                             ItemSpacing="5"
                             ItemSize="70">
            <listView:SfListView.Behaviors>
                <local:Behavior/>
            </listView:SfListView.Behaviors>
             <listView:SfListView.ItemTemplate>
                <DataTemplate x:Name="ItemTemplate" >
                    <Grid x:Name="grid" RowSpacing="0" ColumnSpacing="0" BackgroundColor="{Binding Path=IsSelected,  Converter={StaticResource BoolToColorConverter}}">
                        <Grid.RowDefinitions>
                            <RowDefinition Height="*" />
                            <RowDefinition Height="1" />
                        </Grid.RowDefinitions>
                        <Grid RowSpacing="0" Grid.Row="0" ColumnSpacing="0">
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="40" />
                                <ColumnDefinition Width="*" />
                                <ColumnDefinition Width="Auto" />
                            </Grid.ColumnDefinitions>
                            <Image Source="{Binding SongThumbnail}"
                     HeightRequest="35"
                     WidthRequest="35"
                     VerticalOptions="Center"
                     HorizontalOptions="Center"/>
                       <Grid Grid.Column="1"
                    RowSpacing="1"
                    Padding="15,0,0,0"
                    VerticalOptions="Center">
                                <Grid.RowDefinitions>
                                    <RowDefinition Height="*" />
                                    <RowDefinition Height="*" />
                                </Grid.RowDefinitions>
                  <Label LineBreakMode="NoWrap"
                       TextColor="#474747"
                       Text="{Binding SongTitle}" FontSize="18" />
                                <Grid RowSpacing="0"
                      ColumnSpacing="0" Padding="0,0,0,5"
                      Grid.Row="1">
                                    <Grid.ColumnDefinitions>
                                        <ColumnDefinition Width="Auto" />
                                        <ColumnDefinition Width="0.4*" />
                                    </Grid.ColumnDefinitions>
                    <Label TextColor="#474747" LineBreakMode="NoWrap"
                         Text="{Binding SongAuther}" FontSize="12" VerticalTextAlignment="Center" />
                                    <Label TextColor="#474747" Margin="0,0,10,0"
                         Grid.Column="1" LineBreakMode="NoWrap" VerticalTextAlignment="Center" HorizontalTextAlignment="End"
                         Text="{Binding SongSize}" FontSize="12">
                                </Grid>
                            </Grid>
                             <Image Grid.Column="2" x:Name="selectionImage" Margin="10,0,10,0"
                     HeightRequest="30" WidthRequest="30" VerticalOptions="Center" HorizontalOptions="Center"
                     IsVisible="True"
                     Source="{Binding Path=IsSelected, Converter={StaticResource BoolToImageConverter}}"/>
                        </Grid>
                        <StackLayout Grid.Row="1"  HeightRequest="1"/>
                    </Grid>
                </DataTemplate>
            </listView:SfListView.ItemTemplate>
        </listView:SfListView>
    </Grid>
</ContentPage>
```
Change the background color of the view based on the IsSelected property using the SelectionBoolToBackgroundColorConverter class.

```c#
public class SelectionBoolToBackgroundColorConverter : IValueConverter
{
  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return (bool)value == true ? Color.FromRgb(211,211,211) : Color.White;
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}
```
Please take a moment to peruse the [documentation](https://help.syncfusion.com/maui/listview/selection) to learn more about Selection and its related operations in the SfListView with code examples.