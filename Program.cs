using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace Practicum4
{
    class Program
    {
        static void Main(string[] args)
        {
            Track track1 = new Track { artist = "Dream Theater", title = "6:00", length = TimeSpan.FromSeconds(60)};
            Track track2 = new Track { artist = "Dream Theater", title = "Caught in a Web", length = TimeSpan.FromSeconds(60) };
            Track track3 = new Track { artist = "Dream Theater", title = "Innocence Faded", length = TimeSpan.FromSeconds(60) };

            CD cd1 = new CD { title = "Awake", artist = "Dream Theater", tracks = new List<Track> { track1, track2, track3} };

            XDocument CDXml = cd1.generateXML();


            Console.WriteLine(CDXml.ToString());


            Console.WriteLine("----------------------------------- Deel 2----------------------------------");

            String xmlString;
            using (WebClient wc = new WebClient())
            {
                xmlString = wc.DownloadString(@"https://ws.audioscrobbler.com/2.0/?method=album.getInfo&album=awake&artist=Dream%20Theater&api_key=b5cbf8dcef4c6acfc5698f8709841949");
            }
            XDocument myXMLDoc = XDocument.Parse(xmlString);

            var query =

                from tr in myXMLDoc.Descendants("track")

                where
                !((from tr2 in CDXml.Descendants("track")
                   where tr.Element("name").Value == tr2.Element("title").Value &&
                   tr.Element("artist").Element("name").Value == tr2.Element("artist").Value

                   select tr2).Any())


                select new Track
                {
                    title = tr.Element("name").Value,

                    artist = tr.Element("artist").Element("name").Value,
                    length = TimeSpan.FromSeconds(Int32.Parse(tr.Element("duration").Value))
                };

            foreach (Track tf in query)
            {
                cd1.tracks.Add(tf);
                Console.WriteLine(tf.title);

            }

            Console.WriteLine("-----------------------------------Eindresultaat XML----------------------------------");
            Console.WriteLine(cd1.generateXML().ToString());
            Console.Read();

        }
    }
}
