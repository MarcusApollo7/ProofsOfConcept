namespace CombatPOC.Classes;

public record BattleState(Resolutions resolution, int turnNum, Party party, Party enemy, Party ally)
{
    public Resolutions Resolution = resolution;
    public int TurnNum = turnNum;
    public Party Party = party;
    public Party Enemy = enemy;
    public Party Ally = ally;
}