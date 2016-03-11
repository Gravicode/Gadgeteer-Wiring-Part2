using Gadgeteer.Modules.GHIElectronics;
using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using BreadboardExperiment2.libs;

namespace BreadboardExperiment2
{
    public partial class Program
    {
        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            Debug.Print("Program Started");
            //light + led
            var th1 = new Thread(new ThreadStart(TestLightSensor));
            th1.Start();
            //distance + buzzer
            var th2 = new Thread(new ThreadStart(TestDistanceSensor));
            th2.Start();
            //gas
            //var th3 = new Thread(new ThreadStart(TestGasSensor));
            //th3.Start();
            //sound
            //var th4 = new Thread(new ThreadStart(TestSoundSensor));
            //th4.Start();

        }

        void TestSoundSensor()
        {
            //sound (4)
            var sound = breadBoardX1.CreateAnalogInput(Gadgeteer.Socket.Pin.Four);
            while (true)
            {
                var intensity = sound.ReadProportion();
                Debug.Print("Sound :" + intensity);
                Thread.Sleep(200);
            }
        }
        void TestGasSensor()
        {
            //gas (5)
            var gas = breadBoardX1.CreateAnalogInput(Gadgeteer.Socket.Pin.Five);
            while (true)
            {
                var intensity = gas.ReadProportion();
                Debug.Print("Gas :" + intensity);
                Thread.Sleep(200);
            }
        }
        void TestDistanceSensor()
        {
            //led red (9)
            var redled = breadBoardX1.CreateDigitalOutput(Gadgeteer.Socket.Pin.Nine, false);
            //triger (6), echo (7), 5V + G
            var pin7 = breadBoardX1.Socket.CpuPins[7];
            var pin6 = breadBoardX1.Socket.CpuPins[6];
            HC_SR04 sensor = new HC_SR04(pin6, pin7);
            while (true)
            {
                long ticks = sensor.Ping();

                if (ticks > 0L)
                {
                    double inches = sensor.TicksToInches(ticks);
                    Debug.Print("jarak :" + inches);

                    if (inches < 4)
                    {
                        redled.Write(true);
                    }
                    else
                    {
                        redled.Write(false);
                    }
                }
                Thread.Sleep(100);
            }
        }
        void TestLightSensor()
        {
            //buzzer (8)
            var buzz = breadBoardX1.CreateDigitalOutput(GT.Socket.Pin.Eight, false);

            //light (3)
            var light = breadBoardX1.CreateAnalogInput(Gadgeteer.Socket.Pin.Three);

            while (true)
            {
                var intensity = light.ReadProportion();
                Debug.Print("Cahaya :" + intensity);
                if (intensity < 0.3)
                    buzz.Write(true);
                else
                    buzz.Write(false);
                Thread.Sleep(200);
            }
        }
    }
}
