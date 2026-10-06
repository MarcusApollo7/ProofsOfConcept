using Microsoft.Xna.Framework;
using Gum.GueDeriving;


namespace CombatPOC.Managers;

public class GameUIManager
{
    Gum.GumService GumUI => Gum.GumService.Default;
    public ContainerRuntime SideBar { get; protected set; }
    public ContainerRuntime BottomBar { get; protected set; }
    public ContainerRuntime WorldScreen { get; protected set; }
    public TextRuntime SideBarText { get; protected set; }
    public GameUIManager(Game game)
    {
        GumUI.Initialize(game);
        GumUI.EnableZoomToWindow();
        WorldScreen = new ContainerRuntime
        {
            Name = "WorldScreen",
            Height = 70f,
            HeightUnits = Gum.DataTypes.DimensionUnitType.PercentageOfParent,
            Width = 70f,
            WidthUnits = Gum.DataTypes.DimensionUnitType.PercentageOfParent
        };
        WorldScreen.AddToRoot();
        SideBar = new ContainerRuntime
        {
            Name = "SideBar",
            Height = 70f,
            HeightUnits = Gum.DataTypes.DimensionUnitType.PercentageOfParent,
            Width = 30f,
            WidthUnits = Gum.DataTypes.DimensionUnitType.PercentageOfParent,
            X = 0f,
            XOrigin = RenderingLibrary.Graphics.HorizontalAlignment.Right,
            XUnits = Gum.Converters.GeneralUnitType.PixelsFromLarge,
            Y = 0f
        };
        SideBar.AddToRoot();
        SideBarText = new TextRuntime
        {
            Name = "SideBarText",
            Text = @"Hello, this is the side bar.",
            Color = Color.Black
        };
        SideBar.AddChild(SideBarText);
        BottomBar = new ContainerRuntime
        {
            Name = "BottomBar",
            Height = 30f,
            HeightUnits = Gum.DataTypes.DimensionUnitType.PercentageOfParent,
            Width = 100f,
            WidthUnits = Gum.DataTypes.DimensionUnitType.PercentageOfParent,
            X = 0f,
            Y = 0f,
            YOrigin = RenderingLibrary.Graphics.VerticalAlignment.Bottom,
            YUnits = Gum.Converters.GeneralUnitType.PixelsFromLarge
        };
        BottomBar.AddToRoot();
    }
    public void Update(GameTime gameTime)
    {
        GumUI.Update(gameTime);
    }
    public void Draw()
    {
        GumUI.Draw();
    }
}