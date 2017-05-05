﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NationalInstruments.DAQmx;
using System.Threading;
using System.Diagnostics;

namespace Teflon.SDK
{
    public static class ni
    {
        public static double ReadVoltage(string port_num,int count=50,int milliseconds=500,string dev_name="Dev1", AITerminalConfiguration configuration = AITerminalConfiguration.Rse)
        {
            Task task = new Task();
            task.AIChannels.CreateVoltageChannel(string.Format("{0}/{1}",dev_name,port_num), "", configuration, -10, 10, AIVoltageUnits.Volts);
            AnalogSingleChannelReader reader = new AnalogSingleChannelReader(task.Stream);
            task.Start();
            Thread.Sleep(milliseconds);
            double[] samples = reader.ReadMultiSample(count);
            double sample = samples.Average();
            task.Stop();
            task.Dispose();
            return sample;
        }
        public static void WriteVoltage(string port_num, double value,int milliseconds = 500, string dev_name = "Dev1")
        {
            Task task = new Task();
            task.AOChannels.CreateVoltageChannel(string.Format("{0}/{1}",dev_name,port_num), "", -10, 10, AOVoltageUnits.Volts);
            AnalogSingleChannelWriter writer = new AnalogSingleChannelWriter(task.Stream);
            task.Start();
            Thread.Sleep(milliseconds);
            writer.WriteSingleSample(false, value);
            task.Stop();
            task.Dispose();
        }
        public static void WriteDO(string port_num,bool value,int milliseconds=500,string dev_name="Dev1")
        {
            Task task = new Task();
            string port = port_num.Split(new char[] { '.' })[0];
            string line = port_num.Split(new char[] { '.' })[1];
            task.DOChannels.CreateChannel(string.Format("{0}/port{1}/line{2}",dev_name,port,line), "", ChannelLineGrouping.OneChannelForEachLine);
            DigitalSingleChannelWriter writer = new DigitalSingleChannelWriter(task.Stream);
            task.Start();
            Thread.Sleep(milliseconds);
            writer.WriteSingleSampleSingleLine(false, value);
            task.Stop();
            task.Dispose();
        }
        public static bool ReadDI(string port_num,int milliseconds=500, string dev_name="Dev1")
        {
            Task task = new Task();
            string port = port_num.Split(new char[] { '.' })[0];
            string line = port_num.Split(new char[] { '.' })[1];

            task.DIChannels.CreateChannel(string.Format("{0}/port{1}/line{2}", dev_name, port, line), "", ChannelLineGrouping.OneChannelForEachLine);
            DigitalSingleChannelReader reader = new DigitalSingleChannelReader(task.Stream);
            task.Start();
            Thread.Sleep(milliseconds);
            bool res = reader.ReadSingleSampleSingleLine();
            task.Stop();
            return res;
        }
        public static Tuple<CounterSingleChannelReader,IAsyncResult> BeginReadFrequency(string port_num,double measurement_time=1,int divisor=4,string dev_name="Dev1")
        {
            Task task = new Task();
            task.CIChannels.CreateFrequencyChannel(string.Format("{0}/{1}", dev_name, port_num), "", 1.19, 10000000,
                CIFrequencyStartingEdge.Rising, CIFrequencyMeasurementMethod.LowFrequencyOneCounter, measurement_time, divisor, CIFrequencyUnits.Hertz);
            CounterSingleChannelReader reader = new CounterSingleChannelReader(task.Stream);
            IAsyncResult ar =  reader.BeginReadSingleSampleDouble(null, null);
            return  Tuple.Create(reader, ar);
        }
        public static double EndReadFrequency(Tuple<CounterSingleChannelReader, IAsyncResult> tuple)
        {
            while(!tuple.Item2.IsCompleted)
            {

            }
            return tuple.Item1.EndReadSingleSampleDouble(tuple.Item2);
        }
        public static void Reset()
        {
            var devices = from deviceName in DaqSystem.Local.Devices
                          select DaqSystem.Local.LoadDevice(deviceName);
            foreach (var device in devices)
            {
                device.Reset();
            }
        }
        public static void Reset(string dev_name)
        {
            var devices = DaqSystem.Local.Devices;
            if (devices.Contains(dev_name))
            {
                var device = DaqSystem.Local.LoadDevice(devices.First((s) => (s.Equals(dev_name))));
                device.Reset();
            }
        }
    }
}