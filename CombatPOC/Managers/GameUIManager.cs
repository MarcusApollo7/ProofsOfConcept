
using Gum.Forms;
using Gum.Forms.Controls;
using Microsoft.Xna.Framework;
using MonoGameGum;

namespace CombatPOC.Managers;

public class GameUIManager
{
    GumService GumUI => GumService.Default;

    public GameUIManager(Game game)
    {
        GumService.Default.Initialize(game);
        var mainPanel = new StackPanel();
        mainPanel.AddToRoot();
        var button = new Button();
        // Adds the button as a child so that it is drawn and has its
        // events raised
        mainPanel.AddChild(button);
        // Initial button text before being clicked
        button.Text = "Click Me";
        // Makes the button wider so the text fits
        button.Width = 350;
        // Click event can be handled with a lambda
        button.Click += (_, _) =>
            button.Text = $"Clicked at {System.DateTime.Now}";
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