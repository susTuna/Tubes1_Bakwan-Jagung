using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System.Drawing;

public class Handoko : Bot
{
    private ScannedBotEvent target;
    
    public Handoko() : base(BotInfo.FromFile("Handoko.json")) { }

    public override void Run()
    {
        target = null;
        BodyColor = Color.FromArgb(255, 255, 160, 180);
        TurretColor = Color.FromArgb(255, 255, 255, 255);
        RadarColor = Color.FromArgb(255, 250, 140, 180);
        BulletColor = Color.FromArgb(255, 138, 43, 226);
        ScanColor = Color.FromArgb(255, 0, 128, 0);


        while (IsRunning)
        {
            SetForward(double.PositiveInfinity);
            SetTurnLeft(double.PositiveInfinity);
            Rescan();
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        if (target == null || DistanceTo(e.X,e.Y) < DistanceTo(target.X, target.Y))
            target = e;
        FollowTarget(target);
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
        SetTurnLeft(2*bearing);
    }

    static void Main() => new Handoko().Start();
}
