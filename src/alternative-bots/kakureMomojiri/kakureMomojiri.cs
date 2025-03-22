using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class kakureMomojiri : Bot
{
    private int closeToWall = 0;
    static void Main(string[] args)
    {
        new kakureMomojiri().Start();
    }

    kakureMomojiri() : base(BotInfo.FromFile("kakureMomojiri.json")) { }

    public override void Run()
    {
        BodyColor = Color.Pink;
        TurretColor = Color.PeachPuff;
        RadarColor = Color.Pink;
        BulletColor = Color.Fuchsia;

        while (IsRunning)
        {

            Radar();
            closeToWall = CloseToWall();
            AvoidWalls(closeToWall);
            Move();

        }
    }


    public override void OnScannedBot(ScannedBotEvent e)
    {
        TurnGunLeft(GunBearingTo(e.X, e.Y));

        if (Math.Abs(GunBearingTo(e.X, e.Y)) <= 2.5 && GunHeat == 0 && Energy > 10)
        {
            Fire(Math.Min(2.5, Energy - .1));
        }

        if (GunBearingTo(e.X, e.Y) == 0)
            Rescan();
    }



    public override void OnHitBot(HitBotEvent e)
    {
        SetTurnLeft(90);
        Forward(200);
    }


    public override void OnHitByBullet(HitByBulletEvent e)
    {
        TurnRight(30);
        Forward(200);
    }


    public override void OnHitWall(HitWallEvent e)
    {
        SetTurnLeft(90);
        Forward(150);
    }

    public int CloseToWall()
    {
        if (
                X <= 60 ||
                X >= ArenaWidth - 60 ||
                Y <= 60 ||
                Y >= ArenaHeight - 60
                )
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public void Move()
    {
        SetTurnLeft(30);
        Forward(100);
        SetTurnRight(90);
        Forward(100);
    }

    public void Radar()
    {
        TurnRadarRight(360);
    }

    public void AvoidWalls(int closeToWall)
    {
        if (closeToWall > 0)
        {
            SetTurnLeft(90);
            Forward(150);

            closeToWall = 0;


        }



    }
}


