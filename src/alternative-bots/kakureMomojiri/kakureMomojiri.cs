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

        AdjustRadarForBodyTurn = true;
        AdjustGunForBodyTurn = true;

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
        var bearingFromGun = GunBearingTo(e.X, e.Y); ;


        TurnGunRight(bearingFromGun);

        if (Math.Abs(bearingFromGun) <= 3)
        {
            if (GunHeat == 0 && Energy > 0.2)
            {
                Fire(Math.Min(3 - Math.Abs(bearingFromGun), Energy - .1));
            }
        }
        else
        {
            SetTurnGunRight(bearingFromGun);
        }

        Forward(100);
        Back(100);
        if (bearingFromGun == 0)
        {
            Rescan();
        }




    }


    public override void OnHitBot(HitBotEvent e)
    {
        SetTurnLeft(30);
        Back(100);
        SetTurnRight(90);
        Back(100);
    }


    public override void OnHitByBullet(HitByBulletEvent e)
    {
        TurnRight(30);
        Forward(150);
    }


    public override void OnHitWall(HitWallEvent e)
    {
        SetTurnLeft(90);
        Forward(100);
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
        SetTurnRadarRight(360);
    }

    public void AvoidWalls(int closeToWall)
    {
        if (closeToWall > 0)
        {
            SetTurnLeft(30);
            Back(100);
            SetTurnRight(90);
            Back(100);

            closeToWall = 0;



            // if (X <= 60) 
            // {
            //     TurnLeft(-Direction);
            // }
            // else if (X >= ArenaWidth - 60) 
            // {
            //     TurnRight(Direction);
            // }
            // else if (Y <= 60) 
            //     TurnLeft(-(Direction+90));
            // }

            // else if (Y >= ArenaHeight - 60) 
            // {
            //     TurnRight(Direction);
            // }


            // Forward(100);

        }



    }
}


