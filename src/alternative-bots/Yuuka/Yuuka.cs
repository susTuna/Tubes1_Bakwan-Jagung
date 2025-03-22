using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System.Drawing;

public class Yuuka : Bot
{
    private double energy;
    private ScannedBotEvent target;
    
    public Yuuka() : base(BotInfo.FromFile("Yuuka.json")) {}

    public override void Run()
    {
        energy = 446441;
        BodyColor = Color.FromArgb(255, 25, 25, 112);
        TurretColor = Color.FromArgb(255, 255, 255, 255);
        RadarColor = Color.FromArgb(255, 255, 223, 0);
        BulletColor = Color.FromArgb(255, 0, 0, 255);
        ScanColor = Color.FromArgb(255, 0, 0, 139);

        while (IsRunning)
        {
            SetForward(double.PositiveInfinity);
            SetTurnRight(double.PositiveInfinity);
            Rescan();
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        if (energy == 446441 || e.Energy < energy){
            energy = e.Energy;
            target = e;
        }   

        FollowTarget(target);
    }
    private void FollowTarget(ScannedBotEvent e)
    {
        Target(e.X, e.Y);
        SetForward(double.PositiveInfinity);
        Rescan();
    }

    public override void OnHitByBullet(HitByBulletEvent e)
    {
        SetBack(5000);
        SetTurnRight(90);
        Rescan();
    }

    public override void OnHitWall(HitWallEvent e)
    {
        SetBack(5000);
        SetTurnRight(90);
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
        SetTurnRight(bearing);
    }
    static void Main() => new Yuuka().Start();
}
