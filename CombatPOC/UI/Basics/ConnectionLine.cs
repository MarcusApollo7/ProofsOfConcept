using System;
using System.Collections.Generic;
using CombatPOC.Logic;
using CombatPOC.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace CombatPOC.UI.Basics;

public class ConnectionLine: ScreenElement
{
    private List<Rectangle> _segments; 
    private readonly int _lineWidth;
    private readonly Color _color;
    public ConnectionLine(int lineWidth, Color color)
    {
        _segments = [];
        _lineWidth = lineWidth;
        _color = color;
    }
    public ConnectionLine(TileLocation start, TileLocation end, int lineWidth, Color color): this(lineWidth, color)
    {
        AddSegment(start, end);
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        foreach(Rectangle segment in _segments)
        {
            spriteBatch.Draw(
                CombatManager._CombatUI._whiteRectangle,
                segment,
                null,
                _color,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                Constants.CombatDepth);
        }
    }
    private void AddSegment(TileLocation start, TileLocation end)
    {
        float tileWidth = Helper.RenderedTileWidth;
        float tileHeight = Helper.RenderedTileHeight;
        float startCenterX = start.X * tileWidth + tileWidth / 2;
        float startCenterY = start.Y * tileHeight + tileHeight / 2;
        float endCenterX = end.X * tileWidth + tileWidth / 2;
        float endCenterY = end.Y * tileHeight + tileHeight / 2;

        if (start.X != end.X)
        {
            float thickness = Math.Max(1, _lineWidth * Helper.Scale.Y);
            _segments.Add(CreateRectangle(
                Math.Min(startCenterX, endCenterX),
                startCenterY - thickness / 2,
                Math.Max(startCenterX, endCenterX),
                startCenterY + thickness / 2));
        }
        else if (start.Y != end.Y)
        {
            float thickness = Math.Max(1, _lineWidth * Helper.Scale.X);
            _segments.Add(CreateRectangle(
                startCenterX - thickness / 2,
                Math.Min(startCenterY, endCenterY),
                startCenterX + thickness / 2,
                Math.Max(startCenterY, endCenterY)));
        }
    }

    private static Rectangle CreateRectangle(float left, float top, float right, float bottom)
    {
        int x = (int)MathF.Floor(left);
        int y = (int)MathF.Floor(top);
        int width = (int)MathF.Ceiling(right) - x;
        int height = (int)MathF.Ceiling(bottom) - y;
        return new(x, y, Math.Max(1, width), Math.Max(1, height));
    }
    public void SetSegments(List<TileLocation> segmentDefinition)
    {
        _segments = [];
        TileLocation previousLocation = null;
        foreach(TileLocation nextPoint in segmentDefinition)
        {
            if (previousLocation == null)
                previousLocation = nextPoint;
            else
            {
                AddSegment(previousLocation, nextPoint);
            }
            previousLocation = nextPoint;
        }
    }
}
