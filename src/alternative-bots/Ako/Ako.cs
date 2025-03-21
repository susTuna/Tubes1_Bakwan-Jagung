using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System;
using System.Drawing;

public class Ako : Bot
{
    private int targetId = -1;
    
    public Ako() : base(BotInfo.FromFile("Ako.json")) { }

    public override void Run()
    {
        targetId = -1;
        BodyColor = Color.FromArgb(167, 199, 231);
        TurretColor = Color.White;
        RadarColor = Color.FromArgb(45, 62, 80);
        BulletColor = Color.FromArgb(255, 215, 0);
        ScanColor = Color.FromArgb(0, 255, 255);

        while (IsRunning)
        {
            if (targetId == -1)
            {
                SetForward(double.PositiveInfinity);
                SetTurnLeft(double.PositiveInfinity);
            }
            Go();
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        if (targetId == -1)
        {
            targetId = e.ScannedBotId;
        }

        if (targetId == e.ScannedBotId)
        {
            FollowTarget(e);
        }
    }

    public override void OnHitWall(HitWallEvent e)
    {
        SetTurnRight(90);
        SetForward(1000);
        Rescan();
    }

    public override void OnBotDeath(BotDeathEvent e)
    {
        if (e.VictimId == targetId)
        {
            targetId = -1;
        }
    }

    private void FollowTarget(ScannedBotEvent e)
    {
        Target(e.X, e.Y);
        SetForward(double.PositiveInfinity);
    }

    public override void OnHitBot(HitBotEvent e)
    {
        Target(e.X, e.Y);
        Fire(3);
        Fire(3);
        Fire(3);
        Fire(2);
        Rescan();
    }

    private void Target(double x, double y)
    {
        var bearing = BearingTo(x, y);
        SetTurnLeft(bearing);;
    }

    static void Main() => new Ako().Start();
}
