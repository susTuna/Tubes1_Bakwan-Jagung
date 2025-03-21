using System;
using System.Collections.Generic;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class Amethyst : Bot
{
    static void Main(string[] args)
    {
        new Amethyst().Start();
    }
    Amethyst() : base(BotInfo.FromFile("Amethyst.json")) { }

    private bool isRamming = false; // Status apakah bot sedang dalam mode ramming
    private HashSet<int> Enemies = new HashSet<int>(); // Menyimpan daftar musuh yang telah dipindai
    private int rammingTimeout = 0; // Menghitung berapa lama bot telah berada dalam mode ramming
    private int maxRammingTimeout = 120; // Maksimum durasi ramming sebelum kembali defence

    public override void Run()
    {
        // Tema warna bot
        BodyColor = Color.FromArgb(138, 43, 226);
        TurretColor = Color.FromArgb(230, 230, 250);
        RadarColor = Color.FromArgb(148, 0, 211);
        ScanColor = Color.FromArgb(255, 182, 193);
        TracksColor = Color.FromArgb(75, 0, 130);
        GunColor = Color.FromArgb(102, 51, 153);

        while (IsRunning)
        {
            // Jika tidak sedang ramming atau waktu ramming telah habis, kembali ke mode defence
            if (!isRamming || rammingTimeout > maxRammingTimeout)
            {
                isRamming = false;
                rammingTimeout = 0;
                PerformAdvancedDodging();
            }

            if (isRamming) 
                rammingTimeout++;
            else 
                TurnRadarRight(60); 
        }
    }

    // Gerakan untuk menghindari tembakan musuh dengan menggunakan pola acak
    private void PerformAdvancedDodging()
    {
        int moveChoice = Random.Shared.Next(5);

        switch (moveChoice)
        {
            case 0:
                Forward(100);
                TurnRight(45);
                break;
            case 1:
                Back(90);
                TurnLeft(50);
                break;
            case 2:
                Forward(70);
                TurnRight(30);
                Back(50);
                break;
            case 3:
                Back(110);
                TurnLeft(40);
                break;
            case 4:
                Forward(80);
                TurnRight(20);
                Back(60);
                break;
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        Enemies.Add(e.ScannedBotId); 

        double distance = DistanceTo(e.X, e.Y);
        bool isSingleEnemy = NumberOfEnemies() == 1;

        // Jika musuh memiliki energi rendah (≤40) atau satu-satunya musuh tersisa dan lebih lemah, aktifkan mode ramming
        if ((e.Energy <= 40 || (isSingleEnemy && Energy - e.Energy >= 20)) && distance < 250)
        {
            isRamming = true;
            rammingTimeout = 0;
            LockRadarOnTarget(e);
            PerformSuperRamming(e);
        }
    }

    // Mengunci radar ke musuh dengan energi rendah agar bot tidak kehilangan target
    private void LockRadarOnTarget(ScannedBotEvent e)
    {
        double angleToEnemy = BearingTo(e.X, e.Y) - RadarDirection;
        TurnRadarRight(NormalizeAngle(angleToEnemy));
    }

    private void PerformSuperRamming(ScannedBotEvent e)
    {
        while (isRamming && IsRunning && e.Energy > 0)
        {
            double angleToEnemy = BearingTo(e.X, e.Y) - Direction;
            TurnRight(NormalizeAngle(angleToEnemy));

            Forward(9999);

            if (e.Energy <= 0)
            {
                isRamming = false;
                PerformAdvancedDodging();
            }
        }
    }
    private double NormalizeAngle(double angle)
    {
        angle %= 360;
        if (angle > 180) angle -= 360;
        if (angle < -180) angle += 360;
        return angle;
    }
    private int NumberOfEnemies()
    {
        return Enemies.Count;
    }

    // Langsung menghindar ketika terkena tembakan
    public override void OnHitByBullet(HitByBulletEvent e)
    {
        if (!isRamming)
            PerformAdvancedDodging();
    }

    // Jika menabrak dinding, lakukan manuver agar tidak terjebak
    public override void OnHitWall(HitWallEvent e)
    {
        if (isRamming)
            Back(50);
        TurnRight(135 + Random.Shared.Next(-30, 30));
        Forward(150 + Random.Shared.Next(-30, 30));
    }

    // Jika menabrak bot lain, lakukan ramming 
    public override void OnHitBot(HitBotEvent e)
    {
        if (isRamming && e.Energy > 0)
        {
            Forward(9999);
        }
        else
        {
            Back(100 + Random.Shared.Next(-20, 20));
        }
    }
}
