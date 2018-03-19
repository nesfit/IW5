

# IW5
# Programming in .NET and C# 
# L07 – WPF – Desktop Applications
<div class="right">
[ Jan Pluskal <ipluskal@fit.vutbr.cz> ]
</div>

---

## Agenda
* Introduction to Windows Presentation Foundation (WPF)
* Basic WPF concepts
* Practical examples

---

## What is WPF?
* Windows Presentation Foundation (WPF)
  * "New" graphical systemf for Windows
  * Enables creation of "Rich-media" application
  * Clear separation of roles:
    * *UI* (XAML) 
    * *Business logic* (C#, VB.NET)
  * Based on technologies like *HTML, CSS, Flash*
  * Hardware acceleration

+++

### WPF Vector Graphics
* All application writen with WPF are Direct3D enabled.
 * **Direct3D** (part of **DirectX**) is vector based engine ensures performance
 * *Rendering* is accelerated by *graphics card*
 * Benefits:
   * Low CPU utilization -> low power consumption
   * Quality "rich-media UI"
 * *Vector graphics* enables: *zooming, resolution changes, rotations...* without any quality loss
 * WPF implements:
   * *float point system* of logical pixels
   * *32-bit ARGB* color spectrum
 
+++

#### WPF Rendering
* WPF works on the lowest layer with **shapes** not **pixels** 
* *Shapes* are represented by *vectors* and can be *easily manipulated*
* Developer defines a shape and lets WPF render it in the most optimal way.
* WPF contains multiple predefined shapes like 
  * `Line, Rectangle, Ellipse, Path` etc...
* *Shapes* are used inside *panels* and multiplicity of other WPF component contents.

+++

#### WPF Text Model
* WPF supports a wide range of *typographic* and *text rendering* functions.
* *International fonts* and *composite fonts*.
* WPF rendering engines use *ClearType* technologies.
  * Fonts are *pre-rendered* and *stored in video memory*.
  * WPF architecture *does not depend on resolution*.

+++

### WPF Animations
* WPF supports timed animation
  * **Timers** are *initialized and managed by WPF*
  * Changes are coordinated by **Storyboard**
* WPF Animations can be initialized
  * By *external events*
  * On *user inputs*
* Animations are *defined by XAML* declarations.

+++

### WPF Audio & Video
* WPF supports incorporation of *audio* and *video* into UI.
* *Audio support* utilizes a thin layer based on *Win32* and *WMP* functions.
* *Video support* uses native formats *WMV, MPEG* and subset of *AVI*.
* Interaction between *video* and *animations*
  * Combination of *video* and *animations* creates dynamic content.
  * *Animations* can be synchronized with media.

+++

### WPF – Styles
* **Style** is a *set of properties* applied to the *content*.
  * Defines *changes in rendering*
  * Concept is the same as with *CSS*
  * E.g., change of button text font - *Button control*
* Used for visual state standardization to set the same set of properties for particular items.
* WPF styles contain specific properties for UI creation.
  * E.g., possibility to *begin a set of visual effects* as a *reaction to a user action*. 

+++

### WPF - Templates
* WPF templates enables complex changes to UI state of any WPF items
* Available templates:
  * `ControlTemplate` – UI style sharing across multiple UI controls 
  * `ItemsPanelTemplate` – panel look, e.g., `ListBox`
  * `DataTemplate` – item look inside a panel
  * `HierarchicalDataTemplate` - object look inside panels with hierarchical structure, e.g., `TreeView`

+++

### WPF - Commands
* `Command` is an abstract and *loosely-coupled* version of `event`
* E.g., *Copy, Cut, Paste, Save, etc...*
* WPF command reduces the necessary code amount
* Enables UI changes without a need to change *back-end* logic
* Commands have *action, source, target and binding*

+++

#### Why to use WPF Commands?
* WPF contains a wide range of *predefined commands*
* Commands provide *automated support for user input actions*
* Some WPF components have *built-in support* for command based behavior
  * E.g., `button` has property `Command`
* Benefits:
  *  *Clean Code* without *Code-behind*
* Command 
  * Launches *action*
  * *Checks* if the action is permited to launch

---

## UI Declaration based on XAML
* **XAML** - e**X**tensible **A**pplication **M**arkup **L**anguage
* *Declarative language* - says *What* not *How*
* Declares *behavior* and *interaction* of UI componets
* Form of *serialization of object hierarchy*
* *.NET namespaces* are represented by *XML namespaces*
* Typicaly closely connected with *Code-behind* class

+++

### XAML - Basics
* **XAML** is base on *XML*
* Used for *declaration* and *initiation* of *.NET objects*
* *XAML* is easily edited by developer
* Used to separated *UI* from *Code-behind* - C# code
* Contains element hierarchy representing visual objects
* These objects are called *user interface elements* or *UI elements*

+++

![HelloWPF](/Lectures/Lecture_07/assets/image/HelloWPF.png)

+++?code=/Lectures/Lecture_07/HelloWPF/MainWindow.xaml&lang=XML&title=HelloWPF-Sample_XAML
@[1,13]
@[2]
@[3-7]
@[5,6,8]
@[9]
@[10,12]
@[11]

[Code sample](/Lectures/Lecture_07/HelloWPF/MainWindow.xaml)

+++?code=/Lectures/Lecture_07/HelloWPF/MainWindow.xaml.cs&lang=C#&title=HelloWPF-Sample_Code-behind
@[8-14]
@[8]
@[10-13]
@[12]
@[8-14]
[Code sample](/Lectures/Lecture_07/HelloWPF/MainWindow.xaml.cs)

+++

#### HelloWPF-Sample explanation
* Declarations:
  * *Window/UserControl/…* - inheritance
  * `x:Class` - class containing *Code-behind*
  * `xmlns:x` - mandatory namespace for XAML
  * `xmlns:d` - optional *design time* functionality
  * `mc:Ignorable` - ingnoration of namespaces in *runtime*
  * `xmlns` - namespace with build-in components in WPF
* *Root element* `Windows` declares a partial class
* `Width, Height, Title` are *properties*
* *Element* `Button` declares item button

+++

### Elements & Attributes - Object properties 
* *UI Elements* have common subset of *properties* and *functions*
  * E.g., `Width, Height, Cursor, Tag` properties
* Declaration of XML *element* in XAML
  * Same effect as calling *parameterless constructor*
* Setting of *Attribute* on the element 
  * Same as *assigment to a property* of the same name.  
* **Atribut** – simple property
* **Element** – *UI Element*, complex property, class initialization

+++

### Propertie elements
* Not all *properties* has to contain `string` only
* Properties can contain instances of other objects
* XAML defines syntactical notation for *complex property* definition called **propertie elements**
* Form **TypeName.PropertyName** contained inside **TypeName** element

```XML
<Grid>
   <Grid.ColumnDefinitions>
      <ColumnDefinition Width="1*"/>
      <ColumnDefinition Width="2*"/>
   </Grid.ColumnDefinitions>
</Grid>
```

---

## Declarative UI - WPF principle
* Designer, UI developer
  * Uses **Blend for Visual Studio** former **Expression Blend**
  * Edits only *XAML files*

![Blend](/Lectures/Lecture_07/assets/image/blend.png)

+++

* Developer
  * Uses **Visual Studio**
  * Works with *Code-behind*

![VS](/Lectures/Lecture_07/assets/image/vs.jpg)

* Typically, role of *Designer* and *Developer* overlap.

+++

### UI *declarative* vs. *imperative* notations
* WPF supports both *declarative* and *imperative* UI element instantiations
* There is *no difference* between both approaches
* Instantiation of UI element from *Code-behind* goes against WPF principle of *loose code coupling*
  * This approach was used in *Windows Forms*

MainWindows.xaml:

```XML
   <Button Content="Click ME!" />
```

MainWindow.xaml.cs:

```C#
   var button = new Button();
   button.Content = "Click ME!";
```

+++

### Declarative UI - WPF principle
* What happens when *XAML is no used in WPF*?
  * Idea of **separation of concerns** is lost.
  * *Designer* and *Developer* cannot *coop* on the *same file*.
    * Otherwise they create conflicts in source control.
* What happens when *XAML is used in WPF*?
  * Object is created in declarative manner.
  * *Parameter-less constructor* is called.
  * All *magic* happens in `InitializeComponent()` method call.

---

## Silverlight
* **Silverlight** is a cross-platform, cross-browser plug-in
  * Technology is based on WPF
  * Rich Internet Application (RIA) platform
  * Contains only a subset of WPF
  * Support "rich-media" functionality like *video, vector graphic, animations*
* **Silverlight** and WPF shares the same XAML presentation layer
  * Both technologies are very similar, but Silverlight is limited in some aspects.
* **Deprecated** end of support is scheduled to **5th of October 2021**  

---

## Universal Windows Platform
* Multiple ways how to use it:
  * XAML UI and a C#, VB, or C++ backend 
  * DirectX UI and a C++ backend 
  * JavaScript and HTML 
  
![UWP](/Lectures/Lecture_07/assets/image/uwp.png)

+++

### UWP Device Family

![UWP Device Family](/Lectures/Lecture_07/assets/image/uwp_device_family.png)

---

## Xamarin
* Multi-platform development
* Started for *mobile devices* to unify development for *all device families*
* Nowadays tries to *target all* mobiles, desktop, web...
![Xamarin](/Lectures/Lecture_07/assets/image/Xamarin_TraditionalvsForms.png)

+++

![Xamarin Example](/Lectures/Lecture_07/assets/image/Xamarin_allhanselman.png)

---

## WPF Practically
![Compile...](/Lectures/Lecture_07/assets/image/compile.png)

+++

### Class hierarchy
* `System.Object`
* `System.Windows.DependencyObject`
  * Support dependency properties
* `System.Windows.UIElement`
  * Rendering methods
* `System.Windows.FrameworkElement`
  * Support for data-binding, styles, etc...
* `System.Windows.Controls.Control`
  * Base class for definitions of *UI Elements*

+++

### Panels - layout definition
* Only components that can have multiple descendants
* Used to create **layout** 
* Common practice in WPF
  * Vector graphics
  * UI adaptation to available space
  * "flexible layout"

```
System.Object
  System.Windows.Threading.DispatcherObject
    System.Windows.DependencyObject
      System.Windows.Media.Visual
        System.Windows.UIElement
          System.Windows.FrameworkElement
            System.Windows.Controls.Panel
```

+++

### Layouts
* `Canvas`
  * Absolute positioning in pixels
  * Properties `Canvas.Top, Canvas.Left`
* `Grid`
  * Table like layout
  * Properties `Grid.Row, Grid.Column, Grid.RowSpan, Grid.ColumnSpan`
* `StackPanel`
  * Components beside one-another
  * *Vertical* or *Horizontal* rendering
  *  * Properties `StackPanel.Orientation`
* `WrapPanel`
  * Components beside one-another and if there is no space, another row is created, or vice-versa
  * *Concentration game (Pexeso)* like a design
  * Properties `WrapPanel.Orientation`

+++

### Content Controls
* *Only one descendant*
* `Border` 
  * Border and background around some content
* `Button`
* `Label`
* `CheckBox, RadioButton`
* `ScrollViewer`
  * In case that content is longer or wider than space defined in parent.
  * Creates *scrolling bar*

```
System.Object
  System.Windows.Threading.DispatcherObject
    System.Windows.DependencyObject
      System.Windows.Media.Visual
        System.Windows.UIElement
          System.Windows.FrameworkElement
            System.Windows.Controls.Control
              System.Windows.Controls.ContentControl
```

+++

### Positioning properties
* `Width, MinWidth, MaxWidth`
* `HorizontalAlignment, VerticalAlignment`
  * Alignment related to parrent element
* `HorizontalContentAlignment, VerticalContentAlignment`
  * Alignment of inner content
* `Margin, Padding`
  * Outer and inner borders

```
System.Object
  System.Windows.Threading.DispatcherObject
    System.Windows.DependencyObject
      System.Windows.Media.Visual
        System.Windows.UIElement
          System.Windows.FrameworkElement
```

+++

### Text formating
* Element `TextBlock`
  * Property `TextWrapping`
  * Inner elements:
    * Element `Run`
      * Attributes `FontWeight, FontSize, Foreground…`
    * `LineBreak, Span, Hyperlink, Bold, Italic, Underline`

```XML
<TextBlock>
Sample text with <Bold>bold</Bold>, <Italic>italic</Italic> 
and <Underline>underlined</Underline> words.
Username: <Run FontWeight="Bold" Text="{Binding UserName}"/>
</TextBlock>
```

+++

### Other components
* `Calendar`

![Calendar](/Lectures/Lecture_07/assets/image/calendar.gif)

* `DatePicker`

![DatePicker](/Lectures/Lecture_07/assets/image/datepicker.jpeg)

+++

* `Image` 

* `ProgressBar`

![ProgressBar](/Lectures/Lecture_07/assets/image/progressbar_simple.png)

* `TextBox`

![TextBox](/Lectures/Lecture_07/assets/image/textbox.jpeg)

+++

### DataContex
* Property of `FrameworkElement`
* References parent's `Datacontext` if not set on an element.
* Perfect for *data-binding*
* Type `object`, thus can be set to anything

![DataContext](/Lectures/Lecture_07/assets/image/DataContext.png)

+++

![DataContext Binding Markup](/Lectures/Lecture_07/assets/image/BindingMarkup.png)

+++

![DataContext Example](/Lectures/Lecture_07/assets/image/DataContext_example.png)

+++

### Data-binding types
* Against current `DataContext`
  * `{Binding}`
    * Aktuální DataContext
  * `{Binding Name}`
    * Binds property `Name` on current `DataContext`
  * `{Binding Name.Length}`
    * Binds property `Name.Length` on current `DataContext`
* Against *named element*
  * property `x:Name`
  * `{Binding Path=Text, ElementName=TextBox1}`
    * property `Text` on object `TextBox1`

+++

### Data-binding direction
* Defined by property `Mode`
  * `OneTime`
    * only once when a component is initialized
  * `OneWay`
    * only in one direction, from *source* to *target*
  * `TwoWay`
    * in both directions from *source* to *target* and from *target* to *source*
  * `OneWayToSource`
    * only in one direction, from *target* to *source*
  * `Default`
    * default value, usually:
      * user defined has `TwoWay`
      * other has `OneWay`
* *Source* 
  * Property that we bind to
* *Target*
  * Component, that defines `{Binding}`

+++

### Data-binding how it works?
* `OneWay` and `TwoWay` 
  * React to changes in the source
  * *Source* needs to notify that *something* has changes
    * `class` containing the *source* need to implement `INotifyPropertyChanged`
    * When *something* changes, `PropertyChanged` event needs to `Invoke()`

+++?code=/Lectures/Lecture_07/HelloWPF/BindingSampleViewModel.cs&lang=C#&title=BindingSampleViewModel
@[6-25]
@[6,9,22-24]
@[11-20]
@[13-17]
@[15]
@[16]
@[6-25]


+++

#### Collections
* Property is a Collection
  * Items are represented by a collection of inner elements
  * `System.Object`
    * `System.Collections.*`
  * Implements interface `IEnumerable`
  * *Source* (collection) needs to notify that collection has changed
    * Implementing `INotifyCollectionChanged`

```C#
public class MainViewModel {
   public ObservableCollection<MenuItem> MenuItems { get; } = new ObservableCollection<MenuItem>();
}
```

+++

### ItemsControl - To Visualize Collections
* `ComboBox`
* `ListBox`
* `TabControl`
* `TreeView`
* …
* `System.Windows.Controls.Control`
* `System.Windows.Controls.ItemsControl`

+++

### ItemsControl - To Visualize Collections
* Property `Items`
  * General objects, rendered inside ItemControl
* Property `ItemsSource`
  * Uses `IEnumerable` as a source of rendered items
* Template `ItemTemplate`
  * Defines *look* and *content* of items
    * *DataContext* is set to the *current item*

```XML
<ListBox ItemsSource="{Binding MenuItems}">
   <ListBox.ItemTemplate>  <DataTemplate>
         <StackPanel>
            <TextBlock Text="{Binding Title}" />
            <TextBlock Text="{Binding SubTitle}" />
         </StackPanel>
      </DataTemplate> </ListBox.ItemTemplate>
</ListBox>
```

+++

### ItemsControl - Collection Change
*  How to re-render collection?
*  Property `ItemsSource`
  * Assignment of a different object
    * Content is cleared, now data is generated
  * Change of item in a `ItemsSource` collection
    * Only with objects implementing interface `INotifyPropertyChanged`
  * Adding or Removing items in a collection
    * Collection must implement interface `INotifyCollectionChanged`

+++

### ItemsControl - ListBox
* Property `SelectedItem`
  * Object that is *bindable*
* Property `SelectedValuePath`
  * Defines path to a property that is binded by `SelectedValue`
  * E.g., `Object.Property1.Property2`
* Property `SelectedValue`
  * Value of property defined by `SelectedValuePath`

```XML
<ListBox 
    ItemsSource="{Binding MenuItems}" 
    SelectedItem="{Binding SelectedItem}" 
    SelectedValue="{Binding SelectedTitle}" 
    SelectedValuePath SelectedValuePath="@Title" 
/>
```

+++

### INotifyCollectionChanged
* Implemented by `ObservableCollection<T>`
  * Implements interface `INotifyCollectionChanged`
* User defined collection 
  * To implement interface `INotifyCollectionChanged`  
* Existing collections
  * To create a wrapper implementing `INotifyCollectionChanged` 

+++

### Commands
* Implements interface `ICommand`
  * `public interface ICommand`
* Methods
  * `Execute(Object)`
    * Defines the method to be called when the command is executed.
  * `CanExecute(Object)`
    * Defines the method that checks if the command can be executed 
* Event
  * `CanExecuteChanged`
    * Event that is called when condition used in `CanExecute(Object)` changes.
    * `CanExecute(Object)` is reevaluated, and if changed, the command can be executed.

+++

### Commands - RelayCommand
* RelayCommand – [MVVMLight](https://msdn.microsoft.com/en-us/magazine/dn237302.aspx?f=255&MSPPError=-2147217396), Telerik

* MyViewModel.cs:

```C#
private RelayCommand _myCommand;
public RelayCommand MyCommand => _myCommand ?? 
   (_myCommand = new RelayCommand(Execute,CanExecute);
private void Execute() {
      //...
}
private bool CanExecute() {
   return 1 == 1;
}
```

---

## WPF Demo
![EF](/Lectures/Lecture_07/assets/image/demo.png)

---

## References

[C# 7.0 in a Nutshell: The Definitive Reference](https://www.amazon.com/C-7-0-Nutshell-Definitive-Reference/dp/1491987650)

And ... a lot of Google index images...

