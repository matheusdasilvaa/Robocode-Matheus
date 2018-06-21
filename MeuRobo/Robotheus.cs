using Robocode;
using Robocode.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboTheus
{
    class Robotheus : AdvancedRobot

    {
        int BalaPerdida = 0;
        int BalaCerta = 0;
        int scanDirection = 1;

        public override void Run()
        {
            //colors
            BodyColor = (Color.Black);
            GunColor = (Color.Black);
            RadarColor = (Color.Green);
            BulletColor = (Color.Green);
            ScanColor = (Color.Black);
            BulletColor = (Color.DarkRed);
            //
            
            SetTurnRadarRight(1000);

            while (true)
            {
                SetAhead(30);
                
            }
            
        }

        public override void OnScannedRobot(ScannedRobotEvent e)
        {
            double absoluteBearing = Heading + e.Bearing;
            double bearingFromGun = Utils.NormalRelativeAngleDegrees(absoluteBearing - GunHeading);

            if (Math.Abs(bearingFromGun) <= 3)
            {
                TurnGunRight(bearingFromGun);

                if (GunHeat == 0)
                {
                    Fire(Math.Min(3 - Math.Abs(bearingFromGun), Energy - .1));
                }
            }
            else
            {

                TurnGunRight(bearingFromGun);
            }

            if (bearingFromGun == 0)
            {
                Scan();
            }

        }

        
        public override void OnBulletHitBullet(BulletHitBulletEvent evnt)
        {
            SetTurnLeft(15);
            Fire(1);
        }

        public override void OnHitByBullet(HitByBulletEvent evnt)
        {
            SetTurnRight(60);
            SetAhead(150);
            Execute();
        }

        public override void OnHitWall(HitWallEvent evnt)
        {
            SetBack(100);
            SetTurnLeft(90);
            Execute();
        }

        public override void OnHitRobot(HitRobotEvent evnt)
        {
            SetBack(15);
            Fire(3);
           
        }

        public override void OnBulletMissed(BulletMissedEvent evnt)
        {
            BalaPerdida++;
            BalaCerta = 0;

            if (BalaPerdida > 1)
            {
                BalaPerdida = 0;
                MudarPosicao();
            }
        }

        public override void OnBulletHit(BulletHitEvent e)
        {
            BalaCerta++;
            BalaPerdida = 0;
            if (BalaCerta > 2)
            {
                Fire(1);
            }
            Fire(2);
            FireBullet(5);
        }

        public void MudarPosicao()
        {
            TurnLeft(75);
            Ahead(100);
        }

        public override void OnWin(WinEvent evnt)
        {
            SetTurnGunLeft(50000);
            SetTurnRight(90);
      
        }

       
        





    }
}
