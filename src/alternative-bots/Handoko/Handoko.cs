using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System;
using System.Drawing;

public class Handoko : Bot
{
    private bool movingForward = true;

    public Handoko() : base(BotInfo.FromFile("Handoko.json")) { }

    public override void Run()
    {
        BodyColor = Color.Pink;
        TurretColor = Color.White;
        RadarColor = Color.Blue;
        BulletColor = Color.Purple;
        ScanColor = Color.Green;

        while (IsRunning)
        {
            StrafeMovement();
            SetForward(400000);
            WaitFor(new TurnCompleteCondition(this));
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        double distance = DistanceTo(e.X, e.Y);
        double enemySpeed = e.Speed;
        double bearing = GunBearingTo(e.X, e.Y);

        double lead = enemySpeed / 12.5;  
        if (bearing > 0) bearing += lead;
        else bearing -= lead;
        
        TurnGun(bearing);

        if (distance < 200) Fire(3);
        else if (distance < 400) Fire(2);
        else Fire(1);
    }

    private void StrafeMovement()
    {
        SetTurnRight(30);

        if (X < ArenaWidth * 0.25 || X > ArenaWidth * 0.75 || Y < ArenaHeight * 0.25 || Y > ArenaHeight * 0.75)
            ReverseDirection();
    }

    public override void OnHitWall(HitWallEvent e) => ReverseDirection();

    public override void OnHitBot(HitBotEvent e)
    {
        double distance = DistanceTo(e.X, e.Y);
        double bearing = GunBearingTo(e.X, e.Y);

        if (distance > 50) TurnGun(bearing);
        Fire(3);
        if (e.IsRammed) ReverseDirection();
    }

    private void ReverseDirection()
    {
        movingForward = !movingForward;
        if (movingForward) SetForward(400000);
        else SetBack(400000);
        SetTurnRight(90);
    }

    private void TurnGun(double bearing)
    {
        if (bearing < 0) TurnGunLeft(Math.Abs(bearing));
        else TurnGunRight(bearing);
    }

    static void Main() => new Handoko().Start();
}

public class TurnCompleteCondition : Condition
{
    private readonly Bot bot;
    public TurnCompleteCondition(Bot bot) { this.bot = bot; }
    public override bool Test() => bot.TurnRemaining == 0;
}