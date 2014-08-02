using System;
using System.Collections.Generic;
using Radio.Factories.Programs;
using Radio.Factories.Programs.Denmark;
using Radio.Models;

namespace Radio.Factories
{
    class DenmarkRadioChannelFactory : RadioChannelFactory
    {
        protected override IEnumerable<RadioChannel> CreateNationalRadioChannels()
        {
            yield return new RadioChannel("DR P1", new Uri("ms-appx:/Assets/Stations/Denmark/dr-p1.jpg"),
                new[]
                {
                    new RadioWebFeed("http://live-icy.gss.dr.dk/A/A03H.mp3", "http://live-icy.gss.dr.dk/A/A03L.mp3")
                },
                new[] {
                    new RadioBeacon("Aarhus", 88.1, new WorldCoordinate(56.16294, 10.20392)),
                    new RadioBeacon("Årsballe", 96.2, new WorldCoordinate(55.14499, 14.86963)),
                    new RadioBeacon("Flensborg", 94.2, new WorldCoordinate(54.79374, 9.447)),
                    new RadioBeacon("Frejlev", 93.3, new WorldCoordinate(57.00339, 9.82077)),
                    new RadioBeacon("Gl. Højen", 95.5, new WorldCoordinate(55.66807, 9.51360)),
                    new RadioBeacon("Gladsaxe", 90.8, new WorldCoordinate(55.73346, 12.48869)),
                    new RadioBeacon("Grenå", 89.2, new WorldCoordinate(56.41181, 10.89492)),
                    new RadioBeacon("Hammeren", 91.6, new WorldCoordinate(56.48913, 9.15021)),
                    new RadioBeacon("Hobro", 94.9, new WorldCoordinate(56.63790, 9.79082)),
                    new RadioBeacon("Øldrup", 88.7, new WorldCoordinate(55.93287, 10.07027)),
                    new RadioBeacon("Nakskov", 94.1, new WorldCoordinate(54.83341, 11.14372)),
                    new RadioBeacon("Nordborg", 93.7, new WorldCoordinate(55.05572, 9.75101)),
                    new RadioBeacon("Rangstrup", 95.1, new WorldCoordinate(55.14777, 9.20715)),
                    new RadioBeacon("Sdr. Højrup", 89.0, new WorldCoordinate(55.55664, 9.60327)),
                    new RadioBeacon("Skamlebæk", 88.4, new WorldCoordinate(55.83416, 11.42271)),
                    new RadioBeacon("Thisted", 91.4, new WorldCoordinate(56.95917, 8.70349))
                },
                new DRProgramList("p1")
                );

            yield return new RadioChannel("DR P2", new Uri("ms-appx:/Assets/Stations/Denmark/dr-p2.jpg"),
                new[]
                {
                    new RadioWebFeed("http://live-icy.gss.dr.dk/A/A04H.mp3", "http://live-icy.gss.dr.dk/A/A04L.mp3")
                }, 
                new DRProgramList("p2"));

            yield return new RadioChannel("DR P3", new Uri("ms-appx:/Assets/Stations/Denmark/dr-p3.jpg"),
                new[]
                {
                    new RadioWebFeed("http://live-icy.gss.dr.dk/A/A05H.mp3", "http://live-icy.gss.dr.dk/A/A05L.mp3")
                },
                new DRProgramList("p3"));

            yield return new RadioChannel("DR P4", new Uri("ms-appx:/Assets/Stations/Denmark/dr-p4.jpg"),
                new[]
                {
                    new RadioWebFeed("Bornholm", "http://live-icy.gss.dr.dk/A/A06H.mp3", "http://live-icy.gss.dr.dk/A/A06L.mp3"),
                    new RadioWebFeed("Esbjerg", "http://live-icy.gss.dr.dk/A/A15H.mp3", "http://live-icy.gss.dr.dk/A/A15L.mp3"),
                    new RadioWebFeed("Fyn", "http://live-icy.gss.dr.dk/A/A07H.mp3", "http://live-icy.gss.dr.dk/A/A07L.mp3"),
                    new RadioWebFeed("København", "http://live-icy.gss.dr.dk/A/A08H.mp3", "http://live-icy.gss.dr.dk/A/A08L.mp3"),
                    new RadioWebFeed("Midt- og vestjylland", "http://live-icy.gss.dr.dk/A/A09H.mp3", "http://live-icy.gss.dr.dk/A/A09L.mp3"),
                    new RadioWebFeed("Nordjylland", "http://live-icy.gss.dr.dk/A/A10H.mp3", "http://live-icy.gss.dr.dk/A/A10L.mp3"),
                    new RadioWebFeed("Sjælland", "http://live-icy.gss.dr.dk/A/A11H.mp3", "http://live-icy.gss.dr.dk/A/A11L.mp3"),
                    new RadioWebFeed("Syd", "http://live-icy.gss.dr.dk/A/A12H.mp3", "http://live-icy.gss.dr.dk/A/A12L.mp3"),
                    new RadioWebFeed("Trekanten", "http://live-icy.gss.dr.dk/A/A13H.mp3", "http://live-icy.gss.dr.dk/A/A13L.mp3"),
                    new RadioWebFeed("Østjylland", "http://live-icy.gss.dr.dk/A/A14H.mp3", "http://live-icy.gss.dr.dk/A/A14L.mp3"),
                },
                new[]
                {
                    new RadioBeacon("Varde", 99.0, new WorldCoordinate(55.62315, 8.48215)),
                    new RadioBeacon("Ølgod", 97.7, new WorldCoordinate(55.80733, 8.61842)),
                    new RadioBeacon("Viborg", 102.2, new WorldCoordinate(56.45203, 9.39635)),
                    new RadioBeacon("Tolne", 94.4, new WorldCoordinate(57.48487, 10.30914)),
                    new RadioBeacon("Rangstrup", 99.9, new WorldCoordinate(55.14777, 9.20715)),
                    new RadioBeacon("Randers", 88.9, new WorldCoordinate(56.46058, 10.03654)),
                    new RadioBeacon("Mejrup", 98.5, new WorldCoordinate(56.36397, 8.69291)),
                    new RadioBeacon("Mariager", 89.1, new WorldCoordinate(56.65000, 9.97734)),
                    new RadioBeacon("Øverup", 97.5, new WorldCoordinate(55.24549, 11.80777)),
                    new RadioBeacon("Esbjerg", 103.7, new WorldCoordinate(55.47647, 8.45941)),
                    new RadioBeacon("Aarhus", 95.9, new WorldCoordinate(56.16294, 10.20392)),
                    new RadioBeacon("Årsballe", 99.3, new WorldCoordinate(55.14499, 14.86963)),
                    new RadioBeacon("Flensborg", 96.6, new WorldCoordinate(54.79374, 9.447)),
                    new RadioBeacon("Frejlev", 98.1, new WorldCoordinate(57.00339, 9.82077)),
                    new RadioBeacon("Gl. Højen", 94.0, new WorldCoordinate(55.66807, 9.51360)),
                    new RadioBeacon("Gladsaxe", 96.5, new WorldCoordinate(55.73346, 12.48869)),
                    new RadioBeacon("Grenå", 102.0, new WorldCoordinate(56.41181, 10.89492)),
                    new RadioBeacon("Hammeren", 93.7, new WorldCoordinate(56.48913, 9.15021)),
                    new RadioBeacon("Nakskov", 92.2, new WorldCoordinate(54.83341, 11.14372)),
                    new RadioBeacon("Rangstrup", 99.9, new WorldCoordinate(55.14777, 9.20715)),
                    new RadioBeacon("Sdr. Højrup", 96.8, new WorldCoordinate(55.55664, 9.60327)),
                    new RadioBeacon("Skamlebæk", 92.0, new WorldCoordinate(55.83416, 11.42271)),
                    new RadioBeacon("Thisted", 95.6, new WorldCoordinate(56.95917, 8.70349))
                },
                new DRProgramList("p4"));

            yield return new RadioChannel("DR P5", new Uri("ms-appx:/Assets/Stations/Denmark/dr-p5.jpg"),
                new[]
                {
                    new RadioWebFeed("http://live-icy.gss.dr.dk/A/A25H.mp3", "http://live-icy.gss.dr.dk/A/A25L.mp3")
                }, 
                new DRProgramList("p5"));

            yield return new RadioChannel("DR P6", new Uri("ms-appx:/Assets/Stations/Denmark/dr-p6.jpg"),
                new[]
                {
                    new RadioWebFeed("http://live-icy.gss.dr.dk/A/A29H.mp3", "http://live-icy.gss.dr.dk/A/A29L.mp3")
                }, 
                new DRProgramList("p6beat"));

            yield return new RadioChannel("DR P7", new Uri("ms-appx:/Assets/Stations/Denmark/dr-p7.jpg"),
                new[]
                {
                    new RadioWebFeed("http://live-icy.gss.dr.dk/A/A21H.mp3", "http://live-icy.gss.dr.dk/A/A21L.mp3")
                }, 
                new DRProgramList("p7mix"));

            yield return new RadioChannel("DR P8", new Uri("ms-appx:/Assets/Stations/Denmark/dr-p8.jpg"),
                new[]
                {
                    new RadioWebFeed("http://live-icy.gss.dr.dk/A/A22H.mp3", "http://live-icy.gss.dr.dk/A/A22L.mp3")
                },
                new DRProgramList("p8jazz"));

            yield return new RadioChannel("DR MAMA", new Uri("ms-appx:/Assets/Stations/Denmark/dr-mama.jpg"),
                new[]
                {
                    new RadioWebFeed("http://live-icy.gss.dr.dk/A/A18H.mp3", "http://live-icy.gss.dr.dk/A/A18L.mp3")
                },
                new DRProgramList("mama"));

            yield return new RadioChannel("DR Ramasjang", new Uri("ms-appx:/Assets/Stations/Denmark/dr-ramasjang.jpg"),
                new[]
                {
                    new RadioWebFeed("http://live-icy.gss.dr.dk/A/A24H.mp3", "http://live-icy.gss.dr.dk/A/A24L.mp3")
                },
                new EmptyProgramList());

            yield return new RadioChannel("DR Nyheder", new Uri("ms-appx:/Assets/Stations/Denmark/dr-nyheder.jpg"),
                new[]
                {
                    new RadioWebFeed("http://live-icy.gss.dr.dk/A/A02H.mp3", "http://live-icy.gss.dr.dk/A/A02L.mp3")
                }, new EmptyProgramList());
        }

