using System;
using System.Linq;
using System.Threading;
using Microsoft.Kinect;

namespace Basics
{
    class Program
    {
        private static KinectSensor kinect = KinectSensor.KinectSensors.First(ks => ks.Status == KinectStatus.Connected);

        static void Main(string[] args)
        {
            kinect.SkeletonStream.Enable();
            kinect.Start();
            kinect.AllFramesReady += FrameHandler;

            while(true)
                Thread.Sleep(100);

            kinect.Stop();
        }

        static void FrameHandler(object sender, AllFramesReadyEventArgs afrea)
        {
            using(var sf = afrea.OpenSkeletonFrame())
            {
                var data = new Skeleton[kinect.SkeletonStream.FrameSkeletonArrayLength];
                sf.CopySkeletonDataTo(data);

                foreach(Joint joint in data[0].Joints) {
                    if(joint.JointType == JointType.WristLeft)
                        Console.WriteLine("Left Wrist: ({0},{1},{2})", joint.Position.X,  joint.Position.Y,  joint.Position.Z);
                }
            }
        }
    }
}
