using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System;
using System.Collections.Generic;
using System.Drawing;

public class Yuuka : Bot
{
    private Dictionary<int, ScannedBotEvent> enemies = new();

    private int enemyC;
    private int targetId = -1;
    
    public Yuuka() : base(BotInfo.FromFile("Yuuka.json")) 
    {

    }

    public override void Run()
    {
        BodyColor = Color.FromArgb(167, 199, 231);
        TurretColor = Color.White;
        RadarColor = Color.FromArgb(45, 62, 80);
        BulletColor = Color.FromArgb(255, 215, 0);
        ScanColor = Color.FromArgb(0, 255, 255);
        enemyC = EnemyCount;

        while (IsRunning)
        {
            SetForward(5000);
            WaitFor(new TurnCompleteCondition(this));
            SetTurnRight(45);
            WaitFor(new TurnCompleteCondition(this));
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        enemies[e.ScannedBotId] = e;
        if (targetId == -1 || enemies[targetId].Energy > e.Energy)
            targetId = e.ScannedBotId;

        if (targetId == e.ScannedBotId || enemyC == 1 )
        {
            double distance = DistanceTo(e.X, e.Y);

            if (distance < 200) Fire(3);
            else if (distance < 400) Fire(2);
            else Fire(1);
        }

        Fire(1); //try to gain energy
    }
    public override void OnBotDeath(BotDeathEvent e){
        enemyC--;
        
    }
    public override void OnHitByBullet(HitByBulletEvent e)
    {
        SetBack(10000);
        SetTurnRight(90);
    }

    public override void OnHitWall(HitWallEvent e) {
        SetBack(5000);
        SetTurnRight(90);
    }

    public override void OnHitBot(HitBotEvent e) {
        if (e.IsRammed) {
            Fire(3);
            Fire(1);
            Fire(1);
        }
    }

    static void Main() => new Yuuka().Start();
}

public class TurnCompleteCondition : Condition
{
    private readonly Bot bot;

    public TurnCompleteCondition(Bot bot)
    {
        this.bot = bot;
    }

    public override bool Test()
    {
        return bot.TurnRemaining == 0;
    }
}
