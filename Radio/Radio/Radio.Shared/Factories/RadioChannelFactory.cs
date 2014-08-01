using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Radio.Models;

namespace Radio.Factories
{
    public abstract class RadioChannelFactory
    {

        //private static readonly Assembly Assembly;

        protected abstract IEnumerable<RadioChannel> CreateNationalRadioChannels();
        protected abstract IEnumerable<RadioChannel> CreateLocalRadioChannels();

        static RadioChannelFactory()
        {
            //Assembly = Assembly.Load(new AssemblyName(typeof(RadioChannelFactory).AssemblyQualifiedName));
        }

        public IEnumerable<RadioChannel> CreateRadioChannels()
        {
            var channels = new List<RadioChannel>();

            var nationalRadioChannels = CreateNationalRadioChannels();
            if (nationalRadioChannels != null)
            {
                foreach (var channel in nationalRadioChannels)
                {
                    channel.Coverage = RadioChannel.ChannelCoverage.National;
                    channels.Add(channel);
                }
            }

            var localRadioChannels = CreateLocalRadioChannels();
            if (localRadioChannels != null)
            {
                foreach (var channel in localRadioChannels)
                {
                    channel.Coverage = RadioChannel.ChannelCoverage.Local;
                    channels.Add(channel);
                }
            }

            return channels;
        }

        public static RadioChannelFactory GetNationalChannelFactory(string countryName)
        {

            //TODO: make it localized for real.

            countryName = "Denmark";

            //TODO: make the dynamic instantiation work.
            //var type = Assembly.DefinedTypes.First(t => t.Name == countryName + "RadioChannelFactory").AsType();
            //var instance = Activator.CreateInstance(type) as RadioChannelFactory;

            var instance = new DenmarkRadioChannelFactory();
            return instance;

        }

    }
}
