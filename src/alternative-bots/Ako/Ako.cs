using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System;
using System.Drawing;

public class Ako : Bot
{
    
    public Ako() : base(BotInfo.FromFile("Ako.json")) { }

    public override void Run()
    {
        BodyColor = Color.FromArgb(167, 199, 231);
        TurretColor = Color.White;
        RadarColor = Color.FromArgb(45, 62, 80);
        BulletColor = Color.FromArgb(255, 215, 0);
        ScanColor = Color.FromArgb(0, 255, 255);

        while (IsRunning)
        {
            SetForward(double.PositiveInfinity);
            SetTurnLeft(double.PositiveInfinity);
            Rescan();
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        FollowTarget(e);
    }

    public override void OnHitWall(HitWallEvent e)
    {
        SetBack(5000);
        SetTurnLeft(90);
        Rescan();
    }

    public override void OnBotDeath(BotDeathEvent e)
    {
        Rescan();
    }

    private void FollowTarget(ScannedBotEvent e)
    {
        Target(e.X, e.Y);
        SetForward(double.PositiveInfinity);
        Rescan();
    }

    public override void OnHitBot(HitBotEvent e)
    {
        Target(e.X, e.Y);
        Fire(3);
        Fire(3);
        Fire(3);
        Rescan();
    }

    private void Target(double x, double y)
    {
        var bearing = BearingTo(x, y);
        SetTurnLeft(bearing);
    }

    static void Main() => new Ako().Start();
}
