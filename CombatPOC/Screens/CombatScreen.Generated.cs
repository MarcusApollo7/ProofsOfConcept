//Code for CombatScreen
using CombatPOC.Components;
using Gum;
using Gum.Converters;
using Gum.DataTypes;
using Gum.GueDeriving;
using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using RenderingLibrary.Graphics;
using System.Linq;
namespace CombatPOC.Screens;
partial class CombatScreen : global::Gum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new global::Gum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new global::Gum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("CombatScreen") ?? throw new System.InvalidOperationException("Could not find an element named CombatScreen - did you forget to load a Gum project?");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new CombatScreen(visual);
            visual.Width = 0;
            visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
            visual.Height = 0;
            visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
            return visual;
        });
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(CombatScreen)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("CombatScreen", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public ContainerRuntime SideBar { get; protected set; }
    public ContainerRuntime BottomBar { get; protected set; }
    public ContainerRuntime WorldScreen { get; protected set; }
    public SideBarComponent SideBarComponentInstance { get; protected set; }

    public CombatScreen(InteractiveGue visual) : base(visual)
    {
    }
    public CombatScreen()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        SideBar = this.Visual?.GetGraphicalUiElementByName("SideBar") as global::Gum.GueDeriving.ContainerRuntime;
        BottomBar = this.Visual?.GetGraphicalUiElementByName("BottomBar") as global::Gum.GueDeriving.ContainerRuntime;
        WorldScreen = this.Visual?.GetGraphicalUiElementByName("WorldScreen") as global::Gum.GueDeriving.ContainerRuntime;
        SideBarComponentInstance = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<SideBarComponent>(this.Visual,"SideBarComponentInstance");
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
