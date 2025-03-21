using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System;
using System.Drawing;

public class Saori : Bot
{
    private Random rng = new Random();
    private bool movingForward = true;

    public Saori() : base(BotInfo.FromFile("Saori.json")) { }

    public override void Run()
    {
        BodyColor = Color.Indigo;
        TurretColor = Color.LightCyan;
        RadarColor = Color.White;
        BulletColor = Color.DimGray;
        ScanColor = Color.Lavender;

        while (IsRunning)
        {
            SmartMove();
            SetForward(99999);
            WaitFor(new TurnCompleteCondition(this));
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        double distance = DistanceTo(e.X, e.Y);
        double bearing = GunBearingTo(e.X, e.Y);

        if (distance > 50) 
        {
            TurnGun(bearing);
        }

        if (distance < 200)
        {
            Fire(3);
        }
        else if (distance < 400)
        {
            Fire(2);
        }
        else
        {
            Fire(1);
        }
    }

    private void SmartMove()
    {
        if (rng.NextDouble() > 0.5)
        {
            SetTurnLeft(rng.Next(45, 135));
        }
        else
        {
            SetTurnRight(rng.Next(45, 135));
        }

        if (X < 100 || X > ArenaWidth - 100 || Y < 100 || Y > ArenaHeight - 100)
        {
            ReverseDirection();
        }
    }

    public override void OnHitWall(HitWallEvent e)
    {
        ReverseDirection();
    }

    public override void OnHitBot(HitBotEvent e)
    {
        double distance = DistanceTo(e.X, e.Y);
        double bearing = GunBearingTo(e.X, e.Y);

        if (distance > 50)
        {
            TurnGun(bearing);
        }

        Fire(3);

        if (e.IsRammed)
        {
            ReverseDirection();
        }
    }

    private void ReverseDirection()
    {
        if (movingForward)
        {
            SetBack(99999);
        }
        else
        {
            SetForward(99999);
        }
        movingForward = !movingForward;
        SetTurnRight(90);
    }

    private void TurnGun(double bearing)
    {
        if (bearing < 0)
        {
            TurnGunLeft(Math.Abs(bearing));
        }
        else
        {
            TurnGunRight(bearing);
        }
    }

    static void Main()
    {
        new Saori().Start();
    }
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