        protected override IEnumerable<RadioChannel> CreateLocalRadioChannels()
        {
            yield return new RadioChannel("Radio24Syv", new Uri("ms-appx:/Assets/Stations/Denmark/radio24syv.jpg"),
                new[]
                {
                    new RadioWebFeed("http://rrr.sz.xlcdn.com/?account=Radio24syv&file=ENC4_MP3128&type=live&service=icecast&port=8000&output=m3u", "http://rrr.sz.xlcdn.com/?account=Radio24syv&file=ENC1_Android64&type=live&service=icecast&port=8000&output=m3u"), 
                },
                new[] {
                    new RadioBeacon("Årsballe", 103.5, new WorldCoordinate(55.14499, 14.86963)),
                    new RadioBeacon("Gl. Højen", 100.9, new WorldCoordinate(55.66807, 9.51360)),
                    new RadioBeacon("Gladsaxe", 102.3, new WorldCoordinate(55.73346, 12.48869)),
                    new RadioBeacon("Grenå", 102.5, new WorldCoordinate(56.41181, 10.89492)),
                    new RadioBeacon("Hammeren", 97.3, new WorldCoordinate(56.48913, 9.15021)),
                    new RadioBeacon("Hobro", 101.1, new WorldCoordinate(56.63790, 9.79082)),
                    new RadioBeacon("Ølgod", 102.5, new WorldCoordinate(55.80733, 8.61842)),
                    new RadioBeacon("Nakskov", 98.8, new WorldCoordinate(54.83341, 11.14372)),
                    new RadioBeacon("Rangstrup", 102.1, new WorldCoordinate(55.14777, 9.20715)),
                    new RadioBeacon("Sdr. Højrup", 100.5, new WorldCoordinate(55.55664, 9.60327)),
                    new RadioBeacon("Skamlebæk", 101.1, new WorldCoordinate(55.83416, 11.42271)),
                    new RadioBeacon("Thisted", 101.3, new WorldCoordinate(56.95917, 8.70349)),
                    new RadioBeacon("Aarhus", 88.1, new WorldCoordinate(56.16294, 10.20392))
                }, new EmptyProgramList());

            yield return new RadioChannel("ANR", new Uri("ms-appx:/Assets/Stations/Denmark/anr.jpg"),
                new[]
                {
                    new RadioWebFeed("http://stream.anr.dk/anr", "http://stream.anr.dk/anr"), 
                }, new EmptyProgramList());

            //yield return new RadioChannel("ANR Demo", new Uri("/Assets/Stations/Denmark/anr.jpg"),
            //    new[]
            //    {
            //        new RadioWebFeed("http://stream.anr.dk/anrdemo", "http://stream.anr.dk/anrdemo"), 
            //    }, null);

            //yield return new RadioChannel("ANR Funky", new Uri("/Assets/Stations/Denmark/anr.jpg"),
            //    new[]
            //    {
            //        new RadioWebFeed("http://stream.anr.dk/anrfunky", "http://stream.anr.dk/anrfunky"), 
            //    }, null);

            //yield return new RadioChannel("ANR Rock", new Uri("/Assets/Stations/Denmark/anr.jpg"),
            //    new[]
            //    {
            //        new RadioWebFeed("http://stream.anr.dk/anrrock", "http://stream.anr.dk/anrrock"), 
            //    }, null);

            //yield return new RadioChannel("ANR Hits", new Uri("/Assets/Stations/Denmark/anr.jpg"),
            //    new[]
            //    {
            //        new RadioWebFeed("http://stream.anr.dk/anrhits", "http://stream.anr.dk/anrhits"), 
            //    }, null);

            yield return new RadioChannel("Nova FM", new Uri("ms-appx:/Assets/Stations/Denmark/nova-fm.jpg"),
                new[]
                {
                    new RadioWebFeed("http://stream.novafm.dk/nova128", "http://stream.novafm.dk/nova128"), 
                }, new EmptyProgramList());

            yield return new RadioChannel("Radio 100 FM", new Uri("ms-appx:/Assets/Stations/Denmark/radio-100-fm.jpg"),
                new[]
                {
                    new RadioWebFeed("http://onair.100fmlive.dk/100fm_live.mp3", "http://onair.100fmlive.dk/100fm_live.mp3"), 
                }, new EmptyProgramList());

            yield return new RadioChannel("The Voice", new Uri("ms-appx:/Assets/Stations/Denmark/the-voice.jpg"),
                new[]
                {
                    new RadioWebFeed("http://stream.voice.dk/voice128", "http://stream.voice.dk/voice128"), 
                }, new EmptyProgramList());

        }
    }
}
