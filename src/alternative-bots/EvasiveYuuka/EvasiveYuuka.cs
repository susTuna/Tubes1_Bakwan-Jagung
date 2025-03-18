using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System;
using System.Drawing;

public class EvasiveYuuka : Bot
{
    private Random rng = new Random();
    public EvasiveYuuka() : base(BotInfo.FromFile("EvasiveYuuka.json")) { }

    public override void Run()
    {
        BodyColor = Color.Blue;
        TurretColor = Color.White;
        RadarColor = Color.LightGray;
        BulletColor = Color.LightBlue;
        ScanColor = Color.Purple;

        while (IsRunning)
        {
            SetForward(99999);
            SetTurnLeft(90);
            SmartMove();

            WaitFor(new TurnCompleteCondition(this));

            SmartMove();
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        double distance = DistanceTo(e.X, e.Y);
        double bearing = GunBearingTo(e.X, e.Y);
        TurnGun(bearing);
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
    private void SmartMove(){
        if (rng.NextDouble() > 0.5){
            SetTurnLeft(rng.Next(10,45));
        }
        else{
            SetTurnRight(rng.Next(10,45));
        }
        if (X < 100 || X > ArenaWidth - 100 || Y < 100 || Y > ArenaHeight - 100)
            {
                SetTurnRight(90);
            }
    }
    public override void OnHitWall(HitWallEvent e)
    {  
        SetBack(99999);
        SetTurnRight(90); 
    }

    public override void OnHitBot(HitBotEvent e)
    {
        double bearing = GunBearingTo(e.X, e.Y);
        TurnGun(bearing);
        Fire(3);
        Fire(2);
        Fire(1);
        if(e.IsRammed){
            SetBack(99999);
            SetTurnRight(90);
        }
    }
    private void Foia(double a, double b, double c){
        if (a <= b){
            Fire(c);
        }
    }

    private void TurnGun(double bearing){
        if (bearing < 0) {
            TurnGunLeft(Math.Abs(bearing));
        } else {
            TurnGunRight(bearing);
        }
    }
    static void Main()
    {
        new EvasiveYuuka().Start();
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